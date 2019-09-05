using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillsConfigInfos
{
    public static void loadAllSkillConfigFromLocalConfigFile()//1
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor
            ||
            Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            TextAsset csv = Resources.Load("Account/skillsConfig") as TextAsset;
            if (csv != null)
                skillConfigTable.Load(csv);
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            TextAsset csv = Resources.Load("Account/skillsConfig") as TextAsset;//未定，这个瞎写的。
            if (csv != null)
                skillConfigTable.Load(csv);
        }
    }
}
