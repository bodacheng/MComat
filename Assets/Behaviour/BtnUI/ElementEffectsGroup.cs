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
        var slot = Resources.Load("essentialUIElements/buttonEffects/" + path + "/slot", typeof(GameObject)) as GameObject;
        var Defend = Resources.Load("essentialUIElements/buttonEffects/" + path + "/defend", typeof(GameObject)) as GameObject;
        var Rush = Resources.Load("essentialUIElements/buttonEffects/" + path + "/rush", typeof(GameObject)) as GameObject;
        var refresh = Resources.Load("essentialUIElements/buttonEffects/" + path + "/refresh", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab0 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion0", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab1 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion1", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab2 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion2", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab3 = Resources.Load("essentialUIElements/buttonEffects/" + path + "/explosion3", typeof(GameObject)) as GameObject;
        var pressingExplosionPrefab = Resources.Load("essentialUIElements/buttonEffects/" + path + "/pressing", typeof(GameObject)) as GameObject;
        
        var attackSlot = Object.Instantiate(slot).GetComponent<ParticleSystem>();
        var fire1Slot = Object.Instantiate(slot).GetComponent<ParticleSystem>();
        var fire2Slot = Object.Instantiate(slot).GetComponent<ParticleSystem>();
        
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
            _defendBtn = Object.Instantiate(Defend).GetComponent<ParticleSystem>();
        _rushBtn = Object.Instantiate(Rush).GetComponent<ParticleSystem>();
        _aRefresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        _fire1Refresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        _fire2Refresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        triggerExplosion0 = Object.Instantiate(triggerExplosionPrefab0).GetComponent<ParticleSystem>();
        triggerExplosion1 = Object.Instantiate(triggerExplosionPrefab1).GetComponent<ParticleSystem>();
        triggerExplosion2 = Object.Instantiate(triggerExplosionPrefab2).GetComponent<ParticleSystem>();
        triggerExplosion3 = Object.Instantiate(triggerExplosionPrefab3).GetComponent<ParticleSystem>();
        pressingExplosion = Object.Instantiate(pressingExplosionPrefab).GetComponent<ParticleSystem>();
        
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
        var a1 = Stones.GenerateStoneModel(unitInfo.set.a1, false);
        DicAdd<string, GameObject>.Add(_aEffects, unitInfo.set.a1, a1.gameObject);
        var a2 = Stones.GenerateStoneModel(unitInfo.set.a2, false);
        DicAdd<string, GameObject>.Add(_aEffects, unitInfo.set.a2, a2.gameObject);
        var a3 = Stones.GenerateStoneModel(unitInfo.set.a3, false);
        DicAdd<string, GameObject>.Add(_aEffects, unitInfo.set.a3, a3.gameObject);
        
        var b1 = Stones.GenerateStoneModel(unitInfo.set.b1, false);
        DicAdd<string, GameObject>.Add(_bEffects, unitInfo.set.b1, b1.gameObject);
        var b2 = Stones.GenerateStoneModel(unitInfo.set.b2, false);
        DicAdd<string, GameObject>.Add(_bEffects, unitInfo.set.b2, b2.gameObject);
        var b3 = Stones.GenerateStoneModel(unitInfo.set.b3, false);
        DicAdd<string, GameObject>.Add(_bEffects, unitInfo.set.b3, b3.gameObject);
        
        var c1 = Stones.GenerateStoneModel(unitInfo.set.c1, false);
        DicAdd<string, GameObject>.Add(_cEffects, unitInfo.set.c1, c1.gameObject);
        var c2 = Stones.GenerateStoneModel(unitInfo.set.c2, false);
        DicAdd<string, GameObject>.Add(_cEffects, unitInfo.set.c2, c2.gameObject);
        var c3 = Stones.GenerateStoneModel(unitInfo.set.c3, false);
        DicAdd<string, GameObject>.Add(_cEffects, unitInfo.set.c3, c3.gameObject);

        void Parent(Transform t, Transform target)
        {
            t.SetParent(target);
            t.localPosition = Vector3.zero;
            t.localScale = Vector3.one;
        }

        Parent(a1.transform, Attack.transform);
        Parent(a2.transform, Attack.transform);
        Parent(a3.transform, Attack.transform);

        Parent(b1.transform, Fire1.transform);
        Parent(b2.transform, Fire1.transform);
        Parent(b3.transform, Fire1.transform);

        Parent(c1.transform, Fire2.transform);
        Parent(c2.transform, Fire2.transform);
        Parent(c3.transform, Fire2.transform);
        
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
