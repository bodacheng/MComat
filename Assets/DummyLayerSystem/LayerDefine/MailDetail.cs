using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using PlayFab.ClientModels;

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
    
    public void Read(ItemInstance model)
    {
        title.text = model.DisplayName;
        message.text = "temp";//model.CustomData.Contains("description") ? model.CustomData["description"] : 0;
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
