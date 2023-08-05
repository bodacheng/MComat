using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SizeAdjustBySpriteSize : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] bool fixedHeight = true;

    #region real time size change
    [SerializeField] bool changeSizeRealTime;
    [SerializeField] float upDownSizeInterval = 100;
    #endregion

    
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

    void Start()
    {
        if (changeSizeRealTime)
        {
            var sprite = image.sprite;
            var rectTransform = transform.GetComponent<RectTransform>();
            var targetHeight = Screen.height - upDownSizeInterval * 2;
            rectTransform.sizeDelta = new Vector2(
                sprite.rect.width * targetHeight / sprite.rect.height, 
                targetHeight);
        }
    }
}

#if UNITY_EDITOR
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
#endif