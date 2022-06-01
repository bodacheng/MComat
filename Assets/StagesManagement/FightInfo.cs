using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using dataAccess;
using System.IO;

public class FightInfo : ScriptableObject
{
    public int ID;
    [SerializeField] FightEventType eventType;
    
    public string team1ID{ set; get; }
    public string team2ID{ set; get; }
    
    public FightEventType GetEventType()
    {
        return eventType;
    }
    
    public void SetEventType(FightEventType eventType)
    {
        this.eventType = eventType;
    }
    
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

    public bool team1Auto
    {
        get;
        set;
    }
    
    public bool team2Auto
    {
        get;
        set;
    }
    
    #if UNITY_EDITOR
    public static FightInfo CreateFightInfoAsset(TextAsset file, string path, string fileName)
    {
        var fightInfo = CreateInstance<FightInfo>();
        fightInfo.eventType = FightEventType.Quest;
        if (!Directory.Exists(path))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(path);
        }

        fightInfo.Team1Mode = TeamMode.rotation;
        fightInfo.Team2Mode = TeamMode.rotation;
        
        fightInfo.Script = file;
        AssetDatabase.CreateAsset(fightInfo, path + "/" + fileName + ".asset");
        AssetDatabase.Refresh();
        return fightInfo;
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
            default:
                set = TeamSet.Default;
                break;
        }
        fightMembers.HeroSets = TeamSet.ToDic(set);
    }
    
    public static FightInfo ArenaStage(FightMembers fightUnits)
    {
        var stage = CreateInstance<FightInfo>();
        stage.fightMembers = fightUnits;
        stage.BattleGroundID = 0;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage.eventType = FightEventType.Arena;
        return stage;
    }
    
    public static FightInfo RandomStage()
    {
        var stage = CreateInstance<FightInfo>();
        stage.fightMembers = FightMembers.RandomFight();
        stage.BattleGroundID = 0;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage.eventType = FightEventType.Arena;
        return stage;
    }
    
    public static FightInfo RandomSkillTestStage(TeamMode teamMode)
    {
        var stage = CreateInstance<FightInfo>();
        stage.fightMembers = FightMembers.RandomSkillTest(teamMode);
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
    SkillTest = 5
}

public enum TeamMode
{
    multiRaid = 1,
    rotation = 2
}