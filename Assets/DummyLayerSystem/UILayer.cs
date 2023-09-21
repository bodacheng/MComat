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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="recordId"></param>
    /// <param name="view2D"></param>
    /// <param name="unitOutAnimator"></param>
    /// <param name="distanceToVerticalEdge"> 图片自身的pivot距离高度上（或下？）边缘的距离 </param>
    /// <param name="seenHeightProportionalOfWhole"> 漏出在画面中的高度是图片实际高度的百分之几 </param>
    /// <returns></returns>
    protected async UniTask<Sprite> Set2DView(string recordId, Image view2D, Animator unitOutAnimator, 
        float distanceToVerticalEdge ,float seenHeightProportionalOfWhole, float originX ,float extraYokoSpace)
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

        float seenHeight = PosCal.CanvasHeight - distanceToVerticalEdge;
        float wholeHeight = seenHeight / seenHeightProportionalOfWhole;

        var anchoredPosition = unitImageRect.anchoredPosition;
        unitImageRect.anchoredPosition = new Vector2(originX + extraYokoSpace, anchoredPosition.y);
        unitImageRect.sizeDelta = new Vector2(value.rect.width * wholeHeight / value.rect.height, wholeHeight);
        view2D.sprite = value;
        unitOutAnimator.SetTrigger("select");
        return value;
    }

    protected void ToTop()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
