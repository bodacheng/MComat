using System.Collections;
using System.Collections.Generic;
using mainMenu;
using UnityEngine;

public class ArcadeBtnEffect : MonoBehaviour
{
    [SerializeField] private RectTransform runningGuyT;
    [SerializeField] private GameObject runningGuy;

    // Start is called before the first frame update
    void Start()
    {
        SlotShine(runningGuyT);
    }
    
    void SlotShine(RectTransform t)
    {
        runningGuy.transform.SetParent(t);
        runningGuy.transform.position = 
            PosCal.GetWorldPos(PreScene.target.mainC, 
                PosCal.ConvertAnchorPos(t.GetComponent<RectTransform>().anchoredPosition, Vector2.one, Vector2.zero )
                , 20f);
    }
}
