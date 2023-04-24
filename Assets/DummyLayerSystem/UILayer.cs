using UnityEngine;

public class UILayer : MonoBehaviour
{
    private string index;
    
    public string Index
    {
        get => index;
        set => index = value;
    }

    public virtual void OnDestroy()
    {
        
    }

    protected void CameraConnectorCal(RectTransform target, float cameraConnectorRightSpace, float cameraConnectorVerticalSpace)
    {
        var unitViewSize = (PosCal.canvasWidth - cameraConnectorRightSpace);
        if (unitViewSize > PosCal.canvasHeight - cameraConnectorVerticalSpace)
            unitViewSize = PosCal.canvasHeight - cameraConnectorVerticalSpace;
        target.sizeDelta = new Vector2(unitViewSize, unitViewSize);
    }

    protected void ToTop()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
