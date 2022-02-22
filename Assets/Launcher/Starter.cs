using dataAccess;
using UnityEngine;
using UnityEngine.SceneManagement;
using mainMenu;

public class Starter : MonoBehaviour
{
    [SerializeField] bool enterFrontPageFirst;
    [SerializeField] PlayfabSetting PlayfabSetting;
    [SerializeField] FightGlobalSetting FightGlobalSetting;
    
    void EnterFrontScene()
    {
        FightGlobalSetting.Initialise();
        PlayfabSetting.Initialise();
        
        // 这几个东西用不用执行待定
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        Units.LoadByResource();
        Units.RefreshDic();
        
        if (enterFrontPageFirst)
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            stage.SetEventType(FightEventType.Screensaver);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
            FightLoad.Go(stage);
        }else{
            MainMenuNote.goingtostep = MainSceneStep.FrontPage;
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
                BreakThrough = 0,
                EXP = 0,
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
            
            var INHERENTSkills = INHERENT_SkillTable.GetINHERENTSkill(unitConfig.RECORD_ID);
            if (INHERENTSkills.Key != null)
            {
                var stoneInfo = new StoneOfPlayerInfo
                {
                    InstanceId = (Stones.Dic.Count + 1).ToString(),
                    skillId = INHERENTSkills.Key,
                    EXP = 0,
                    BreakThrough = 0,
                    Inherent = "true",
                    inUsingUnitInstanceId = i.ToString(),
                    inUsingSkillSlot = "1"
                };
                Stones.Add(stoneInfo);
            }
            Debug.Log("尝试将角色" + unitConfig.REAL_NAME + "加入存档");
            DicAdd<string, UnitInfo>.Add(MyMonsters.Dic, unitInfo.id, unitInfo);
            i++;
        }
        SceneManager.LoadScene(1);
    }
    
    // 启动网络模式
    public void BeginNetMode()
    {
        PlayFabReadClient.LoginByDevice(
            result => {
                CloudScript.CheckIn();
                EnterFrontScene();
            },
            fail => {
                Debug.Log("login fail");
            }
        );
    }
}
