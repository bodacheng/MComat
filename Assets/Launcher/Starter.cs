using dataAccess;
using UnityEngine;
using UnityEngine.SceneManagement;
using mainMenu;
using System.Collections.Generic;
using Skill;

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
            FightInfo stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            stage.SetEventType(FightEventType.Screensaver);
            stage.team1ID = Account._AccInfo.playerID;
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
        foreach (KeyValuePair<string, SkillConfig> _pair in SkillConfigTable.SkillConfigRefDic)
        {
            //Debug.Log("尝试于本地存档追加石：" + _pair.Value.REAL_NAME);
            StoneOfPlayerInfo stoneInfo = new StoneOfPlayerInfo
            {
                InstanceId = (Stones.Dic.Count + 1).ToString(),
                skillId = _pair.Value.RECORD_ID,
                BreakThrough = 0,
                EXP = 0,
                Inherent = "false"
            };
            Stones.Add(stoneInfo);
        }
        
        List<CharConfig> charList = Units.RowToConfigList(Units.rowList);
        int i = 0;
        foreach (CharConfig _CharConfig in charList)
        {
            UnitInfo _Char = new UnitInfo
            {
                id = _CharConfig.RECORD_ID,
                r_id = i.ToString()
            };
                
            KeyValuePair<string, string> INHERENTSkills = INHERENT_SkillTable.GetINHERENTSkill(_CharConfig.RECORD_ID);
            if (INHERENTSkills.Key != null)
            {
                StoneOfPlayerInfo stoneInfo = new StoneOfPlayerInfo
                {
                    InstanceId = (Stones.Dic.Count + 1).ToString(),
                    skillId = INHERENTSkills.Key,
                    EXP = 0,
                    BreakThrough = 0,
                    Inherent = "true",
                    inUsingMonsterOfPlayerId = i.ToString(),
                    inUsingSkillSlot = "1"
                };
                Stones.Add(stoneInfo);
            }
            Debug.Log("尝试将角色" + _CharConfig.REAL_NAME + "加入存档");
            DicAdd<string, UnitInfo>.Add(MyMonsters.Dic, _Char.id, _Char);
            i++;
        }
        SceneManager.LoadScene(1);
    }
    
    // 启动网络模式
    public void BeginNetMode()
    {
        PlayFabReadClient.CustomIDLogin(
            result => {
                Debug.Log(" 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken);
                Account._AccInfo = new PlayerAccountInfo
                {
                    playerID = result.PlayFabId
                };
                CloudScript.CheckIn();
                EnterFrontScene();
            },
            fail => {
                Debug.Log("login fail");
            }
        );
    }
}
