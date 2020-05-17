using System.Collections;
using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public partial class FightPreparePage : MonoBehaviour
    {
        IEnumerator Arcade()
        {
            ToBeLoad.LoadLocalFightFromScript(ToBeLoad.Script);
            void GoToTeamEdit_Arcade()
            {
                TeamSet.SwitchTargetTeam(TeamSetGameMode.story);
                PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront,true);
            }
            EditTeamButton.gameObject.SetActive(true);
            EditTeamButton.onClick.RemoveAllListeners();
            EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
            PosKeySet set = TeamSet.Default;
            IEnumerator getDefaultTeamSet = TeamSet.MyTeamByEntryLimit(ToBeLoad.EntryMemberNum, set);
            yield return getDefaultTeamSet;
            if (getDefaultTeamSet.Current == null)
            {
                Debug.Log("获取我方人员错误");
                yield break;
            }
            ToBeLoad.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getDefaultTeamSet.Current;
        }
    }
}