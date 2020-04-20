using UnityEngine;
using UnityEngine.UI;
using mainMenu;

public class AreanaSetDisplay : MonoBehaviour
{
    public Text RanKInfo;
    public Button PrepareFightButton;
    public RectTransform iconsT;
    
    public void AddFightToList(HeroIcon FighterIcon, StageScriptableObject _SO)
    {
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
