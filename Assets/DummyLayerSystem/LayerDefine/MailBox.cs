using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;

public class MailBox : UILayer
{
    #region MailBox
    [SerializeField] MailListView mailListViewPretab;
    [SerializeField] RectTransform MailBoxT;
    [SerializeField] Button ReadAll;
    [SerializeField] Button DeleteAllRead;
    #endregion

    private List<MailListView> currentMailListViews = new List<MailListView>();
    
    public static MailBox Open()
    {
        var returnValue = UILayerLoader.Load(PreScene.target.T,"MailBox") as MailBox;
        returnValue.GenerateMailModels();
        returnValue.DeleteAllRead.onClick.AddListener(() =>
        {
            PlayFabReadClient.DeleteAllLocalMails();
            returnValue.GenerateMailModels();
        });
        
        returnValue.ReadAll.onClick.AddListener(() =>
        {
            PlayFabReadClient.claimAllPresentMails(PlayFabReadClient.SaveReadMailAsJson);
        });
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("MailBox");
    }
    
    /// <summary>
    /// 建立邮件的viewlist，运行次函数的时间点邮件已经读取至_myMailList
    /// </summary>
    void GenerateMailModels()
    {
        foreach (Transform t in MailBoxT)
        {
            Destroy(t.gameObject);
        }
        currentMailListViews.Clear();
        
        List<MailItemInstance> _myMailList = PlayFabReadClient.GetMailsData();
        for (int i = 0; i < _myMailList.Count; i++)
        {
            MailListView mailListView = Instantiate(mailListViewPretab);
            mailListView.PassMailInfo(_myMailList[i], Sort);
            currentMailListViews.Add(mailListView);
        }
        Sort();
    }

    void Sort()
    {
        float wholeheight = 0; 
        foreach (MailListView t in currentMailListViews)
        {
            t.transform.SetParent(null);
            wholeheight += t.GetComponent<RectTransform>().rect.height;
        }
        
        foreach (MailListView t in currentMailListViews)
        {
            if (!t.claimed)
                t.transform.SetParent(MailBoxT);
            t.transform.localPosition = Vector3.zero;
            t.transform.localScale = Vector3.one;
            t.gameObject.SetActive(true);
        }
        foreach (MailListView t in currentMailListViews)
        {
            if (t.claimed)
                t.transform.SetParent(MailBoxT);
            t.transform.localPosition = Vector3.zero;
            t.transform.localScale = Vector3.one;
            t.gameObject.SetActive(true);
        }
        MailBoxT.sizeDelta = new Vector2(MailBoxT.sizeDelta.x, wholeheight);
    }

    public void AddButtonFeatures()
    {

    }
}
