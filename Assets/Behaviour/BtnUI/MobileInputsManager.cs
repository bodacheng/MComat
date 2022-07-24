using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Soul;
using Skill;
using FightScene;

public enum InputKey
{
    Null = -1,
    Attack1 = 0,
    Attack2 = 1,
    Attack3 = 2,
    Acc = 5,
    Defend = 3,
    Defend_Cancel = 4,
    Any = 6
}

public class MobileInputsManager : MonoBehaviour {

    [SerializeField] Button Attack;
    [SerializeField] Button Fire1;
    [SerializeField] Button Fire2;
    [SerializeField] Button Defend;
    [SerializeField] Button Dash;
    [SerializeField] Transform effectsParent;
    
    readonly IDictionary<Element, ElementEffectsGroup> ElementEffects = new Dictionary<Element, ElementEffectsGroup>();
    Element _focusing;
    
    public bool inputting = false;
    
    private BehaviorRunner watch;
    
    public void FocusUnit(Data_Center center)
    {
        if (watch != null)
        {
            watch.InputsManager = null;
        }
        if (center != null)
        {
            center._MyBehaviorRunner.InputsManager = this;
            watch = center._MyBehaviorRunner;
            SwitchElementEffects(center.element);
            SuddenRefreshButtons(watch);
        }else{
            TurnOffButtons();
        }
    }
    
    public void Clear()
    {
        ElementEffects.Clear();
        Destroy(gameObject);
    }
    
    // 切换输入按键表现层（红黄蓝绿）.这个函数使用的前提是所有用的上的控制器组都已经注册并初始化
    void SwitchElementEffects(Element element)
    {
        if (ElementEffects.ContainsKey(_focusing))
        {
            ElementEffects[_focusing].Close();
        }
        
        if (ElementEffects.ContainsKey(element))
        {
            _focusing = element;
            
            ElementEffects[element].Open(
                PosCal.GetWorldPos(NetFightScene.target.fxCamera, Defend.GetComponent<RectTransform>(), 5), 
                PosCal.GetWorldPos(NetFightScene.target.fxCamera, Dash.GetComponent<RectTransform>(), 5)
            );
        }else{
            Debug.Log("见鬼了。检查手机控制器渲染模块加载顺序");
        }
    }
    
    public async UniTask ElementRegister(Element element, UnitInfo unitInfo)
    {
        if (!ElementEffects.ContainsKey(element))
        {
            var elementEffect = new ElementEffectsGroup();
            await elementEffect.INICommon(effectsParent, element, Attack, Fire1, Fire2);
            ElementEffects.Add(element, elementEffect);
        }
        ElementEffects[element].INIBtn(Attack, Fire1, Fire2, unitInfo);
        ElementEffects[element].Close();
    }
    
    ParticleSystem _targetExplode;
    public void SkillExplosion(InputKey key, int spLevel)
    {
        if (!ElementEffects.ContainsKey(_focusing))
        {
            Debug.Log("读取流程产生错误："+_focusing);
            return;
        }
        
        switch(spLevel)
        {
            case 0:
                _targetExplode = ElementEffects[_focusing].triggerExplosion0;
            break;
            case 1:
                _targetExplode = ElementEffects[_focusing].triggerExplosion1;
            break;
            case 2:
                _targetExplode = ElementEffects[_focusing].triggerExplosion2;
            break;
            case 3:
                _targetExplode = ElementEffects[_focusing].triggerExplosion3;
            break;
            default:
                return;
        }
    
        switch (key)
        {
            case InputKey.Attack1:
                _targetExplode.transform.position = PosCal.GetWorldPos(NetFightScene.target.fxCamera, Attack.GetComponent<RectTransform>(), 3);
                break;
            case InputKey.Attack2:
                _targetExplode.transform.position = PosCal.GetWorldPos(NetFightScene.target.fxCamera, Fire1.GetComponent<RectTransform>(), 3);
                break;
            case InputKey.Attack3:
                _targetExplode.transform.position = PosCal.GetWorldPos(NetFightScene.target.fxCamera, Fire2.GetComponent<RectTransform>(), 3);
                break;
        }
        _targetExplode.Play();
    }

    //下面这些是说，每当有技能爆炸特效也就代表技能表更新，那么需要整体刷新特效 刷新特效都是三个键位一起出现，省的给人种误导好像我技能没变
    public void BtnRefreshEffects()
    {
        foreach (var keyValue in ElementEffects[_focusing].BtnRefreshEffects)
        {
            keyValue.Value.transform.position = PosCal.GetWorldPos(NetFightScene.target.fxCamera, keyValue.Key.GetComponent<RectTransform>(),4);
            keyValue.Value.Play(true);
        }
    }
    
    // 等把机动和防御分离后，要做这样的事情：
    // 根据玩家的技能列表来决定防御，机动，三攻击键分别存在与否。
    // 然后，refresh button是要看情况的，攻击键要么是变成空按钮，要么应该是就没有按钮。。。？
    // 而防御与机动则是确定一直显示。
    void StartPressing(Button targetBtn)
    {
        targetPos = PosCal.GetWorldPos(NetFightScene.target.fxCamera, targetBtn.GetComponent<RectTransform>(), 7);
        if (ElementEffects.ContainsKey(_focusing))
        {
            ElementEffects[_focusing].pressingExplosion.transform.position = targetPos;
            ElementEffects[_focusing].pressingExplosion.Play();
        }
    }

    void StopPressing()
    {
        if (ElementEffects.ContainsKey(_focusing))
        {
            ElementEffects[_focusing].pressingExplosion.Stop();
        }
    }
    
    // 如果不是对准角色，不会跑。
    static float h;
    static float v;
    void CheckIfPlayerIsInputting()
    {
        inputting = defendButtonHover || attack || fire1 || fire2;
        if (inputting)
        {
            return;
        }
        h = UnityEngine.Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("joystick");
        v = UnityEngine.Input.GetAxis("Vertical") + UltimateJoystick.GetVerticalAxis("joystick");
        inputting = (h > 0f || h < 0 || v > 0f || v < 0f);
    }
    
    readonly Dictionary<InputKey, SkillEntity> Options_lastframe = new Dictionary<InputKey, SkillEntity>()
    {
        {InputKey.Attack1,null},
        {InputKey.Attack2,null},
        {InputKey.Attack3,null}
    };
    
    // 动态按钮系统是基于状态流动
    SkillEntity Behavior_preview_button1, Behavior_preview_button2, Behavior_preview_button3;
    public void ButtonsFeatureLoad(List<SkillEntity> Options_preview)
    {
        Behavior_preview_button1 = null; 
        Behavior_preview_button2 = null;
        Behavior_preview_button3 = null;
        
        for (var i = 0; i < Options_preview.Count; i++)
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
            RefreshPattern(Attack, Behavior_preview_button1 != null ? Behavior_preview_button1.SkillID : string.Empty);
        }
        if (Options_lastframe[InputKey.Attack2] != Behavior_preview_button2)
        {
            RefreshPattern(Fire1, Behavior_preview_button2 != null ? Behavior_preview_button2.SkillID : string.Empty);
        }
        if (Options_lastframe[InputKey.Attack3] != Behavior_preview_button3)
        {
            RefreshPattern(Fire2, Behavior_preview_button3 != null ? Behavior_preview_button3.SkillID : string.Empty);
        }
        
        Options_lastframe[InputKey.Attack1] = Behavior_preview_button1;
        Options_lastframe[InputKey.Attack2] = Behavior_preview_button2;
        Options_lastframe[InputKey.Attack3] = Behavior_preview_button3;
    }
    
    // 直接根据角色状态刷新按钮。因为动态按钮系统是基于状态流动
    void SuddenRefreshButtons(BehaviorRunner behaviorRunner)
    {
        Options_lastframe[InputKey.Attack1] = null;
        Options_lastframe[InputKey.Attack2] = null;
        Options_lastframe[InputKey.Attack3] = null;
        ButtonsFeatureLoad(behaviorRunner.GetNextSkills());
    }
    
    public static bool defendButtonHover;
    public bool DefendExitTrigger()
    {
        return !defendButtonHover;
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

    void TurnOffButtons()
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

        if (ElementEffects.ContainsKey(_focusing))
        {
            ElementEffects[_focusing].Close();
        }
    }
    
    void Update()
    {
        CheckIfPlayerIsInputting();
    }
    
    Vector3 targetPos;
    void RefreshPattern(Button button, string skillId)//按钮切换也可以在这里做文章
    {
        targetPos = PosCal.GetWorldPos(NetFightScene.target.fxCamera, button.GetComponent<RectTransform>(), 5);
        if (ElementEffects.ContainsKey(_focusing))
        {
            ElementEffects[_focusing].RefreshBtn(button, skillId, targetPos);
        }
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