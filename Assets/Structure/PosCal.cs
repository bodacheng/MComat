using UnityEngine;

public static class PosCal
{
    public static Canvas canvas;

    private static float canvasWidth => canvas.GetComponent<RectTransform>().rect.width;
    private static float canvasHeight => canvas.GetComponent<RectTransform>().rect.height;
    
    /// <summary>
    /// 这个函数在目前所用的地方为什么能得到正确的值我们压根不理解。主要不理解rect.transform.position到底是什么
    /// </summary>
    /// <param name="refC"></param>
    /// <param name="rect"></param>
    /// <param name="z_offset"></param>
    /// <returns></returns>
    public static Vector3 GetWorldPos(Camera refC, RectTransform rect, float z_offset)
    {
        Vector3 rectPos = rect.transform.position;
        Vector2 trueAnchorPos = new Vector2(rectPos.x, rectPos.y);
        Vector3 WorldPos = refC.ScreenToWorldPoint(trueAnchorPos);
        WorldPos = new Vector3(WorldPos.x, WorldPos.y, refC.transform.position.z + z_offset);
        return WorldPos;
    }
    
    /// 这个是一律和ConvertAnchorPos配合使用，rectPos指的是UI元素在Canvas内的anchoredPosition，它并不等同于Screen Position。
    /// 前者的最大值是Canvas的长和宽，后者的最大值是设备分辨率
    /// </param>
    /// <param name="rectPos"> 这个值是UI元素的坐标，指的应该是我们希望把某个特效给定到的位置 </param>
    /// <param name="z_offset"></param>
    /// <returns></returns>
    public static Vector3 GetWorldPos(Camera refC, Vector3 rectPos, float z_offset)
    {
        Vector2 screenPos = new Vector2(Screen.width * rectPos.x/ canvasWidth, Screen.height * rectPos.y/ canvasHeight);
        Vector3 WorldPos = refC.ScreenToWorldPoint(screenPos);
        WorldPos = new Vector3(WorldPos.x, WorldPos.y, refC.transform.position.z + z_offset);
        return WorldPos;
    }
    
    /// <summary>
    /// 获取某ui元素在某个不同的anchor下，所在位置的anchorPosition值
    /// </summary>
    /// <param name="anchoredPosition">
    ///  formerAnchor下的原RectTransform.anchorPosition
    /// </param>
    /// <param name="formerAnchor">
    /// 之前的Anchor，比如说1，1代表 anchor在右上角
    /// </param>
    /// <param name="targetAnchor">
    /// 目标Anchor，比如说0，0代表 anchor在左下角
    /// </param>
    /// <returns></returns>
    public static Vector3 ConvertAnchorPos(Vector3 anchoredPosition, Vector2 formerAnchor, Vector2 targetAnchor)
    {
        float newX = canvasWidth - anchoredPosition.x * (targetAnchor.x - formerAnchor.x) / 1;
        float newY = canvasHeight - anchoredPosition.y * (targetAnchor.y - formerAnchor.y) / 1;
        return new Vector3(newX, newY, 0);
    }
}
