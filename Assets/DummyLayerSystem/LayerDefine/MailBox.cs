using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class MailBox : UILayer
{
    #region MailBox
    [SerializeField] MailListView mailListViewPretab;
    [SerializeField] RectTransform MailBoxT;
    [SerializeField] Button GetAll;
    [SerializeField] Button DeleteAllRead;
    #endregion
    
    /// <summary>
    /// 建立邮件的viewlist，运行次函数的时间点邮件已经读取至_myMailList
    /// </summary>
    public void GenerateMailModels()
    {
        foreach (Transform t in MailBoxT)
        {
            Destroy(t.gameObject);
        }
        
        List<ItemInstance> _myMailList = PlayFabReadClient.GetMailsData();

        float wholeheight = 0; 
        for (int i = 0; i < _myMailList.Count; i++)
        {
            MailListView mailListView = Instantiate(mailListViewPretab);
            mailListView.PassMailInfo(_myMailList[i]);
            mailListView.transform.SetParent(MailBoxT);
            mailListView.transform.localPosition = Vector3.zero;
            mailListView.transform.localScale = Vector3.one;
            mailListView.gameObject.SetActive(true);
            wholeheight += mailListView.GetComponent<RectTransform>().rect.height;
        }
        MailBoxT.sizeDelta = new Vector2(MailBoxT.sizeDelta.x, wholeheight);
    }

    public void AddButtonFeatures()
    {
        DeleteAllRead.onClick.AddListener(() =>
            {
                PlayFabReadClient.DeleteAllLocalMails();
                GenerateMailModels();
            }
        );
        
    }
}
