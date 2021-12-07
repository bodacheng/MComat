using UnityEngine;
using FightScene;

public class ExBarBurn : MonoBehaviour
{
    void Awake()
    {
        EffectsManager.INIEffectsPool("ui_exbarburn", FightGlobalSetting.EffectPathDefine(Zokusei.Null), 3);
    }

    void OnDisable()
    {
        Burn();
    }

    public void Burn()
    {
        if (NetFightScene.target.fxCamera != null)
        {
            EffectsManager.GenerateEffect(
            "ui_exbarburn", null, 
            PosCal.GetWorldPos(NetFightScene.target.fxCamera, transform.GetComponent<RectTransform>(), 3), 
            Quaternion.identity, null);
        }
    }
}
