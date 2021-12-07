using System.Collections.Generic;
using UnityEngine;

public class ZokuseiStoneTagsGroup
{
    //技能石盒分类系成员
    public IDictionary<int, ParticleSystem> btnEffectsSetsForStoneBox = new Dictionary<int, ParticleSystem>();
    public IDictionary<int, ParticleSystem> btnPressedEffects = new Dictionary<int, ParticleSystem>();
    public IDictionary<int, ParticleSystem> exTagEffects = new Dictionary<int, ParticleSystem>();
    
    public void Close_skillstoneboxtageffects()
    {
        foreach(KeyValuePair<int, ParticleSystem> keyValuePair in btnEffectsSetsForStoneBox)
        {
            keyValuePair.Value.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }       
    }
    
    public void INI_forSkillStoneBox(Zokusei zokusei,Transform effectObjectParent)
    {
        btnEffectsSetsForStoneBox = new Dictionary<int, ParticleSystem>();
        
        GameObject normaltab = CreateOneButtonIcon(zokusei, 0);
        GameObject ex1tab = CreateOneButtonIcon(zokusei, 1);
        GameObject ex2tab = CreateOneButtonIcon(zokusei, 2);
        GameObject ex3tab = CreateOneButtonIcon(zokusei, 3);
        
        normaltab.transform.SetParent(effectObjectParent);
        ex1tab.transform.SetParent(effectObjectParent);
        ex2tab.transform.SetParent(effectObjectParent);
        ex3tab.transform.SetParent(effectObjectParent);
        
        btnEffectsSetsForStoneBox.Add(0, normaltab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(1, ex1tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(2, ex2tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(3, ex3tab.GetComponent<ParticleSystem>());

        LoadPressedEffect(zokusei, effectObjectParent);
    }
    
    public static GameObject CreateOneButtonIcon(Zokusei zokusei, int SpLevel)
    {
        string buttoneffectspath = FightGlobalSetting.EffectPathDefine(zokusei);
        switch(SpLevel)
        {
            case 0:
                GameObject normal = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/normal", typeof(GameObject)) as GameObject;
                return Object.Instantiate(normal);
            case 1:
                GameObject EX1 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/EX1", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX1);
            case 2:
                GameObject EX2 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/EX2", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX2);
            case 3:
                GameObject EX3 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/EX3", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX3);
        }
        return null;
    }
    
    public void LoadPressedEffect(Zokusei zokusei, Transform T)
    {
        string buttoneffectspath = FightGlobalSetting.EffectPathDefine(zokusei);
        
        GameObject triggerExplosionPrefab0 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/explosion0", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPrefab1 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/explosion1", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPrefab2 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/explosion2", typeof(GameObject)) as GameObject;
        GameObject triggerExplosionPrefab3 = Resources.Load("essentialUIElements/buttonEffects/" + buttoneffectspath + "/explosion3", typeof(GameObject)) as GameObject;
        
        ParticleSystem triggerExplosion0 = Object.Instantiate(triggerExplosionPrefab0).GetComponent<ParticleSystem>();
        ParticleSystem triggerExplosion1 = Object.Instantiate(triggerExplosionPrefab1).GetComponent<ParticleSystem>();
        ParticleSystem triggerExplosion2 = Object.Instantiate(triggerExplosionPrefab2).GetComponent<ParticleSystem>();
        ParticleSystem triggerExplosion3 = Object.Instantiate(triggerExplosionPrefab3).GetComponent<ParticleSystem>();
        
        triggerExplosion0.transform.SetParent(T);
        triggerExplosion1.transform.SetParent(T);
        triggerExplosion2.transform.SetParent(T);
        triggerExplosion3.transform.SetParent(T);
        
        btnPressedEffects.Add(0,triggerExplosion0);
        btnPressedEffects.Add(1,triggerExplosion1);
        btnPressedEffects.Add(2,triggerExplosion2);
        btnPressedEffects.Add(3,triggerExplosion3);
    }
    
    public void RefreshSTBoxEffects(int eX, Vector3 pos)
    {
        if (exTagEffects.ContainsKey(eX))
            return;
        ParticleSystem p = btnEffectsSetsForStoneBox[eX];
        p.gameObject.name = "UIExTag"+ eX;
        exTagEffects.Add(eX,p);
        p.gameObject.transform.position = pos;
        p.Play(true);
    }
    
    public void Clear()
    {
        foreach (var VARIABLE in exTagEffects)
        {
            if (VARIABLE.Value != null)
                GameObject.Destroy(VARIABLE.Value.gameObject);
        }
        exTagEffects.Clear();
        
        foreach (var VARIABLE in btnPressedEffects)
        {
            if (VARIABLE.Value != null)
                GameObject.Destroy(VARIABLE.Value.gameObject);
        }
        btnPressedEffects.Clear();
        
        foreach (var VARIABLE in btnEffectsSetsForStoneBox)
        {
            if (VARIABLE.Value != null)
                GameObject.Destroy(VARIABLE.Value.gameObject);
        }
        btnEffectsSetsForStoneBox.Clear();
    }
}
