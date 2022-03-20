using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementEffectsGroup
{
    //攻击键系成员
    IDictionary<Button, IDictionary<int, ParticleSystem>> buttonEffectsSets = new Dictionary<Button, IDictionary<int, ParticleSystem>>(); 
    IDictionary<int, ParticleSystem> Attack1ButtonEffects;
    IDictionary<int, ParticleSystem> Fire1ButtonEffects;
    IDictionary<int, ParticleSystem> Fire2ButtonEffects;
    public IDictionary<Button, ParticleSystem> buttonRefreshEffects;
    public ParticleSystem triggerExplosion0;
    public ParticleSystem triggerExplosion1;
    public ParticleSystem triggerExplosion2;
    public ParticleSystem triggerExplosion3;
    public ParticleSystem pressingExplosion;//这个不需要对象池子。

    IDictionary<Button, ParticleSystem> buttonSlotEffects;

    ParticleSystem _defendbutton;
    ParticleSystem _rushbutton;
    ParticleSystem _arefresh;
    ParticleSystem _fire1Refresh;
    ParticleSystem _fire2Refresh;
    
    public void Close()
    {
        foreach(var keyValuePair in buttonEffectsSets)
        {
            foreach(var exPPair in keyValuePair.Value)
            {
                if (exPPair.Value != null)
                    exPPair.Value.Stop(true);
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
        
        foreach (KeyValuePair<Button, ParticleSystem> keyValue in buttonRefreshEffects)
        {
            keyValue.Value.Stop(true);
        }
        pressingExplosion.Stop(true);
        _rushbutton.Stop(true);
        
        if (FightGlobalSetting._hasDefend)
            _defendbutton.Stop(true);
    }
        
    public void Open(Vector3 defendBtnPos, Vector3 rushBtnPos)
    {
        foreach(var keyValuePair in buttonEffectsSets)
        {
            foreach(var exPPair in keyValuePair.Value)
            {
                exPPair.Value.Stop(true);
            }
        }
        triggerExplosion0.Stop(true);
        triggerExplosion1.Stop(true);
        triggerExplosion2.Stop(true);
        triggerExplosion3.Stop(true);
        
        foreach (KeyValuePair<Button, ParticleSystem> keyValue in buttonRefreshEffects)
        {
            keyValue.Value.Stop(true);
        }
        pressingExplosion.Stop(true);
        _rushbutton.gameObject.transform.position = rushBtnPos;
        _rushbutton.Play(true);
        
        if (FightGlobalSetting._hasDefend)
        {
            _defendbutton.gameObject.transform.position = defendBtnPos;
            _defendbutton.Play(true);
        }
    }
                
    public void INI(Transform targetRectT, Element element,Button Attack, Button Fire1, Button Fire2)
    {
        string buttoneffectspath = FightGlobalSetting.EffectPathDefine(element);
                
        var buttonslot = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/slot", typeof(GameObject)) as GameObject;
        var normal = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/normal", typeof(GameObject)) as GameObject;
        var EX1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX1", typeof(GameObject)) as GameObject;
        var EX2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX2", typeof(GameObject)) as GameObject;
        var EX3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/EX3", typeof(GameObject)) as GameObject;
        var Defend = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/defend", typeof(GameObject)) as GameObject;
        var Rush = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/rush", typeof(GameObject)) as GameObject;
        var refresh = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/refresh", typeof(GameObject)) as GameObject;
        var triggerExplosionPretab0 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion0", typeof(GameObject)) as GameObject;
        var triggerExplosionPretab1 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion1", typeof(GameObject)) as GameObject;
        var triggerExplosionPretab2 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion2", typeof(GameObject)) as GameObject;
        var triggerExplosionPretab3 = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/explosion3", typeof(GameObject)) as GameObject;
        var pressingExplosionPretab = Resources.Load("essentialUIElements/buttonEffects" + "/" + buttoneffectspath + "/pressing", typeof(GameObject)) as GameObject;

        var attackbuttonslot = Object.Instantiate(buttonslot).GetComponent<ParticleSystem>();
        var fire1buttonslot = Object.Instantiate(buttonslot).GetComponent<ParticleSystem>();
        var fire2buttonslot = Object.Instantiate(buttonslot).GetComponent<ParticleSystem>();
        
        attackbuttonslot.transform.SetParent(targetRectT);
        fire1buttonslot.transform.SetParent(targetRectT);
        fire2buttonslot.transform.SetParent(targetRectT);
        
        buttonSlotEffects = new Dictionary<Button, ParticleSystem>
        {
            { Attack, attackbuttonslot },
            { Fire1, fire1buttonslot },
            { Fire2, fire2buttonslot }
        };
        
        if (FightGlobalSetting._hasDefend)
            _defendbutton = Object.Instantiate(Defend).GetComponent<ParticleSystem>();
        _rushbutton = Object.Instantiate(Rush).GetComponent<ParticleSystem>();
        _arefresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        _fire1Refresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        _fire2Refresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        triggerExplosion0 = Object.Instantiate(triggerExplosionPretab0).GetComponent<ParticleSystem>();
        triggerExplosion1 = Object.Instantiate(triggerExplosionPretab1).GetComponent<ParticleSystem>();
        triggerExplosion2 = Object.Instantiate(triggerExplosionPretab2).GetComponent<ParticleSystem>();
        triggerExplosion3 = Object.Instantiate(triggerExplosionPretab3).GetComponent<ParticleSystem>();
        pressingExplosion = Object.Instantiate(pressingExplosionPretab).GetComponent<ParticleSystem>();

        if (FightGlobalSetting._hasDefend)
            _defendbutton.transform.SetParent(targetRectT);
        _rushbutton.transform.SetParent(targetRectT);
        _arefresh.transform.SetParent(targetRectT);
        _fire1Refresh.transform.SetParent(targetRectT);
        _fire2Refresh.transform.SetParent(targetRectT);
        triggerExplosion0.transform.SetParent(targetRectT);
        triggerExplosion1.transform.SetParent(targetRectT);
        triggerExplosion2.transform.SetParent(targetRectT);
        triggerExplosion3.transform.SetParent(targetRectT);
        pressingExplosion.transform.SetParent(targetRectT);

        buttonRefreshEffects = new Dictionary<Button, ParticleSystem>
        {
            { Attack, _arefresh },
            { Fire1, _fire1Refresh },
            { Fire2, _fire2Refresh }
        };
        
        Attack1ButtonEffects = new Dictionary<int, ParticleSystem>();
        Fire1ButtonEffects = new Dictionary<int, ParticleSystem>();
        Fire2ButtonEffects = new Dictionary<int, ParticleSystem>();

        var a_normal = Object.Instantiate(normal);
        a_normal.name = "attack_normal"+buttoneffectspath;
        var a_ex1 = Object.Instantiate(EX1);
        a_ex1.name = "attack_ex1"+buttoneffectspath;
        var a_ex2 = Object.Instantiate(EX2);
        a_ex2.name = "attack_ex2"+buttoneffectspath;
        var a_ex3 = Object.Instantiate(EX3);
        a_ex3.name = "attack_ex3"+buttoneffectspath;
        
        var fire1_normal = Object.Instantiate(normal);
        fire1_normal.name = "fire1_normal"+buttoneffectspath;
        var fire1_ex1 = Object.Instantiate(EX1);
        fire1_ex1.name = "fire1_ex1"+buttoneffectspath;
        var fire1_ex2 = Object.Instantiate(EX2); 
        fire1_ex2.name = "fire1_ex2"+buttoneffectspath;
        var fire1_ex3 = Object.Instantiate(EX3);
        fire1_ex3.name = "fire1_ex3"+buttoneffectspath;
        
        var fire2_normal = Object.Instantiate(normal);
        fire2_normal.name = "fire2_normal"+buttoneffectspath;
        var fire2_ex1 = Object.Instantiate(EX1);
        fire2_ex1.name = "fire2_ex1"+buttoneffectspath;
        var fire2_ex2 = Object.Instantiate(EX2); 
        fire2_ex2.name = "fire2_ex2"+buttoneffectspath;
        var fire2_ex3 = Object.Instantiate(EX3);
        fire2_ex3.name = "fire2_ex3"+buttoneffectspath;
        
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
        
        Attack1ButtonEffects.Add(0, a_normal.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(1, a_ex1.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(2, a_ex2.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(3, a_ex3.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(-1, attackbuttonslot.GetComponent<ParticleSystem>());
        
        Fire1ButtonEffects.Add(0, fire1_normal.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(1, fire1_ex1.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(2, fire1_ex2.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(3, fire1_ex3.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(-1, fire1buttonslot.GetComponent<ParticleSystem>());
        
        Fire2ButtonEffects.Add(0, fire2_normal.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(1, fire2_ex1.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(2, fire2_ex2.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(3, fire2_ex3.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(-1, fire2buttonslot.GetComponent<ParticleSystem>());

        buttonEffectsSets = new Dictionary<Button, IDictionary<int, ParticleSystem>>
        {
            { Attack, Attack1ButtonEffects },
            { Fire1, Fire1ButtonEffects },
            { Fire2, Fire2ButtonEffects }
        };
    }
    
    IDictionary<int, ParticleSystem> _target;
    public void RefreshBtn(Button button, int eX, Vector3 pos)
    {
        _target = buttonEffectsSets[button];
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
                //if (!pair.Value.isPlaying)
                    pair.Value.Play(true);
            }
            else
            {
                pair.Value.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
                //pair.Value.Clear(true);
            }
        }
    }
}
