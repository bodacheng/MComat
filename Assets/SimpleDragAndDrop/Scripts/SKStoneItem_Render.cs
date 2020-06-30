using UnityEngine;
using Api.Dto.Model;
using UnityEngine.EventSystems;
using EckTechGames.FloatingCombatText;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void LevelUpShow(float beforeexp, float afterexp)
    {
        int beforeLevel = SkillStoneOfPlayerInfoModel.ExpToLevel(beforeexp);
        int aferlevel = SkillStoneOfPlayerInfoModel.ExpToLevel(afterexp);
        if (aferlevel > beforeLevel)
        {
            OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.LevelUp, aferlevel);
        }
    }
}