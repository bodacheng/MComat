using System;
using UnityEngine;
using UnityEngine.UI;

public class DropTablePage : MonoBehaviour
{
    public RectTransform parentT;
    [SerializeField] private string itemId;
    [SerializeField] private string currencyCode;
    [SerializeField] private int currencyCount;
    [SerializeField] private Button gotcha;
    [SerializeField] private Button openDropTableInfo;
    public string ItemId => itemId;

    public void Setup(Action<string,string,int> nine, Action<string> dropTableInfo)
    {
        gotcha.onClick.AddListener(() => { nine(itemId, currencyCode, currencyCount);});
        openDropTableInfo.onClick.AddListener(()=> dropTableInfo.Invoke(itemId));
    }
}
