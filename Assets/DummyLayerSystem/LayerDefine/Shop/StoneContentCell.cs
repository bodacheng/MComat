using dataAccess;
using UnityEngine;

public class StoneContentCell : MonoBehaviour
{
    [SerializeField] private RectTransform stoneIconParent;
    [SerializeField] private string skillId;

    void Start()
    {
        Render();
    }

    async void Render()
    {
        var icon = await Stones.GenerateStoneModel(skillId, false);
        if (this == null)
        {
            return;
        }
        icon.transform.SetParent(stoneIconParent);
        icon.gameObject.SetActive(true);
        icon.transform.localPosition = Vector3.zero;
        icon.transform.localScale = Vector3.one;
        icon.transform.GetComponent<RectTransform>().sizeDelta = stoneIconParent.transform.GetComponent<RectTransform>().sizeDelta;
    }
}
