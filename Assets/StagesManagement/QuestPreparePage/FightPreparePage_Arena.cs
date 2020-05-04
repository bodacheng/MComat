using System.Collections;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public partial class FightPreparePage : MonoBehaviour
    {
        IEnumerator Arena3V3()
        {
            void GoToTeamEdit_Arena()
            {
                TeamSet.SwitchTargetTeam(TeamSetGameMode.arena3V3);
                PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront,true);
            }
            EditTeamButton.gameObject.SetActive(true);
            EditTeamButton.onClick.RemoveAllListeners();
            EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
            PosKeySet set = TeamSet.Arena3V3;
            IEnumerator getArenaTeam = TeamSet.MyTeamByEntryLimit(ToBeLoad.EntryMemberNum, set);
            yield return getArenaTeam;
            if (getArenaTeam.Current == null)
            {
                Debug.Log("获取我方人员错误");
                yield break;
            }
            ToBeLoad.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getArenaTeam.Current;
        }
    }
}