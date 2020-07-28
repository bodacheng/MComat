using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Skill;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        #region 初始化相关
        public List<SkillEntity> SkillEntity_List;
        Behaviors_Incubator _States_Incubator;
        #endregion

        #region 辅助模块：技能链接时机判断器
        public SkillCancelFlag _SkillCancelFlag;
        #endregion
        
        #region 辅助模块：控制器
        public Controller controller;
        #endregion

        #region 运行时活参数
        public SingleFightLog SingleFightLog = new SingleFightLog();
        public IDictionary<string, Behavior> BehaviourDic = new Dictionary<string, Behavior>();
        public IDictionary<string, SkillEntity> SkillEntityDic;//大状态机真正的运行依据，其他内容都是为了生成它而存在的中间变量
        public SkillEntity CurrentSKillEntity;
        SkillEntity tempSKillEntity;

        Empty_State empty_State = new Empty_State();
        Behavior now_Behavior;
        Behavior last_Behavior;
        Behavior try_Behavior;
        public Behavior commandWaitingState;//所谓的待机状态。和首发状态分开处理，因为有实际作用的技能肯定要优先释放，没有的话才进行一些移动等等。
        
        public bool scarecrow;
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

        void Update()
        {
            if (IfRunning())
            {
                BehaviourTransitionEngine();
                if (!scarecrow)
                {
                    #region 决策制定
                    controller.PlayerControll(this, CanTranTo, !((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this));
                    #endregion
                }
                controller.Resetter(this);                
                if (now_Behavior != null)
                {
                    now_Behavior._State_Update();
                }
                SingleFightLog.AnalysisLog(ConditionAndRespondPriority);
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
            BehaviourDic.TryGetValue(num, out try_Behavior);
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
            if (GetNowState().StateKey == "Empty")
            {
                Debug.Log(this + " special case");
                return;// 找不到轮番模式下多个角色可能同时在场的原因，怀疑可能是因为待机角色因某种角色“受伤”而从empty状态脱离
            }
            BehaviourDic.TryGetValue(num, out try_Behavior);
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
            BehaviourDic.TryGetValue(commandWaitingState.StateKey, out try_Behavior);
            if (try_Behavior != GetNowState())//避免战斗待机状态重复进入
            {
                ChangeState(commandWaitingState.StateKey);
            }
        }
        
        public void ChangeToTestMode()
        {
            BehaviourDic.TryGetValue("Test_Move", out try_Behavior);
            commandWaitingState = try_Behavior;
            ChangeToWaitingState();          
        }
      
        public void INIStates(Data_Center data_Center)
        {
            if (BehaviourDic == null)
            {
                Debug.Log("严重错误");
                return;
            }
            foreach (KeyValuePair<string, Behavior> s in BehaviourDic)
            {
                s.Value._DATA_CENTER = data_Center;
                s.Value.Pre_process_before_enter();
            }

            BehaviourDic.TryGetValue("Empty", out now_Behavior);
            if ((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this)
            {
                now_Behavior.C_State_enter();
            }
            else
            {
                now_Behavior.AI_State_enter();
            }
        }       
    }
}