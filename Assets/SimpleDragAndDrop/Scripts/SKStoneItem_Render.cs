using UnityEngine;
using Api.Dto.Model;
using UnityEngine.EventSystems;
using EckTechGames.FloatingCombatText;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void LevelUpShow(float beforeexp, float afterexp)
    {
        LevelCal levelCal = new LevelCal();
        levelCal.INI();
        int beforeLevel = levelCal.GetCurrentLevel((int)beforeexp).currentLevel;
        int aferlevel = levelCal.GetCurrentLevel((int)afterexp).currentLevel;
        if (aferlevel > beforeLevel)
        {
            OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.LevelUp, aferlevel);
        }
    }
}