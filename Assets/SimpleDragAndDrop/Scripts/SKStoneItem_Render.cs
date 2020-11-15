using UnityEngine;
using UnityEngine.EventSystems;
using EckTechGames.FloatingCombatText;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void LevelUpShow(float beforeexp, float afterexp)
    {
        int beforeLevel = LevelExpConfig.GetCurrentInfo((int)beforeexp).currentLevel;
        int aferlevel = LevelExpConfig.GetCurrentInfo((int)afterexp).currentLevel;
        if (aferlevel > beforeLevel)
        {
            OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.LevelUp, aferlevel);
        }
    }

    public static void SeletedRender(SKStoneItem item, GameObject _Selected)
    {
        if (item != null)
        {
            StoneCell cell = item.GetCell();
            StoneCell.SeletedRender(cell, _Selected);
        }
        else
        {
            StoneCell.SeletedRender(null, _Selected);
        }
    }
}