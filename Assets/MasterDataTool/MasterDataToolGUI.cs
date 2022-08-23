#if UNITY_EDITOR

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEditor;

public class LocalMasterDataToolGUI : EditorWindow {

    bool Initialized;
    MasterDataTool tool = new ();
    
    void OnGUI()
    {
        if (!Initialized)
        {
            Initialized = true;
        }
        
        if (GUILayout.Button("(playFab)输出Json格式技能石定义文件"))
        {
            tool.OutputSKStonesCatalog();
        }

        if (GUILayout.Button("(playFab)输出Json格式技能石商店文件"))
        {
            tool.OutputSKStonesStore();
        }
        
        if (GUILayout.Button("(playFab)输出Json格式角色定义文件"))
        {
            tool.OutputMonstersCatalog();
        }

        if (GUILayout.Button("(playFab)输出Json格式角色商店文件"))
        {
            tool.OutputMonsterStore();
        }
        
        if (GUILayout.Button("输出最新技能数值参考文件（技能详细画面用）目前需要play模式下执行"))
        {
            SkillConfigTable.LoadAllSkillConfigs();
            PowerEstimateTable.Save("human").Forget();
        }
        
        if (GUILayout.Button("(playFab)输出Json格式关卡报酬定义文件"))
        {
            FightMemberManager.ExportStageAward();
        }
    }
}
#endif