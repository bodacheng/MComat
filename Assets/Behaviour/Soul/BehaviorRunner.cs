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
        BehaviorsIncubator _States_Incubator;
        #endregion
        
        #region 辅助模块：技能链接时机判断器
        public SkillCancelFlag _SkillCancelFlag;
        #endregion
        
        #region 运行时活参数
        public readonly SingleFightLog SingleFightLog = new SingleFightLog();
        public IDictionary<string, Behavior> BehaviourDic = new Dictionary<string, Behavior>();
        public IDictionary<string, SkillEntity> SkillEntityDic;//大状态机真正的运行依据，其他内容都是为了生成它而存在的中间变量
        public SkillEntity CurrentSKillEntity;
        SkillEntity tempSKillEntity;
        
        #region 辅助模块：控制器
        readonly Controller controller = new Controller();
        #endregion
        
        readonly Empty_State empty_State = new Empty_State();
        Behavior _nowBehavior;
        Behavior _lastBehavior;
        Behavior _tryBehavior;
        public Behavior _commandWaitingState;//所谓的待机状态。和首发状态分开处理，因为有实际作用的技能肯定要优先释放，没有的话才进行一些移动等等。
        #endregion
        
        public MobileInputsManager InputsManager
        {
            get;
            set;
        }
        
        public bool BeingControl()
        {
            return InputsManager!= null && InputsManager.inputting;
        }
        
        void Awake()
        {
            _nowBehavior = empty_State;   
        }
        
        public bool AI { set; get; }

        public bool IfRunning()
        {
            return _nowBehavior != empty_State;
        }
        
        public Behavior GetNowState()
        {
            return _nowBehavior;
        }
        public Behavior GetLastState()
        {
            return _lastBehavior;
        }
        
        void Update()
        {
            if (IfRunning())
            {
                BehaviourTransitionEngine();
                
                #region 决策制定
                controller.Decision(this, _canTranTo, AI && !BeingControl());
                #endregion
                
                _nowBehavior?._State_Update();
            }
        }
        
        void FixedUpdate()
        {
            if (IfRunning())
            {
                if (_nowBehavior != null)
                {
                    if (AI && !BeingControl())
                    {
                        _nowBehavior._State_FixedUpdate1();
                        _nowBehavior._State_FixedUpdate2();
                    }
                    else
                    {
                        _nowBehavior._c_State_FixedUpdate1();
                        _nowBehavior._c_State_FixedUpdate2();
                    }
                }
            }
        }
        
        public void ChangeState(string num)
        {
            _SkillCancelFlag.turn_off_flag();
            BehaviourDic.TryGetValue(num, out _tryBehavior);
            if (_nowBehavior != null)
            {
                _nowBehavior.AI_State_exit();
            }

            //注意看changeState环节，上一个状态的exit和下一个状态的enter是同一个帧执行的。
            //从这里我们曾经发现了动画播放模块一个重要问题，就是在特定情况下，
            //比如defend状态的exit里有PlayLayerAnim(_animator_layer_index, null)，防御后接攻击，
            //那么先执行PlayLayerAnim(_animator_layer_index, null) ，同一帧执行PlayLayerAnim(_animator_layer_index, clip_name);
            //就会产生bug：动画器无法正常播放攻击动画，角色会立在那里。这是我们动画模块的一个性质。
            // 我们把defend状态exit中的PlayLayerAnim(_animator_layer_index, null)删除了后就不再产生对应bug。
            // 关于动画模块的“技能动作清空”，我们是把它放在了move状态的开头，从而避免了清空函数与触发动画函数在同一帧执行。
            _lastBehavior = _nowBehavior;
            _nowBehavior = _tryBehavior;

            if (_nowBehavior == null)
            {
                Debug.Log("尝试读取未定义的状态" + num);
                return;
            }
            
            if (AI && !BeingControl())
            {
                _nowBehavior.AI_State_enter();
            }
            else
            {
                _nowBehavior.C_State_enter();
            }
        }
        
        public void ChangeState(string num, V_Damage damage)
        {
            BehaviourDic.TryGetValue(num, out _tryBehavior);
            if (_nowBehavior != null)
                _nowBehavior.AI_State_exit();
            
            _lastBehavior = _nowBehavior;
            _nowBehavior = _tryBehavior;
            
            if (_nowBehavior == null)
            {
                Debug.Log("尝试读取未定义的状态" + num);
                return;
            }
            if (AI && !BeingControl())
                _nowBehavior.AI_State_enter(damage);
            else
                _nowBehavior.C_State_enter(damage);
        }

        public void ChangeToWaitingState()
        {
            BehaviourDic.TryGetValue(_commandWaitingState.StateKey, out _tryBehavior);
            if (_tryBehavior != GetNowState())//避免战斗待机状态重复进入
            {
                ChangeState(_commandWaitingState.StateKey);
            }
        }
        
        public void ChangeToTestMode()
        {
            BehaviourDic.TryGetValue(_commandWaitingState.StateKey, out _tryBehavior);
            var move_State = (Move_State)_tryBehavior;
            move_State._AIMoveStyle = AIMoveMode.test;
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

            BehaviourDic.TryGetValue("Empty", out _nowBehavior);
            
            if (!AI)
            {
                _nowBehavior.C_State_enter();
            }
            else
            {
                _nowBehavior.AI_State_enter();
            }
        }       
    }
}