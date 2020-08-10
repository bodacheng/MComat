using UnityEngine;

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
        if (MobileInputsManager.target.fxCamera != null)
        {
            EffectsManager.GenerateEffect(
            "ui_exbarburn", null, 
            ScreenPositionCal.Cal(2, MobileInputsManager.target.fxCamera, transform.GetComponent<RectTransform>(), 3), 
            Quaternion.identity, null);
        }
    }
}
