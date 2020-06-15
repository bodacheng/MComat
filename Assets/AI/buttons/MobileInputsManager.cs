using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Soul;
using Skill;

public class MobileInputsManager : MonoBehaviour {

    public Button Attack;
    public Button Fire1;
    public Button Fire2;
    public Button Defend;
    public Button Dash;
    
    //2019.3.26 折腾了整整两天的移动端按键粒子特效。留下的唯一一点不足是，没有针对防御状态，rush状态的有无来决定防御键是否显示，也没有针对未来可能出现的耗气式防御或rush状态来刷新两个键的显示状态。
    public Camera fxCamera;
    public Transform effectsParent;
    
    public static MobileInputsManager target;
    static IDictionary<Zokusei, zokuseiButtonEffectsGroup> zokuseiButtonEffects = new Dictionary<Zokusei, zokuseiButtonEffectsGroup>();
    static zokuseiButtonEffectsGroup _focusingButtonEffectsGroup;
    public BehaviorRunner Observing_Runner;
    
    public static bool playerMode;
    public static bool inputting;
    
    void Awake()
    {
        target = this;
    }

    public static void SetPlayerMode(bool result)
    {
        playerMode = result;
        inputting = false;
    }
    
    public void FocusCharInputs(BehaviorRunner focusingCharInputManger,Zokusei zokusei)
    {
        Observing_Runner = focusingCharInputManger;
        if (Observing_Runner != null)
        {
            SwitchZokuseiButtons(zokusei);
        }else{
            TurnOffButtons();
        }
    }

    public void Clear()
    {
        zokuseiButtonEffects.Clear();
        _focusingButtonEffectsGroup = null;
    }
    
    // 切换输入按键表现层（红黄蓝绿）.这个函数使用的前提是所有用的上的控制器组都已经注册并初始化
    void SwitchZokuseiButtons(Zokusei zokusei)
    {
        if (_focusingButtonEffectsGroup != null)
            _focusingButtonEffectsGroup.Close();
        if (zokuseiButtonEffects.ContainsKey(zokusei))
        {
            _focusingButtonEffectsGroup = zokuseiButtonEffects[zokusei];
            _focusingButtonEffectsGroup.Open(ButtonEffectInFxCameraWorldSpace(Defend,5),ButtonEffectInFxCameraWorldSpace(Dash,5));
        }else{
            Debug.Log("见鬼了。检查手机控制器渲染模块加载顺序");
        }
    }

    public void ZokuseiButtonRegister(Zokusei zokusei)
    {
        zokuseiButtonEffectsGroup zokuseiButtons = new zokuseiButtonEffectsGroup();
        zokuseiButtons.INI(effectsParent, zokusei, Attack, Fire1, Fire2);
        zokuseiButtons.Close();
        if (!zokuseiButtonEffects.ContainsKey(zokusei))
        {
            zokuseiButtonEffects.Add(zokusei, zokuseiButtons);
        } else {
            zokuseiButtonEffects[zokusei] = zokuseiButtons;
        }
    }

    static ParticleSystem targetexplode;
    public static void SkillButtonExplosion(InputKey inputs_Defined,int eX)
    {
        switch(eX)
        {
            case 0:
                targetexplode = _focusingButtonEffectsGroup.triggerExplosion0;
            break;
            case 1:
                targetexplode = _focusingButtonEffectsGroup.triggerExplosion1;
            break;
            case 2:
                targetexplode = _focusingButtonEffectsGroup.triggerExplosion2;
            break;
            case 3:
                targetexplode = _focusingButtonEffectsGroup.triggerExplosion3;
            break;
            case -1:
                return;
        }
    
        switch (inputs_Defined)
        {
            case InputKey.Attack1:
                targetexplode.transform.position = ButtonEffectInFxCameraWorldSpace(target.Attack,3);
                break;
            case InputKey.Attack2:
                targetexplode.transform.position = ButtonEffectInFxCameraWorldSpace(target.Fire1,3);
                break;
            case InputKey.Attack3:
                targetexplode.transform.position = ButtonEffectInFxCameraWorldSpace(target.Fire2,3);
                break;
        }
        targetexplode.Play();
        
        //下面这些是说，每当有技能爆炸特效也就代表技能表更新，那么需要整体刷新特效
        foreach (KeyValuePair<Button, ParticleSystem> keyValue in _focusingButtonEffectsGroup.buttonRefreshEffects)
        {
            keyValue.Value.transform.position = ButtonEffectInFxCameraWorldSpace(keyValue.Key,4);//也就是说，刷新特效都是三个键位一起出现，省的给人种误导好像我技能没变
            keyValue.Value.Play(true);
        }
    }

    // 等把机动和防御分离后，要做这样的事情：
    // 根据玩家的技能列表来决定防御，机动，三攻击键分别存在与否。
    // 然后，refresh button是要看情况的，攻击键要么是变成空按钮，要么应该是就没有按钮。。。？
    // 而防御与机动则是确定一直显示。
    void StartPressing(Button targetBUtton)
    {
        targetButtonPos = ButtonEffectInFxCameraWorldSpace(targetBUtton, 7);
        if (_focusingButtonEffectsGroup != null)
        {
            _focusingButtonEffectsGroup.pressingExplosion.transform.position = targetButtonPos;
            _focusingButtonEffectsGroup.pressingExplosion.Play();
        }
    }

    void StopPressing()
    {
        if (_focusingButtonEffectsGroup != null)
            _focusingButtonEffectsGroup.pressingExplosion.Stop();
    }
    
    public static void CheckIfPlayerIsInputting() // 如果不是对准角色，不会跑。
    {
        inputting = defendButtonHover;
        if (inputting)
        {
            return;
        }
        float h = 0f;
        float v = 0f;
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor 
        || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            h = ETCInput.GetAxis("Horizontal");
            v = ETCInput.GetAxis("Vertical");
        }
        inputting = (h > 0f || h < 0 || v > 0f || v < 0f);
    }

    public static bool defendButtonHover;
    public bool DefendExitTrigger()
    {
        return !defendButtonHover;
    }

    readonly Dictionary<InputKey, SkillEntity> Options_lastframe = new Dictionary<InputKey, SkillEntity>()
    {
        {InputKey.Attack1,null},
        {InputKey.Attack2,null},
        {InputKey.Attack3,null}
    };
    
    SkillEntity Behavior_preview_button1, Behavior_preview_button2, Behavior_preview_button3;
    public void ButtonsFeatureLoad(List<SkillEntity> Options_preview)
    {        
        Behavior_preview_button1 = null; Behavior_preview_button2 = null; Behavior_preview_button3 = null;
        for (int i = 0; i < Options_preview.Count; i++)
        {
            switch (Options_preview[i].EnterInput)
            {
                case InputKey.Attack1:
                    Behavior_preview_button1 = Options_preview[i];
                    break;
                case InputKey.Attack2:
                    Behavior_preview_button2 = Options_preview[i];
                    break;
                case InputKey.Attack3:
                    Behavior_preview_button3 = Options_preview[i];
                    break;
            }
        }
        if (Options_lastframe[InputKey.Attack1] != Behavior_preview_button1)
        {
            ChangeButtonPatternNewTest(Attack, Behavior_preview_button1 != null ? Behavior_preview_button1.SP_LEVEL : -1);
        }
        if (Options_lastframe[InputKey.Attack2] != Behavior_preview_button2)
        {
            ChangeButtonPatternNewTest(Fire1, Behavior_preview_button2 != null ? Behavior_preview_button2.SP_LEVEL : -1);
        }
        if (Options_lastframe[InputKey.Attack3] != Behavior_preview_button3)
        {
            ChangeButtonPatternNewTest(Fire2, Behavior_preview_button3 != null ? Behavior_preview_button3.SP_LEVEL : -1);
        }
        
        Options_lastframe[InputKey.Attack1] = Behavior_preview_button1;
        Options_lastframe[InputKey.Attack2] = Behavior_preview_button2;
        Options_lastframe[InputKey.Attack3] = Behavior_preview_button3;
    }

    void Update()
    {
        CheckIfPlayerIsInputting();
    }

    public static bool attack;
    public void AttackDown()
    {
        StartPressing(Attack);
        attack = true;
    }
    public void AttackUp()
    {
        StopPressing();
        attack = false;
    }
    
    public static bool fire1;
    public void Fire1Down()
    {
        fire1 = true;
        StartPressing(Fire1);
    }
    public void Fire1Up()
    {
        fire1 = false;
        StopPressing();
    }
    
    public static bool fire2;
    public void Fire2Down()
    {
        fire2 = true;
        StartPressing(Fire2);
    }
    public void Fire2Up()
    {
        fire2 = false;
        StopPressing();
    }

    public void DefendDown()
    {
        defendButtonHover = true;
        StartPressing(Defend);
    }
    public void DefendUp()
    {
        defendButtonHover = false;
        StopPressing();
    }

    public static bool acc;
    public void RushDown()
    {
        acc = true;
        StartPressing(Dash);
    }
    public void RushUp()
    {
        acc = false;
        StopPressing();
    }

    public void TurnOnButtons()
    {
        Attack.gameObject.SetActive(true);
        Fire1.gameObject.SetActive(true);
        Fire2.gameObject.SetActive(true);
        Dash.gameObject.SetActive(true);
        attack = false;
        fire1 = false;
        fire2 = false;
        acc = false;
        
        if (FightGlobalSetting._hasDefend)
        {
            Defend.gameObject.SetActive(true);
            defendButtonHover = false;
        }
    }

    public void TurnOffButtons()
    {
        Attack.gameObject.SetActive(false);
        Fire1.gameObject.SetActive(false);
        Fire2.gameObject.SetActive(false);
        Dash.gameObject.SetActive(false);
        
        attack = false;
        fire1 = false;
        fire2 = false;
        acc = false;
        
        if (FightGlobalSetting._hasDefend)
        {
            Defend.gameObject.SetActive(false);
            defendButtonHover = false;
        }
        
        Observing_Runner = null;
        if (_focusingButtonEffectsGroup != null)
        {
            _focusingButtonEffectsGroup.Close();
        }
    }

    static Vector2 buttonAnchorPosition;
    static Vector2 true_buttonAnchorPosition;
    static Vector3 buttonWorldPosition;
    static Vector3 ButtonEffectInFxCameraWorldSpace(Button button,float z_offset)//这个函数是以攻击钮与防御，闪避钮在右下角为前提写的。
    {
        //buttonAnchorPosition = button.GetComponent<RectTransform>().anchoredPosition;
        //true_buttonAnchorPosition = new Vector2(Screen.width + buttonAnchorPosition.x,buttonAnchorPosition.y);
        //buttonWorldPosition = s_fxCamera.ScreenToWorldPoint(true_buttonAnchorPosition);
        //buttonWorldPosition = new Vector3(buttonWorldPosition.x,buttonWorldPosition.y,s_fxCamera.transform.position.z + z_offset);
        //return buttonWorldPosition;
        
        buttonAnchorPosition = button.GetComponent<RectTransform>().transform.position;
        true_buttonAnchorPosition = new Vector2(buttonAnchorPosition.x, buttonAnchorPosition.y);
        buttonWorldPosition = target.fxCamera.ScreenToWorldPoint(true_buttonAnchorPosition);
        buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, target.fxCamera.transform.position.z + z_offset);
        return buttonWorldPosition;
    }
    
    Vector3 targetButtonPos;
    void ChangeButtonPatternNewTest(Button button,int sp_level)//按钮切换也可以在这里做文章
    {
        targetButtonPos = ButtonEffectInFxCameraWorldSpace(button,5);
        _focusingButtonEffectsGroup.Refreshforbutton(button,sp_level,targetButtonPos);
    }

    //void changeButtonPatternParticleVer(Button button,EX sp_level)//按钮切换也可以在这里做文章
    //{
    //    targetButtonPos = ButtonEffectInFxCameraWorldSpace(button);
        
    //    GameObject refresh_Explosion = _focusingButtonEffectsGroup.refreshPool.TryGetNextObject(button.transform.position, Quaternion.identity);
    //    refresh_Explosion.SetActive(true);
    //    refresh_Explosion.transform.position = targetButtonPos;
        
    //    GameObject EffectICon = null;
    //    switch (sp_level)
    //    {
    //        case EX.normal:
    //            EffectICon = _focusingButtonEffectsGroup.normalPool.TryGetNextObject(button.transform.position, Quaternion.identity);

    //            if (EffectICon != null)
    //                EffectICon.SetActive(true);
    //            else
    //            {
    //                Debug.Log("特效物体丢失");
    //                return;
    //            }
    //            EffectICon.transform.position = targetButtonPos;
    //            break;
    //        case EX.EX1:
    //            EffectICon = _focusingButtonEffectsGroup.EX1Pool.TryGetNextObject(button.transform.position, Quaternion.identity);

    //            if (EffectICon != null)
    //                EffectICon.SetActive(true);
    //            else
    //            {
    //                Debug.Log("特效物体丢失");
    //                return;
    //            }

    //            EffectICon.transform.position = targetButtonPos;
    //            break;
    //        case EX.EX2:
    //            EffectICon = _focusingButtonEffectsGroup.EX2Pool.TryGetNextObject(button.transform.position, Quaternion.identity);

    //            if (EffectICon != null)
    //                EffectICon.SetActive(true);
    //            else
    //            {
    //                Debug.Log("特效物体丢失");
    //                return;
    //            }
    //            EffectICon.transform.position = targetButtonPos;
    //            break;
    //        case EX.EX3:
    //            EffectICon = _focusingButtonEffectsGroup.EX3Pool.TryGetNextObject(button.transform.position, Quaternion.identity);

    //            if (EffectICon != null)
    //                EffectICon.SetActive(true);
    //            else
    //            {
    //                Debug.Log("特效物体丢失");
    //                return;
    //            }
    //            EffectICon.transform.position = targetButtonPos;
    //            break;
    //        case EX.NULL:
    //            break;
    //    }
    //}    
}

    //void Awake()
    //{
        //normal.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
        //EX1.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
        //EX2.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
        //EX3.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
        //pressedExplosion.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
        //refreshExplosion.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
        //pressingExplosion.GetComponent<RectTransform>().sizeDelta = Attack.GetComponent<RectTransform>().sizeDelta;
    //}

    //void changeButtonPattern(Button button,EX sp_level)//按钮切换也可以在这里做文章
    //{
        //GameObject refresh_Explosion = refreshExplosionPool.TryGetNextObject(button.transform.position, Quaternion.identity);
        //refresh_Explosion.SetActive(true);
        //refresh_Explosion.transform.SetParent(button.transform);
        //refresh_Explosion.transform.SetSiblingIndex(2);
        //refresh_Explosion.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,-1f);

        //if (SKillIcons[button] != null)
        //{
        //    SKillIcons[button].SetActive(false);
        //}

        //GameObject EffectICon = null;
        //switch (sp_level)
        //{
        //    case EX.normal:
        //        EffectICon = normalPool.TryGetNextObject(button.transform.position, Quaternion.identity);

        //        if (EffectICon != null)
        //            EffectICon.SetActive(true);
        //        else
        //        {
        //            Debug.Log("特效物体丢失");
        //            return;
        //        }

        //        EffectICon.transform.SetParent(button.transform);
        //        EffectICon.transform.SetSiblingIndex(1);
        //        EffectICon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        //        break;
        //    case EX.EX1:
        //        EffectICon = EX1Pool.TryGetNextObject(button.transform.position, Quaternion.identity);

        //        if (EffectICon != null)
        //            EffectICon.SetActive(true);
        //        else
        //        {
        //            Debug.Log("特效物体丢失");
        //            return;
        //        }

        //        EffectICon.transform.SetParent(button.transform);
        //        EffectICon.transform.SetSiblingIndex(1);
        //        EffectICon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //        break;
        //    case EX.EX2:
        //        EffectICon = EX2Pool.TryGetNextObject(button.transform.position, Quaternion.identity);

        //        if (EffectICon != null)
        //            EffectICon.SetActive(true);
        //        else
        //        {
        //            Debug.Log("特效物体丢失");
        //            return;
        //        }

        //        EffectICon.transform.SetParent(button.transform);
        //        EffectICon.transform.SetSiblingIndex(1);
        //        EffectICon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //        break;
        //    case EX.EX3:
        //        EffectICon = EX3Pool.TryGetNextObject(button.transform.position, Quaternion.identity);

        //        if (EffectICon != null)
        //            EffectICon.SetActive(true);
        //        else
        //        {
        //            Debug.Log("特效物体丢失");
        //            return;
        //        }

        //        EffectICon.transform.SetParent(button.transform);
        //        EffectICon.transform.SetSiblingIndex(1);
        //        EffectICon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //        break;
        //    case EX.NULL:
        //        break;
        //}

        //SKillIcons[button] = EffectICon;

        // 其实下面这些不会在运行了，因为现在所有的气力不足都是在上面的EX.Null case 里
        //if (hasPlentyGauge(sp_level))
        //{
        //    button.normalColor.a = 1f;
        //    button.pressedColor.a = 1f;
        //}else{
        //    button.pressedSprite = button.normalSprite;
        //    button.normalColor.a = 0.5f;
        //    button.pressedColor.a = 0.5f;
        //}
    //}

//底下这些成了我们开发以来最可笑的笑话之一，证明实在应该早睡否则脑子会混乱
//class inputAdvance
//{
//    public mobileInputsManager _mobileInputsManager;
//    public int inAdvanceFrames = 10;
//    int counter = 0;

//    public inputAdvance(mobileInputsManager _mobileInputsManager, int inAdvanceFrames)
//    {
//        this._mobileInputsManager = _mobileInputsManager;
//        this.inAdvanceFrames = inAdvanceFrames;
//        this.counter = 0;
//        this.nextInput = inputs_defined.Null;
//    }

//    public inputs_defined nextInput;

//    public void update()
//    {
//        if (nextInput != inputs_defined.Null)
//        {
//            counter++;
//            if (counter > inAdvanceFrames)
//            {
//                switch (nextInput)
//                {
//                    case inputs_defined.Attack:
//                        _mobileInputsManager.attackButtonUp();
//                        break;
//                    case inputs_defined.Fire1:
//                        _mobileInputsManager.Fire1ButtonUp();
//                        break;
//                    case inputs_defined.Fire2:
//                        _mobileInputsManager.Fire2ButtonUp();
//                        break;
//                }
//                nextInput = inputs_defined.Null;
//                counter = 0;
//            }else{
//                switch (nextInput)
//                {
//                    case inputs_defined.Attack:
//                        _mobileInputsManager.attackButtonDown();
//                        _mobileInputsManager.Fire1ButtonUp();
//                        _mobileInputsManager.Fire2ButtonUp();
//                        break;
//                    case inputs_defined.Fire1:
//                        _mobileInputsManager.Fire1ButtonDown();
//                        _mobileInputsManager.attackButtonUp();
//                        _mobileInputsManager.Fire2ButtonUp();
//                        break;
//                    case inputs_defined.Fire2:
//                        _mobileInputsManager.Fire2ButtonDown();
//                        _mobileInputsManager.attackButtonUp();
//                        _mobileInputsManager.Fire1ButtonUp();
//                        break;
//                }
//            }
//        }
//    }

//    public void clear()
//    {
//        nextInput = inputs_defined.Null;
//        counter = 0;
//    }

//    public void inputNextInAdvance(inputs_defined nextInput)
//    {
//        counter = 0;
//        this.nextInput = nextInput;
//    }
//}