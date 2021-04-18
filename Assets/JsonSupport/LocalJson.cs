using System.IO;
using UnityEngine;
using System;

namespace Json
{
    public static class LocalJson
    {
        public static void SaveInfoToJsonFile_persistentDataPath(string subpath, string filename, string json)
        {
            //string wholepath = Path.Combine(Application.persistentDataPath, subpath);
            string wholepath;
            if (subpath != null)
            {
                if (!Directory.Exists(Application.persistentDataPath + "/" + subpath))
                {
                    //if it doesn't, create it
                    Directory.CreateDirectory(Application.persistentDataPath + "/" + subpath);
                }
                wholepath = Application.persistentDataPath + "/" + subpath + "/" + filename;
            }
            else
            {
                wholepath = Application.persistentDataPath + "/" + filename;
            }
            Debug.Log("try:"+wholepath);
            try
            {
                if (!File.Exists(wholepath))
                {
                    File.Create(wholepath).Close();
                }
                File.WriteAllText(wholepath, json, System.Text.Encoding.UTF8);
                Debug.Log(wholepath);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public static void SaveInfoToJsonFile_dataPath(string subpath, string filename, string json)
        {
            //string wholepath = Path.Combine(Application.persistentDataPath, subpath);
            string wholepath;
            if (subpath != null)
            {
                if (!Directory.Exists(Application.dataPath + "/" + subpath))
                {
                    //if it doesn't, create it
                    Directory.CreateDirectory(Application.dataPath + "/" + subpath);
                }
                wholepath = Application.dataPath + "/" + subpath + "/" + filename;
            }
            else
            {
                wholepath = Application.dataPath + "/" + filename;
            }
            
            try
            {
                if (!File.Exists(wholepath))
                {
                    File.Create(wholepath).Close();
                }
                Debug.Log("文件生成"+ wholepath);
                File.WriteAllText(wholepath, json, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public static void DeleteAllUnderFolder(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    foreach (string file in Directory.GetFiles(filePath))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
}

