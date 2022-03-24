using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementEffectsGroup
{
    //攻击键系成员
    IDictionary<Button, IDictionary<int, ParticleSystem>> btnEffectsSets = new Dictionary<Button, IDictionary<int, ParticleSystem>>();
    readonly IDictionary<int, ParticleSystem> _attack1Effects = new Dictionary<int, ParticleSystem>();
    readonly IDictionary<int, ParticleSystem> _fire1Effects = new Dictionary<int, ParticleSystem>();
    readonly IDictionary<int, ParticleSystem> _fire2Effects = new Dictionary<int, ParticleSystem>();
    public IDictionary<Button, ParticleSystem> BtnRefreshEffects = new Dictionary<Button, ParticleSystem>();
    public ParticleSystem triggerExplosion0;
    public ParticleSystem triggerExplosion1;
    public ParticleSystem triggerExplosion2;
    public ParticleSystem triggerExplosion3;
    public ParticleSystem pressingExplosion;//这个不需要对象池子。

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
                    exPPair.Value.Stop(true);
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
                exPPair.Value.Stop(true);
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
                
    public void INI(Transform targetRectT, Element element, Button Attack, Button Fire1, Button Fire2)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        var slot = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/slot", typeof(GameObject)) as GameObject;
        var normal = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/normal", typeof(GameObject)) as GameObject;
        var EX1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/EX1", typeof(GameObject)) as GameObject;
        var EX2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/EX2", typeof(GameObject)) as GameObject;
        var EX3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/EX3", typeof(GameObject)) as GameObject;
        var Defend = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/defend", typeof(GameObject)) as GameObject;
        var Rush = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/rush", typeof(GameObject)) as GameObject;
        var refresh = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/refresh", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab0 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/explosion0", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/explosion1", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/explosion2", typeof(GameObject)) as GameObject;
        var triggerExplosionPrefab3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/explosion3", typeof(GameObject)) as GameObject;
        var pressingExplosionPrefab = Resources.Load("essentialUIElements/buttonEffects" + "/" + path + "/pressing", typeof(GameObject)) as GameObject;
        
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
        
        var a_normal = Object.Instantiate(normal);
        a_normal.name = "attack_normal"+path;
        var a_ex1 = Object.Instantiate(EX1);
        a_ex1.name = "attack_ex1"+path;
        var a_ex2 = Object.Instantiate(EX2);
        a_ex2.name = "attack_ex2"+path;
        var a_ex3 = Object.Instantiate(EX3);
        a_ex3.name = "attack_ex3"+path;
        
        var fire1_normal = Object.Instantiate(normal);
        fire1_normal.name = "fire1_normal"+path;
        var fire1_ex1 = Object.Instantiate(EX1);
        fire1_ex1.name = "fire1_ex1"+path;
        var fire1_ex2 = Object.Instantiate(EX2); 
        fire1_ex2.name = "fire1_ex2"+path;
        var fire1_ex3 = Object.Instantiate(EX3);
        fire1_ex3.name = "fire1_ex3"+path;
        
        var fire2_normal = Object.Instantiate(normal);
        fire2_normal.name = "fire2_normal"+path;
        var fire2_ex1 = Object.Instantiate(EX1);
        fire2_ex1.name = "fire2_ex1"+path;
        var fire2_ex2 = Object.Instantiate(EX2); 
        fire2_ex2.name = "fire2_ex2"+path;
        var fire2_ex3 = Object.Instantiate(EX3);
        fire2_ex3.name = "fire2_ex3"+path;
        
        a_normal.transform.SetParent(targetRectT);
        a_ex1.transform.SetParent(targetRectT);
        a_ex2.transform.SetParent(targetRectT);
        a_ex3.transform.SetParent(targetRectT);
        fire1_normal.transform.SetParent(targetRectT);
        fire1_ex1.transform.SetParent(targetRectT);
        fire1_ex2.transform.SetParent(targetRectT);
        fire1_ex3.transform.SetParent(targetRectT);
        fire2_normal.transform.SetParent(targetRectT);
        fire2_ex1.transform.SetParent(targetRectT);
        fire2_ex2.transform.SetParent(targetRectT);
        fire2_ex3.transform.SetParent(targetRectT);
        
        _attack1Effects.Add(0, a_normal.GetComponent<ParticleSystem>());
        _attack1Effects.Add(1, a_ex1.GetComponent<ParticleSystem>());
        _attack1Effects.Add(2, a_ex2.GetComponent<ParticleSystem>());
        _attack1Effects.Add(3, a_ex3.GetComponent<ParticleSystem>());
        _attack1Effects.Add(-1, attackSlot.GetComponent<ParticleSystem>());
        
        _fire1Effects.Add(0, fire1_normal.GetComponent<ParticleSystem>());
        _fire1Effects.Add(1, fire1_ex1.GetComponent<ParticleSystem>());
        _fire1Effects.Add(2, fire1_ex2.GetComponent<ParticleSystem>());
        _fire1Effects.Add(3, fire1_ex3.GetComponent<ParticleSystem>());
        _fire1Effects.Add(-1, fire1Slot.GetComponent<ParticleSystem>());
        
        _fire2Effects.Add(0, fire2_normal.GetComponent<ParticleSystem>());
        _fire2Effects.Add(1, fire2_ex1.GetComponent<ParticleSystem>());
        _fire2Effects.Add(2, fire2_ex2.GetComponent<ParticleSystem>());
        _fire2Effects.Add(3, fire2_ex3.GetComponent<ParticleSystem>());
        _fire2Effects.Add(-1, fire2Slot.GetComponent<ParticleSystem>());

        btnEffectsSets = new Dictionary<Button, IDictionary<int, ParticleSystem>>
        {
            { Attack, _attack1Effects },
            { Fire1, _fire1Effects },
            { Fire2, _fire2Effects }
        };
    }
    
    public void RefreshBtn(Button button, int eX, Vector3 pos)
    {
        var _target = btnEffectsSets[button];
        if (eX == -1)
        {
            buttonSlotEffects[button].transform.position = pos;
            buttonSlotEffects[button].Play(true);
        }else{
            buttonSlotEffects[button].Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
        }
                
        foreach(KeyValuePair<int, ParticleSystem> pair in _target)
        {
            if (pair.Key == eX)
            {
                pair.Value.gameObject.transform.position = pos;
                pair.Value.gameObject.SetActive(true);
                pair.Value.Play(true);
            }
            else
            {
                pair.Value.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
                pair.Value.gameObject.SetActive(false);
            }
        }
    }
}
