using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Newtonsoft.Json;
using Json;

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
        presentlifeRemain.text = model.presentlifeRemain;
        ClaimPresentBtn.onClick.RemoveAllListeners();
        ClaimPresentBtn.onClick.AddListener(() => PlayFabReadClient.ClaimPresent(model.itemId, () => SaveReadMailAsJson(model)));
    }

    void SaveReadMailAsJson(MailOfPlayerModel mailOfPlayer)
    {
        string json = JsonConvert.SerializeObject(mailOfPlayer);
        LocalJson.SaveToJsonFile_persistentDataPath("readmail", mailOfPlayer.mailId + ".json", json);
    }
}
