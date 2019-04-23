using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.UI;

public class zokuseiSkillStoneTagsGroup
{
    public zokusei zokusei;
    
    //技能石盒分类系成员
    public IDictionary<EX, ParticleSystem> buttonEffectsSetsForSkillStoneBox = new Dictionary<EX, ParticleSystem>(); 

    public void close_skillstoneboxtageffects()
    {
        foreach(KeyValuePair<EX, ParticleSystem> keyValuePair in buttonEffectsSetsForSkillStoneBox)
        {
            keyValuePair.Value.Stop(true);
        }       
    }
 
    public void INI_forSkillStoneBox(zokusei zokusei)
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
        
        GameObject buttonslot = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/slot", typeof(GameObject)) as GameObject;
        GameObject normal = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/normal", typeof(GameObject)) as GameObject;
        GameObject EX1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX1", typeof(GameObject)) as GameObject;
        GameObject EX2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX2", typeof(GameObject)) as GameObject;
        GameObject EX3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX3", typeof(GameObject)) as GameObject;
        GameObject Defend = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/defend", typeof(GameObject)) as GameObject;
        GameObject Rush = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/rush", typeof(GameObject)) as GameObject;
        GameObject refresh = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/refresh", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPretab0 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion0", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPretab1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion1", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPretab2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion2", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPretab3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion3", typeof(GameObject)) as GameObject;
        GameObject pressingExplosionPretab = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/pressing", typeof(GameObject)) as GameObject;
        
        buttonEffectsSetsForSkillStoneBox = new Dictionary<EX, ParticleSystem>();
        if (normal)
            buttonEffectsSetsForSkillStoneBox.Add(EX.normal,Object.Instantiate(normal).GetComponent<ParticleSystem>());
        if (EX1)
            buttonEffectsSetsForSkillStoneBox.Add(EX.EX1,Object.Instantiate(EX1).GetComponent<ParticleSystem>());
        if (EX2)
            buttonEffectsSetsForSkillStoneBox.Add(EX.EX2,Object.Instantiate(EX2).GetComponent<ParticleSystem>());
        if (EX3)
            buttonEffectsSetsForSkillStoneBox.Add(EX.EX3,Object.Instantiate(EX3).GetComponent<ParticleSystem>());
    }
    
    public void refreshforbuttonForSkillStoneBox(EX eX,Vector3 pos)
    {
        ParticleSystem p = buttonEffectsSetsForSkillStoneBox[eX];
        p.gameObject.transform.position = pos;
        p.Play(true);
    }
}
