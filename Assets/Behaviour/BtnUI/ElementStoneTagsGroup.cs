using System.Collections.Generic;
using UnityEngine;

//技能石盒分类系成员
public class ElementStoneTagsGroup
{
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
        
        var normalTab = CreateOneButtonIcon(element, 0);
        var ex1Tab = CreateOneButtonIcon(element, 1);
        var ex2Tab = CreateOneButtonIcon(element, 2);
        var ex3Tab = CreateOneButtonIcon(element, 3);
        
        normalTab.transform.SetParent(effectObjectParent);
        ex1Tab.transform.SetParent(effectObjectParent);
        ex2Tab.transform.SetParent(effectObjectParent);
        ex3Tab.transform.SetParent(effectObjectParent);
        
        btnEffectsSetsForStoneBox.Add(0, normalTab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(1, ex1Tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(2, ex2Tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(3, ex3Tab.GetComponent<ParticleSystem>());

        LoadPressedEffect(element, effectObjectParent);
    }
    
    public static GameObject CreateOneButtonIcon(Element element, int SpLevel)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        switch(SpLevel)
        {
            case 0:
                var normal = Resources.Load("essentialUIElements/buttonEffects/" + path + "/normal", typeof(GameObject)) as GameObject;
                return Object.Instantiate(normal);
            case 1:
                var EX1 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/EX1", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX1);
            case 2:
                var EX2 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/EX2", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX2);
            case 3:
                var EX3 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/EX3", typeof(GameObject)) as GameObject;
                return Object.Instantiate(EX3);
        }
        return null;
    }
    
    void LoadPressedEffect(Element element, Transform T)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        
        var triggerExplosionPrefab0 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion0", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab1 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion1", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab2 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion2", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab3 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion3", typeof(GameObject)) as GameObject;
        
        var triggerExplosion0 = Object.Instantiate(triggerExplosionPrefab0).GetComponent<ParticleSystem>();
        var triggerExplosion1 = Object.Instantiate(triggerExplosionPrefab1).GetComponent<ParticleSystem>();
        var triggerExplosion2 = Object.Instantiate(triggerExplosionPrefab2).GetComponent<ParticleSystem>();
        var triggerExplosion3 = Object.Instantiate(triggerExplosionPrefab3).GetComponent<ParticleSystem>();
        
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
