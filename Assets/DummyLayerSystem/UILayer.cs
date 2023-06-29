using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
        var unitViewSize = (PosCal.CanvasWidth - cameraConnectorRightSpace);
        if (unitViewSize > PosCal.CanvasHeight - cameraConnectorVerticalSpace)
            unitViewSize = PosCal.CanvasHeight - cameraConnectorVerticalSpace;
        target.sizeDelta = new Vector2(unitViewSize, unitViewSize);
    }
    
    protected async UniTask<Sprite> Set2DView(string recordId, Image view2D, Animator unitOutAnimator)
    {
        string key = "unit_image/" + recordId;
        if (!AddressablesLogic.CheckKeyExist("unit_image", key))
        {
            unitOutAnimator.SetTrigger("reset");
            return null;
        }
        
        var value = await AddressablesLogic.LoadT<Sprite>(key);
        if (unitOutAnimator == null)
        {
            return null;
        }
        var unitImageRect = view2D.GetComponent<RectTransform>();
        unitImageRect.sizeDelta = new Vector2(value.rect.width * unitImageRect.rect.height / value.rect.height, unitImageRect.rect.height);
        view2D.sprite = value;
        unitOutAnimator.SetTrigger("select");
        return value;
    }

    protected void ToTop()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
