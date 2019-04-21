using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

//进入关卡前的读取工作。因为要有一个简单的加载角色信息列表所以需要和数据库，本地config文档等等连接在一块。
public class QuestPreparePage : MonoBehaviour {

    public preparingScene _preparingScene;//准备由这个模块来切换关卡。
    public CharsManager _CharsManager;

    [Space(7)]
    [Header("UI elements")]
    public Text QuestName;
    public Text QuestEntryNum;
    public charIcon FighterIcon;//多种属性框？
    public Button EditTeam;
    public Button enterQuest;
    public RectTransform myTeamShowT;
    public RectTransform enemyTeamShowT;

    private Stage _Stage;

    public void clearFightInfo()
    {
        _Stage._LocalFight.Enemies = new CharacterDataInfo[0];
        _Stage._LocalFight.team1members = new CharacterDataInfo[0];
    }

    public void EditTeamButtonBehaviour()
    {
        _preparingScene.setBattleEntryNum( _Stage._LocalFight.EntryMemberNum);
        _preparingScene.trySwitchToStep(MainSceneStep.TeamEditFront, true);
    }

    //这个函数只考虑了队员的加载。。。
    public IEnumerator getReadyToBattle(Stage stage,SceneMode sceneMode)
    {
        foreach (Transform _child in myTeamShowT)
        {
            Destroy(_child.gameObject);
        }
        foreach (Transform _child in enemyTeamShowT)
        {
            Destroy(_child.gameObject);
        }
        myModelPool.Instance.ModelDicBasedOnEnemiesLocalID.Clear();//不保存敌人模型。这个字典每局战斗都刷新，加入新模型
        _Stage = stage;
        int playerEntryNum = _Stage._LocalFight.EntryMemberNum;
        QuestEntryNum.text = playerEntryNum.ToString();
        QuestName.text = _Stage.battleNameENG;

        bool MemberOnsetProblem = false;

        if (_Stage._LocalFight == null)
        {
            yield return null;
            //refresh4V4ModeSaveData(50);//暂定 这个要根据玩家等级进行变化
        }

        foreach (CharacterDataInfo oneMember in _Stage._LocalFight.team1members)
        {
            charIcon MyMemberIcon = GameObject.Instantiate(FighterIcon);
            CharacterResourceInfo _CharacterResourceInfo = CharsManager.getCharacterResourceInfo(oneMember.resource_num);
            
            MyMemberIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(_CharacterResourceInfo.charResouceNum),_CharacterResourceInfo._zokusei);
            
            MyMemberIcon.transform.SetParent(myTeamShowT);
            MyMemberIcon.transform.localPosition = Vector3.one;
            MyMemberIcon.transform.localScale = Vector3.one;
            MyMemberIcon.gameObject.SetActive(true);
        }

        foreach (CharacterDataInfo OneEnemy in _Stage._LocalFight.Enemies)
        {
            charIcon MyEnemyIcon = GameObject.Instantiate(FighterIcon);
            CharacterResourceInfo _CharacterResourceInfo = CharsManager.getCharacterResourceInfo(OneEnemy.resource_num);

            MyEnemyIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(_CharacterResourceInfo.charResouceNum),_CharacterResourceInfo._zokusei);

            MyEnemyIcon.transform.SetParent(enemyTeamShowT);
            MyEnemyIcon.transform.localPosition = new Vector3(1, 1, 1);
            MyEnemyIcon.transform.localScale = new Vector3(1, 1, 1);
            MyEnemyIcon.gameObject.SetActive(true);
        }

        if (MemberOnsetProblem)
        {
            Debug.Log("4V4阵容设置存在问题（成员）");
            yield return null;
        }

        GoingToLoadFight.Instance.nextBattle = new Stage();
        GoingToLoadFight.Instance.nextBattle = _Stage;
        _preparingScene.trySwitchToStep(MainSceneStep.QuestInfo, true);

        enterQuest.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction Go = () =>
        {
            _preparingScene.askIfLoadFight(sceneMode,GoingToLoadFight.Instance.nextBattle._LocalFight.BattleGroundID);
        };
        enterQuest.onClick.AddListener(Go);
        yield return null;
    }
}
