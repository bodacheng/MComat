using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Linq;

public partial class AccountCharsSet{

    //这个环节到时候只能是数据库
    public IEnumerator loadMyCharsByXMLFile()// 解决不同平台路径问题
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            ownedChars = loadMyCharsByXMLFile("/Resources/" + "Account/MyOwnedChars" + ".xml");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer
            ||
            Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            ownedChars = loadMyCharsByXMLFile("Account/MyOwnedChars");
        }

        foreach (CharacterDataInfo _characterDataInfo in ownedChars)
        {
            CharacterResourceInfo _CharacterResourceInfo = CharsManager._monstersConfigTable.RowToCharacterResourceInfo(
                CharsManager._monstersConfigTable.Find_ID(_characterDataInfo.resource_num.ToString()));
            _characterDataInfo._NineAndTwo.sortNineAndTwo(_CharacterResourceInfo.getPassiveSkillConfigs());
        }

        yield break;
    }

    public CharacterDataInfo[] loadMyCharsByXMLFile(string accountInfoPath)
    {
        FileStream FileStream = null;
        try
        {
            CharacterDataInfo[] info = new CharacterDataInfo[] { };
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(CharacterDataInfo[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                Debug.Log("尝试从下文件地址读取拥有角色信息：" + Application.dataPath + accountInfoPath);
                FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Open);
                info = XmlSerializer.Deserialize(FileStream) as CharacterDataInfo[];
                FileStream.Close();
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                Debug.Log("尝试从下面的地址load存档信息：" + accountInfoPath);
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                XmlSerializer serializer = new XmlSerializer(typeof(CharacterDataInfo[]));
                var reader = new System.IO.StringReader(xmlData.text);
                info = serializer.Deserialize(reader) as CharacterDataInfo[];
                Debug.Log("玩家账户信息读取成功");
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                XmlSerializer serializer = new XmlSerializer(typeof(CharacterDataInfo[]));
                var reader = new System.IO.StringReader(xmlData.text);
                info = serializer.Deserialize(reader) as CharacterDataInfo[];
                Debug.Log("玩家账户信息读取成功");
            }
            ownedChars = info;
            return ownedChars;
        }
        catch (Exception e)
        {
            Debug.Log("玩家账户信息读取失败");
            Debug.Log(e.ToString());
            if (FileStream != null)
                FileStream.Close();
            return null;
        }
    }

    public void overrideMyCharsInfoOnXMLFile()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            overrideMyOwnedCharsInfoXML("Resources/Account/MyOwnedChars.xml", ownedChars);
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer
            ||
            Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            overrideMyOwnedCharsInfoXML("Account/MyOwnedChars", ownedChars);
    }

    public bool overrideMyOwnedCharsInfoXML(string accountInfoPath, CharacterDataInfo[] _ownedChars)
    {
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(CharacterDataInfo[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                FileStream FileStream = new FileStream(Application.dataPath + "/" + accountInfoPath, FileMode.Create);
                ownedChars = _ownedChars;
                XmlSerializer.Serialize(FileStream, ownedChars);
                Debug.Log(accountInfoPath + " saved" + " 玩家信息保存成功");
                FileStream.Close();
                return true;
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer
               ||
                Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                ownedChars = _ownedChars;
                StreamWriter writer = new StreamWriter(Application.dataPath + "/" + accountInfoPath);
                XmlSerializer.Serialize(writer, ownedChars);
                writer.Close();
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Debug.Log("玩家信息保存失败");
            Debug.Log(e.ToString());
            return false;
        }
    }

    public static void addNewCharToXMLAccount(CharacterDataInfo _CharacterDataInfo)
    {
        CharacterDataInfo[] myOwnedChars = AccountCharsSet.ownedChars;
        List<int> currentLocalIDList = new List<int>();
        if (myOwnedChars == null)
            myOwnedChars = new CharacterDataInfo[] { };

        foreach (CharacterDataInfo one in myOwnedChars)
        {
            currentLocalIDList.Add(one.localID);
        }

        currentLocalIDList.Sort((a, b) => AccountCharsSet.Instance.intCompare(a, b));

        List<CharacterDataInfo> newOwnedChars = myOwnedChars.ToList();

        int i = 0;
        for (i = 0; i < currentLocalIDList.Count; i++)
        {
            if (i + 1 < currentLocalIDList.Count)
            {
                if (currentLocalIDList[i + 1] - currentLocalIDList[i] > 1)
                {
                    _CharacterDataInfo.localID = currentLocalIDList[i] + 1;
                    newOwnedChars.Add(_CharacterDataInfo);
                    AccountCharsSet.ownedChars = newOwnedChars.ToArray();
                    AccountCharsSet.Instance.overrideMyCharsInfoOnXMLFile();
                    return;
                }
            }
        }

        if (currentLocalIDList.Count > 0)
            _CharacterDataInfo.localID = currentLocalIDList[currentLocalIDList.Count - 1] + 1;
        else
            _CharacterDataInfo.localID = 0;
        newOwnedChars.Add(_CharacterDataInfo);
        AccountCharsSet.ownedChars = newOwnedChars.ToArray();
        AccountCharsSet.Instance.overrideMyCharsInfoOnXMLFile();
    }
}
