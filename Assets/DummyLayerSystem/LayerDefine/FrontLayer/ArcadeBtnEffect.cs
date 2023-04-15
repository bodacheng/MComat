using mainMenu;
using UnityEngine;

public class ArcadeBtnEffect : MonoBehaviour
{
    [SerializeField] private RectTransform runningGuyT;
    [SerializeField] private string runningGuyResourceKey = "FrontLayerArcadeBtn/runShadow.prefab";

    // Start is called before the first frame update
    void Start()
    {
        SlotShine(runningGuyT);
    }
    
    async void SlotShine(RectTransform t)
    {
        var guy = await AddressablesLogic.LoadObject(runningGuyResourceKey);
        guy.transform.SetParent(t);
        guy.transform.position = PosCal.GetWorldPos(
            PreScene.target.postProcessCamera,
            PosCal.CalculateAnchoredPositionInNewAnchor(t.GetComponent<RectTransform>(), Vector2.zero)
            , 20f
        );
        // runningGuy.transform.localScale = 
        //     new Vector3(runningGuy.transform.localScale.x * PosCal.EffectScaleRate(),
        //         runningGuy.transform.localScale.y * PosCal.EffectScaleRate(),
        //         runningGuy.transform.localScale.z);
    }
}
