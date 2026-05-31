using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using dataAccess;
using System.IO;
using System.Linq;
using mainMenu;
using MCombat.Shared.Combat;
using NoSuchStudio.Common;
using PlayFab.ClientModels;

public partial class FightInfo : ScriptableObject
{
    public int battleGroundID;
    public int fightBGM = 0;
    [SerializeField] string storyKey;
    // 底下这个记录的是敌人的信息
    [SerializeField] List<UnitInfo> unitsData = new List<UnitInfo>();
    [SerializeField] FightMode fightMode = FightMode.Rotate;
    public float team1HpRate = 1f;
    public float team2HpRate = 1f;
    public CriticalGaugeMode team1CGMode = CriticalGaugeMode.Normal;
    public CriticalGaugeMode team2CGMode = CriticalGaugeMode.Normal;
    public AIMode team1AIMode = AIMode.Aggressive;
    public AIMode team2AIMode = AIMode.Aggressive;
    public int dumbAIDecisionDelay = 20;
    public int dreamComboAIRateNum = 5;
    public float stageRefLevel;
    
    public string StoryKey => storyKey;
    
    public FightMode FightMode
    {
        get => fightMode;
        set => fightMode = value;
    }

    public bool IsGroupBattle => FightControlPolicy.IsGroupBattle(FightMode, EventType);
    public bool AllowsManualUnitControl => FightControlPolicy.AllowsManualUnitControl(FightMode, EventType);
    public bool ShouldForceAutoBattle =>
        FightControlPolicy.ShouldForceAutoBattle(FightMode, EventType);
    public bool ShouldRunFirstQuestTutorial =>
        FightControlPolicy.ShouldRunFirstQuestTutorial(FightMode, ID, EventType);

    public UnitInfo GetRepresentUnitInfo()
    {
        return FightMembers.EnemySets.GetValues().FirstOrDefault(x => x != null && x.id != null && Units.GetUnitConfig(x.r_id) != null);
    }
    
    public List<UnitInfo> UnitsData
    {
        get => unitsData;
        set => unitsData = value;
    }
    
    public string GetBGMKey()
    {
        switch (fightBGM)
        {
            case 0:
                return CommonSetting.FightThemeAddressKey1;
            case 1:
                return CommonSetting.FightThemeAddressKey2;
            default:
                return CommonSetting.FightThemeAddressKey1;
        }
    }
    
    public FightEventType EventType
    {
        set;
        get;
    }
    
    public void SetUnitLevelByRefLevel()
    {
        if (fightMode != FightMode.Evolve)
        {
            foreach (var data in UnitsData)
            {
                data.level = stageRefLevel;
            }
        }
        else
        {
            // 原本希望让敌人按登场顺序逐渐等级提升。。。
            for (var index = 0; index < UnitsData.Count; index++)
            {
                var unitInfo = UnitsData[index];
                unitInfo.level = stageRefLevel;
            }
        }
    }
    
    private List<List<int>> EnemyForEvolutionTeamUnitSets = new List<List<int>>
    {
        new List<int>(){8,11,16,1},
        new List<int>(){8,11,16,2},
        new List<int>(){8,11,16,3},
        new List<int>(){9,9,9,2},
        new List<int>(){13,13,13,5},
        new List<int>(){12,12,12,6},
        new List<int>(){10,10,10,14},
        new List<int>(){9,9,9,1},
        new List<int>(){11,11,11,15},
        new List<int>(){16,16,16,3},
        new List<int>(){1,2,3,7},
        new List<int>(){5,5,5,6},
        new List<int>(){3,6,4,14},
        new List<int>(){13,13,13,12},
        new List<int>(){15,15,15,15},
        new List<int>(){9,9,9,10},
    };

    // 我们设想这个玩法下玩家一共进化三次
    private readonly int _evolutionEnemyCount = 4;
    public void AutoFillEvolution(FightMembers target, string type)
    {
        var enemyRSet = EnemyForEvolutionTeamUnitSets.Random();
        var recordIds = Units.GetMonsterIDsAndNamesDic(type).Keys.ToList();
        for (var index = 0; index < _evolutionEnemyCount; index++)
        {
            var currentUnit = target.EnemySets.Get(0, index);
            var config = Units.GetUnitConfig(currentUnit?.r_id);
            if (currentUnit != null && config != null) continue;
            
            var unitInfo = new UnitInfo
            {
                id = index.ToString(),
                r_id = enemyRSet.Count > index ? enemyRSet[index].ToString() : recordIds.Random()
            };
            target.EnemySets.Set(0, index, unitInfo);
            SaveDicToData();
        }
        
        for (var index = 0; index < UnitsData.Count; index++)
        {
            var unitInfo = UnitsData[index];
            if (unitInfo.set.CheckEdit() != SkillSet.SkillEditError.Empty)
            {
                continue;
            }
            
            var form = new SkillStonesBox.StoneFilterForm
            {
                Type = type,
                ExType = new[] { 0 }
            };
            var passiveSKillRecordId = UnitPassiveTable.GetUnitPassiveRecordId(unitInfo.r_id);
            switch (index)
            {
                case 0:
                    unitInfo.set =  SkillSet.RandomSkillSet(passiveSKillRecordId,  false, form, false);
                    break;
                case 1:
                    form = new SkillStonesBox.StoneFilterForm
                    {
                        Type = type,
                        ExType = new[] { 0 , 1 }
                    };
                    unitInfo.set =  SkillSet.RandomSkillSet(passiveSKillRecordId, false, form, false);
                    break;
                case 2:
                    form = new SkillStonesBox.StoneFilterForm
                    {
                        Type = type,
                        ExType = new[] { 0, 1, 2 }
                    };
                    unitInfo.set =  SkillSet.RandomSkillSet(passiveSKillRecordId, false, form, false);
                    break;
                default:
                    form = new SkillStonesBox.StoneFilterForm
                    {
                        Type = type,
                        ExType = new[] { 0, 1, 2, 3 }
                    };
                    unitInfo.set =  SkillSet.RandomSkillSet(passiveSKillRecordId, false, form, false);
                    break;
            }
        }
    }
    
    public bool RunTutorial
    {
        set;
        get;
    }

    public string Team1OneWord
    {
        set;
        get;
    }
    
    public string Team2OneWord
    {
        set;
        get;
    }
    
    public void Awake()
    {
        OpenAndSetEnemyDataOnPlace();
    }

    public string ID
    {
        set;
        get;
    }
    public string Team1ID { set; get; }
    public string Team2ID { set; get; }
    
    public PlayerLeaderboardEntry Team1LeaderboardEntry {
        set;
        get;
    }
    
    public PlayerLeaderboardEntry Team2LeaderboardEntry {
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
        
        AssetDatabase.CreateAsset(fightInfo, path + "/" + fileName + ".asset");
        Debug.Log("Generated：" + path + "/" + fileName + ".asset");
        AssetDatabase.Refresh();
        return fightInfo;
    }
    
    #endif

    public void OpenAndSetEnemyDataOnPlace()
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
        foreach (var info in FightMembers.EnemySets.GetValues())
        {
            if (info != default)
                unitsData.Add(info);
        }
    }
    
    public void LoadMyTeam()
    {
        PosKeySet set;
        switch (EventType)
        {
            case FightEventType.Quest:
                if (fightMode == FightMode.Group)
                {
                    set = TeamSet.Gangbang;
                }
                else
                {
                    if (fightMode == FightMode.Evolve)
                    {
                        set = TeamSet.Default;
                    }
                    else
                    {
                        set = TeamSet.Origin;
                    }
                }
                break;
            case FightEventType.Arena:
                set = TeamSet.Arena3V3;
                break;
            case FightEventType.Event:
                set = TeamSet.Origin;
                break;
            default:
                set = TeamSet.Default;
                break;
        }
        
        FightMembers.HeroSets = set.LoadTeamDic();
        Team1ID = PlayerAccountInfo.Me.PlayFabId;
    }
    
    public static FightInfo ArenaStage(FightMembers fightUnits)
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = fightUnits;
        stage.battleGroundID = 0;
        stage.EventType = FightEventType.Arena;
        return stage;
    }

    public static FightInfo Copy(FightInfo source)
    {
        var stage = CreateInstance<FightInfo>();
        
        stage.ID = source.ID;
        stage.FightMembers = source.FightMembers;
        stage.battleGroundID = source.battleGroundID;
        stage.stageRefLevel = source.stageRefLevel;
        stage.fightBGM = source.fightBGM;
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
        stage.Team1LeaderboardEntry = source.Team1LeaderboardEntry;
        stage.Team2LeaderboardEntry = source.Team2LeaderboardEntry;
        stage.RunTutorial = source.RunTutorial;
        stage.fightMode = source.fightMode;
        stage.EventType = source.EventType;
        stage.dreamComboAIRateNum = source.dreamComboAIRateNum;
        stage.storyKey = source.storyKey;
        
        stage.UnitsData = new List<UnitInfo>(source.UnitsData);
        stage.team1GroupSet = new List<SoldierGroupSet>(source.team1GroupSet);
        stage.team2GroupSet = new List<SoldierGroupSet>(source.team2GroupSet);
        return stage;
    }
    
    public static FightInfo RandomSkillTestStage(FightMode fightMode, CriticalGaugeMode criticalGaugeMode)
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = FightMembers.RandomSkillTest(criticalGaugeMode);
        stage.battleGroundID = 0;
        stage.fightBGM = 0;
        stage.Team1Auto = true;
        stage.Team2Auto = true;
        stage.fightMode = fightMode;
        stage.team1CGMode = criticalGaugeMode;
        stage.team2CGMode = criticalGaugeMode;
        stage.EventType = FightEventType.SkillTest;
        return stage;
    }
    
    public static FightInfo ScreenSaverStage(FightMode fightMode)
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = FightMembers.ScreenSaver(fightMode);
        stage.battleGroundID = 0;
        stage.fightBGM = 0;
        stage.Team1Auto = true;
        stage.Team2Auto = true;
        stage.EventType = FightEventType.SkillTest;
        return stage;
    }
    
    public static FightInfo RandomStage(CriticalGaugeMode mode = CriticalGaugeMode.Normal, int unitCount = 3)
    {
        var stage = CreateInstance<FightInfo>();
        stage.FightMembers = FightMembers.RandomFight(mode, unitCount, true);
        stage.battleGroundID = 0;
        stage.fightBGM = 0;
        stage.EventType = FightEventType.Arena;
        return stage;
    }
}
