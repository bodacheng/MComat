using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


public class mobileInputsManager : MonoBehaviour {

    //2019.3.26 折腾了整整两天的移动端按键粒子特效。留下的唯一一点不足是，没有针对防御状态，rush状态的有无来决定防御键是否显示，也没有针对未来可能出现的耗气式防御或rush状态来刷新两个键的显示状态。
    public Camera fxCamera;
    public Transform effectsParent;
    static IDictionary<Zokusei, zokuseiButtonEffectsGroup> zokuseiButtonEffects = new Dictionary<Zokusei, zokuseiButtonEffectsGroup>();
    static private zokuseiButtonEffectsGroup _focusingButtonEffectsGroup;
    
    public Button Attack;
    public Button Fire1;
    public Button Fire2;
    public Button Defend;
    public Button Dash;
    
    static Camera s_fxCamera;
    static Button s_Attack;
    static Button s_Fire1;
    static Button s_Fire2;
    static Button s_Defend;
    static Button s_Dash;
        
    private IDictionary<inputs_defined, int> lastSkillSPlevel = new Dictionary<inputs_defined, int>()
    {
        {inputs_defined.Attack,-1},
        {inputs_defined.Fire1,-1},
        {inputs_defined.Fire2,-1}
    };
    public static inputManager watchingInputManger;

    void Awake()
    {
        s_fxCamera = fxCamera;
        s_Attack = Attack;
        s_Fire1 = Fire1;
        s_Fire2 = Fire2;
        s_Defend = Defend;
        s_Dash = Dash;
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

    public void FocusCharInputs(inputManager focusingCharInputManger,Zokusei zokusei)
    {
        mobileInputsManager.watchingInputManger = focusingCharInputManger;
        if (mobileInputsManager.watchingInputManger != null)
        {
            SwitchZokuseiButtons(zokusei);
        }else{
            TurnOffButtons();
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
    public static void Skillbuttonexplosion(inputs_defined inputs_Defined,int eX)
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
            case inputs_defined.Attack:
                targetexplode.transform.position = ButtonEffectInFxCameraWorldSpace(s_Attack,3);
                break;
            case inputs_defined.Fire1:
                targetexplode.transform.position = ButtonEffectInFxCameraWorldSpace(s_Fire1,3);
                break;
            case inputs_defined.Fire2:
                targetexplode.transform.position = ButtonEffectInFxCameraWorldSpace(s_Fire2,3);
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
    private void StartPressing(Button targetBUtton)
    {
        targetButtonPos = ButtonEffectInFxCameraWorldSpace(targetBUtton,7);
        if (_focusingButtonEffectsGroup != null)
        {
            _focusingButtonEffectsGroup.pressingExplosion.transform.position = targetButtonPos;
            _focusingButtonEffectsGroup.pressingExplosion.Play();
        }
    }

    private void StopPressing()
    {
        if (_focusingButtonEffectsGroup != null)
            _focusingButtonEffectsGroup.pressingExplosion.Stop();
    }

    public void AttackButtonDown()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Attack.input_state = true;
        }
        StartPressing(Attack);
    }
    public void AttackButtonUp()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Attack.input_state = false;
        }
        StopPressing();
    }

    public void Fire1ButtonDown()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Fire1.input_state = true;
        }
        StartPressing(Fire1);
    }
    public void Fire1ButtonUp()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Fire1.input_state = false;
        }
        StopPressing();
    }

    public void Fire2ButtonDown()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Fire2.input_state = true;
        }
        StartPressing(Fire2);
    }
    public void Fire2ButtonUp()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Fire2.input_state = false;
        }
        StopPressing();
    }

    public void DefendDown()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Defend.input_state = true;
            watchingInputManger.Defend_Cancel.input_state = false;
        }
        StartPressing(Defend);
    }

    public void DefendUp()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Defend_Cancel.input_state = true;
            watchingInputManger.Defend.input_state = false;
        }
        StopPressing();
    }

    public void RushDown()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Dash.input_state = true;
        }
        StartPressing(Dash);
    }

    public void RushUp()
    {
        if (watchingInputManger != null)
        {
            watchingInputManger.Dash.input_state = false;
        }
        StopPressing();
    }

    public void TurnOnButtons()
    {
        Attack.gameObject.SetActive(true);
        Fire1.gameObject.SetActive(true);
        Fire2.gameObject.SetActive(true);

        if (watchingInputManger.inputStateDic.ContainsKey(inputs_defined.Defend))
            Defend.gameObject.SetActive(true);
        else
            Defend.gameObject.SetActive(false);

        if (watchingInputManger.inputStateDic.ContainsKey(inputs_defined.Dash))
            Dash.gameObject.SetActive(true);
        else
            Dash.gameObject.SetActive(false);

        lastSkillSPlevel = new Dictionary<inputs_defined, int>()
        {
            {inputs_defined.Attack,-1},
            {inputs_defined.Fire1,-1},
            {inputs_defined.Fire2,-1}
        };
    }

    public void TurnOffButtons()
    {
        Attack.gameObject.SetActive(false);
        Fire1.gameObject.SetActive(false);
        Fire2.gameObject.SetActive(false);
        Defend.gameObject.SetActive(false);
        Dash.gameObject.SetActive(false);

        if (watchingInputManger != null)
        {
            watchingInputManger.Attack.input_state = false;
            watchingInputManger.Fire1.input_state = false;
            watchingInputManger.Fire2.input_state = false;
            watchingInputManger.Defend.input_state = false;
            watchingInputManger.Defend_Cancel.input_state = false;
            watchingInputManger.Dash.input_state = false;

            watchingInputManger = null;
        }
        if (_focusingButtonEffectsGroup != null)
            _focusingButtonEffectsGroup.Close();
    }
        
    public void RefreshButtonPattern()
    {
        if (watchingInputManger == null)
            return;
        //那么这里面就完全不包括对防御和机动键的处理了。。。去看nextSkillSPlevel这个东西里面也是只有三个攻击键。
        //如此一来我们是打算把防御和机动键给做成完全固定的。
        foreach (KeyValuePair<inputs_defined, int> _pair in watchingInputManger.nextSkillSPlevel)
        {
            switch (_pair.Key)
            {
                case inputs_defined.Attack:
                    if (_pair.Value != lastSkillSPlevel[_pair.Key])
                        ChangeButtonPatternNewTest(Attack, _pair.Value);
                    break;
                case inputs_defined.Fire1:
                    if (_pair.Value != lastSkillSPlevel[_pair.Key])
                        ChangeButtonPatternNewTest(Fire1, _pair.Value);
                    break;
                case inputs_defined.Fire2:
                    if (_pair.Value != lastSkillSPlevel[_pair.Key])
                    {
                        ChangeButtonPatternNewTest(Fire2, _pair.Value);
                    }
                    break;
            }

            if (_pair.Value == -1)
            {
                watchingInputManger.inputStateDic[_pair.Key].input_state = false;
            }
            lastSkillSPlevel[_pair.Key] = _pair.Value;
        }        
    }
    
    static Vector2 buttonAnchorPosition;
    static Vector2 true_buttonAnchorPosition;
    static Vector3 buttonWorldPosition;
    private static Vector3 ButtonEffectInFxCameraWorldSpace(Button button,float z_offset)//这个函数是以攻击钮与防御，闪避钮在右下角为前提写的。
    {
        //buttonAnchorPosition = button.GetComponent<RectTransform>().anchoredPosition;
        //true_buttonAnchorPosition = new Vector2(Screen.width + buttonAnchorPosition.x,buttonAnchorPosition.y);
        //buttonWorldPosition = s_fxCamera.ScreenToWorldPoint(true_buttonAnchorPosition);
        //buttonWorldPosition = new Vector3(buttonWorldPosition.x,buttonWorldPosition.y,s_fxCamera.transform.position.z + z_offset);
        //return buttonWorldPosition;
        
        buttonAnchorPosition = button.GetComponent<RectTransform>().transform.position;
        true_buttonAnchorPosition = new Vector2(buttonAnchorPosition.x, buttonAnchorPosition.y);
        buttonWorldPosition = s_fxCamera.ScreenToWorldPoint(true_buttonAnchorPosition);
        buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, s_fxCamera.transform.position.z + z_offset);
        return buttonWorldPosition;
    }
    
    Vector3 targetButtonPos;
    private void ChangeButtonPatternNewTest(Button button,int sp_level)//按钮切换也可以在这里做文章
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

