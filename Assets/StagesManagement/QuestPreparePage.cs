using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using dataAccess;
using System.Collections.Generic;

namespace mainMenu
{
    //进入关卡前的读取工作。因为要有一个简单的加载角色信息列表所以需要和数据库，本地config文档等等连接在一块。
    public class QuestPreparePage : MonoBehaviour
    {
        public preparingScene _preparingScene;//准备由这个模块来切换关卡。
        public CharsManager _CharsManager;
        public SingleThreadProcesser mainProcessRunner;

        [Space(7)]
        [Header("UI elements")]
        public Text QuestName;
        public charIcon FighterIcon;//多种属性框？
        public Button EditTeam;
        public Button enterQuest;
        public RectTransform myTeamShowT;
        public RectTransform enemyTeamShowT;

        public StageScriptableObject _Stage;

        public void EditTeamButtonBehaviour()
        {
            _preparingScene.trySwitchToStep(MainSceneStep.TeamEditFront, true);
        }

        //这个函数只考虑了队员的加载。。。
        public IEnumerator getReadyToBattle(StageScriptableObject stage, SceneMode sceneMode)
        {
            _preparingScene._LoadingCanvas.DarkOff(1f);
            foreach (Transform _child in myTeamShowT)
            {
                Destroy(_child.gameObject);
            }
            foreach (Transform _child in enemyTeamShowT)
            {
                Destroy(_child.gameObject);
            }

            _Stage = stage;
            foreach(CharacterDataInfo oneMember in _Stage.localFight.HeroSets.values)
            {
                charIcon MyMemberIcon = Instantiate(FighterIcon);
                CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(oneMember.monsterId);
                MyMemberIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(_CharacterResourceInfo.RECORD_ID), _CharacterResourceInfo._zokusei);
                MyMemberIcon.transform.SetParent(myTeamShowT);
                MyMemberIcon.transform.localPosition = Vector3.one;
                MyMemberIcon.transform.localScale = Vector3.one;
                MyMemberIcon.gameObject.SetActive(true);
            }

            foreach(CharacterDataInfo oneMember in _Stage.localFight.EnemySets.values)
            {
                charIcon MyMemberIcon = Instantiate(FighterIcon);
                CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(oneMember.monsterId);
                MyMemberIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(_CharacterResourceInfo.RECORD_ID), _CharacterResourceInfo._zokusei);
                MyMemberIcon.transform.SetParent(enemyTeamShowT);
                MyMemberIcon.transform.localPosition = Vector3.one;
                MyMemberIcon.transform.localScale = Vector3.one;
                MyMemberIcon.gameObject.SetActive(true);
            }

            enterQuest.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction Go = () =>
            {
                _preparingScene.askIfLoadFight(sceneMode, _Stage);
            };
            enterQuest.onClick.AddListener(Go);

            _preparingScene.trySwitchToStep(MainSceneStep.QuestInfo, true);
            _preparingScene._LoadingCanvas.LightUp();
            yield break;
        }

        // 这个函数目前是固定使用“默认队伍配置”
        public IEnumerator loadStageByScriptThenGetReadyForIt(StageScriptableObject _StageScriptableObject)
        {
            QuestName.text = _StageScriptableObject.battleNameJPG;
            _StageScriptableObject.loadLocalFightFromScript();
            IEnumerator getPlayerOne = TeamSet.Instance.myTeamMembersByEntryMemberNum(_StageScriptableObject.EntryMemberNum, TeamSet.Instance.storyModeTeamSet);
            yield return getPlayerOne;
            _StageScriptableObject.localFight.HeroSets = (MultiDictionary<int, int, CharacterDataInfo>)getPlayerOne.Current;
            if (_StageScriptableObject.localFight.HeroSets == null)
            {
                Debug.Log("严重错误。get不到队员"); yield break;
            }
            mainProcessRunner.triggerMainProcess(getReadyToBattle(_StageScriptableObject, SceneMode.QuestFight));
            yield break;
        }


    }
}