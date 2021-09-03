using UnityEngine;
using UnityEngine.UI;
using dataAccess;
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
    
    List<MailOfPlayerModel> _myMailList = new List<MailOfPlayerModel>();
    
    void Awake()
    {
        target = this;
    }

    public void AddMailData(MailOfPlayerModel mailData)
    {
        _myMailList.Add(mailData);
    }
    
    public MailOfPlayerModel Get(string mailID)
    {
        for (int i = 0; i < _myMailList.Count; i++)
        {
            if (_myMailList[i].mailId == mailID)
                return _myMailList[i];
        }
        return null;
    }
    
    public void Read(MailOfPlayerModel model)
    {
        title.text = model.title;
        message.text = model.message;
        presentlifeRemain.text = model.presentlifeRemain;
    }
    
    public void GenerateMailModels()
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
}