using UnityEngine;

public class IconAndSKillShowUISet : MonoBehaviour
{
    public RectTransform iconPlace;
    public RectTransform nineSKillPlace;
    
    public void Set(SideCharIcon sideCharIcon, NineForShow nineForShow)
    {
        sideCharIcon.transform.SetParent(iconPlace);
        sideCharIcon.transform.localPosition = Vector3.zero;
        sideCharIcon.transform.localScale = Vector3.one;
        sideCharIcon.gameObject.SetActive(true);
        
        nineForShow.transform.SetParent(nineSKillPlace);
        nineForShow.transform.localPosition = Vector3.zero;
        nineForShow.transform.localScale = Vector3.one;
        nineForShow.gameObject.SetActive(true);
    }
}
