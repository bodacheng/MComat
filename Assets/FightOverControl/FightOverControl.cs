using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using UnityEngine.SceneManagement;
using Soul;
using Log;

namespace FightScene
{
    public class FightOverControl : MonoBehaviour
    {
        public static FightOverControl target;
        
        void Awake()
        {
            target = this;
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