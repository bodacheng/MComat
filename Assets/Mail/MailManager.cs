using dataAccess;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    
    void Awake()
    {
        target = this;
    }

    public IEnumerator RequestMails()
    {
        switch(AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.formalVersion:
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                break;
            case PlayerInfoRefMode.localTestSaveData:
                break;
        }
        yield break;
    }
}