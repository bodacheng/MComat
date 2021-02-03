using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using UnityEngine.SceneManagement;
using Soul;
using Log;

namespace FightScene
{
    public class FightOverControl : MonoBehaviour
    {
        public static FightOverControl target;
        
        [Header("FightOverCanvas")]
        public Canvas FightOverCanvas;
        
        [Header("WIN")]
        public GameObject win_textanimation;
        
        [Header("LOSE")]
        public GameObject lose_textanimation;
        
        [Header("技能与角色头像T")]
        public RectTransform Step1;
        public RectTransform Step2;
        public RectTransform IconAndSKillShowUISetT;
        
        [Header("IconWithSkillShow")]
        public IconAndSKillShowUISet IconAndSKillShowUISetPretab;
        
        [Space(11)]
        [Header("战斗的最后一击时候的处理")]
        public Button ReStart;
        
        [Space(11)]
        [Header("战斗的最后一击时候的处理")]
        public Button NextLevelButton;
                
        [Space(11)]
        [Header("返回主页面")]
        public Button ReturnButton;
        
        [Header("NineForShow")]
        public NineForShow NineForShowPretab;
        
        [Header("Rewards")]
        public RectTransform RewardsTransform;
        public Text goldrewards;
        public Text diamondrewards;
        
        [Header("RankInfo")]
        public RankInfo rankInfo;
        
        void Awake()
        {
            target = this;
            ReStart.onClick.RemoveAllListeners();
            ReStart.onClick.AddListener(LocalGameRestart);
            ReturnButton.onClick.RemoveAllListeners();
            ReturnButton.onClick.AddListener(ReturnToFront);
        }
        
        public void Clear()
        {
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(false);
            FightOverCanvas.gameObject.SetActive(false);
            foreach(NineForShow nineForShow in NineForShows)
            {
                nineForShow.ClearCurrent();
            }
        }
        
        // 战斗结束后统计技能石升级情况时的画面显示
        List<NineForShow> NineForShows = new List<NineForShow>();
        public IEnumerator ShowSKillSets(FightTeam fightTeam)
        {
            NineForShows.Clear();
            foreach (Transform child in IconAndSKillShowUISetT) 
            {
                Destroy(child.gameObject);
            }
            
            foreach (KeyValuePair<Data_Center, CharDataInfo> keyValuePair in fightTeam.CharDataInfoRef)
            {
                IconAndSKillShowUISet iconAndSKillShowUISet = Instantiate(IconAndSKillShowUISetPretab);
                SideCharIcon sideCharIcon = fightTeam.GetSideIcon(keyValuePair.Key);
                NineForShow nineForShow = Instantiate(NineForShowPretab);
                NineForShows.Add(nineForShow);
                iconAndSKillShowUISet.Set(sideCharIcon, nineForShow);
                iconAndSKillShowUISet.transform.SetParent(IconAndSKillShowUISetT);
                iconAndSKillShowUISet.transform.localPosition = Vector3.zero;
                iconAndSKillShowUISet.transform.localScale = Vector3.one;
                nineForShow.ShowStones_Acc(keyValuePair.Value.monsterOfPlayerId);
            }
            yield break;
        }
        
        // 胜利字幕与对应页面加载
        public IEnumerator WINProcess()
        {
            Step1.gameObject.SetActive(true);
            win_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            win_textanimation.gameObject.SetActive(false);
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(true);
        }
        
        // 失败字幕与对应页面加载
        public IEnumerator LoseProcess()
        {
            Step1.gameObject.SetActive(true);
            lose_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            lose_textanimation.gameObject.SetActive(false);
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(true);
        }
        
        public void ShowRewards(int golds, int diamond)
        {
            goldrewards.text = golds.ToString();
            diamondrewards.text = diamond.ToString();
            RewardsTransform.gameObject.SetActive(true);
        }
        
        // ArcadeNext
        public void CheckNextArcadeLevel()
        {
            if (FightSceneNote.nextBattle._fightEventType == FightEventType.Quest)
            {
                if (ArcadeManager.ArcadeStages.ContainsKey(FightSceneNote.nextBattle.LocalFightID + 1))
                {
                    NextLevelButton.onClick.RemoveAllListeners();
                    void LoadNextLevel()
                    {
                        FightSceneNote.nextBattle = ArcadeManager.ArcadeStages[FightSceneNote.nextBattle.LocalFightID + 1].stageConfig;
                        FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
                    }
                    NextLevelButton.onClick.AddListener(LoadNextLevel);
                    NextLevelButton.gameObject.SetActive(true);
                }else{
                    NextLevelButton.gameObject.SetActive(false);
                }
            }else{
                NextLevelButton.gameObject.SetActive(false);
            }
        }
        
        // 这个函数应该包括一些更深层的考虑。
        public void ReturnToFront()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            List<Data_Center> data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values);
            data_Centers.AddRange(RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
            SkillLog(data_Centers);
            foreach (Data_Center one in data_Centers)
            {
                one.CleanClear();
            }
            RealTimeGameProcessManager.target.Clear();
            FightLogger.target.WatchMissionsAbandon();
            FSceneProcessesRunner.Main.Clear();
            MainMenuNote.goingtostep = MainSceneStep.FrontPage;
            HitBoxesProcesser.Instance.processingDecompositioners.Clear();
            SingleAssignmentDisposableCleaner.Clear();
            SceneManager.LoadScene(1);
        }

        //本地系函数 
        public void LocalGameRestart()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        }
        
        public void SkillLog(List<Data_Center> members)
        {
            List<SingleFightLog> singleFightLogs = new List<SingleFightLog>();
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i] != null)
                {
                    singleFightLogs.Add(members[i]._MyBehaviorRunner.SingleFightLog);
                }
            }

            if (FightGlobalSetting.HitBoxLogger)
            {
                HitBoxLogTable.Instance.Load(HitBoxLogger.Instance.LoadCurrentToString());
                HitBoxLogger.Instance.LogSummit();
                for (int i = 0; i < singleFightLogs.Count; i++)
                {
                    singleFightLogs[i].Summary();
                }
                HitBoxLogTable.Instance.SaveByCurrentRows_HitBoxLog(Application.persistentDataPath + "/HitBoxLog.csv", HitBoxLogger.Instance, singleFightLogs);
                for (int i = 0; i < singleFightLogs.Count; i++)
                {
                    singleFightLogs[i].Clear();
                }
                HitBoxLogger.Instance.Clear();
            }
        }
    }
}