using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Every item's cell must contain this script
/// </summary>

[RequireComponent(typeof(Image))]
public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public static void SelectedRender(StoneCell cell, GameObject _Selected)
    {
        if (cell == null)
        {
            _Selected.SetActive(false);
            return;
        }
        _Selected.SetActive(true);
        _Selected.transform.SetParent(cell.GetComponent<RectTransform>());
        _Selected.transform.localPosition = Vector3.zero;
        _Selected.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        _Selected.GetComponent<RectTransform>().localScale = Vector3.one;
        _Selected.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        _Selected.gameObject.SetActive(true);
        _Selected.transform.SetAsFirstSibling();
    }
}
