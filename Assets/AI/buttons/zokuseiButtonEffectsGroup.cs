using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zokuseiButtonEffectsGroup
{
    //攻击键系成员
    public IDictionary<Button, IDictionary<int, ParticleSystem>> buttonEffectsSets = new Dictionary<Button, IDictionary<int, ParticleSystem>>(); 
    public IDictionary<int, ParticleSystem> Attack1ButtonEffects;
    public IDictionary<int, ParticleSystem> Fire1ButtonEffects;
    public IDictionary<int, ParticleSystem> Fire2ButtonEffects;
    public IDictionary<Button, ParticleSystem> buttonRefreshEffects;
    public ParticleSystem triggerExplosion0;
    public ParticleSystem triggerExplosion1;
    public ParticleSystem triggerExplosion2;
    public ParticleSystem triggerExplosion3;
    public ParticleSystem pressingExplosion;//这个不需要对象池子。
    
    ParticleSystem defendbutton;
    ParticleSystem rushbutton;
    ParticleSystem attackbuttonslot;
    ParticleSystem fire1buttonslot;
    ParticleSystem fire2buttonslot;
    ParticleSystem arefresh;
    ParticleSystem fire1refresh;
    ParticleSystem fire2refresh;

    public void Close()
    {
        foreach(KeyValuePair<Button, IDictionary<int, ParticleSystem>> keyValuePair in buttonEffectsSets)
        {
            foreach(KeyValuePair<int, ParticleSystem> exPPair in keyValuePair.Value)
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
        defendbutton.Stop(true);
        rushbutton.Stop(true);
    }
        
    public void Open(Vector3 defendbuttonpos,Vector3 rushbuttonpos)
    {
        foreach(KeyValuePair<Button, IDictionary<int, ParticleSystem>> keyValuePair in buttonEffectsSets)
        {
            foreach(KeyValuePair<int, ParticleSystem> exPPair in keyValuePair.Value)
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
        
        defendbutton.gameObject.transform.position = defendbuttonpos;
        defendbutton.Play(true);
        rushbutton.gameObject.transform.position = rushbuttonpos;
        rushbutton.Play(true);
    }
                
    public void INI(Transform targetRectT, Zokusei zokusei,Button Attack, Button Fire1, Button Fire2)
    {
        string buttoneffectspath = FightGlobalSetting.EffectPathDefine(zokusei);
                
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

        attackbuttonslot = Object.Instantiate(buttonslot).GetComponent<ParticleSystem>();
        fire1buttonslot = Object.Instantiate(buttonslot).GetComponent<ParticleSystem>();
        fire2buttonslot = Object.Instantiate(buttonslot).GetComponent<ParticleSystem>();
        defendbutton = Object.Instantiate(Defend).GetComponent<ParticleSystem>();
        rushbutton = Object.Instantiate(Rush).GetComponent<ParticleSystem>();
        arefresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        fire1refresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        fire2refresh = Object.Instantiate(refresh).GetComponent<ParticleSystem>();
        triggerExplosion0 = Object.Instantiate(triggerExplosionPretab0).GetComponent<ParticleSystem>();
        triggerExplosion1 = Object.Instantiate(triggerExplosionPretab1).GetComponent<ParticleSystem>();
        triggerExplosion2 = Object.Instantiate(triggerExplosionPretab2).GetComponent<ParticleSystem>();
        triggerExplosion3 = Object.Instantiate(triggerExplosionPretab3).GetComponent<ParticleSystem>();
        pressingExplosion = Object.Instantiate(pressingExplosionPretab).GetComponent<ParticleSystem>();

        attackbuttonslot.transform.SetParent(targetRectT);
        fire1buttonslot.transform.SetParent(targetRectT);
        fire2buttonslot.transform.SetParent(targetRectT);
        defendbutton.transform.SetParent(targetRectT);
        rushbutton.transform.SetParent(targetRectT);
        arefresh.transform.SetParent(targetRectT);
        fire1refresh.transform.SetParent(targetRectT);
        fire2refresh.transform.SetParent(targetRectT);
        triggerExplosion0.transform.SetParent(targetRectT);
        triggerExplosion1.transform.SetParent(targetRectT);
        triggerExplosion2.transform.SetParent(targetRectT);
        triggerExplosion3.transform.SetParent(targetRectT);
        pressingExplosion.transform.SetParent(targetRectT);

        buttonRefreshEffects = new Dictionary<Button, ParticleSystem>
        {
            { Attack, arefresh },
            { Fire1, fire1refresh },
            { Fire2, fire2refresh }
        };

        Attack1ButtonEffects = new Dictionary<int, ParticleSystem>();
        Fire1ButtonEffects = new Dictionary<int, ParticleSystem>();
        Fire2ButtonEffects = new Dictionary<int, ParticleSystem>();

        GameObject a_normal = Object.Instantiate(normal);
        a_normal.name = "attack_normal"+buttoneffectspath;
        GameObject a_ex1 = Object.Instantiate(EX1);
        a_ex1.name = "attack_ex1"+buttoneffectspath;
        GameObject a_ex2 = Object.Instantiate(EX2);
        a_ex2.name = "attack_ex2"+buttoneffectspath;
        GameObject a_ex3 = Object.Instantiate(EX3);
        a_ex3.name = "attack_ex3"+buttoneffectspath;
        
        GameObject fire1_normal = Object.Instantiate(normal);
        fire1_normal.name = "fire1_normal"+buttoneffectspath;
        GameObject fire1_ex1 = Object.Instantiate(EX1);
        fire1_ex1.name = "fire1_ex1"+buttoneffectspath;
        GameObject fire1_ex2 = Object.Instantiate(EX2); 
        fire1_ex2.name = "fire1_ex2"+buttoneffectspath;
        GameObject fire1_ex3 = Object.Instantiate(EX3);
        fire1_ex3.name = "fire1_ex3"+buttoneffectspath;
        
        GameObject fire2_normal = Object.Instantiate(normal);
        fire2_normal.name = "fire2_normal"+buttoneffectspath;
        GameObject fire2_ex1 = Object.Instantiate(EX1);
        fire2_ex1.name = "fire2_ex1"+buttoneffectspath;
        GameObject fire2_ex2 = Object.Instantiate(EX2); 
        fire2_ex2.name = "fire2_ex2"+buttoneffectspath;
        GameObject fire2_ex3 = Object.Instantiate(EX3);
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
    
    IDictionary<int, ParticleSystem> tartget;
    public void Refreshforbutton(Button button,int eX,Vector3 pos)
    {
        tartget = buttonEffectsSets[button];
        foreach(KeyValuePair<int, ParticleSystem> pair in tartget)
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
