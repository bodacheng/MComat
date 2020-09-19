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
    
    public static void SeletedRender(SKStoneItem item ,GameObject _Selected)
    {
        _Selected.SetActive(true);
        _Selected.transform.SetParent(item.GetComponent<RectTransform>());
        _Selected.transform.localPosition = Vector3.zero;
        _Selected.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        _Selected.GetComponent<RectTransform>().localScale = Vector3.one;
        _Selected.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        _Selected.gameObject.SetActive(true);
        _Selected.transform.SetAsFirstSibling();
    }
}