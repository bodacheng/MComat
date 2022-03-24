using System.Collections.Generic;
using UnityEngine;

public class ZokuseiStoneTagsGroup
{
    //技能石盒分类系成员
    IDictionary<int, ParticleSystem> btnEffectsSetsForStoneBox = new Dictionary<int, ParticleSystem>();
    public readonly IDictionary<int, ParticleSystem> btnPressedEffects = new Dictionary<int, ParticleSystem>();
    readonly IDictionary<int, ParticleSystem> exTagEffects = new Dictionary<int, ParticleSystem>();
    readonly IDictionary<int, ParticleSystem> slotEffects = new Dictionary<int, ParticleSystem>();
    
    public void CloseTagEffects()
    {
        foreach(var keyValuePair in btnEffectsSetsForStoneBox)
        {
            keyValuePair.Value.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
    
    public void INI_forSkillStoneBox(Element element,Transform effectObjectParent)
    {
        btnEffectsSetsForStoneBox = new Dictionary<int, ParticleSystem>();
        
        var normaltab = CreateOneButtonIcon(element, 0);
        var ex1tab = CreateOneButtonIcon(element, 1);
        var ex2tab = CreateOneButtonIcon(element, 2);
        var ex3tab = CreateOneButtonIcon(element, 3);
        
        normaltab.transform.SetParent(effectObjectParent);
        ex1tab.transform.SetParent(effectObjectParent);
        ex2tab.transform.SetParent(effectObjectParent);
        ex3tab.transform.SetParent(effectObjectParent);
        
        btnEffectsSetsForStoneBox.Add(0, normaltab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(1, ex1tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(2, ex2tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(3, ex3tab.GetComponent<ParticleSystem>());

        LoadPressedEffect(element, effectObjectParent);
    }
    
    public static GameObject CreateOneButtonIcon(Element element, int SpLevel)
    {
        string buttoneffectspath = FightGlobalSetting.EffectPathDefine(element);
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
    
    public void LoadPressedEffect(Element element, Transform T)
    {
        string buttoneffectspath = FightGlobalSetting.EffectPathDefine(element);
        
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
        var p = btnEffectsSetsForStoneBox[eX];
        p.gameObject.name = "UIExTag"+ eX;
        exTagEffects.Add(eX,p);
        p.gameObject.transform.position = pos;
        p.Play(true);
    }
    
    public void RefreshSlotEffects(int slotNum, int eX, Vector3 pos)
    {
        if (slotEffects.ContainsKey(slotNum) && slotEffects[slotNum] != null)
        {
            GameObject.Destroy(slotEffects[slotNum].gameObject);
        }
        
        if (!btnEffectsSetsForStoneBox.ContainsKey(eX)) return;
        var prefab = btnEffectsSetsForStoneBox[eX];
        var slotEffect = GameObject.Instantiate(prefab);
        DicAdd<int, ParticleSystem>.Add(slotEffects, slotNum, slotEffect);
        slotEffect.gameObject.name = "slotEffect"+ slotNum;
        slotEffect.gameObject.transform.position = pos;
        slotEffect.Play(true);
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
        
        foreach (var VARIABLE in slotEffects)
        {
            if (VARIABLE.Value != null)
                GameObject.Destroy(VARIABLE.Value.gameObject);
        }
        slotEffects.Clear();
    }
}
