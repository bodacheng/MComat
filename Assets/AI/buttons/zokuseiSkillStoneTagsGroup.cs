using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zokuseiSkillStoneTagsGroup
{
    public zokusei zokusei;
    
    //技能石盒分类系成员
    public IDictionary<int, ParticleSystem> buttonEffectsSetsForSkillStoneBox = new Dictionary<int, ParticleSystem>(); 

    public void close_skillstoneboxtageffects()
    {
        foreach(KeyValuePair<int, ParticleSystem> keyValuePair in buttonEffectsSetsForSkillStoneBox)
        {
            keyValuePair.Value.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }       
    }
 
    public void INI_forSkillStoneBox(zokusei zokusei,Transform effectObjectParent)
    {
        this.zokusei = zokusei;
        string buttoneffectspath;
        switch(zokusei)
        {
                case zokusei.blueMagic:
                buttoneffectspath = "blueMagic";
                break;
                case zokusei.darkMagic:
                buttoneffectspath = "darkMagic";
                break;
                case zokusei.greenMagic:
                buttoneffectspath = "greenMagic";
                break;
                case zokusei.lightMagic:
                buttoneffectspath = "lightMagic";
                break;
                case zokusei.redMagic:
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
