using UnityEngine;

public static class ScreenPositionCal
{
    static Vector2 buttonAnchorPosition;
    static Vector2 true_buttonAnchorPosition;
    static Vector3 buttonWorldPosition;
    
    // 1: canvas screen space 2: UI元素在左下角？忘了
    public static Vector3 Cal(int worldSpaceConvertMode, Camera refC, RectTransform rect, float z_offset)//这个函数是以攻击钮与防御，闪避钮在右下角为前提写的。
    {
        return Cal(worldSpaceConvertMode, refC, rect.transform.position, z_offset);
    }
    
    public static Vector3 Cal(int worldSpaceConvertMode, Camera refC, Vector3 rectPos, float z_offset)//这个函数是以攻击钮与防御，闪避钮在右下角为前提写的。
    {
        switch (worldSpaceConvertMode)
        {
            case 1:
                buttonWorldPosition = rectPos;
                buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, buttonWorldPosition.z + z_offset);
                return buttonWorldPosition;
            case 2:
                buttonAnchorPosition = rectPos;
                true_buttonAnchorPosition = new Vector2(buttonAnchorPosition.x, buttonAnchorPosition.y);
                buttonWorldPosition = refC.ScreenToWorldPoint(true_buttonAnchorPosition);
                buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, refC.transform.position.z + z_offset);
                return buttonWorldPosition;
            case 3:
                Vector3 forwardOfCamera = rectPos - refC.transform.position;
                return refC.transform.position + forwardOfCamera.normalized * z_offset;
        }
        return Vector3.zero;
    }
}
