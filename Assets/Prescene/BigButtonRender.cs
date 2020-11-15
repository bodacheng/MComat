using mainMenu;
using UnityEngine;

public class BigButtonRender : MonoBehaviour
{
    Decompositioner Star;
    public void TestOn(RectTransform T)
    {
        Star = EffectsManager.GenerateEffect("bigButtonBK", FightGlobalSetting.EffectPathDefine(Zokusei.Null), 
                                                ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, 
                                                    T,9), Quaternion.identity, null);
    }
    
    public void TestOff()
    {
        Star.EnergyRessolve();
    }
}
