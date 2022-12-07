using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using dataAccess;
using System.IO;

public class FightInfo : ScriptableObject
{
    [SerializeField]
    public int battleGroundID;
    
    [SerializeField]
    public string battleNameENG;
    [SerializeField]
    public string battleNameJPG;
    [SerializeField]
    public string battleNameCH;
    
    [SerializeField]
    public Sprite stageButtonSprite;

    [SerializeField] private List<UnitInfo> unitsData = new ();
    
    public FightEventType EventType
    {
        set;
        get;
    }
    
    public float team1HpRate = 1f;
    public float team2HpRate = 1f;
    public CriticalGaugeMode team1CGMode = CriticalGaugeMode.Normal;
    public CriticalGaugeMode team2CGMode = CriticalGaugeMode.Normal;
    public TeamMode team1Mode = TeamMode.Rotation;
    public TeamMode team2Mode = TeamMode.Rotation;
    public AIMode team1AIMode = AIMode.Aggressive;
    public AIMode team2AIMode = AIMode.Aggressive;
    
    public int dumbAIDecisionDelay = 50;

    public bool RunTutorial
    {
        set;
        get;
    }
    
    public void Awake()
    {
        Open();
    }

    public string ID
    {
        set;
        get;
    }
    public string Team1ID{ set; get; }
    public string Team2ID{ set; get; }
    
    public int Team2ArenaPoint {
        set;
        get;
    }
    
    public FightMembers FightMembers
    {
        set;
        get;
    }
    
    public bool Team1Auto
    {
        get;
        set;
    }
    
    public bool Team2Auto
    {
        get;
        set;
    }
    
    #if UNITY_EDITOR
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetTeam"></param>
    /// <param name="path">"Assets/" 开头</param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static FightInfo CreateFightInfoAsset(FightMembers targetTeam, string path, string fileName)
    {
        var fightInfo = CreateInstance<FightInfo>();
        if (!Directory.Exists(path))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(path);
        }
        
        fightInfo.FightMembers = targetTeam;
        fightInfo.SaveDicToData();
        fightInfo.team1Mode = TeamMode.Rotation;
        fightInfo.team2Mode = TeamMode.Rotation;
        
        AssetDatabase.CreateAsset(fightInfo, path + "/" + fileName + ".asset");
        Debug.Log("Generated：" + path + "/" + fileName + ".asset");
        AssetDatabase.Refresh();
        return fightInfo;
    }
    #endif

    public void Open()
    {
        ID = this.name;
        FightMembers = new FightMembers();
        for (var i = 0; i < unitsData.Count; i++)
        {
            FightMembers.EnemySets.Set(0,i, unitsData[i]);
        }
    }

    public void SaveDicToData()
    {
        unitsData = new List<UnitInfo>();
        var one = FightMembers.EnemySets.Get(0, 0);
        var two = FightMembers.EnemySets.Get(0, 1);
        var three = FightMembers.EnemySets.Get(0, 2);

        if (one != default)
            unitsData.Add(one);
        if (two != default)
            unitsData.Add(two);
        if (three != default)
            unitsData.Add(three);
    }
    
    public void LoadMyTeam()
    {
        PosKeySet set;
        switch (EventType)
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
        FightMembers.HeroSets = TeamSet.ToDic(set);
    }
    
    public static FightInfo ArenaStage(FightMembers fightUnits)
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = fightUnits;
        stage.battleGroundID = 0;
        stage.team1Mode = TeamMode.Rotation;
        stage.team2Mode = TeamMode.Rotation;
        stage.EventType = FightEventType.Arena;
        return stage;
    }

    public static FightInfo Copy(FightInfo source)
    {
        var stage = CreateInstance<FightInfo>();
        
        stage.ID = source.ID;
        stage.FightMembers = source.FightMembers;
        stage.battleGroundID = source.battleGroundID;
        stage.team1Mode = source.team1Mode;
        stage.team2Mode = source.team2Mode;
        stage.Team1Auto = source.Team1Auto;
        stage.Team2Auto = source.Team2Auto;
        stage.team1AIMode = source.team1AIMode;
        stage.team2AIMode = source.team2AIMode;
        stage.Team1ID = source.Team1ID;
        stage.Team2ID = source.Team2ID;
        stage.team1HpRate = source.team1HpRate;
        stage.team2HpRate = source.team2HpRate;
        stage.team1CGMode = source.team1CGMode;
        stage.team2CGMode = source.team2CGMode;
        stage.Team2ArenaPoint = source.Team2ArenaPoint;
        stage.RunTutorial = source.RunTutorial;
        stage.battleNameCH = source.battleNameCH;
        stage.battleNameENG = source.battleNameENG;
        stage.battleNameJPG = source.battleNameJPG;
        stage.stageButtonSprite = source.stageButtonSprite;
        stage.EventType = source.EventType;
        
        Debug.Log("已经获取");
        return stage;
    }
    
    public static FightInfo RandomSkillTestStage(TeamMode teamMode)
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = FightMembers.RandomSkillTest(teamMode);
        stage.battleGroundID = 0;
        stage.team1Mode = teamMode;
        stage.team2Mode = teamMode;
        stage.EventType = FightEventType.SkillTest;
        return stage;
    }
    
    public static FightInfo RandomStage()
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = FightMembers.RandomFight();
        stage.battleGroundID = 0;
        stage.team1Mode = TeamMode.Rotation;
        stage.team2Mode = TeamMode.Rotation;
        stage.EventType = FightEventType.Arena;
        return stage;
    }
}

public enum CriticalGaugeMode
{
    Normal,
    DoubleGain,
    Unlimited
}

public enum AIMode
{
    Aggressive,
    Dumb
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
    MultiRaid = 1,
    Rotation = 2
}