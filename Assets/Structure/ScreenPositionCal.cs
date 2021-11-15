using UnityEngine;

public static class ScreenPositionCal
{
    static Vector2 AnchorPos;
    static Vector2 true_AnchorPos;
    static Vector3 WorldPos;
    
    // 1: canvas screen space 2: UI元素在左下角？忘了
    public static Vector3 Cal(int ConvertMode, Camera refC, RectTransform rect, float z_offset)
    {
        Debug.Log(rect.transform.position);
        return Cal(ConvertMode, refC, rect.transform.position, z_offset);
    }
    
    public static Vector3 Cal2(int ConvertMode, Camera refC, RectTransform rect, float z_offset)
    {
        Debug.Log(rect.anchoredPosition);
        return Cal(ConvertMode, refC, rect.anchoredPosition, z_offset);
    }
    
    public static Vector3 Cal(int ConvertMode, Camera refC, Vector3 rectPos, float z_offset)
    {
        switch (ConvertMode)
        {
            case 1:
                WorldPos = rectPos;
                WorldPos = new Vector3(WorldPos.x, WorldPos.y, WorldPos.z + z_offset);
                return WorldPos;
            case 2:
                AnchorPos = rectPos;
                true_AnchorPos = new Vector2(AnchorPos.x, AnchorPos.y);
                //refC.transform.SetParent(null);
                WorldPos = refC.ScreenToWorldPoint(true_AnchorPos);
                Debug.Log("!"+ WorldPos);
                WorldPos = new Vector3(WorldPos.x, WorldPos.y, refC.transform.position.z + z_offset);
                return WorldPos;
            case 3:
                Vector3 forwardOfCamera = rectPos - refC.transform.position;
                return refC.transform.position + forwardOfCamera.normalized * z_offset;
        }
        return Vector3.zero;
    }
}
