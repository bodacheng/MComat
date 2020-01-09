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
                buttoneffectspath = "darkMagic";
                break;
        }
        
        GameObject normal = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/normal", typeof(GameObject)) as GameObject;
        GameObject EX1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX1", typeof(GameObject)) as GameObject;
        GameObject EX2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX2", typeof(GameObject)) as GameObject;
        GameObject EX3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX3", typeof(GameObject)) as GameObject;

        buttonEffectsSetsForSkillStoneBox = new Dictionary<int, ParticleSystem>();
        
        GameObject normaltab = Object.Instantiate(normal);
        GameObject ex1tab = Object.Instantiate(EX1);
        GameObject ex2tab = Object.Instantiate(EX2);
        GameObject ex3tab = Object.Instantiate(EX3);
        
        normaltab.transform.SetParent(effectObjectParent);
        ex1tab.transform.SetParent(effectObjectParent);
        ex2tab.transform.SetParent(effectObjectParent);
        ex3tab.transform.SetParent(effectObjectParent);
        
        if (normal)
            buttonEffectsSetsForSkillStoneBox.Add(0,normaltab.GetComponent<ParticleSystem>());
        if (EX1)
            buttonEffectsSetsForSkillStoneBox.Add(1,ex1tab.GetComponent<ParticleSystem>());
        if (EX2)
            buttonEffectsSetsForSkillStoneBox.Add(2,ex2tab.GetComponent<ParticleSystem>());
        if (EX3)
            buttonEffectsSetsForSkillStoneBox.Add(3,ex3tab.GetComponent<ParticleSystem>());
    }
    
    public void refreshforbuttonForSkillStoneBox(int eX,Vector3 pos)
    {
        ParticleSystem p = buttonEffectsSetsForSkillStoneBox[eX];
        p.gameObject.transform.position = pos;
        p.Play(true);
    }
}
