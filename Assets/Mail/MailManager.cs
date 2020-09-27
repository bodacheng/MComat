using dataAccess;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Api.Dto.Form;
using Api.Common;
using Api.Dto.Model;
using System.Collections.Generic;

public class MailManager : MonoBehaviour {

    public Canvas MailCanvas;

    #region MailBox
    public RectTransform BoxPartT;
    public MailModel mailModelPretab;    
    public RectTransform MailBoxT;
    public Button GetAll;
    public Button DeleteAllRead;
    #endregion
    
    #region MailDetail
    public RectTransform detailPartT;
    public Image mailIcon;
    public Text title;
    public Text message;
    public Text presentlifeRemain;
    #endregion
    
    public static MailManager target;

    List<GetMailOfPlayerModel> myMailList = new List<GetMailOfPlayerModel>();
    
    void Awake()
    {
        target = this;
    }
    
    public void GenerateMailModels()
    {
        foreach (Transform t in BoxPartT)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < myMailList.Count; i++)
        {
            MailModel mailModel = Instantiate(mailModelPretab) as MailModel;
            mailModel.mailId = myMailList[i].mailId;
            // 内容设置
            mailModel.title.text = myMailList[i].title;
            mailModel.message.text = myMailList[i].message;
            mailModel.presentlifeRemain.text = myMailList[i].presentlifeRemain;
            mailModel.transform.SetParent(BoxPartT);
        }
    }

    public IEnumerator RequestMails(ApiLanguage apiLanguage)
    {
        GetMailListForm getMailListForm = new GetMailListForm();
        yield return Request(
            getMailListForm,
            model => {
                myMailList = model.myMailList;
                GenerateMailModels();
            },
            model => {
            
            },
            apiLanguage
        );
    }
    
    static IEnumerator Request(GetMailListForm form, SuccessDelegate<GetMailsOfPlayerModel> success, FailDelegate<GetMailsOfPlayerModel> fail, ApiLanguage apiLanguage)
    {
        switch(AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.formalVersion:
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<GetMailsOfPlayerModel, GetMailListForm>
                    (
                        "http://160.16.187.230/AssetStoreFight/skillStone/getSkillStoneOfPlayerInfo",
                        form,
                        ApiCaller.Instance.getHeader(apiLanguage), 
                        model => {
                            success(model.data);
                        },
                        model => {
                            fail(model.data);
                        }
                    );
                break;
            case PlayerInfoRefMode.localTestSaveData:
                break;
        }
        yield break;
    }
}