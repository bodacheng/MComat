using System;
using dataAccess;
using UnityEngine;
using UnityEngine.SceneManagement;
using mainMenu;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Starter))]
public class StarterGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var s = (Starter)target;
        if (GUILayout.Button("Refresh"))
        {
            s.Initialise();
        }
    }
}
#endif

[ExecuteInEditMode]
public class Starter : MonoBehaviour
{
    [SerializeField] bool enterFrontPageFirst;
    [SerializeField] PlayfabSetting PlayfabSetting;
    [SerializeField] FightGlobalSetting FightGlobalSetting;
    [SerializeField] KeywordSetting keywordSetting;
    
    public void Initialise()
    {
        Debug.Log("files loads...");
        FightGlobalSetting.Initialise();
        PlayfabSetting.Initialise();
        keywordSetting.Initialise();
        SkillConfigTable.LoadAllSkillConfigs();
        PowerEstimateTable.LoadByResource();
        Units.LoadByResource();
        Units.RefreshDic();
    }
    
    void EnterFrontScene()
    {
        Initialise();
        Debug.Log("hello");
        if (enterFrontPageFirst)
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            stage.SetEventType(FightEventType.Screensaver);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
            FightLoad.Go(stage);
        }else{
            MainMenuNote.goingtostep = MainSceneStep.FrontPage;
            Debug.Log("场景开始迁移");
            SceneManager.LoadScene(1);
        }
    }
    
    // 启动技能浏览器模式
    public void ToSkillShowerMode()
    {
        SkillConfigTable.LoadAllSkillConfigs();
        foreach (var _pair in SkillConfigTable.SkillConfigRefDic)
        {
            var stoneInfo = new StoneOfPlayerInfo
            {
                InstanceId = (Stones.Dic.Count + 1).ToString(),
                skillId = _pair.Value.RECORD_ID,
                level = 1,
                Inherent = "false"
            };
            Stones.Add(stoneInfo);
        }
        
        var unitConfigs = Units.RowToConfigList(Units.rowList);
        var i = 0;
        foreach (var unitConfig in unitConfigs)
        {
            var unitInfo = new UnitInfo
            {
                id = unitConfig.RECORD_ID,
                r_id = i.ToString()
            };
            
            Debug.Log("尝试将角色" + unitConfig.REAL_NAME + "加入存档");
            DicAdd<string, UnitInfo>.Add(MyMonsters.Dic, unitInfo.id, unitInfo);
            
            var INHERENTSkills = SkillConfigTable.GetPassiveSkills(unitConfig.RECORD_ID);
            if (INHERENTSkills == null || INHERENTSkills.RECORD_ID == null)
            {
                continue;
            }
            
            var stoneInfo = new StoneOfPlayerInfo
            {
                InstanceId = (Stones.Dic.Count + 1).ToString(),
                skillId = INHERENTSkills.RECORD_ID,
                level = 1,
                Inherent = "true",
                unitInstanceId = i.ToString(),
                slot = "1"
            };
            Stones.Add(stoneInfo);
            i++;
        }
        SceneManager.LoadScene(1);
    }
    
    // 启动网络模式
    public void BeginNetMode()
    {
        Debug.Log("try loggin");
        PlayFabReadClient.LoginByDevice(
            result => {
                Debug.Log("loggin");
                CloudScript.CheckIn();
                EnterFrontScene();
            },
            fail => {
                Debug.Log("login fail");
            }
        );
    }
}
