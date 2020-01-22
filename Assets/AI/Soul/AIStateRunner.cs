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
        public List<Behavior_Transition_Set> State_Transition_Set_List;//
        NineAndTwo readingNineAndTwo;
        Behaviors_Incubator _States_Incubator;
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
        Behavior now_Behavior;
        Behavior last_Behavior;
        Behavior try_Behavior;
        string current_state_num;
        IDictionary<string, Behavior> state_Dictionary = new Dictionary<string, Behavior>();
        IDictionary<string, Behavior_Transition_Set> state_Transition_Dictionary;//大状态机真正的运行依据，其他内容都是为了生成它而存在的中间变量
        List<Behavior_Transition_Set> avaliable_casual_Transitions = new List<Behavior_Transition_Set>();
        List<Behavior_Transition_Set> TransitionsPrioritys = new List<Behavior_Transition_Set>();
        List<Behavior_Transition_Set> finalDecisions = new List<Behavior_Transition_Set>();
        
        List<string> avaliable_forced_Transitions = new List<string>();
        Behavior_Transition_Set CurrentStateTransitionSet;
        Behavior commandWaitingState;//所谓的待机状态。和首发状态分开处理，因为有实际作用的技能肯定要优先释放，没有的话才进行一些移动等等。
        #endregion

        public void SetPlayerMode(bool result)
        {
            playerMode = result;
            _inputManager.PlayerInputting = false;
        }
        
        public bool IfRunning()
        {
            return now_Behavior != empty_State;
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

        public Behavior GetNowState()
        {
            return now_Behavior;
        }
        public Behavior GetLastState()
        {
            return last_Behavior;
        }
        public Behavior GetTryState()
        {
            return try_Behavior;
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
            now_Behavior = empty_State;   
        }

        void FixedUpdate()
        {
            if (IfRunning())
            {
                if (MobileInputsManager.target.watchingInputManger == _inputManager)
                    _inputManager.CheckIfPlayerIsInputting();
                StateTransitionEngine_new(state_Transition_Dictionary);
                if (now_Behavior != null)
                {
                    if (playerMode || (!playerMode && _inputManager.PlayerInputting))
                    {
                        now_Behavior._c_State_FixedUpdate1();
                        now_Behavior._c_State_FixedUpdate2();
                    }
                    else
                    {
                        now_Behavior._State_FixedUpdate1();
                        now_Behavior._State_FixedUpdate2();
                    }
                }
            }
        }

        public void ChangeState(string num)
        {
            current_state_num = num;
            state_Dictionary.TryGetValue(current_state_num, out try_Behavior);
            if (now_Behavior != null)
                now_Behavior.AI_State_exit();

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
            last_Behavior = now_Behavior;
            now_Behavior = try_Behavior;

            if (now_Behavior == null)
            {
                Debug.Log("尝试读取未定义的状态" + current_state_num);
                return;
            }
            if (playerMode || _inputManager.PlayerInputting)
                now_Behavior.C_State_enter();
            else
                now_Behavior.AI_State_enter();
        }
        
        public void ChangeState(string num, V_Damage newvalue)
        {
            current_state_num = num;
            state_Dictionary.TryGetValue(current_state_num, out try_Behavior);
            if (now_Behavior != null)
                now_Behavior.AI_State_exit();

            last_Behavior = now_Behavior;
            now_Behavior = try_Behavior;

            if (now_Behavior == null)
            {
                Debug.Log("尝试读取未定义的状态" + current_state_num);
                return;
            }
            if (playerMode || _inputManager.PlayerInputting)
                now_Behavior.C_State_enter(newvalue);
            else
                now_Behavior.AI_State_enter(newvalue);
        }

        public void StartToGo()
        {
            string[] startOffState = { "Move_normal", "Move_slow", "Move_fast", "Test_Move" };
            for (int i = 0; i < startOffState.Length; i++)
            {
                state_Transition_Dictionary.TryGetValue(startOffState[i], out Behavior_Transition_Set _State_Transition);
                if (_State_Transition != null)
                {
                    this.ChangeState(startOffState[i]);
                    break;
                }
            }
        }

        public void FormFightingSetsByNineAndTwo(string type, NineAndTwo nineAndTwo)
        {
            if (nineAndTwo == null)
            {
                Debug.Log("九宫格为空，返回");
                return;
            }
            readingNineAndTwo = nineAndTwo;
            readingNineAndTwo.SortNineAndTwo();
            //这上下两个函数之间存在一个chuanEndCasualT0的问题，从而必须一前一后紧密连接，下次review时候可以看看代码能不能整更利索一些。
            state_Transition_Dictionary = readingNineAndTwo.GenerateBeheviourSets();
            State_Transition_Set_List = readingNineAndTwo.ReturnSTSlist();//这一行于本游戏本身已经无用，但该列表牵扯到开发环境下角色技能详细的显示，以及框架本身保存xml战斗脚本的功能。
            
            bool hasD, hasR;
            hasD = readingNineAndTwo.GetDConfig() != null;
            hasR = readingNineAndTwo.GetRConfig() != null;

            if (_inputManager == null)
            {
                _inputManager = new InputManager();
            }
            _inputManager.INI(hasD, hasR, this);

            _States_Incubator = new Behaviors_Incubator(type, empty_State,this.state_Transition_Dictionary);
            List<BehaviorIndex_With_Behavior> Num_State_List = _States_Incubator.Num_State_List; // 理解整个系统的关键
            state_Dictionary = new Dictionary<string, Behavior>();

            foreach (BehaviorIndex_With_Behavior s in Num_State_List)
            {
                if (state_Transition_Dictionary.ContainsKey(s.num))
                {
                    s.state.StateKey = state_Transition_Dictionary[s.num].StateKey;
                    s.state.splevel = state_Transition_Dictionary[s.num].SPLevel;
                    s.state.enterInput = state_Transition_Dictionary[s.num].enterInput;
                    s.state.exitInput = state_Transition_Dictionary[s.num].exitInput;
                    s.state.behaviorEnterRanges = state_Transition_Dictionary[s.num].ai_trigger_ranges;
                    AddAITriggerConditionToBehavior(s.state);
                    state_Dictionary.Add(new KeyValuePair<string, Behavior>(s.num, s.state));
                }
                else
                {
                    Debug.Log("没用上的key？：" + s.num);
                }
            }
            if (readingNineAndTwo.GetM_STS() != null)
            {
                commandWaitingState = state_Dictionary[readingNineAndTwo.GetM_STS().StateKey];
            }
        }

        //List<Behavior> AINextPriority1 = new List<Behavior>();
        //List<Behavior> AINextPriority2 = new List<Behavior>();
        //List<Behavior> AINextPriority3 = new List<Behavior>();        
        IDictionary<string, List<string>> RespondAndCondition = new Dictionary<string, List<string>>();
        void RegisterConditionToRespond(KeyValuePair<string, string> BeheviourAndConditioncode)//string target_beheviour,string condition_code
        {
            if (RespondAndCondition.ContainsKey(BeheviourAndConditioncode.Key))
            {
                RespondAndCondition[BeheviourAndConditioncode.Key].Add(BeheviourAndConditioncode.Value);
            }
            else{
                RespondAndCondition.Add(BeheviourAndConditioncode.Key,new List<string>() { BeheviourAndConditioncode.Value});
            }
        }
        
        IDictionary<KeyValuePair<string, string>, int> RespondAndConditionPriority = new Dictionary<KeyValuePair<string, string>, int>();
        void AddAITriggerConditionToBehavior(Behavior behavior)
        {
            switch(behavior.StateType)
            {
                case BehaviorType.MV:
                    behavior.strategic_exit_condition_code = "TimeToStopRunning";
                    break;
                case BehaviorType.AC:
                    KeyValuePair<string, string> keyValuePair1 = new KeyValuePair<string, string>(behavior.StateKey, "LosingDefendStrength");
                    KeyValuePair<string, string> keyValuePair2 = new KeyValuePair<string, string>(behavior.StateKey, "DangerousNearby");
                    RegisterConditionToRespond(keyValuePair1);
                    RegisterConditionToRespond(keyValuePair2);
                    RespondAndConditionPriority.Add(keyValuePair1,1);
                    RespondAndConditionPriority.Add(keyValuePair2,2);
          
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.CT:                    
                    KeyValuePair<string, string> keyValuePair_ct = new KeyValuePair<string, string>(behavior.StateKey, "DangerousClose");
                    RegisterConditionToRespond(keyValuePair_ct);
                    RespondAndConditionPriority.Add(keyValuePair_ct,1);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.Def:
                    KeyValuePair<string, string> keyValuePair_def1 = new KeyValuePair<string, string>(behavior.StateKey, "DangerousVeryClose");
                    KeyValuePair<string, string> keyValuePair_def2 = new KeyValuePair<string, string>(behavior.StateKey, "MayBeDefend");
                    RegisterConditionToRespond(keyValuePair_def1);
                    RegisterConditionToRespond(keyValuePair_def2);
                    RespondAndConditionPriority.Add(keyValuePair_def1,1);
                    RespondAndConditionPriority.Add(keyValuePair_def2,2);

                    behavior.strategic_exit_condition_code = "TimeToRespond";
                    break;
                case BehaviorType.GR:
                    KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>(behavior.StateKey, "TimeToAttack");
                    RegisterConditionToRespond(keyValuePair_gr);
                    RespondAndConditionPriority.Add(keyValuePair_gr,2);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.GI:
                    KeyValuePair<string, string> keyValuePair_gi = new KeyValuePair<string, string>(behavior.StateKey, "TimeToAttack");
                    RegisterConditionToRespond(keyValuePair_gi);
                    RespondAndConditionPriority.Add(keyValuePair_gi,2);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.GM:
                    KeyValuePair<string, string> keyValuePair_gm = new KeyValuePair<string, string>(behavior.StateKey, "TimeToDashAttack");
                    RegisterConditionToRespond(keyValuePair_gm);
                    RespondAndConditionPriority.Add(keyValuePair_gm,2);
                    
                    behavior.strategic_exit_condition_code = null;                  
                    break;
                default:
                    break;
            }
        }

        public void IniStates(Data_Center data_Center)
        {
            if (state_Dictionary == null)
            {
                Debug.Log("严重错误");
                return;
            }
            foreach (KeyValuePair<string, Behavior> s in state_Dictionary)
            {
                s.Value._DATA_CENTER = data_Center;
                s.Value.Pre_process_before_enter();
            }

            current_state_num = "Empty";
            state_Dictionary.TryGetValue(current_state_num, out now_Behavior);
            if (playerMode)
                now_Behavior.C_State_enter();
            else
                now_Behavior.AI_State_enter();
        }
    }
}



