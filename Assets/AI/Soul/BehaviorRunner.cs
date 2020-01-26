using System.Collections.Generic;
using UnityEngine;
using HittingDetection;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        #region 初始化相关
        public string characterType;
        public List<Behavior_Transition_Set> State_Transition_Set_List;
        Behaviors_Incubator _States_Incubator;
        #endregion

        #region 辅助模块：技能链接时机判断器
        public SkillCancelFlag _SkillCancelFlag;
        #endregion
        
        #region 辅助模块：控制器
        public Controller controller;
        #endregion
        
        #region 运行时活参数
        public IDictionary<string, Behavior> Behaviour_Dictionary = new Dictionary<string, Behavior>();
        public IDictionary<string, Behavior_Transition_Set> Behaviour_Transition_Dictionary;//大状态机真正的运行依据，其他内容都是为了生成它而存在的中间变量
        public Behavior_Transition_Set CurrentBehaviorTransitionSet;
        
        Empty_State empty_State = new Empty_State();
        Behavior now_Behavior;
        Behavior last_Behavior;
        Behavior try_Behavior;
        Behavior commandWaitingState;//所谓的待机状态。和首发状态分开处理，因为有实际作用的技能肯定要优先释放，没有的话才进行一些移动等等。
        #endregion

        void Awake()
        {
            now_Behavior = empty_State;   
        }

        public bool IfRunning()
        {
            return now_Behavior != empty_State;
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

        void Update()
        {
            if (IfRunning())
            {
                BehaviourTransitionEngine();
            }
        }

        void FixedUpdate()
        {
            if (IfRunning())
            {
                if (now_Behavior != null)
                {
                    if ((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this)
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
            _SkillCancelFlag.turn_off_flag();
            Behaviour_Dictionary.TryGetValue(num, out try_Behavior);
            if (now_Behavior != null)
            {
                now_Behavior.AI_State_exit();
            }

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
                Debug.Log("尝试读取未定义的状态" + num);
                return;
            }
            if ((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this)
            {
                now_Behavior.C_State_enter();
            }
            else
            {
                now_Behavior.AI_State_enter();
            }
        }
        
        public void ChangeState(string num, V_Damage newvalue)
        {
            Behaviour_Dictionary.TryGetValue(num, out try_Behavior);
            if (now_Behavior != null)
                now_Behavior.AI_State_exit();

            last_Behavior = now_Behavior;
            now_Behavior = try_Behavior;

            if (now_Behavior == null)
            {
                Debug.Log("尝试读取未定义的状态" + num);
                return;
            }
            if ((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this)
                now_Behavior.C_State_enter(newvalue);
            else
                now_Behavior.AI_State_enter(newvalue);
        }
        
        public void ChangeToWaitingState()
        {
            ChangeState(commandWaitingState.StateKey);
        }
      
        public void INIStates(Data_Center data_Center)
        {
            if (Behaviour_Dictionary == null)
            {
                Debug.Log("严重错误");
                return;
            }
            foreach (KeyValuePair<string, Behavior> s in Behaviour_Dictionary)
            {
                s.Value._DATA_CENTER = data_Center;
                s.Value.Pre_process_before_enter();
            }

            Behaviour_Dictionary.TryGetValue("Empty", out now_Behavior);
            if ((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this)
            {
                now_Behavior.C_State_enter();
            }
            else
            {
                now_Behavior.AI_State_enter();
            }
        }
        
        public void StartToGo()
        {
            string[] startOffState = { "Move_normal", "Move_slow", "Move_fast", "Test_Move" };
            for (int i = 0; i < startOffState.Length; i++)
            {
                Behaviour_Transition_Dictionary.TryGetValue(startOffState[i], out Behavior_Transition_Set _State_Transition);
                if (_State_Transition != null)
                {
                    ChangeState(startOffState[i]);
                    break;
                }
            }
        }
    }
}



