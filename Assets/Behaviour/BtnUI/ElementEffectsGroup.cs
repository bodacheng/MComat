using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Object = UnityEngine.Object;

public class ElementEffectsGroup
{
    //攻击键系成员
    IDictionary<Button, IDictionary<string, GameObject>> btnEffectsSets = new Dictionary<Button, IDictionary<string, GameObject>>();
    readonly IDictionary<string, GameObject> _aEffects = new Dictionary<string, GameObject>();
    readonly IDictionary<string, GameObject> _bEffects = new Dictionary<string, GameObject>();
    readonly IDictionary<string, GameObject> _cEffects = new Dictionary<string, GameObject>();
    public IDictionary<Button, ParticleSystem> BtnRefreshEffects = new Dictionary<Button, ParticleSystem>();
    public ParticleSystem triggerExplosion0;
    public ParticleSystem triggerExplosion1;
    public ParticleSystem triggerExplosion2;
    public ParticleSystem triggerExplosion3;
    public ParticleSystem pressingExplosion;//这个不需要对象池。

    IDictionary<Button, ParticleSystem> buttonSlotEffects;

    ParticleSystem _defendBtn;
    ParticleSystem _rushBtn;
    ParticleSystem _aRefresh;
    ParticleSystem _fire1Refresh;
    ParticleSystem _fire2Refresh;
    
    public void Close()
    {
        foreach(var keyValuePair in btnEffectsSets)
        {
            foreach(var exPPair in keyValuePair.Value)
            {
                if (exPPair.Value != null)
                {
                    exPPair.Value.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("错误"+keyValuePair.Value );
                }
            }
        }
        
        triggerExplosion0.Stop(true);
        triggerExplosion1.Stop(true);
        triggerExplosion2.Stop(true);
        triggerExplosion3.Stop(true);
        
        foreach (var keyValue in BtnRefreshEffects)
        {
            keyValue.Value.Stop(true);
        }
        pressingExplosion.Stop(true);
        _rushBtn.Stop(true);
        
        if (FightGlobalSetting._hasDefend)
            _defendBtn.Stop(true);
    }
    
    public void Open(Vector3 defendBtnPos, Vector3 rushBtnPos)
    {
        foreach(var keyValuePair in btnEffectsSets)
        {
            foreach(var exPPair in keyValuePair.Value)
            {
                exPPair.Value.gameObject.SetActive(false);
            }
        }
        triggerExplosion0.Stop(true);
        triggerExplosion1.Stop(true);
        triggerExplosion2.Stop(true);
        triggerExplosion3.Stop(true);
        
        foreach (var keyValue in BtnRefreshEffects)
        {
            keyValue.Value.Stop(true);
        }
        pressingExplosion.Stop(true);
        _rushBtn.gameObject.transform.position = rushBtnPos;
        _rushBtn.Play(true);
        
        if (FightGlobalSetting._hasDefend)
        {
            _defendBtn.gameObject.transform.position = defendBtnPos;
            _defendBtn.Play(true);
        }
    }

    public void INICommon(Transform targetRectT, Element element, Button Attack, Button Fire1, Button Fire2)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        var attackSlot = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab");
        var fire1Slot = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab");
        var fire2Slot = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab");
        
        attackSlot.transform.SetParent(targetRectT);
        fire1Slot.transform.SetParent(targetRectT);
        fire2Slot.transform.SetParent(targetRectT);
        
        buttonSlotEffects = new Dictionary<Button, ParticleSystem>
        {
            { Attack, attackSlot },
            { Fire1, fire1Slot },
            { Fire2, fire2Slot }
        };

        if (FightGlobalSetting._hasDefend)
        {
            _defendBtn = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/defend.prefab");
        }
        
        _rushBtn = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/rush.prefab");
        _aRefresh = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab");
        _fire1Refresh = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab");
        _fire2Refresh = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab");
        triggerExplosion0 = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion0.prefab");
        triggerExplosion1 = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion1.prefab");
        triggerExplosion2 = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion2.prefab");
        triggerExplosion3 = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion3.prefab");
        pressingExplosion = AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/pressing.prefab");
        
        if (FightGlobalSetting._hasDefend)
            _defendBtn.transform.SetParent(targetRectT);
        _rushBtn.transform.SetParent(targetRectT);
        _aRefresh.transform.SetParent(targetRectT);
        _fire1Refresh.transform.SetParent(targetRectT);
        _fire2Refresh.transform.SetParent(targetRectT);
        triggerExplosion0.transform.SetParent(targetRectT);
        triggerExplosion1.transform.SetParent(targetRectT);
        triggerExplosion2.transform.SetParent(targetRectT);
        triggerExplosion3.transform.SetParent(targetRectT);
        pressingExplosion.transform.SetParent(targetRectT);

        BtnRefreshEffects = new Dictionary<Button, ParticleSystem>
        {
            { Attack, _aRefresh },
            { Fire1, _fire1Refresh },
            { Fire2, _fire2Refresh }
        };
    }
    
    public void INIBtn(Button Attack, Button Fire1, Button Fire2, UnitInfo unitInfo)
    {
        void process(Button btn, string skillID, IDictionary<string, GameObject> dic)
        {
            var icon = Stones.GenerateStoneModel(skillID, false);
            if (icon == null) return;
            DicAdd<string, GameObject>.Add(dic, skillID, icon.gameObject);
            Parent(icon.transform, btn.transform);
        }
        
        process(Attack, unitInfo.set.a1, _aEffects);
        process(Attack, unitInfo.set.a2, _aEffects);
        process(Attack, unitInfo.set.a3, _aEffects);
        
        process(Fire1, unitInfo.set.b1, _bEffects);
        process(Fire1, unitInfo.set.b2, _bEffects);
        process(Fire1, unitInfo.set.b3, _bEffects);
        
        process(Fire2, unitInfo.set.c1, _cEffects);
        process(Fire2, unitInfo.set.c2, _cEffects);
        process(Fire2, unitInfo.set.c3, _cEffects);
        
        void Parent(Transform t, Transform target)
        {
            t.SetParent(target);
            t.localPosition = Vector3.zero;
            t.localScale = Vector3.one;
        }
        
        btnEffectsSets = new Dictionary<Button, IDictionary<string, GameObject>>
        {
            { Attack, _aEffects },
            { Fire1, _bEffects },
            { Fire2, _cEffects }
        };
    }
    
    public void RefreshBtn(Button button, string skillId, Vector3 pos)
    {
        var _target = btnEffectsSets[button];
        if (skillId == String.Empty)
        {
            buttonSlotEffects[button].transform.position = pos;
            buttonSlotEffects[button].Play(true);
        }else{
            buttonSlotEffects[button].Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
        }
                
        foreach(var pair in _target)
        {
            pair.Value.gameObject.SetActive(pair.Key == skillId);
        }
    }
}
