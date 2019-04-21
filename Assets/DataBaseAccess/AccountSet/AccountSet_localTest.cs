using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public partial class AccountSet
{
    public IEnumerator loadCustomerInfoViaLocalFile()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            localCustomerInfo = loadCustomerInfoViaLocalFile("/Resources/" + "Account/localAccountInfo" + ".xml");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer
                ||
                Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            localCustomerInfo = loadCustomerInfoViaLocalFile("Account/localAccountInfo");
        }
        yield return null;
    }

    public PlayerAccountInfo loadCustomerInfoViaLocalFile(string accountInfoPath)
    {
        try
        {
            PlayerAccountInfo info = new PlayerAccountInfo();
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(PlayerAccountInfo));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                FileStream FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Open);
                info = XmlSerializer.Deserialize(FileStream) as PlayerAccountInfo;
                FileStream.Close();
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer
                    ||
                    Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                XmlSerializer serializer = new XmlSerializer(typeof(PlayerAccountInfo));
                var reader = new System.IO.StringReader(xmlData.text);
                info = serializer.Deserialize(reader) as PlayerAccountInfo;
                Debug.Log("玩家账户信息读取成功");
            }

            // 那么也就是说每次程序启动，我们为玩家所拥有的所有角色添加的这个key其实都是临时给加的，方便本地索引。这么做有无风险？
            this.localCustomerInfo = info;
            return localCustomerInfo;
        }
        catch (Exception e)
        {
            Debug.Log("玩家账户信息读取失败");
            Debug.Log(e.ToString());
            return null;
        }
    }

    public IEnumerator overrideAccountOnLocalFile()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor
            ||
            Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            overrideLocalCustomerInfoOnLocalFile("/Resources/Account/" + "localAccountInfo" + ".xml", this.localCustomerInfo);
        }
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {

        }
        yield break;
    }

    public bool overrideLocalCustomerInfoOnLocalFile(string accountInfoPath, PlayerAccountInfo refreshedPlayerAccountInfo)
    {
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(PlayerAccountInfo));
            FileStream FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Create);
            this.localCustomerInfo = refreshedPlayerAccountInfo;
            XmlSerializer.Serialize(FileStream, this.localCustomerInfo);
            Debug.Log(accountInfoPath + " saved" + " 玩家信息保存成功");
            FileStream.Close();
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("玩家信息保存失败");
            Debug.Log(e.ToString());
            return false;
        }
    }
}
