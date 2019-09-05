using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using Api.Dto.Model;

namespace dataAccess
{
    //Resource模式读取应该是可以用于一些临时剧情人物的读取？最典型的，教学模式角色读取。
    public partial class AccountCharsSet
    {

        public GetMonsterOfPlayerDetailModel loadMyCharsByXMLFile(string Path)
        {
            FileStream FileStream = null;
            try
            {
                Debug.Log("尝试从下面的路径读取角色信息：" + Path);
                GetMonsterOfPlayerDetailModel info = new GetMonsterOfPlayerDetailModel { };
                XmlSerializer XmlSerializer = new XmlSerializer(typeof(GetMonsterOfPlayerDetailModel));
                if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    FileStream = new FileStream(Path, FileMode.Open);
                    info = XmlSerializer.Deserialize(FileStream) as GetMonsterOfPlayerDetailModel;
                    FileStream.Close();
                }
                else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer
                    ||
                    Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
                {
                    TextAsset xmlData = Resources.Load(Path) as TextAsset;
                    XmlSerializer serializer = new XmlSerializer(typeof(GetMonsterOfPlayerDetailModel));
                    var reader = new System.IO.StringReader(xmlData.text);
                    info = serializer.Deserialize(reader) as GetMonsterOfPlayerDetailModel;
                }
                return info;
            }
            catch (Exception e)
            {
                Debug.Log("读取失败" + e.ToString());
                if (FileStream != null)
                    FileStream.Close();
                return null;
            }
        }

        // 请参考generateStoryCharsIntoXMLFile函数来安排文件路径，是否包括文件后缀等问题
        public bool overrideMyOwnedCharsInfoXML(string Path, GetMonsterOfPlayerDetailModel _ownedChars)
        {
            try
            {
                XmlSerializer XmlSerializer = new XmlSerializer(typeof(GetMonsterOfPlayerDetailModel));
                if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    FileStream FileStream = new FileStream(Path, FileMode.Create);
                    XmlSerializer.Serialize(FileStream, _ownedChars);
                    FileStream.Close();
                    return true;
                }
                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer ||
                    Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    StreamWriter writer = new StreamWriter(Path);
                    XmlSerializer.Serialize(writer, _ownedChars);
                    writer.Close();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.Log("玩家信息保存失败" + e.ToString());
                return false;
            }
        }

        // 获取临时剧情用角色，比如用在Tutorial
        public IEnumerator loadStoryCharsByXMLFile()
        {
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
                yield return loadMyCharsByXMLFile(Application.dataPath + "/Resources/Account/StoryChars.xml");
            else if
                (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
                yield return loadMyCharsByXMLFile("Account/StoryChars");

            yield break;
        }

        // 存储一套角色信息到StoryChars.xml文件
        public void generateStoryCharsIntoXMLFile(GetMonsterOfPlayerDetailModel _Chars)
        {
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
                overrideMyOwnedCharsInfoXML(Application.dataPath + "/Resources/Account/StoryChars.xml", _Chars);

            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer ||
                Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                overrideMyOwnedCharsInfoXML(Application.dataPath + "/Resources/Account/StoryChars", _Chars);
            Debug.Log("应该已经在Resource/Account 下生成了文件StoryChars.xml.");
        }
    }
}