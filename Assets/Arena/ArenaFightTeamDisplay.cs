using UnityEngine;
using UnityEngine.UI;
using mainMenu;

public class ArenaFightTeamDisplay : MonoBehaviour
{
    public Text RanKInfo;
    public Button PrepareFightButton;
    public RectTransform iconsT;
        
    // 供选择挑战对象
    public void AddFightToList(HeroIcon FighterIcon, StageScriptableObject _SO)
    {
        foreach (Transform _child in iconsT)
        {
            Destroy(_child.gameObject);
        }
        foreach(CharDataInfo oneMember in _SO.localFight.EnemySets.values)
        {
            HeroIcon.ArrangeHeroIconToT(FighterIcon,oneMember,iconsT);
        }
        void PrepareForIt()
        {
            QuestPreparePage.target.mainProcessRunner.Run(QuestPreparePage.target.LoadStageByScriptThenGetReadyForIt(_SO));
        }
        PrepareFightButton.onClick.AddListener(PrepareForIt);
    }
}
