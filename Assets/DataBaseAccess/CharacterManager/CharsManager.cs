using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class CharsManager : MonoBehaviour {

    public Transform _dontDestroyOnLoadParent;
    public static Transform dontDestroyOnLoadParent;
    public static IDictionary<int, CharacterResourceInfo> CharacterResourceInfoDic = new Dictionary<int, CharacterResourceInfo>();
    public static monstersConfigTable _monstersConfigTable = new monstersConfigTable();

    public TeamConfig heroTeamConfig = new TeamConfig(Team.player1, new List<Team>() { Team.player2 });
    public TeamConfig EnemyTeamConfig = new TeamConfig(Team.player2, new List<Team>() { Team.player1 });
    public IDictionary<Team, List<Data_Center>> TeamMembers;//keys: Player1 Player2

    void Awake()
    {
        heroTeamConfig = new TeamConfig(Team.player1, new List<Team>() { Team.player2 });
        EnemyTeamConfig = new TeamConfig(Team.player2, new List<Team>() { Team.player1 });
        TeamMembers = new Dictionary<Team, List<Data_Center>>();
        if (dontDestroyOnLoadParent == null)
            dontDestroyOnLoadParent = _dontDestroyOnLoadParent;
        else
            Debug.Log("已经找到非销毁对象parent");
        DontDestroyOnLoad(dontDestroyOnLoadParent);
    }

    public static CharacterResourceInfo getCharacterResourceInfo(int resourceId)
    {
        if (CharacterResourceInfoDic.ContainsKey(resourceId))
            return CharacterResourceInfoDic[resourceId];
        else
            return null;
    }

    public static void loadMonsterDataBaseFileByResource()
    {
        //暂时做如下处理
        TextAsset CSV = Resources.Load("Account/MonstersConfig") as TextAsset;
        if (CSV)
        {
            _monstersConfigTable.Load(CSV);
        }
        else
            Debug.Log("没能读取到角色数据库文件。");
    }
    
    public static void refreshCharacterResourceInfoDic()
    {
        List<monstersConfigTable.Row> rows = _monstersConfigTable.rowList;
        CharacterResourceInfoDic.Clear();
        List<CharacterResourceInfo> characterResourceInfos = _monstersConfigTable.RowToCharacterResourceInfoList(rows);
        foreach (CharacterResourceInfo one in characterResourceInfos)            
        {
            CharacterResourceInfoDic.Add(one.charResouceNum,one);
        }
    }

    public void preventTheseMyModelsFromDestroying(List<int> myCharLocalIDForNextBattle)
    {
        if (myCharLocalIDForNextBattle == null)
            return;

        for (int i = 0; i < myCharLocalIDForNextBattle.Count;i++)
        {
            if (myModelPool.Instance.ModelDicBasedOnPlayerLocalID.ContainsKey(myCharLocalIDForNextBattle[i])
               &&
                myModelPool.Instance.ModelDicBasedOnPlayerLocalID[myCharLocalIDForNextBattle[i]] != null)
            {
                myModelPool.Instance.ModelDicBasedOnPlayerLocalID[myCharLocalIDForNextBattle[i]].transform.parent = dontDestroyOnLoadParent;
            }
        }
    }

    //这个的目的是把加载好的各个角色给放到预定的位置上去。从而把安排角色位置这个工作给从角色生成环节给分离出去。
    public void ArrangeAllCharacterToPosition(
        LocalFight localFight,
        Transform[] Team1StandPoints, Transform[] Team2StandPoints,
        IDictionary<int, GameObject> Team1ModelReferenceDic, IDictionary<int, GameObject> Team2ModelReferenceDic,
        positionLocalCharKeySet _positionLocalCharKeySetTeam1,
        positionLocalCharKeySet _positionLocalCharKeySetTeam2)
    {
        foreach (CharacterDataInfo _CharacterDataInfo in localFight.team1members)
        {
            GameObject OneOfEnemies;
            Team1ModelReferenceDic.TryGetValue(_CharacterDataInfo.localID, out OneOfEnemies);

            if (OneOfEnemies == null)
            {
                Debug.Log("以下team1角色加载出错。LocalID" + _CharacterDataInfo.localID);
                continue;
            }

            switch (_positionLocalCharKeySetTeam1.getPosMemInfoByLocalID(_CharacterDataInfo.localID).posNum)
            {
                case PosNum.back:
                    OneOfEnemies.transform.position = Team1StandPoints[0].position;
                    OneOfEnemies.transform.rotation = Team1StandPoints[0].rotation;
                    break;
                case PosNum.left:
                    OneOfEnemies.transform.position = Team1StandPoints[1].position;
                    OneOfEnemies.transform.rotation = Team1StandPoints[1].rotation;
                    break;
                case PosNum.front:
                    OneOfEnemies.transform.position = Team1StandPoints[2].position;
                    OneOfEnemies.transform.rotation = Team1StandPoints[2].rotation;
                    break;
                case PosNum.right:
                    OneOfEnemies.transform.position = Team1StandPoints[3].position;
                    OneOfEnemies.transform.rotation = Team1StandPoints[3].rotation;
                    break;
            }
            OneOfEnemies.transform.parent = null;
            OneOfEnemies.SetActive(true);
        }

        foreach (CharacterDataInfo _CharacterDataInfo in localFight.Enemies)
        {
            GameObject OneOfEnemies;
            Team2ModelReferenceDic.TryGetValue(_CharacterDataInfo.localID, out OneOfEnemies);

            if (OneOfEnemies == null)
            {
                Debug.Log("以下team2角色加载出错。LocalID"+ _CharacterDataInfo.localID);
                continue;
            }

            switch (_positionLocalCharKeySetTeam2.getPosMemInfoByLocalID(_CharacterDataInfo.localID).posNum)
            {
                case PosNum.back:
                    OneOfEnemies.transform.position = Team2StandPoints[0].position;
                    OneOfEnemies.transform.rotation = Team2StandPoints[0].rotation;
                    break;
                case PosNum.left:
                    OneOfEnemies.transform.position = Team2StandPoints[1].position;
                    OneOfEnemies.transform.rotation = Team2StandPoints[1].rotation;
                    break;
                case PosNum.front:
                    OneOfEnemies.transform.position = Team2StandPoints[2].position;
                    OneOfEnemies.transform.rotation = Team2StandPoints[2].rotation;
                    break;
                case PosNum.right:
                    OneOfEnemies.transform.position = Team2StandPoints[3].position;
                    OneOfEnemies.transform.rotation = Team2StandPoints[3].rotation;
                    break;
            }
            OneOfEnemies.transform.parent = null;
            OneOfEnemies.SetActive(true);
        }
    }

    public void step3IniForALLPlayers()
    {
        foreach (KeyValuePair<Team, List<Data_Center>> _pair in TeamMembers)
        {
            foreach (Data_Center _Data_Center in _pair.Value)
            {
                switch (_pair.Key)
                {
                    case Team.player1:
                        _Data_Center.step3Initialize(heroTeamConfig,new playerBattleInfo());
                        break;
                    case Team.player2:
                        _Data_Center.step3Initialize(EnemyTeamConfig, new playerBattleInfo());
                        break;
                }
            }
        }
    }
    
        // 本地系函数 （只有本地游戏可用，否则可能产生逻辑问题，因为牵扯到网络物体控制权问题）
    public void changeAllCharactersState(string num)
    {
        foreach (KeyValuePair<Team, List<Data_Center>> _KeyValuePair in TeamMembers)
        {
            foreach(Data_Center _char in _KeyValuePair.Value)
            {
                _char.getRunner().changeState(num);
            }
        }
    }

    // 通用系函数
    public List<Data_Center> getTeamMembers(Team team)
    {
        if (TeamMembers == null)
            return null;
        List<Data_Center> Members = null;
        TeamMembers.TryGetValue(team, out Members);
        return Members;
    }

    public void addNewMemberToTeamMemberDic(Data_Center _newMem,Team team)
    {
        Debug.Log(team + "加入队员字典" + _newMem.gameObject.name);
        List<Data_Center> membersOftheTeam = getTeamMembers(team);
        if (membersOftheTeam == null)
        {
            membersOftheTeam = new List<Data_Center>() { _newMem };
            if (TeamMembers.ContainsKey(team))
                TeamMembers[team] = membersOftheTeam;
            else
                TeamMembers.Add(new KeyValuePair<Team,List<Data_Center>>(team,membersOftheTeam));
        }
        else{
            membersOftheTeam.Add(_newMem);
        }
    }

    // 通用系函数
    public bool checkIfEveryTeamHasMember()
    {
        if (TeamMembers != null)
        {
            if (TeamMembers.Count < 2)
            {
                return false;
            }
            foreach (KeyValuePair<Team, List<Data_Center>> MembersOfOneTeam in TeamMembers)
            {
                if (MembersOfOneTeam.Value.Count() == 0)
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    // 通用系函数
    public void arrangeMemberOfEveryTeam()//这是个从场景里找DATACENTER这个元件的过程.其实我们已经对这个环节相当怀疑，因为这个函数的存在违背了一些事理观。哪个队伍都有哪些角色，系统自己在造角色钱不知道吗？
    {
        IDictionary<Team, List<Data_Center>> _dic = new Dictionary<Team, List<Data_Center>>();
        List<Data_Center> _charsOfTheFight = Transform.FindObjectsOfType<Data_Center>().ToList();
        List<string> tags = new List<string>();
        foreach (Data_Center _D in _charsOfTheFight)
        {
            if (_D._TeamConfig != null)
            {
                if (!tags.Contains(_D._TeamConfig.my_tag))
                {
                    tags.Add(_D._TeamConfig.my_tag);
                    _dic.Add(new KeyValuePair<Team, List<Data_Center>>(_D._TeamConfig.myTeam, new List<Data_Center>() { _D }));
                }
                else
                {
                    List<Data_Center> membersOfTheTag = new List<Data_Center>();
                    _dic.TryGetValue(_D._TeamConfig.myTeam, out membersOfTheTag);
                    membersOfTheTag.Add(_D);//这个环节有我们对C#的一点疑问。
                }
            }
        }
        TeamMembers = _dic;
    }

    // 本地系函数 （只有本地游戏可用，否则可能产生逻辑问题，因为牵扯到网络物体控制权问题）
    public void changeTeamCharactersState(Team team, string num)
    {
        foreach (Data_Center _char in getTeamMembers(team))
        {
            _char.getRunner().changeState(num);
        }
    }

    public CharacterDataInfo gotcha()
    {
        return null;
    }
    
    //现在我们发现其实在一般战斗模式下，场景的大部分时间是角色记载的时间，这些东西在内存里完成后其实并不急于立刻安排其位置。
    //我们现在要有一个机制把所有角色都在内存加载后，靠一些动画来把角色稳定的放在指定位置并可以方便编辑演出。。。
    //感觉这个事情其实也不那么需要这么深入的贯彻什么系统化思想，只要把所有那几个基本元件的位置啥的都在场景里给固定好了就可以了
    public bool ifAllCharsPreparedForBattle()
    {
        if (TeamMembers == null)
            return false;
        
        foreach (KeyValuePair<Team, List<Data_Center>> oneTeam in TeamMembers)
        {
            foreach (Data_Center oneMember in oneTeam.Value)
            {
                if (!oneMember.ifPreparedForBattle())
                    return false;
            }
        }
        return true;
    }

    //这个函数是建立在这样的前提下：我们认为从数据库获取的玩家拥有角色，localid是正常的(不重复)
    //如果localid产生重复的情况下这个函数被执行，将产生大量紊乱。
    public IEnumerator buildTheseMyModels(CharacterDataInfo[] myChars)
    {
        foreach(CharacterDataInfo one in myChars)
        {
            if (one != null)
                yield return (buildShowModel(one));
        }
    }

    public IEnumerator buildShowModel(CharacterDataInfo myChar)
    {
        if (myChar == null)
        {
            Debug.Log("流程错误");
            yield break;
        }
        switch (defaultPools.Instance.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return (CreateModelForShowingByCach(myModelPool.Instance.ModelDicBasedOnPlayerLocalID, myChar.localID, myChar));
                break;
            case ResourceLoadMode.StreamingAssetAB:
                yield return (CreateModelForShowingByStreamingAssets(myModelPool.Instance.ModelDicBasedOnPlayerLocalID, myChar.localID, myChar));
                break;
            case ResourceLoadMode.Resource:
                yield return (CreateModelForShowingByResource(myModelPool.Instance.ModelDicBasedOnPlayerLocalID, myChar.localID, myChar));
                break;
        }
    }
    
    //这些都是中间变量
    GameObject _TempModel;
    NineAndTwo _TempNineAndTwo;
    CharacterResourceInfo _TempCharacterResourceInfo;
    AI_DATA_CENTER _TempDATACENTER;
    public IEnumerator CreateCharacter(IDictionary<int, GameObject> ReferenceDic, int IDinReferenceDic, CharacterDataInfo _CharacterDataInfo, Team team)
    {
        switch(defaultPools.Instance.ModelLoadingMode)
        {
                case ResourceLoadMode.CachAB:
                yield return (CreateModelForShowingByCach(ReferenceDic, IDinReferenceDic, _CharacterDataInfo));
                    addNewMemberToTeamMemberDic(_TempDATACENTER, team);
                break;
                case ResourceLoadMode.Resource:
                yield return (CreateModelForShowingByResource(ReferenceDic, IDinReferenceDic, _CharacterDataInfo));
                    addNewMemberToTeamMemberDic(_TempDATACENTER, team);
                break;
                case ResourceLoadMode.StreamingAssetAB:
                yield return (CreateModelForShowingByStreamingAssets(ReferenceDic, IDinReferenceDic, _CharacterDataInfo));
                    addNewMemberToTeamMemberDic(_TempDATACENTER, team);
                break;
        }
        yield return (_TempDATACENTER.step2Initialize
            (_TempCharacterResourceInfo.type,
             _TempNineAndTwo,
             _TempCharacterResourceInfo.getPassiveSkillConfigs(),
             _TempNineAndTwo.level,
             _TempCharacterResourceInfo._zokusei,
             _TempCharacterResourceInfo.personalMagicPack));         
    }

    public void changeAllCharactersState(List<AIStateRunner> list,string num)
	{
		foreach(AIStateRunner A_player in list)
		{
			A_player.changeState(num);
		}
	}
}

//   public void CreateCharacterNet(string type, int resource_num, int AISeries, int level, Vector3 position)
//{     
//       photonView.RPC("CreateCharacterRPC", PhotonTargets.All, PhotonNetwork.player.ID, type,resource_num, AISeries, level, position);
//}

//   public void CreateCharacterRPC(int playerID, string type, int resource_num ,int AISeries,int level,Vector3 position) 
//{        
//  string tag_selected = "Player" + playerID;
//  string[] enemy_tag_selected;
//  if (tag_selected == "Player1") {
//      enemy_tag_selected = new string[]{ "Player2" };
//  } else {
//      enemy_tag_selected = new string[]{ "Player1" };
//  }               

//       GameObject pretab = null;
//       charDataBaseSet targetCharDataBaseSet = this.getCharDataBaseSetByType(type);
//       if (targetCharDataBaseSet != null)
//       {
//           pretab = targetCharDataBaseSet._CharResourceDataBase.GetByID(resource_num).prefab;
//           if (prefab)
//           {
//               if (prefab.GetComponent<AI_DATA_CENTER>())
//               {
//                   prefab.GetComponent<AI_DATA_CENTER>()._loadMode = loadMode.fightModel;
//               }
//           }
//           else
//           {
//               Debug.Log(targetCharDataBaseSet + "pretab不存在");
//               return;
//           }
//       }
//       else
//       {
//           return;
//           Debug.Log(type + "类型角色不存在");
//       }

//       GameObject one_char = PhotonNetwork.InstantiateSceneObject(pretab.name, position, new Quaternion(0, 0, 0, 0),0,null);
//  //one_char.GetComponent<AI_DATA_CENTER>().callInitiate(tag_selected,enemy_tag_selected);
//  //one_char.GetComponent<AIStateRunner> ().callStatesTransitionInitiate (AISeries,level);
//  if (one_char.GetComponent<AIStateRunner>().State_Transition_Set_List.Count < 1)
//  {
//      Debug.Log("AI script loading error,this is fatal");
//  }
//  one_char.GetComponent<AIStateRunner>().changeState("Empty");//这个，其实如果是要立刻转移至其他客户端的角色的话，就有点微妙。
//  if (playerID != PhotonNetwork.room.MasterClientId) 
//  {
//      one_char.GetComponent<PhotonView> ().TransferOwnership (playerID);
//      Debug.Log ("TransRemotePlayer : " + playerID);
//  } else {
//      one_char.GetComponent<PhotonView> ().TransferOwnership (PhotonNetwork.room.MasterClientId);
//      Debug.Log ("TransToMaster" + PhotonNetwork.room.MasterClientId);
//  } // 两种情况下都是进行了控制权分配，因为即便是master客户端，生成的东西也是默认属于Scene。如此一类其实现在是完全平等了不是吗？
//  return;
//}

