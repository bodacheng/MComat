using mainMenu;
using UnityEngine;
using System.Collections.Generic;

public class BigButtonRender : MonoBehaviour
{
    public static BigButtonRender target;

    private void Awake()
    {
        target = this;
    }

    readonly List<Decompositioner> Stars = new List<Decompositioner>();
    public void TestOn(RectTransform T)
    {
        Decompositioner Star = EffectsManager.GenerateEffect("bigButtonBK", FightGlobalSetting.EffectPathDefine(Zokusei.Null), ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, T,9), Quaternion.identity, null);
        Stars.Add(Star);
    }
    
    public void TestOff()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            Stars[i].EnergyRessolve();
        }
        Stars.Clear();
    }
}
