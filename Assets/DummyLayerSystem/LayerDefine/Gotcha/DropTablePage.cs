using System;
using UnityEngine;

public class DropTablePage : MonoBehaviour
{
    public RectTransform parentT;
    [SerializeField] private string itemId;
    [SerializeField] private string currencyCode;
    [SerializeField] private int currencyCount;
    [SerializeField] private BOButton gotcha;
    [SerializeField] private BOButton openDropTableInfo;
    public string ItemId => itemId;

    public void Setup(Action<string,string,int> nine, Action<string> dropTableInfo, bool tutorial)
    {
        gotcha.onClick.AddListener(() => { nine(itemId, currencyCode, currencyCount);});
        openDropTableInfo.gameObject.SetActive(!tutorial);
        openDropTableInfo.onClick.AddListener(()=> dropTableInfo.Invoke(itemId));
    }
}
