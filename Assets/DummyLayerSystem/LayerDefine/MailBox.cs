using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using System.Collections.Generic;

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

        List<MailOfPlayerModel> _myMailList = PlayFabReadClient.GetMailsData();
        
        for (int i = 0; i < _myMailList.Count; i++)
        {
            MailListView mailListView = Instantiate(mailListViewPretab);
            mailListView.ItemInstanceId = _myMailList[i].ItemInstanceId;
            mailListView.ItemId = _myMailList[i].ItemId;
            // 内容设置
            mailListView.title.text = _myMailList[i].title;
            if (_myMailList[i].Expiration.HasValue)
            {
                mailListView.presentlifeRemain.text = _myMailList[i].Expiration.Value.ToString("yyyy-MM-dd");
            }
            mailListView.AsRead(_myMailList[i].read);
            mailListView.transform.SetParent(MailBoxT);
            mailListView.transform.localPosition = Vector3.zero;
            mailListView.transform.localScale = Vector3.one;
            mailListView.gameObject.SetActive(true);
        }
    }
}
