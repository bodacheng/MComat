using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Linq;

public partial class TeamSet
{
    public void loadMyTeamSetInfo()//load完了之后，如果_positionLocalCharKeySet4V4Mode为null或有错，直接新建并保存。
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            _positionLocalCharKeySet4V4Mode = loadMyTeamSetInfo("Resources/" + "Account/TeamSet" + ".xml");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer
                ||
                 Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            _positionLocalCharKeySet4V4Mode = loadMyTeamSetInfo("Account/TeamSet");
        }
    }

    public positionLocalCharKeySet loadMyTeamSetInfo(string accountInfoPath)
    {
        try
        {
            positionLocalCharKeySet info = new positionLocalCharKeySet();
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(positionLocalCharKeySet));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                FileStream FileStream = new FileStream(Application.dataPath + "/" + accountInfoPath, FileMode.Open);
                info = XmlSerializer.Deserialize(FileStream) as positionLocalCharKeySet;
                FileStream.Close();
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                XmlSerializer serializer = new XmlSerializer(typeof(positionLocalCharKeySet));
                var reader = new System.IO.StringReader(xmlData.text);
                info = serializer.Deserialize(reader) as positionLocalCharKeySet;
                Debug.Log("阵容站位读取成功");
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                XmlSerializer serializer = new XmlSerializer(typeof(positionLocalCharKeySet));
                var reader = new System.IO.StringReader(xmlData.text);
                info = serializer.Deserialize(reader) as positionLocalCharKeySet;
                Debug.Log("阵容站位读取成功");
            }

            // 那么也就是说每次程序启动，我们为玩家所拥有的所有角色添加的这个key其实都是临时给加的，方便本地索引。这么做有无风险？
            this._positionLocalCharKeySet4V4Mode = info;
            return this._positionLocalCharKeySet4V4Mode;
        }
        catch (Exception e)
        {
            Debug.Log("阵容站位读取失败");
            _positionLocalCharKeySet4V4Mode = new positionLocalCharKeySet();
            overrideTeamSetInfo();
            Debug.Log(e.ToString());
            return _positionLocalCharKeySet4V4Mode;
        }
    }

    /// <summary>
    /// 
    /// </summary>

    public void overrideTeamSetInfo()
    {
        overrideTeamSetInfo("/Resources/Account/TeamSet.xml", this._positionLocalCharKeySet4V4Mode);
    }

    public bool overrideTeamSetInfo(string accountInfoPath, positionLocalCharKeySet _positionLocalCharKeySet)
    {
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(positionLocalCharKeySet));
            FileStream FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Create);
            this._positionLocalCharKeySet4V4Mode = _positionLocalCharKeySet;
            XmlSerializer.Serialize(FileStream, this._positionLocalCharKeySet4V4Mode);
            Debug.Log(accountInfoPath + " saved" + " 玩家出战阵容保存成功");
            FileStream.Close();
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("玩家出战阵容保存失败");
            Debug.Log(e.ToString());
            return false;
        }
    }
}
