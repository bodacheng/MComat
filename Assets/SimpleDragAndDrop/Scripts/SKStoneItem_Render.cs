using UnityEngine;
using Api.Dto.Model;
using DG.Tweening;
using UnityEngine.EventSystems;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void LevelUpShow(float beforeexp, float afterexp)
    {
        int beforeLevel = SkillStoneOfPlayerInfoModel.ExpToLevel(beforeexp);
        int aferlevel = SkillStoneOfPlayerInfoModel.ExpToLevel(afterexp);
        
        if (aferlevel > beforeLevel)
        {
            transform.GetComponent<RectTransform>().DOScale(Vector3.one * 10, 1f).OnComplete
            (
                () =>
                {
                    transform.GetComponent<RectTransform>().DOScale(Vector3.one, 1f);
                }
            );
        }
    }
}
