using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum inputs_defined:int
{
    Null = -1,
    Attack = 0,
    Fire1 = 1,
    Fire2 =2,
    Dash = 5,
    Defend = 3,
    Defend_Cancel = 4,
}

//这个模块的职责现在多了一个，就是像mobile按钮来汇报那些按钮应该开始闪灯。。
public class inputManager {

    private AIStateRunner myfocusingRunner;
    private mobileInputsManager _mobileInputsManager;
    private bool PlayerInputting = false;
    private List<inputs_defined> WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge = new List<inputs_defined>();
    public IDictionary<inputs_defined,input> inputStateDic;
    public buttonDownTypeInput Attack = new buttonDownTypeInput(inputs_defined.Attack, "Attack");
    public buttonDownTypeInput Fire1 = new buttonDownTypeInput(inputs_defined.Fire1, "Fire1");
    public buttonDownTypeInput Fire2 = new buttonDownTypeInput(inputs_defined.Fire2, "Fire2");
    public buttonDownTypeInput Dash = new buttonDownTypeInput(inputs_defined.Dash, "Rush");
    public buttonDownTypeInput Defend = new buttonDownTypeInput(inputs_defined.Defend, "Defend");
    public buttonOffTypeInput Defend_Cancel = new buttonOffTypeInput(inputs_defined.Defend_Cancel, "Defend");
    public IDictionary<inputs_defined, EX> nextSkillSPlevel = new Dictionary<inputs_defined, EX>();

    public bool ifPlayerIsInputting()
    {
        return PlayerInputting;
    }

    public void setMobileInputsManager(mobileInputsManager _mobileInputsManager)
    {
        this._mobileInputsManager = _mobileInputsManager;
    }

    public void buttonRefreshForCasualTransition(List<State_Rate_Set> avaliable_casual_Transitions,BO_Health _BO_Health)
    {
        WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Clear();
        foreach (State_Rate_Set transition_key_value in avaliable_casual_Transitions)
        {
            if (_BO_Health.hasPlentyGauge(transition_key_value.SPLevel))
            {
                if (transition_key_value.casualToNextInputDepend && transition_key_value.enterInput != inputs_defined.Null)
                {
                    WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Add(transition_key_value.enterInput);
                    refreshSPLevelButtonsInfo(transition_key_value.enterInput, transition_key_value.SPLevel);
                }
            }
        }

        //首发技能中如果三个键位里有发动不了的，那键位也应该是灰色或半透明来表示无法发动。
        if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(inputs_defined.Attack))
        {
            refreshSPLevelButtonsInfo(inputs_defined.Attack, EX.NULL);
        }
        if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(inputs_defined.Fire1))
        {
            refreshSPLevelButtonsInfo(inputs_defined.Fire1, EX.NULL);
        }
        if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(inputs_defined.Fire2))
        {
            refreshSPLevelButtonsInfo(inputs_defined.Fire2, EX.NULL);
        }
    }

    public void buttonRefreshFromStart(List<AI_State> States_for_AbsoluteInput,BO_Health _BO_Health)
    {
        WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Clear();
        foreach (AI_State _AS in States_for_AbsoluteInput)
        {
            if (_BO_Health.hasPlentyGauge(_AS.splevel) && _AS.playerModeInputDepend)
            {
                if (_AS.enterInput != inputs_defined.Null)
                {
                    WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Add(_AS.enterInput);
                    refreshSPLevelButtonsInfo(_AS.enterInput, _AS.splevel);
                }
            }
        }
        //首发技能中如果三个键位里有发动不了的，那键位也应该是灰色或半透明来表示无法发动。
        if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(inputs_defined.Attack))
        {
            refreshSPLevelButtonsInfo(inputs_defined.Attack, EX.NULL);
        }
        if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(inputs_defined.Fire1))
        {
            refreshSPLevelButtonsInfo(inputs_defined.Fire1, EX.NULL);
        }
        if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(inputs_defined.Fire2))
        {
            refreshSPLevelButtonsInfo(inputs_defined.Fire2, EX.NULL);
        }
    }

    public void INI(bool hasD,bool hasR, AIStateRunner myfocusingRunner) 
    {
        this.myfocusingRunner = myfocusingRunner;
        inputStateDic = new Dictionary<inputs_defined, input>();
        nextSkillSPlevel = new Dictionary<inputs_defined, EX>();

        // 我们下面这个部分是和refreshSPLevelButtonsInfo函数内的处理进行呼应
        // 注意看那里面如果ContainsKey为非的话就不做任何处理，
        // 因为我们的这套按钮刷新机制只运用于Attack，Fire1，Fire2这三个攻击键。
        // 而这里就是提前打好招呼，我们的nextSkillSPlevel只有这三个key
        nextSkillSPlevel.Add(inputs_defined.Attack, EX.NULL);
        nextSkillSPlevel.Add(inputs_defined.Fire1, EX.NULL);
        nextSkillSPlevel.Add(inputs_defined.Fire2, EX.NULL);

        inputStateDic.Add(Attack.input_num, Attack);
        inputStateDic.Add(Fire1.input_num, Fire1);
        inputStateDic.Add(Fire2.input_num, Fire2);

        if (hasR)
            inputStateDic.Add(Dash.input_num, Dash);
        if (hasD)
        {
            inputStateDic.Add(Defend.input_num, Defend);
            inputStateDic.Add(Defend_Cancel.input_num, Defend_Cancel);
        }
    }
	
    public void refreshSPLevelButtonsInfo(inputs_defined _input,EX _spLevel)
    {
        if (nextSkillSPlevel.ContainsKey(_input))
        {
            nextSkillSPlevel[_input] = _spLevel;
        }
    }

    public void SkillButtonPressedExplode(inputs_defined inputs_Defined,EX eX)
    {
        if (this._mobileInputsManager != null)
            this._mobileInputsManager.skillbuttonexplosion(inputs_Defined,eX);
    }

    // Update is called once per frame

    float h,v;
    public void Update()
    {
        if (AIStateRunner._focusing == myfocusingRunner)
        {
            PlayerInputting = false;
            if (inputStateDic != null)
            {
                foreach (KeyValuePair<inputs_defined, input> _set in inputStateDic)
                {
                    _set.Value.updateInputState();
                    if (_set.Value.button_down)
                        PlayerInputting = true;
                }

                h = 0f;
                v = 0f;
                if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
                {
                    h = Input.GetAxis("Horizontal");
                    v = Input.GetAxis("Vertical");
                    //h = ETCInput.GetAxis("Horizontal");
                    //v = ETCInput.GetAxis("Vertical");
                }
                else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    h = ETCInput.GetAxis("Horizontal");
                    v = ETCInput.GetAxis("Vertical");
                }
                if (h > 0f || h < 0 || v > 0f || v < 0f)
                {
                    PlayerInputting = true;
                }
            }
        }
    }
}
