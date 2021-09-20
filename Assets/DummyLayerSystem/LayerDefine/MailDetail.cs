using UnityEngine;
using UnityEngine.UI;
using dataAccess;


public class MailDetail : UILayer
{
    #region MailDetail
    public RectTransform detailPartT;
    public Image mailIcon;
    public Text title;
    public Text message;
    public Text presentlifeRemain;
    public Button ClaimPresentBtn;
    #endregion
    
    public void Read(MailOfPlayerModel model)
    {
        title.text = model.title;
        message.text = model.message;
        if (model.Expiration.HasValue)
            presentlifeRemain.text = model.Expiration.Value.ToString("yyyy-MM-dd");
        else
        {
            presentlifeRemain.text = "无时间限制？";
        }
        ClaimPresentBtn.onClick.RemoveAllListeners();
        ClaimPresentBtn.onClick.AddListener(
            () => PlayFabReadClient.ClaimPresent(model.ItemId, 
                PlayFabReadClient.SaveReadMailAsJson)
        );
    }
}
