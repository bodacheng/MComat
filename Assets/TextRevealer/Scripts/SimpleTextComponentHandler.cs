using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTextComponentHandler : TextComponentHandler
{
    private const string ColorFieldName = "m_Color.a";
    private Text textComponent;
    private int fontSize = 0;

    public SimpleTextComponentHandler(Text textComponent)
    {
        this.textComponent = textComponent;
    }

    public override List<Transform> CreateSlices(Transform slicedParent)
    {
        List<Transform> slices = new List<Transform>();
        List<CharacterInfo> characterInfos = GetCharacterInfo();

        int i = 0;
        foreach(var characterInfo in characterInfos)
        {
            if (char.IsWhiteSpace(characterInfo.TextValue) || characterInfo.TextValue == '\n')
            {
                continue;
            }

            GameObject slice = new GameObject(textComponent.gameObject.name + "_slice_" + i++.ToString());
            slice.transform.SetParent(slicedParent);
            
            Text sliceText = slice.AddComponent<Text>();
            sliceText.text = characterInfo.TextValue.ToString();
            sliceText.alignment = TextAnchor.MiddleCenter;
            slice.transform.localPosition = characterInfo.Position;
            CopyTextFields(sliceText, textComponent);

            slice.transform.localRotation = Quaternion.identity;
            slice.transform.localScale = Vector3.one;
            slices.Add(slice.transform);
        }

        return slices;
    }

    public override void TurnOffTextAlpha()
    {
        OriginalOpacity = textComponent.color.a;
        textComponent.color = GetTransparentColor();
    }
    
    public override void RevertTextAlpha()
    {
        Color currentColor = textComponent.color;
        textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, OriginalOpacity);
    }

    public override void SetColorCurve(AnimationClip clip, string sliceName, AnimationCurve curveColor)
    {
        clip.SetCurve(sliceName, typeof(Text), ColorFieldName, curveColor);
    }

    private Color GetTransparentColor()
    {
        Color currentColor = textComponent.color;
        return new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
    }

    private List<CharacterInfo> GetCharacterInfo()
    {
        List<CharacterInfo> indexes = new List<CharacterInfo>();
        TextGenerator textGen = new TextGenerator(textComponent.text.Length);
        Vector2 extents = textComponent.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(textComponent.text, textComponent.GetGenerationSettings(extents));
        float pixelsPerUnit = textComponent.pixelsPerUnit > 0f ? textComponent.pixelsPerUnit : 1f;

        // TextGenerator reports the screen-scaled font size. Each generated slice is
        // another Text component, so it applies pixelsPerUnit again when rendering.
        // Convert back to canvas units to avoid enlarging and overlapping letters on
        // high-resolution displays (or shrinking them on low-resolution displays).
        fontSize = Mathf.Max(1, Mathf.RoundToInt(textGen.fontSizeUsedForBestFit / pixelsPerUnit));

        int index = 0;
        foreach (char c in textComponent.text)
        {
#if UNITY_2019_1_OR_NEWER
            if (!char.IsWhiteSpace(c))
#endif
            {
                if (index * 4 + 2 >= textGen.vertexCount)
                {
                    continue;
                }

                Vector3 locUpperLeft = new Vector3(textGen.verts[index * 4].position.x, textGen.verts[index * 4].position.y, textGen.verts[index * 4].position.z);
                Vector3 locBottomRight = new Vector3(textGen.verts[index * 4 + 2].position.x, textGen.verts[index * 4 + 2].position.y, textGen.verts[index * 4 + 2].position.z);

                Vector3 mid = new Vector3((locUpperLeft.x + locBottomRight.x) / 2.0f, (locUpperLeft.y + locBottomRight.y) / 2.0f, (locUpperLeft.z + locBottomRight.z) / 2.0f);

                // TextGenerator vertices use the Text component's pixels-per-unit scale.
                // Canvas.scaleFactor converts canvas units to screen pixels and must not be
                // applied here; doing so makes the letter spacing depend on resolution.
                Vector3 position = mid / pixelsPerUnit;
                indexes.Add(new CharacterInfo() { Position = position, TextValue = c });
                index++;
            }
        }

        return indexes;
    }

    private void CopyTextFields(Text sliceText, Text textComponent)
    {
        sliceText.font = textComponent.font;
        sliceText.fontSize = fontSize;
        sliceText.fontStyle = textComponent.fontStyle;
        sliceText.color = textComponent.color;
        sliceText.alignByGeometry = true;
        sliceText.lineSpacing = textComponent.lineSpacing;
        sliceText.material = textComponent.material;
        sliceText.resizeTextForBestFit = false;
        sliceText.horizontalOverflow = HorizontalWrapMode.Wrap;
        sliceText.verticalOverflow = VerticalWrapMode.Overflow;
    }
}
