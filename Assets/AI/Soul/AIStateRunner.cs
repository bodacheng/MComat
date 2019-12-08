using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Inputs;

namespace Soul
{
    public partial class AIStateRunner : MonoBehaviour
    {
        bool playerMode;

        #region 初始化相关
        public string characterType;
        public List<State_Transition_Set> State_Transition_Set_List;//
        NineAndTwo readingNineAndTwo;
        States_Incubator _States_Incubator;
        #endregion

        #region 移动端输入器相关
        public InputManager _inputManager = new InputManager();//_inputManager.refreshSPLevelButtonsInfo在本脚本中的两处出现，一个是在状态运行引擎函数的“next state”判断处，
        #endregion

        #region 辅助模块：查看当前EX槽
        public FightAttriCalReference _BO_Health;
        #endregion

        #region 辅助模块：技能链接时机判断器
        public SkillCancelFlag _SkillCancelFlag;
        #endregion

        #region 运行时活参数
        Empty_State empty_State = new Empty_State();
        AI_State now_state;
        AI_State lastState;
        AI_State try_state;
        string current_state_num;
        IDictionary<string, AI_State> state_Dictionary = new Dictionary<string, AI_State>();
        IDictionary<string, State_Transition_Set> state_Transition_Dictionary;//大状态机真正的运行依据，其他内容都是为了生成它而存在的中间变量
        List<State_Rate_Set> avaliable_casual_Transitions = new List<State_Rate_Set>();
        List<State_Rate_Set> casual_TransitionsPriority1 = new List<State_Rate_Set>();
        List<State_Rate_Set> casual_TransitionsPriority2 = new List<State_Rate_Set>();
        List<State_Rate_Set> casual_TransitionsPriority3 = new List<State_Rate_Set>();
        List<string> avaliable_forced_Transitions = new List<string>();
        State_Transition_Set CurrentStateTransitionSet;
        List<AI_State> AINext = new List<AI_State>();
        List<AI_State> AINextPriority1 = new List<AI_State>();
        List<AI_State> AINextPriority2 = new List<AI_State>();
        List<AI_State> AINextPriority3 = new List<AI_State>();
        List<AI_State> States_for_AbsoluteInput = new List<AI_State>();//该列表在处理的时候不包括待机状态
        AI_State commandWaitingState;//所谓的待机状态。和首发状态分开处理，因为有实际作用的技能肯定要优先释放，没有的话才进行一些移动等等。
        #endregion

        public void SetPlayerMode(bool result)
        {
            playerMode = result;
            this._inputManager.PlayerInputting = false;
        }
        
        public bool IfRunning()
        {
            return empty_State != this.now_state;
        }

        Inputs.Input _input;
        public bool CheckInput(Inputs_defined num)
        {
            _inputManager.inputStateDic.TryGetValue(num, out _input);
            if (_input != null)
                return _input.CheckInputState();
            Debug.Log("卧槽什么情况：" + num);
            return false;
        }

        public AI_State GetNowState()
        {
            return now_state;
        }
        public AI_State GetLastState()
        {
            return lastState;
        }
        public AI_State GetTryState()
        {
            return try_state;
        }

        public NineAndTwo GetReadingNineAndTwo()
        {
            return readingNineAndTwo;
        }

        public string GetCurrentStateNum()
        {
            return current_state_num;
        }

        public InputManager GetInputManager()
        {
            return _inputManager;
        }

        void Awake()
        {
            now_state = empty_State;   
        }

        void FixedUpdate()
        {
            if (IfRunning())
            {
                if (MobileInputsManager.target.watchingInputManger == _inputManager)
                    _inputManager.CheckIfPlayerIsInputting();
                StateTransitionEngine_new(state_Transition_Dictionary);
                if (now_state != null)
                {
                    if (playerMode || (!playerMode && _inputManager.PlayerInputting))
                    {
                        now_state._c_State_FixedUpdate1();
                        now_state._c_State_FixedUpdate2();
                    }
                    else
                    {
                        now_state._State_FixedUpdate1();
                        now_state._State_FixedUpdate2();
                    }
                }
            }
        }

        public void ChangeState(string num)
        {
            current_state_num = num;
            state_Dictionary.TryGetValue(current_state_num, out try_state);
            if (now_state != null)
                now_state.AI_State_exit();

            //if(now_state == try_state)
            //{
            //    Debug.Log(current_state_num +"可能出现进入与退出条件的逻辑不相反问题");
            //}
            //注意看changeState环节，上一个状态的exit和下一个状态的enter是同一个帧执行的。
            //从这里我们曾经发现了动画播放模块一个重要问题，就是在特定情况下，
            //比如defend状态的exit里有PlayLayerAnim(_animator_layer_index, null)，防御后接攻击，
            //那么先执行PlayLayerAnim(_animator_layer_index, null) ，同一帧执行PlayLayerAnim(_animator_layer_index, clip_name);
            //就会产生bug：动画器无法正常播放攻击动画，角色会立在那里。这是我们动画模块的一个性质。
            // 我们把defend状态exit中的PlayLayerAnim(_animator_layer_index, null)删除了后就不再产生对应bug。
            // 关于动画模块的“技能动作清空”，我们是把它放在了move状态的开头，从而避免了清空函数与触发动画函数在同一帧执行。
            lastState = now_state;
            now_state = try_state;

            if (now_state == null)
            {
                Debug.Log("尝试读取未定义的状态" + current_state_num);
                return;
            }
            if (playerMode || _inputManager.PlayerInputting)
                now_state.C_State_enter();
            else
                now_state.AI_State_enter();
        }
        
        public void ChangeState(string num, V_Damage newvalue)
        {
            current_state_num = num;
            state_Dictionary.TryGetValue(current_state_num, out try_state);
            if (now_state != null)
                now_state.AI_State_exit();

            lastState = now_state;
            now_state = try_state;

            if (now_state == null)
            {
                Debug.Log("尝试读取未定义的状态" + current_state_num);
                return;
            }
            if (playerMode || _inputManager.PlayerInputting)
                now_state.C_State_enter(newvalue);
            else
                now_state.AI_State_enter(newvalue);
        }

        public void StartToGo()
        {
            string[] startOffState = { "Move_normal", "Move_slow", "Move_fast", "Test_Move" };
            for (int i = 0; i < startOffState.Length; i++)
            {
                state_Transition_Dictionary.TryGetValue(startOffState[i], out State_Transition_Set _State_Transition);
                if (_State_Transition != null)
                {
                    this.ChangeState(startOffState[i]);
                    break;
                }
            }
        }

        public void FormFightingSetsByNineAndTwo(string type, NineAndTwo nineAndTwo, int AI_level)
        {
            if (nineAndTwo == null)
            {
                Debug.Log("九宫格为空，返回");
                return;
            }
            readingNineAndTwo = nineAndTwo;
            readingNineAndTwo.SortNineAndTwo();
            //这上下两个函数之间存在一个chuanEndCasualT0的问题，从而必须一前一后紧密连接，下次review时候可以看看代码能不能整更利索一些。
            state_Transition_Dictionary = this.readingNineAndTwo.GenerateBeheviourSets(100);
            State_Transition_Set_List = this.readingNineAndTwo.ReturnSTSlist();//这一行于本游戏本身已经无用，但该列表牵扯到开发环境下角色技能详细的显示，以及框架本身保存xml战斗脚本的功能。

            States_for_AbsoluteInput.Clear();
            bool hasD, hasR;
            hasD = readingNineAndTwo.GetDConfig() != null;
            hasR = readingNineAndTwo.GetRConfig() != null;

            if (_inputManager == null)
                _inputManager = new InputManager();
            _inputManager.INI(hasD, hasR, this);

            _States_Incubator = new States_Incubator(type, empty_State,this.state_Transition_Dictionary);
            List<AI_Num_With_State> Num_State_List = _States_Incubator.Num_State_List;// 理解整个系统的关键
            state_Dictionary = new Dictionary<string, AI_State>();

            foreach (AI_Num_With_State s in Num_State_List)
            {
                if (state_Transition_Dictionary.ContainsKey(s.num))
                {
                    s.state.StateKey = this.state_Transition_Dictionary[s.num].StateKey;
                    s.state.splevel = this.state_Transition_Dictionary[s.num].SPLevel;
                    s.state.enterInput = this.state_Transition_Dictionary[s.num].enterInput;
                    s.state.exitInput = this.state_Transition_Dictionary[s.num].exitInput;
                    s.state.behaviorEnterRanges = this.state_Transition_Dictionary[s.num].ai_trigger_ranges;
                    state_Dictionary.Add(new KeyValuePair<string, AI_State>(s.num, s.state));
                }
                else
                {
                    Debug.Log("没用上的key？：" + s.num);
                }
            }

            if (this.readingNineAndTwo.GetAttackChuan()[1] != null)
                States_for_AbsoluteInput.Add(state_Dictionary[this.readingNineAndTwo.GetAttackChuan()[1].StateKey]);
            if (this.readingNineAndTwo.GetFire1Chuan()[1] != null)
                States_for_AbsoluteInput.Add(state_Dictionary[this.readingNineAndTwo.GetFire1Chuan()[1].StateKey]);
            if (this.readingNineAndTwo.GetFire2Chuan()[1] != null)
                States_for_AbsoluteInput.Add(state_Dictionary[this.readingNineAndTwo.GetFire2Chuan()[1].StateKey]);
            if (this.readingNineAndTwo.GetD_STS() != null)
                States_for_AbsoluteInput.Add(state_Dictionary[this.readingNineAndTwo.GetD_STS().StateKey]);
            if (this.readingNineAndTwo.GetR_STS() != null)
                States_for_AbsoluteInput.Add(state_Dictionary[this.readingNineAndTwo.GetR_STS().StateKey]);
            if (this.readingNineAndTwo.GetM_STS() != null)
                commandWaitingState = state_Dictionary[this.readingNineAndTwo.GetM_STS().StateKey];
        }

        public void IniStates(Data_Center data_Center)
        {
            if (state_Dictionary == null)
            {
                Debug.Log("严重错误");
                return;
            }
            foreach (KeyValuePair<string, AI_State> s in state_Dictionary)
            {
                s.Value._DATA_CENTER = data_Center;
                s.Value.Pre_process_before_enter();
            }

            current_state_num = "Empty";
            state_Dictionary.TryGetValue(current_state_num, out now_state);
            if (playerMode)
                now_state.C_State_enter();
            else
                now_state.AI_State_enter();
        }
    }
}



