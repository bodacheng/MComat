using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

public class ElementEffectsGroup
{
    //攻击键系成员
    IDictionary<Button, IDictionary<string, GameObject>> btnEffectsSets = new Dictionary<Button, IDictionary<string, GameObject>>();
    readonly IDictionary<string, GameObject> _aEffects = new Dictionary<string, GameObject>();
    readonly IDictionary<string, GameObject> _bEffects = new Dictionary<string, GameObject>();
    readonly IDictionary<string, GameObject> _cEffects = new Dictionary<string, GameObject>();
    IDictionary<Button, ParticleSystem> _btnRefreshEffects = new Dictionary<Button, ParticleSystem>();
    ParticleSystem _triggerExplosion0;
    ParticleSystem _triggerExplosion1;
    ParticleSystem _triggerExplosion2;
    ParticleSystem _triggerExplosion3;
    IDictionary<Button, ParticleSystem> _buttonSlotEffects;
    ParticleSystem _defendBtn;
    ParticleSystem _rushBtn;
    ParticleSystem _pressingExplosion; // 这个不需要对象池。
    
    public void StartPressing(Button targetBtn)
    {
        var targetPos = PosCal.GetWorldPos(FightScene.FightScene.target.fxCamera, targetBtn.GetComponent<RectTransform>(), 7);
        _pressingExplosion.transform.position = targetPos;
        _pressingExplosion.Play();
    }
    
    public void StopPressing()
    {
        _pressingExplosion.Stop();
    }
    
    public void BtnRefreshEffect()
    {
        foreach (var pair in _btnRefreshEffects)
        {
            pair.Value.transform.position = PosCal.GetWorldPos(FightScene.FightScene.target.fxCamera, pair.Key.GetComponent<RectTransform>(),4);
            pair.Value.Play(true);
        }
    }

    public ParticleSystem GetExplosionEffect(int spLevel)
    {
        ParticleSystem targetExplode;
        switch(spLevel)
        {
            case 0:
                targetExplode = _triggerExplosion0;
                break;
            case 1:
                targetExplode = _triggerExplosion1;
                break;
            case 2:
                targetExplode = _triggerExplosion2;
                break;
            case 3:
                targetExplode = _triggerExplosion3;
                break;
            default:
                return null;
        }
        return targetExplode;
    }
    
    public void Close(ParticleSystemStopBehavior systemStopBehavior)
    {
        foreach(var kv in btnEffectsSets)
        {
            foreach(var pair in kv.Value)
            {
                if (pair.Value != null)
                {
                    pair.Value.gameObject.SetActive(false);
                }
            }
        }

        foreach (var kv in _buttonSlotEffects)
        {
            kv.Value.Stop(true, systemStopBehavior);
        }
        
        _triggerExplosion0.Stop(true, systemStopBehavior);
        _triggerExplosion1.Stop(true, systemStopBehavior);
        _triggerExplosion2.Stop(true, systemStopBehavior);
        _triggerExplosion3.Stop(true, systemStopBehavior);
        
        foreach (var keyValue in _btnRefreshEffects)
        {
            keyValue.Value.Stop(true, systemStopBehavior);
        }
        _pressingExplosion.Stop(true, systemStopBehavior);
        _rushBtn.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        if (FightGlobalSetting.HasDefend)
            _defendBtn.Stop(true, systemStopBehavior);
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
        _triggerExplosion0.Stop(true);
        _triggerExplosion1.Stop(true);
        _triggerExplosion2.Stop(true);
        _triggerExplosion3.Stop(true);
        
        foreach (var keyValue in _btnRefreshEffects)
        {
            keyValue.Value.Stop(true);
        }
        _pressingExplosion.Stop(true);
        _rushBtn.gameObject.transform.position = rushBtnPos;
        _rushBtn.Play(true);
        
        if (FightGlobalSetting.HasDefend)
        {
            _defendBtn.gameObject.transform.position = defendBtnPos;
            _defendBtn.Play(true);
        }
    }

    public async UniTask InitializeCommon(Transform targetRectT, Element element, Button a1Btn, Button a2Btn, Button a3Btn)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        var attackSlot = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab");
        var fire1Slot = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab");
        var fire2Slot = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab");
        
        attackSlot.transform.SetParent(targetRectT);
        fire1Slot.transform.SetParent(targetRectT);
        fire2Slot.transform.SetParent(targetRectT);
        
        _buttonSlotEffects = new Dictionary<Button, ParticleSystem>
        {
            { a1Btn, attackSlot },
            { a2Btn, fire1Slot },
            { a3Btn, fire2Slot }
        };

        if (FightGlobalSetting.HasDefend)
        {
            _defendBtn = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/defend.prefab");
        }
        
        _rushBtn = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/rush.prefab");
        var a1Refresh = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab");
        var a2Refresh = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab");
        var a3Refresh = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab");
        _triggerExplosion0 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion0.prefab");
        _triggerExplosion1 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion1.prefab");
        _triggerExplosion2 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion2.prefab");
        _triggerExplosion3 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion3.prefab");
        _pressingExplosion = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/pressing.prefab");
        
        if (FightGlobalSetting.HasDefend)
            _defendBtn.transform.SetParent(targetRectT);
        _rushBtn.transform.SetParent(targetRectT);
        a1Refresh.gameObject.name = "a1Refresh";
        a2Refresh.gameObject.name = "a2Refresh";
        a3Refresh.gameObject.name = "a3Refresh";
        a1Refresh.transform.SetParent(targetRectT);
        a2Refresh.transform.SetParent(targetRectT);
        a3Refresh.transform.SetParent(targetRectT);
        _triggerExplosion0.transform.SetParent(targetRectT);
        _triggerExplosion1.transform.SetParent(targetRectT);
        _triggerExplosion2.transform.SetParent(targetRectT);
        _triggerExplosion3.transform.SetParent(targetRectT);
        _pressingExplosion.transform.SetParent(targetRectT);

        _btnRefreshEffects = new Dictionary<Button, ParticleSystem>
        {
            { a1Btn, a1Refresh },
            { a2Btn, a2Refresh },
            { a3Btn, a3Refresh }
        };
    }
    
    public async UniTask InitializeBtn(Button a1Btn, Button a2Btn, Button a3Btn, UnitInfo unitInfo)
    {
        async UniTask Process(Button btn, string skillID, IDictionary<string, GameObject> dic)
        {
            if (!dic.ContainsKey(skillID))
            {
                var icon = await Stones.GenerateStoneModel(skillID, false);
                if (icon == null) return;
                DicAdd<string, GameObject>.Add(dic, skillID, icon.gameObject);
                Parent(icon.transform, btn.transform);
            }
        }
        
        await Process(a1Btn, unitInfo.set.a1, _aEffects);
        await Process(a1Btn, unitInfo.set.a2, _aEffects);
        await Process(a1Btn, unitInfo.set.a3, _aEffects);
        
        await Process(a2Btn, unitInfo.set.b1, _bEffects);
        await Process(a2Btn, unitInfo.set.b2, _bEffects);
        await Process(a2Btn, unitInfo.set.b3, _bEffects);
        
        await Process(a3Btn, unitInfo.set.c1, _cEffects);
        await Process(a3Btn, unitInfo.set.c2, _cEffects);
        await Process(a3Btn, unitInfo.set.c3, _cEffects);
        
        void Parent(Transform t, Transform target)
        {
            t.SetParent(target);
            t.localPosition = Vector3.zero;
            t.localScale = Vector3.one;
        }
        
        btnEffectsSets = new Dictionary<Button, IDictionary<string, GameObject>>
        {
            { a1Btn, _aEffects },
            { a2Btn, _bEffects },
            { a3Btn, _cEffects }
        };
    }
    
    public void RefreshBtn(Button button, string skillId, Vector3 pos)
    {
        var target = btnEffectsSets[button];
        if (skillId == String.Empty)
        {
            _buttonSlotEffects[button].transform.position = pos;
            _buttonSlotEffects[button].Play(true);
        }else{
            _buttonSlotEffects[button].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        
        foreach(var pair in target)
        {
            pair.Value.gameObject.SetActive(pair.Key == skillId);
        }
    }
}
