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
    
    List<MailOfPlayerModel> myMailList = new List<MailOfPlayerModel>();
    
    void Awake()
    {
        target = this;
    }
    
    public MailOfPlayerModel Get(string mailID)
    {
        for (int i = 0; i < myMailList.Count; i++)
        {
            if (myMailList[i].mailId == mailID)
                return myMailList[i];
        }
        return null;
    }
    
    public void Read(MailOfPlayerModel model)
    {
        title.text = model.title;
        message.text = model.message;
        presentlifeRemain.text = model.presentlifeRemain;
    }
    
    public void GenerateMailModels(List<MailOfPlayerModel> _myMailList)
    {
        foreach (Transform t in MailBoxT)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < _myMailList.Count; i++)
        {
            MailModel mailModel = Instantiate(mailModelPretab) as MailModel;
            mailModel.mailId = _myMailList[i].mailId;
            // 内容设置
            mailModel.title.text = _myMailList[i].title;
            mailModel.message.text = _myMailList[i].message;
            mailModel.presentlifeRemain.text = _myMailList[i].presentlifeRemain;
            mailModel.transform.SetParent(MailBoxT);
            mailModel.transform.localPosition = Vector3.zero;
            mailModel.transform.localScale = Vector3.one;
        }
    }
    
    public IEnumerator RequestMails(ApiLanguage apiLanguage)
    {
        yield return Request(
            model => {
                myMailList = model.myMailList;

                /// test //
                MailOfPlayerModel mail1 = new MailOfPlayerModel();
                MailOfPlayerModel mail2 = new MailOfPlayerModel();
                myMailList.Add(mail1);
                myMailList.Add(mail2);
                ///////////
                
                GenerateMailModels(myMailList);
            },
            model => {
            
            },
            apiLanguage
        );
    }
    
    static IEnumerator Request(SuccessDelegate<GetMailsOfPlayerModel> success, FailDelegate<GetMailsOfPlayerModel> fail, ApiLanguage apiLanguage)
    {
        switch(AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.formalVersion:
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                break;
            case PlayerInfoRefMode.localTestSaveData:
                success(new GetMailsOfPlayerModel());
                break;
        }
        yield break;
    }
}