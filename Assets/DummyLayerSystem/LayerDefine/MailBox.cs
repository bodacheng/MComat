using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Serialization;

public class MailBox : UILayer
{
    #region MailBox
    [FormerlySerializedAs("mailViewPretab")] [FormerlySerializedAs("mailModelPretab")] [SerializeField] MailListView mailListViewPretab;
    [SerializeField] RectTransform MailBoxT;
    [SerializeField] Button GetAll;
    [SerializeField] Button DeleteAllRead;
    #endregion

    /// <summary>
    /// 已读取邮件的获取
    /// </summary>
    public static void LoadReadMails()
    {
        string path = Application.persistentDataPath + "/readmail";
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    string dataAsJson = File.ReadAllText(file);
                    MailOfPlayerModel mailOfPlayerModel = JsonConvert.DeserializeObject<MailOfPlayerModel>(dataAsJson);
                    PlayFabReadClient.AddMailData(mailOfPlayerModel);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
    }

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
            mailListView.mailId = _myMailList[i].mailId;
            // 内容设置
            mailListView.title.text = _myMailList[i].title;
            mailListView.presentlifeRemain.text = _myMailList[i].presentlifeRemain;
            mailListView.transform.SetParent(MailBoxT);
            mailListView.transform.localPosition = Vector3.zero;
            mailListView.transform.localScale = Vector3.one;
            mailListView.gameObject.SetActive(true);
        }
    }
}
