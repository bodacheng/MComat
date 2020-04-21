using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System.Collections;
using dataAccess;

public class ArenaFightTeamDisplay : MonoBehaviour
{
    public Text RanKInfo;
    public HeroIcon member1, member2, member3;
    public Button PrepareFightButton;
    
    // 供选择挑战对象
    public IEnumerator AddFightToList(StageScriptableObject _SO)
    {
        for (int index = 0; index < _SO.localFight.EnemySets.values.Count; index++)
        {
            HeroIcon target = null;
            switch(index)
            {
                case 0:
                    target = member1;
                break;
                case 1:
                    target = member2;
                break;
                case 2:
                    target = member3;
                break;
            }
            HeroIcon.ChangeHeroIconByMonsterID(_SO.localFight.EnemySets.values[index].ResourceID, target);
        }
        PrepareFightButton.onClick.RemoveAllListeners();
        void PrepareForIt()
        {
            QuestPreparePage.target.mainProcessRunner.Run(QuestPreparePage.target.GetReadyForStageASTeam(_SO,TeamSet.Instance.Arena3V3));
        }
        PrepareFightButton.onClick.AddListener(PrepareForIt);
        yield break;
    }
}
