using System.Collections.Generic;
using UnityEngine;

public class ZokuseiSkillStoneTagsGroup
{
    public Zokusei zokusei;
    
    //技能石盒分类系成员
    public IDictionary<int, ParticleSystem> buttonEffectsSetsForSkillStoneBox = new Dictionary<int, ParticleSystem>(); 

    public void Close_skillstoneboxtageffects()
    {
        foreach(KeyValuePair<int, ParticleSystem> keyValuePair in buttonEffectsSetsForSkillStoneBox)
        {
            keyValuePair.Value.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }       
    }
    
    public void INI_forSkillStoneBox(Zokusei zokusei,Transform effectObjectParent)
    {
        this.zokusei = zokusei;
        buttonEffectsSetsForSkillStoneBox = new Dictionary<int, ParticleSystem>();
        
        GameObject normaltab = CreateOneButtonIcon(zokusei,0);
        GameObject ex1tab = CreateOneButtonIcon(zokusei,1);
        GameObject ex2tab = CreateOneButtonIcon(zokusei,2);
        GameObject ex3tab = CreateOneButtonIcon(zokusei,3);
        
        normaltab.transform.SetParent(effectObjectParent);
        ex1tab.transform.SetParent(effectObjectParent);
        ex2tab.transform.SetParent(effectObjectParent);
        ex3tab.transform.SetParent(effectObjectParent);
        
        buttonEffectsSetsForSkillStoneBox.Add(0,normaltab.GetComponent<ParticleSystem>());
        buttonEffectsSetsForSkillStoneBox.Add(1,ex1tab.GetComponent<ParticleSystem>());
        buttonEffectsSetsForSkillStoneBox.Add(2,ex2tab.GetComponent<ParticleSystem>());
        buttonEffectsSetsForSkillStoneBox.Add(3,ex3tab.GetComponent<ParticleSystem>());
    }

    public static GameObject CreateOneButtonIcon(Zokusei zokusei, int SpLevel)
    {
        string buttoneffectspath;
        switch(zokusei)
        {
            case Zokusei.blueMagic:
            buttoneffectspath = "blueMagic";
            break;
            case Zokusei.darkMagic:
            buttoneffectspath = "darkMagic";
            break;
            case Zokusei.greenMagic:
            buttoneffectspath = "greenMagic";
            break;
            case Zokusei.lightMagic:
            buttoneffectspath = "lightMagic";
            break;
            case Zokusei.redMagic:
            buttoneffectspath = "redMagic";
            break;
            default:
            buttoneffectspath = "defaultMagic";
            break;
        }
        
        switch(SpLevel)
        {
            case 0:
                GameObject normal = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/normal", typeof(GameObject)) as GameObject;
                return Object.Instantiate(normal);
            case 1:
                GameObject EX1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX1", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX1);
            case 2:
                GameObject EX2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX2", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX2);
            case 3:
                GameObject EX3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX3", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX3);
        }
        return null;
    }
    
    public void RefreshSTBoxEffects(int eX, Vector3 pos)
    {
        ParticleSystem p = buttonEffectsSetsForSkillStoneBox[eX];
        p.gameObject.transform.position = pos;
        p.Play(true);
    }
}
