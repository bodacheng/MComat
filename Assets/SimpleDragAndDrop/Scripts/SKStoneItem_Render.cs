using UnityEngine;
using UnityEngine.EventSystems;
using EckTechGames.FloatingCombatText;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void LevelUpShow(float beforeexp, float afterexp)
    {
        int beforeLevel = LevelCal.Instance.GetCurrentInfo((int)beforeexp).currentLevel;
        int aferlevel = LevelCal.Instance.GetCurrentInfo((int)afterexp).currentLevel;
        if (aferlevel > beforeLevel)
        {
            OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.LevelUp, aferlevel);
        }
    }
}