using UnityEngine;

public static class PosCal
{
    public static Vector3 GetWorldPos(Camera refC, RectTransform rect, float z_offset)
    {
        return GetWorldPos(refC, rect.transform.position, z_offset);
    }
    
    /// 指的是一个UI元素的screen position
    /// 这个数值大部分情况下就是对象元素的RectTransform.transform.position
    /// 而且哪怕它是某个节点的子节点这么写也灵（虽然我不知道为什么）
    /// 这是为什么我们有了GetWorldPos(Camera refC, RectTransform rect, float z_offset)这个overwrite
    /// 然而某些情况下RectTransform.transform.position却似乎得到不正确的结果，
    /// 这样的情况下为了得到正确的UI元素screen position，
    /// 我们引入了ConvertAnchorPos
    /// </param>
    /// <param name="rectPos"> 这个值是UI元素的坐标，指的应该是我们希望把某个特效给定到的位置 </param>
    /// <param name="z_offset"></param>
    /// <returns></returns>
    public static Vector3 GetWorldPos(Camera refC, Vector3 rectPos, float z_offset)
    {
        Vector2 AnchorPos = rectPos;
        Vector2 true_AnchorPos = new Vector2(AnchorPos.x, AnchorPos.y);
        Vector3 WorldPos = refC.ScreenToWorldPoint(true_AnchorPos);
        WorldPos = new Vector3(WorldPos.x, WorldPos.y, refC.transform.position.z + z_offset);
        return WorldPos;
    }
    
    /// <summary>
    /// 获取某ui元素在某个不同的anchor下，所在位置的anchorPosition值
    /// </summary>
    /// <param name="pos">
    ///  formerAnchor下的原anchorPosition
    /// </param>
    /// <param name="formerAnchor">
    /// 之前的Anchor，比如说1，1代表 anchor在右上角
    /// </param>
    /// <param name="targetAnchor">
    /// 目标Anchor，比如说0，0代表 anchor在左下角
    /// </param>
    /// <returns></returns>
    public static Vector3 ConvertAnchorPos(Vector3 pos, Vector2 formerAnchor, Vector2 targetAnchor)
    {
        float newX = Screen.width - pos.x * (targetAnchor.x - formerAnchor.x) / 1;
        float newY = Screen.height - pos.y * (targetAnchor.y - formerAnchor.y) / 1;
        return new Vector3(newX, newY, 0);
    }
}
