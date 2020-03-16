using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    //进入关卡前的读取工作。因为要有一个简单的加载角色信息列表所以需要和数据库，本地config文档等等连接在一块。
    public class QuestPreparePage : MonoBehaviour
    {
        public CharsManager _CharsManager;
        public SingleThreadProcesser mainProcessRunner;

        [Space(7)]
        [Header("UI elements")]
        public Text QuestName;
        public charIcon FighterIcon;//多种属性框？
        public Button EditTeam;
        public Button EnterQuest;
        public RectTransform myTeamShowT;
        public RectTransform enemyTeamShowT;

        public StageScriptableObject _Stage;
        
        public void EditTeamButtonBehaviour()
        {
            PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront, true);
        }

        //这个函数只考虑了队员的加载。。。
        public IEnumerator GetReadyToBattle(StageScriptableObject stage, SceneMode sceneMode)
        {
            EnterQuest.gameObject.SetActive(false);
            LoadingCanvas.target.DarkOff(1f);
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
                CharacterResourceInfo _CharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(oneMember.ResourceID);
                MyMemberIcon.ChangeIcon(monsterIconsDic.Instance.GetMonsterIconSyn(_CharacterResourceInfo.RECORD_ID), _CharacterResourceInfo._zokusei);
                MyMemberIcon.transform.SetParent(myTeamShowT);
                MyMemberIcon.transform.localPosition = Vector3.one;
                MyMemberIcon.transform.localScale = Vector3.one;
                MyMemberIcon.gameObject.SetActive(true);
            }

            foreach(CharacterDataInfo oneMember in _Stage.localFight.EnemySets.values)
            {
                charIcon EnemyMemberIcon = Instantiate(FighterIcon);
                CharacterResourceInfo _CharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(oneMember.ResourceID);
                EnemyMemberIcon.ChangeIcon(monsterIconsDic.Instance.GetMonsterIconSyn(_CharacterResourceInfo.RECORD_ID), _CharacterResourceInfo._zokusei);
                EnemyMemberIcon.transform.SetParent(enemyTeamShowT);
                EnemyMemberIcon.transform.localPosition = Vector3.one;
                EnemyMemberIcon.transform.localScale = Vector3.one;
                EnemyMemberIcon.gameObject.SetActive(true);
            }
            
            EnterQuest.onClick.RemoveAllListeners();
            void Go()
            {
                PreScene.Instance.AskIfLoadFight(sceneMode, _Stage);
            }
            EnterQuest.onClick.AddListener(Go);
            EnterQuest.gameObject.SetActive(true);
            PreScene.Instance.trySwitchToStep(MainSceneStep.QuestInfo, true);
            LoadingCanvas.target.LightUp();
            yield break;
        }

        // 这个函数目前是固定使用“默认队伍配置”
        public IEnumerator LoadStageByScriptThenGetReadyForIt(StageScriptableObject _StageScriptableObject)
        {
            QuestName.text = _StageScriptableObject.battleNameJPG;
            _StageScriptableObject.LoadLocalFightFromScript();
            IEnumerator getPlayerOne = TeamSet.Instance.MyTeamMembersByEntryMemberNum(_StageScriptableObject.EntryMemberNum, TeamSet.Instance.storyModeTeamSet);
            yield return getPlayerOne;
            _StageScriptableObject.localFight.HeroSets = (MultiDictionary<int, int, CharacterDataInfo>)getPlayerOne.Current;
            if (_StageScriptableObject.localFight.HeroSets == null)
            {
                Debug.Log("严重错误。get不到队员"); yield break;
            }
            mainProcessRunner.TriggerMainProcess(GetReadyToBattle(_StageScriptableObject, SceneMode.QuestFight));
            yield break;
        }
    }
}