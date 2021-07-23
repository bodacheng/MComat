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
        
        [Header("IconWithSkillShow")]
        public IconAndSKillShowUISet IconAndSKillShowUISetPretab;
        
        [Space(11)]
        [Header("战斗的最后一击时候的处理")]
        public Button NextLevelButton;
        
        [Header("NineForShow")]
        public NineForShow NineForShowPretab;
        
        void Awake()
        {
            target = this;
        }
        
        public void Clear()
        {
            FightOverCanvas.gameObject.SetActive(false);
            foreach(NineForShow nineForShow in NineForShows)
            {
                nineForShow.ClearCurrent();
            }
        }
        
        // 战斗结束后统计技能石升级情况时的画面显示
        List<NineForShow> NineForShows = new List<NineForShow>();
        public void ShowSKillSets(TeamUIManager teamUIManager, RectTransform IconAndSKillShowUISetT)
        {
            NineForShows.Clear();
            foreach (Transform child in IconAndSKillShowUISetT) 
            {
                Destroy(child.gameObject);
            }
            
            foreach (KeyValuePair<Data_Center, CharDataInfo> keyValuePair in RTFightManager.target.CharDataInfoRef)
            {
                IconAndSKillShowUISet iconAndSKillShowUISet = Instantiate(IconAndSKillShowUISetPretab);
                SideCharIcon sideCharIcon = teamUIManager.GetSideIcon(keyValuePair.Key);
                NineForShow nineForShow = Instantiate(NineForShowPretab);
                NineForShows.Add(nineForShow);
                iconAndSKillShowUISet.Set(sideCharIcon, nineForShow);
                iconAndSKillShowUISet.transform.SetParent(IconAndSKillShowUISetT);
                iconAndSKillShowUISet.transform.localPosition = Vector3.zero;
                iconAndSKillShowUISet.transform.localScale = Vector3.one;
                nineForShow.ShowStones_Acc(keyValuePair.Value.id);
            }
        }
        
        // 胜利字幕与对应页面加载
        public IEnumerator WINProcess()
        {
            win_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            win_textanimation.gameObject.SetActive(false);
        }
        
        // 失败字幕与对应页面加载
        public IEnumerator LoseProcess()
        {
            lose_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            lose_textanimation.gameObject.SetActive(false);
        }
        
        // 这个函数应该包括一些更深层的考虑。
        public void ReturnToFront()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            List<Data_Center> data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RTFightManager.target.team1.TeamMembers.GetValues());
            data_Centers.AddRange(RTFightManager.target.team2.TeamMembers.GetValues());
            SkillLog(data_Centers);
            foreach (Data_Center one in data_Centers)
            {
                one.CleanClear();
            }
            RTFightManager.target.Clear();
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