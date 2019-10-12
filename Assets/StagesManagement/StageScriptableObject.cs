using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

public class StageScriptableObject : ScriptableObject
{
    [SerializeField]
    public string LocalFightID;
    [SerializeField]
    public int EntryMemberNum;
    [SerializeField]
    public int BattleGroundID;
    
    [SerializeField]
    public string battleNameENG = null;
    [SerializeField]
    public string battleNameJPG = null;
    [SerializeField]
    public string battleNameCH = null;

    [SerializeField]
    public PlayableAsset beforefightstory;
    [SerializeField]
    public TextAsset Script;

    [SerializeField]
    public Sprite StageButtonSprite;

    public LocalFight localFight;
    
    public fightModeType fightModeType = fightModeType.combat;
    public fightEventType _fightEventType = fightEventType.Arena;

    public TeamMode Team1Mode = TeamMode.multiraid;
    public TeamMode Team2Mode = TeamMode.multiraid;

#if UNITY_EDITOR
    [MenuItem ("Stage/Create StageScriptEditor")]
    static void CreateExampleAsset ()
    {
        var exampleAsset = CreateInstance<StageScriptableObject> ();
    
        AssetDatabase.CreateAsset (exampleAsset, "Assets/StagesManagement/ExampleStageAsset.asset");
        AssetDatabase.Refresh ();
    }
#endif
    
    public void loadLocalFightFromScript()//这个函数的运行必须十分谨慎
    {
        localFight = LocalFight.loadOneLocalFightByScript(Script);    
    }
    
    public List<string> getTeam1EnterRingLocalIds(LocalFight localFight)
    {
        List<string> enterRingLocalIDs = new List<string>();
        foreach(CharacterDataInfo _one in localFight.HeroSets.values)
        {
            if (!enterRingLocalIDs.Contains(_one.monsterOfPlayerId))
                enterRingLocalIDs.Add(_one.monsterOfPlayerId);
        }
        return enterRingLocalIDs;
    }
}

// 这个东西是用来规定我每一场战斗结束之后所自动加载的事件
// 其实相当程度上说这个也决定了每一个关卡的event类型。
// 系统会根据这个量来决定一场战斗结束后应该做什么。
// 比如一个剧情战斗，他结束了后应该是播放某个动画片，
// 再比如是自己打自己的一个战斗，结束后回到的应该是那个自己打自己的选人菜单。
public enum fightEventType
{
    Tutorial_Basic = 0,
    Tutorial_Story_AdamVsGuards = 3,
    Quest = 1,
    Arena = 2,
    Self = 4,
}

public enum fightModeType
{
    combat = 1,
    tower = 2,
}

public enum TeamMode
{
    multiraid = 1,
    rotation = 2,
}