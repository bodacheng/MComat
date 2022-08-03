using Cysharp.Threading.Tasks;
using UnityEngine;
using FightScene;

public class ExBarBurn : MonoBehaviour
{
    void OnDisable()
    {
        Burn();
    }

    void Burn()
    {
        if (NetFightScene.target.fxCamera != null)
        {
            EffectsManager.GenerateEffect(
            "ui_exbarburn", null, 
            PosCal.GetWorldPos(NetFightScene.target.fxCamera, transform.GetComponent<RectTransform>(), 3), 
            Quaternion.identity, null).Forget();
        }
    }
}
