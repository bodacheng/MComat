using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using dataAccess;

public class FightInfo : ScriptableObject
{
    public FightEventType eventType;
    
    [SerializeField]
    public int LocalFightID;
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
    
    public FightMembers fightMembers = new FightMembers();
    public int stageLevel = 1;
    public float Team1HpRate = 1f;
    public float Team2HpRate = 1f;
    public CriticalGaugeMode team1CGMode = CriticalGaugeMode.normal;
    public CriticalGaugeMode team2CGMode = CriticalGaugeMode.normal;
    public TeamMode Team1Mode;
    public TeamMode Team2Mode;
    
    #if UNITY_EDITOR
    [MenuItem ("Stage/Create StageScriptEditor")]
    static void CreateExampleAsset()
    {
        var exampleAsset = CreateInstance<FightInfo> ();
        AssetDatabase.CreateAsset (exampleAsset, "Assets/StagesManagement/ExampleStageAsset.asset");
        AssetDatabase.Refresh();
    }
    #endif
    
    public void LoadLocalFightFromScript()
    {
        fightMembers = FightMembers.LoadEnemies_Json(Script);
        if (fightMembers != null)
            fightMembers.SetEnemyLevel(stageLevel);
    }

    public void LoadMyTeam()
    {
        PosKeySet set = null;
        switch (eventType)
        {
            case FightEventType.Quest:
                set = TeamSet.Default;
                break;
            case FightEventType.Arena:
                set = TeamSet.Arena3V3;
                break;
        }
        fightMembers.HeroSets = TeamSet.ToDic(set);
    }
    
    public List<string> GetTeam1EnterRingLocalIds(FightMembers localFight)
    {
        List<string> enterRingLocalIDs = new List<string>();
        foreach(CharDataInfo _one in localFight.HeroSets.GetValues())
        {
            if (!enterRingLocalIDs.Contains(_one.id))
                enterRingLocalIDs.Add(_one.id);
        }
        return enterRingLocalIDs;
    }

    public static FightInfo ArenaStage(FightMembers LocalFight)
    {
        FightInfo stage = CreateInstance<FightInfo>();
        stage.fightMembers = LocalFight;
        stage.BattleGroundID = 0;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage.eventType = FightEventType.Arena;
        return stage;
    }
    
    public static FightInfo RandomStage()
    {
        FightInfo stage = CreateInstance<FightInfo>();
        stage.fightMembers = StagesManager.RandomFight();
        stage.BattleGroundID = 0;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage.eventType = FightEventType.Arena;
        return stage;
    }
    
    public static FightInfo RandomSkillTestStage(TeamMode teamMode)
    {
        FightInfo stage = CreateInstance<FightInfo>();
        stage.fightMembers = StagesManager.RandomSkillTest(teamMode);
        stage.BattleGroundID = 0;
        stage.Team1Mode = teamMode;
        stage.Team2Mode = teamMode;
        stage.eventType = FightEventType.SkillTest;
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