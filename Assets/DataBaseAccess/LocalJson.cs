using System.IO;
using UnityEngine;
using System;

namespace dataAccess
{
    public class LocalJson
    {
        public static void saveInfoToJsonFile(string subpath, string filename, string json)
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

            try
            {
                if (!File.Exists(wholepath))
                {
                    File.Create(wholepath).Close();
                }
                File.WriteAllText(wholepath, json, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}

