using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SizeAdjustBySpriteSize : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] bool fixedHeight = true;
    
    public void AdjustSize()
    {
        var sprite = image.sprite;
        var rectTransform = transform.GetComponent<RectTransform>();
        if (fixedHeight)
        {
            rectTransform.sizeDelta = new Vector2(
                sprite.rect.width * rectTransform.rect.height / sprite.rect.height, 
                rectTransform.rect.height);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(
                rectTransform.rect.width, 
                sprite.rect.height * rectTransform.rect.width / sprite.rect.width);
        }
    }
}

[CustomEditor(typeof(SizeAdjustBySpriteSize))]
public class SizeAdjustBySpriteSizeGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var _target = (SizeAdjustBySpriteSize)target;
        if (GUILayout.Button("Adjust Size"))
        {
            _target.AdjustSize();
        }
    }
}
