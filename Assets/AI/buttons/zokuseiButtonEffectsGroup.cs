using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.UI;

public class zokuseiButtonEffectsGroup
{
    public zokusei zokusei;
    
    //攻击键系成员
    public IDictionary<Button, IDictionary<EX, ParticleSystem>> buttonEffectsSets = new Dictionary<Button, IDictionary<EX, ParticleSystem>>(); 
    public IDictionary<EX, ParticleSystem> Attack1ButtonEffects;
    public IDictionary<EX, ParticleSystem> Fire1ButtonEffects;
    public IDictionary<EX, ParticleSystem> Fire2ButtonEffects;
    public IDictionary<Button, ParticleSystem> buttonRefreshEffects;
    public ParticleSystem triggerExplosion0;
    public ParticleSystem triggerExplosion1;
    public ParticleSystem triggerExplosion2;
    public ParticleSystem triggerExplosion3;
    public ParticleSystem pressingExplosion;//这个不需要对象池子。
    private ParticleSystem defendbutton;
    private ParticleSystem rushbutton;
    private ParticleSystem attackbuttonslot;
    private ParticleSystem fire1buttonslot;
    private ParticleSystem fire2buttonslot;
    private ParticleSystem arefresh;
    private ParticleSystem fire1refresh;
    private ParticleSystem fire2refresh;
    
    public void close()
    {
        foreach(KeyValuePair<Button, IDictionary<EX, ParticleSystem>> keyValuePair in buttonEffectsSets)
        {
            foreach(KeyValuePair<EX, ParticleSystem> exPPair in keyValuePair.Value)
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
        
    public void open(Vector3 attackbuttonpos,Vector3 fire1buttonpos,Vector3 fire2buttonpos,Vector3 defendbuttonpos,Vector3 rushbuttonpos)
    {
        foreach(KeyValuePair<Button, IDictionary<EX, ParticleSystem>> keyValuePair in buttonEffectsSets)
        {
            foreach(KeyValuePair<EX, ParticleSystem> exPPair in keyValuePair.Value)
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
                
    public void INI(zokusei zokusei,Button Attack,Button Fire1,Button Fire2)
    {
        this.zokusei = zokusei;
        string buttoneffectspath;
        switch(zokusei)
        {
                case zokusei.blueMagic:
                buttoneffectspath = "blueMagic";
                break;
                case zokusei.darkMagic:
                buttoneffectspath = "darkMagic";
                break;
                case zokusei.greenMagic:
                buttoneffectspath = "greenMagic";
                break;
                case zokusei.lightMagic:
                buttoneffectspath = "lightMagic";
                break;
                case zokusei.redMagic:
                buttoneffectspath = "redMagic";
                break;
                default:
                buttoneffectspath = "darkMagic";
                break;
        }
        
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

        buttonRefreshEffects = new Dictionary<Button, ParticleSystem>();
        buttonRefreshEffects.Add(Attack,arefresh);
        buttonRefreshEffects.Add(Fire1,fire1refresh);
        buttonRefreshEffects.Add(Fire2,fire2refresh);
        
        Attack1ButtonEffects = new Dictionary<EX, ParticleSystem>();
        Fire1ButtonEffects = new Dictionary<EX, ParticleSystem>();
        Fire2ButtonEffects = new Dictionary<EX, ParticleSystem>();

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
        
        Attack1ButtonEffects.Add(EX.normal,a_normal.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(EX.EX1,a_ex1.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(EX.EX2,a_ex2.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(EX.EX3,a_ex3.GetComponent<ParticleSystem>());
        Attack1ButtonEffects.Add(EX.NULL,attackbuttonslot.GetComponent<ParticleSystem>());
        
        Fire1ButtonEffects.Add(EX.normal,fire1_normal.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(EX.EX1,fire1_ex1.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(EX.EX2,fire1_ex2.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(EX.EX3,fire1_ex3.GetComponent<ParticleSystem>());
        Fire1ButtonEffects.Add(EX.NULL,fire1buttonslot.GetComponent<ParticleSystem>());
        
        Fire2ButtonEffects.Add(EX.normal,fire2_normal.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(EX.EX1,fire2_ex1.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(EX.EX2,fire2_ex2.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(EX.EX3,fire2_ex3.GetComponent<ParticleSystem>());
        Fire2ButtonEffects.Add(EX.NULL,fire2buttonslot.GetComponent<ParticleSystem>());
        
        buttonEffectsSets = new Dictionary<Button, IDictionary<EX, ParticleSystem>>();
        buttonEffectsSets.Add(Attack,Attack1ButtonEffects);
        buttonEffectsSets.Add(Fire1,Fire1ButtonEffects);
        buttonEffectsSets.Add(Fire2,Fire2ButtonEffects);
    }
    
    IDictionary<EX, ParticleSystem> tartget;
    public void refreshforbutton(Button button,EX eX,Vector3 pos)
    {
        tartget = buttonEffectsSets[button];
        foreach(KeyValuePair<EX, ParticleSystem> pair in tartget)
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
