using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    
    public async UniTask INI_forSkillStoneBox(Element element,Transform effectObjectParent)
    {
        btnEffectsSetsForStoneBox = new Dictionary<int, ParticleSystem>();
        
        var normalTab = await CreateOneButtonIcon(element, 0);
        var ex1Tab = await CreateOneButtonIcon(element, 1);
        var ex2Tab = await CreateOneButtonIcon(element, 2);
        var ex3Tab = await CreateOneButtonIcon(element, 3);
        
        normalTab.transform.SetParent(effectObjectParent);
        ex1Tab.transform.SetParent(effectObjectParent);
        ex2Tab.transform.SetParent(effectObjectParent);
        ex3Tab.transform.SetParent(effectObjectParent);
        
        btnEffectsSetsForStoneBox.Add(0, normalTab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(1, ex1Tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(2, ex2Tab.GetComponent<ParticleSystem>());
        btnEffectsSetsForStoneBox.Add(3, ex3Tab.GetComponent<ParticleSystem>());

        await LoadPressedEffect(element, effectObjectParent);
    }
    
    public static UniTask<GameObject> CreateOneButtonIcon(Element element, int SpLevel)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        switch(SpLevel)
        {
            case 0:
                return AddressablesLogic.LoadObject("ButtonEffects/" + path + "/normal.prefab");
            case 1:
                return AddressablesLogic.LoadObject("ButtonEffects/" + path + "/EX1.prefab");
            case 2:
                return AddressablesLogic.LoadObject("ButtonEffects/" + path + "/EX2.prefab");
            case 3:
                return AddressablesLogic.LoadObject("ButtonEffects/" + path + "/EX3.prefab");
            default:
                return default;
        }
    }
    
    async UniTask LoadPressedEffect(Element element, Transform T)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        
        var triggerExplosion0 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion0.prefab");
        var triggerExplosion1 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion1.prefab");
        var triggerExplosion2 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion2.prefab");
        var triggerExplosion3 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion3.prefab");
        
        triggerExplosion0.transform.SetParent(T);
        triggerExplosion1.transform.SetParent(T);
        triggerExplosion2.transform.SetParent(T);
        triggerExplosion3.transform.SetParent(T);
        
        btnPressedEffects.Add(0, triggerExplosion0);
        btnPressedEffects.Add(1, triggerExplosion1);
        btnPressedEffects.Add(2, triggerExplosion2);
        btnPressedEffects.Add(3, triggerExplosion3);
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
    
    public async void RefreshSlotEffects(int slotNum, int eX, Vector3 pos, Transform parent)
    {
        if (slotEffects.ContainsKey(slotNum) && slotEffects[slotNum] != null)
        {
            GameObject.Destroy(slotEffects[slotNum].gameObject);
        }
        
        if (!btnEffectsSetsForStoneBox.ContainsKey(eX)) return;
        string effectName;
        switch (eX)
        {
            case 1:
                effectName = "SlotEffects/ex1";
                break;
            case 2:
                effectName = "SlotEffects/ex2";
                break;
            case 3:
                effectName = "SlotEffects/ex3";
                break;
            default:
                effectName = "SlotEffects/normal";
                break;
        }
        var slotEffect = await AddressablesLogic.LoadTOnObject<ParticleSystem>(effectName);
        slotEffect.transform.SetParent(parent);
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
