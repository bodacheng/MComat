using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class MailBox : UILayer
{
    #region MailBox
    [SerializeField] MailModel mailModelPretab;
    [SerializeField] RectTransform MailBoxT;
    [SerializeField] Button GetAll;
    [SerializeField] Button DeleteAllRead;
    #endregion

    static List<MailOfPlayerModel> _myMailList = new List<MailOfPlayerModel>();

    public static void AddMailData(MailOfPlayerModel mailData)
    {
        _myMailList.Add(mailData);
    }

    public static MailOfPlayerModel Get(string mailID)
    {
        for (int i = 0; i < _myMailList.Count; i++)
        {
            if (_myMailList[i].mailId == mailID)
                return _myMailList[i];
        }
        return null;
    }

    void LoadReadMails()
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
                    _myMailList.Add(mailOfPlayerModel);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
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
            mailModel.presentlifeRemain.text = _myMailList[i].presentlifeRemain;
            mailModel.transform.SetParent(MailBoxT);
            mailModel.transform.localPosition = Vector3.zero;
            mailModel.transform.localScale = Vector3.one;
            mailModel.gameObject.SetActive(true);
        }
    }
}
