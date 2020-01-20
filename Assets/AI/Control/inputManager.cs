using System.Collections.Generic;
using UnityEngine;
using Soul;

namespace Inputs
{
    public class Input
    {
        public Inputs_defined input_num;
        public bool input_state;
    
        public Input()
        {
        }
    
        public Input(Inputs_defined input_num)
        {
            this.input_num = input_num;
            input_state = false;
        }
    
        public virtual bool CheckInputState()
        {
            return input_state;
        }
    }
    
    //这个模块的职责现在多了一个，就是像mobile按钮来汇报那些按钮应该开始闪灯。。
    public class InputManager {
    
        public IDictionary<Inputs_defined,Input> inputStateDic;
        public Input Attack = new Input(Inputs_defined.Attack);
        public Input Fire1 = new Input(Inputs_defined.Fire1);
        public Input Fire2 = new Input(Inputs_defined.Fire2);
        public Input Dash = new Input(Inputs_defined.Dash);
        public Input Defend = new Input(Inputs_defined.Defend);
        public Input Defend_Cancel = new Input(Inputs_defined.Defend_Cancel);
        public IDictionary<Inputs_defined, int> nextSkillSPlevel = new Dictionary<Inputs_defined, int>();
        public bool PlayerInputting { get; set;}
        
        AIStateRunner myfocusingRunner;
        List<Inputs_defined> WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge = new List<Inputs_defined>();
    
        public void ButtonRefreshForCasualTransition(List<Behavior_Rate_Set> avaliable_casual_Transitions,FightAttriCalReference _BO_Health)
        {
            WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Clear();
            foreach (Behavior_Rate_Set transition_key_value in avaliable_casual_Transitions)
            {
                if (_BO_Health.HasPlentyGauge(transition_key_value.SPLevel))
                {
                    if (transition_key_value.enterInput != Inputs_defined.Null)
                    {
                        WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Add(transition_key_value.enterInput);
                        RefreshSPLevelButtonsInfo(transition_key_value.enterInput, transition_key_value.SPLevel);
                    }
                }
            }
    
            // 首发技能中如果三个键位里有发动不了的，那键位也应该是灰色或半透明来表示无法发动。
            if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(Inputs_defined.Attack))
            {
                RefreshSPLevelButtonsInfo(Inputs_defined.Attack, -1);
            }
            if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(Inputs_defined.Fire1))
            {
                RefreshSPLevelButtonsInfo(Inputs_defined.Fire1,-1);
            }
            if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(Inputs_defined.Fire2))
            {
                RefreshSPLevelButtonsInfo(Inputs_defined.Fire2, -1);
            }
        }
    
        public void ButtonRefreshFromStart(List<Behavior> States_for_AbsoluteInput,FightAttriCalReference _BO_Health)
        {
            WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Clear();
            foreach (Behavior _AS in States_for_AbsoluteInput)
            {
                if (_BO_Health.HasPlentyGauge(_AS.splevel))
                {
                    if (_AS.enterInput != Inputs_defined.Null)
                    {
                        WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Add(_AS.enterInput);
                        RefreshSPLevelButtonsInfo(_AS.enterInput, _AS.splevel);
                    }
                }
            }
            //首发技能中如果三个键位里有发动不了的，那键位也应该是灰色或半透明来表示无法发动。
            if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(Inputs_defined.Attack))
            {
                RefreshSPLevelButtonsInfo(Inputs_defined.Attack, -1);
            }
            if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(Inputs_defined.Fire1))
            {
                RefreshSPLevelButtonsInfo(Inputs_defined.Fire1, -1);
            }
            if (!WeUseThisToSeeIfNextWazaForInputHasPlentyOfGrauge.Contains(Inputs_defined.Fire2))
            {
                RefreshSPLevelButtonsInfo(Inputs_defined.Fire2, -1);
            }
        }
    
        public void INI(bool hasD,bool hasR, AIStateRunner myfocusingRunner) 
        {
            this.myfocusingRunner = myfocusingRunner;
            inputStateDic = new Dictionary<Inputs_defined, Input>();
            nextSkillSPlevel = new Dictionary<Inputs_defined, int>
            {
                // 我们下面这个部分是和refreshSPLevelButtonsInfo函数内的处理进行呼应
                // 注意看那里面如果ContainsKey为非的话就不做任何处理，
                // 因为我们的这套按钮刷新机制只运用于Attack，Fire1，Fire2这三个攻击键。
                // 而这里就是提前打好招呼，我们的nextSkillSPlevel只有这三个key
                { Inputs_defined.Attack, -1 },
                { Inputs_defined.Fire1, -1 },
                { Inputs_defined.Fire2, -1 }
            };
    
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
        
        public void RefreshSPLevelButtonsInfo(Inputs_defined _input,int _spLevel)
        {
            if (nextSkillSPlevel.ContainsKey(_input))
            {
                nextSkillSPlevel[_input] = _spLevel;
            }
        }
    
        float h,v;
        public void CheckIfPlayerIsInputting() // 如果不是对准角色，不会跑。
        {
            PlayerInputting = false;
            foreach (KeyValuePair<Inputs_defined, Input> _set in inputStateDic)
            {
                if (_set.Key == Inputs_defined.Defend_Cancel)
                    continue;// 一个特例。不这么处理的话会造成Defend_Cancel一直判断为true，导致角色不动
                PlayerInputting |= _set.Value.input_state;
                if (PlayerInputting)
                    return;
            }
            h = 0f;
            v = 0f;
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor 
            || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                h = UnityEngine.Input.GetAxis("Horizontal");
                v = UnityEngine.Input.GetAxis("Vertical");
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                h = ETCInput.GetAxis("Horizontal");
                v = ETCInput.GetAxis("Vertical");
            }
            PlayerInputting |= (h > 0f || h < 0 || v > 0f || v < 0f);
        }
    }    
}

public enum Inputs_defined
{
    Null = -1,
    Attack = 0,
    Fire1 = 1,
    Fire2 = 2,
    Dash = 5,
    Defend = 3,
    Defend_Cancel = 4,
    Any = 6
}