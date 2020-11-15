using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

public class StageScriptableObject : ScriptableObject
{
    public FightEventType _fightEventType = FightEventType.Arena;
    
    [SerializeField]
    public int LocalFightID;
    [SerializeField]
    public int EntryMemberNum;
    [SerializeField]
    public int BattleGroundID;
    
    [SerializeField]
    public string battleNameENG;
    [SerializeField]
    public string battleNameJPG;
    [SerializeField]
    public string battleNameCH;
    
    [SerializeField]
    public PlayableAsset beforefightstory;
    [SerializeField]
    public TextAsset Script;
    [SerializeField]
    public Sprite StageButtonSprite;
    
    public LocalFight localFight = new LocalFight();
    public int stageLevel = 1;
    public float Team1HpRate = 1f;
    public float Team2HpRate = 1f;
    public CriticalGaugeMode team1CGMode = CriticalGaugeMode.normal;
    public CriticalGaugeMode team2CGMode = CriticalGaugeMode.normal;
    public TeamMode Team1Mode = TeamMode.multiraid;
    public TeamMode Team2Mode = TeamMode.multiraid;
    
    #if UNITY_EDITOR
    [MenuItem ("Stage/Create StageScriptEditor")]
    static void CreateExampleAsset ()
    {
        var exampleAsset = CreateInstance<StageScriptableObject> ();
        AssetDatabase.CreateAsset (exampleAsset, "Assets/StagesManagement/ExampleStageAsset.asset");
        AssetDatabase.Refresh();
    }
    #endif
    
    public void LoadLocalFightFromScript()
    {
        localFight = LocalFight.LoadOneLocalFightByScript(Script);
        localFight.SetEnemyLevel(stageLevel);
    }
    
    public List<string> GetTeam1EnterRingLocalIds(LocalFight localFight)
    {
        List<string> enterRingLocalIDs = new List<string>();
        foreach(CharDataInfo _one in localFight.HeroSets.values)
        {
            if (!enterRingLocalIDs.Contains(_one.monsterOfPlayerId))
                enterRingLocalIDs.Add(_one.monsterOfPlayerId);
        }
        return enterRingLocalIDs;
    }
    
    public static StageScriptableObject ArenaStage(LocalFight LocalFight)
    {
        StageScriptableObject stage = CreateInstance<StageScriptableObject>();
        stage.localFight = LocalFight;
        stage.BattleGroundID = 0;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage._fightEventType = FightEventType.Arena;
        return stage;
    }
    
    public static StageScriptableObject RandomStage()
    {
        StageScriptableObject stage = CreateInstance<StageScriptableObject>();
        stage.localFight = StagesManager.RandomFight();
        stage.BattleGroundID = 0;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage._fightEventType = FightEventType.Arena;
        return stage;
    }
    
    public static StageScriptableObject RandomSkillTestStage(TeamMode teamMode)
    {
        StageScriptableObject stage = CreateInstance<StageScriptableObject>();
        stage.localFight = StagesManager.RandomSkillTest(teamMode);
        stage.BattleGroundID = 0;
        stage.Team1Mode = teamMode;
        stage.Team2Mode = teamMode;
        stage._fightEventType = FightEventType.SkillTest;
        return stage;
    }
}

public enum CriticalGaugeMode
{
    normal,
    doubleGain,
    Unlimited
}

// 系统会根据这个量来决定一场战斗结束后应该做什么。
// 比如一个剧情战斗，他结束了后应该是播放某个动画片，
// 再比如是自己打自己的一个战斗，结束后回到的应该是那个自己打自己的选人菜单。
public enum FightEventType
{
    Screensaver = 0,
    Quest = 1,
    Arena = 2,
    Self = 4,
    SkillTest = 5,
    Test = 6
}

public enum TeamMode
{
    multiraid = 1,
    rotation = 2
}