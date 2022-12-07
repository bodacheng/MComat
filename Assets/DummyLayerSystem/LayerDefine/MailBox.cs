using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MailBox : UILayer
{
    #region MailBox
    [SerializeField] MailListView mailListViewPrefab;
    [SerializeField] RectTransform MailBoxT;
    [SerializeField] Button ReadAll;
    [SerializeField] Button DeleteAllRead;
    #endregion
    
    private readonly List<MailListView> _currentMailListViews = new();
    
    public void Setup()
    {
        GenerateMailModels();
        DeleteAllRead.onClick.AddListener(() =>
        {
            PlayFabReadClient.DeleteAllLocalMails();
            GenerateMailModels();
        });
        
        ReadAll.onClick.AddListener(() =>
        {
            PlayFabReadClient.ClaimAllPresentMails(PlayFabReadClient.SaveReadMailAsJson);
        });
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
        _currentMailListViews.Clear();
        
        var myMailList = PlayFabReadClient.GetMailsData();
        for (var i = 0; i < myMailList.Count; i++)
        {
            var mailListView = Instantiate(mailListViewPrefab);
            mailListView.PassMailInfo(myMailList[i], Sort);
            _currentMailListViews.Add(mailListView);
        }
        Sort();
    }

    void Sort()
    {
        float rectHeight = 0; 
        foreach (var t in _currentMailListViews)
        {
            t.transform.SetParent(null);
            rectHeight += t.GetComponent<RectTransform>().rect.height;
        }
        foreach (var t in _currentMailListViews)
        {
            if (!t.claimed)
                t.transform.SetParent(MailBoxT);
            t.transform.localPosition = Vector3.zero;
            t.transform.localScale = Vector3.one;
            t.gameObject.SetActive(true);
        }
        foreach (var t in _currentMailListViews)
        {
            if (t.claimed)
                t.transform.SetParent(MailBoxT);
            t.transform.localPosition = Vector3.zero;
            t.transform.localScale = Vector3.one;
            t.gameObject.SetActive(true);
        }
        MailBoxT.sizeDelta = new Vector2(MailBoxT.sizeDelta.x, rectHeight);
    }

    public void AddButtonFeatures()
    {

    }
}
