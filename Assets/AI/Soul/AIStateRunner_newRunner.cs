using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public partial class AIStateRunner : MonoBehaviour
    {
        List<State_Rate_Set> SRTListForCasualTransitionbuttonRefresh = new List<State_Rate_Set>();
        public void StateTransitionEngine_new(IDictionary<string, State_Transition_Set> state_Transition_Dictionary)
        {
            avaliable_casual_Transitions.Clear();
            casual_TransitionsPriority1.Clear();
            casual_TransitionsPriority2.Clear();
            casual_TransitionsPriority3.Clear();
            avaliable_forced_Transitions.Clear();
            SRTListForCasualTransitionbuttonRefresh.Clear();

            if (current_state_num != null && state_Transition_Dictionary != null)
                state_Transition_Dictionary.TryGetValue(current_state_num, out CurrentStateTransitionSet);

            #region Forced state transition 
            if (CurrentStateTransitionSet.forced_to_state_nums != null && CurrentStateTransitionSet.forced_to_state_nums.Length > 0)
            {
                foreach (string num in CurrentStateTransitionSet.forced_to_state_nums)
                {
                    state_Dictionary.TryGetValue(num, out try_state);
                    if (try_state.Force_enter_condition())
                    {
                        avaliable_forced_Transitions.Add(num);
                    }
                }
            }
            if (avaliable_forced_Transitions.Count > 0)
            {
                ChangeState(avaliable_forced_Transitions[0]);
                return; // Once a state is forced to trigger, there is no need for the rest of codes to run at this frame
            }
            #endregion

            ///////// 18.10.17追加修改:
            /// 下面这些部分是17日所追加的全新部分，目的在于更正确的适配技能输入图标。
            /// 重点1:一旦发动了某一技能后，技能图标应该立刻更新，而不是等待可以super cancel之后，因此一旦avaliable_casual_Transitions.Count > 0
            /// 我们就立刻开始了对输入按键的图标刷新工作
            /// 重点2:如果在一个技能的连击选择中，attack，fire1，fire2中任何一个技能因为没蓝等原因无法发动，应该给准确的予以表示，而不是不进行任何键位刷新处理
            /// 另外详见refreshSPLevelButtonsInfo在NewCommandWaiting中的运行机制，那里也做了非常重要的修改
            ////////////////////////////////////////////////////////////

            bool exitCommandFufilled = true;
            if (playerMode || _inputManager.PlayerInputting)
            {
                exitCommandFufilled = CurrentStateTransitionSet.exitInput == Inputs_defined.Null || CheckInput(CurrentStateTransitionSet.exitInput);
            }

            if (CurrentStateTransitionSet.casual_to_state_Sets != null && CurrentStateTransitionSet.casual_to_state_Sets.Length > 0)
            {
                foreach (State_Rate_Set state_set in CurrentStateTransitionSet.casual_to_state_Sets)
                {
                    state_Dictionary.TryGetValue(state_set.AI_State_Number, out try_state);
                    if (try_state == null)
                    {
                        Debug.Log("以下路径脚本运行器致命错误:" + this.AI_States_path + " 的状态： " + state_set.AI_State_Number);
                        continue;
                    }

                    if (try_state.Capacity_enter_condition())
                    {
                        SRTListForCasualTransitionbuttonRefresh.Add(state_set);
                        if (((state_set.can_be_cancelled_to && _SkillCancelFlag.Cancel_Flag))||
                            (now_state.Naturally_exit_condition() && exitCommandFufilled))
                        {
                            if (playerMode || _inputManager.PlayerInputting)
                            {
                                if ((state_set.enterInput != Inputs_defined.Null && CheckInput(state_set.enterInput)) ||
                                    state_set.enterInput == Inputs_defined.Null)
                                {
                                    if (try_state.Enter_condition_priority1())
                                        casual_TransitionsPriority1.Add(state_set);
                                    if (try_state.Enter_condition_priority2())
                                        casual_TransitionsPriority2.Add(state_set);
                                    if (try_state.Enter_condition_priority3())
                                        casual_TransitionsPriority3.Add(state_set);
                                    avaliable_casual_Transitions.Add(state_set);
                                }
                            }
                            else
                            {
                                if (try_state.Enter_condition_priority1())
                                    casual_TransitionsPriority1.Add(state_set);
                                if (try_state.Enter_condition_priority2())
                                    casual_TransitionsPriority2.Add(state_set);
                                if (try_state.Enter_condition_priority3())
                                    casual_TransitionsPriority3.Add(state_set);

                                avaliable_casual_Transitions.Add(state_set);
                            }
                        }
                    }
                }
                if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                    _inputManager.ButtonRefreshForCasualTransition(SRTListForCasualTransitionbuttonRefresh, _BO_Health);
            }

            if (CurrentStateTransitionSet.casual_to_state_Sets == null || CurrentStateTransitionSet.casual_to_state_Sets.Length == 0)
            {
                //if (now_state.casual_exit_condition())
                if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                    _inputManager.ButtonRefreshFromStart(this.States_for_AbsoluteInput, _BO_Health);
            }

            #region 状态迁移判断
            if (avaliable_casual_Transitions.Count > 0)
            {
                if (playerMode || _inputManager.PlayerInputting)
                {
                    if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                        MobileInputsManager.Skillbuttonexplosion(avaliable_casual_Transitions[0].enterInput, avaliable_casual_Transitions[0].SPLevel);
                    _SkillCancelFlag.turn_off_flag();
                    ChangeState(avaliable_casual_Transitions[0].AI_State_Number);
                    return;
                }
                else
                {
                    if (casual_TransitionsPriority1.Count > 0)
                    {
                        RandomTransitionToRun(casual_TransitionsPriority1);
                        return;
                    }
                    if (casual_TransitionsPriority2.Count > 0)
                    {
                        RandomTransitionToRun(casual_TransitionsPriority2);
                        return;
                    }
                    if (casual_TransitionsPriority3.Count > 0)
                    {
                        RandomTransitionToRun(casual_TransitionsPriority3);
                        return;
                    }
                }
            }

            if (!now_state.Naturally_exit_condition())
            {
                return;
            }
                
            if (playerMode || _inputManager.PlayerInputting)
            {
                if (!exitCommandFufilled)
                {
                    return;
                }
            }
            else
            {
                if (!now_state.Strategic_exit_condition())
                {
                    return;
                } 
            }

            NewCommandWaiting();//没有当前状态没有自然迁移的情况，一般来说其实就是从奔跑行走到决定开始一个状态。
            #endregion
        }

        void RandomTransitionToRun(List<State_Rate_Set> TransitionsToRun)
        {
            int next = UnityEngine.Random.Range(0, TransitionsToRun.Count);
            if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                MobileInputsManager.Skillbuttonexplosion(TransitionsToRun[next].enterInput, TransitionsToRun[next].SPLevel);
            _SkillCancelFlag.turn_off_flag();
            ChangeState(TransitionsToRun[next].AI_State_Number);
        }

        public bool HaveFirstSkillToTrigger()
        {
            foreach (AI_State _AS in States_for_AbsoluteInput)
            {
                if (_AS.StateType != stateType.Def && _AS.StateType != stateType.AC && _AS.StateType != stateType.NONE)
                {
                    if (_AS.Capacity_enter_condition())
                        return true;
                }
            }
            return false;
        }

        public void NewCommandWaiting()
        {
            AINext.Clear();
            AINextPriority1.Clear();
            AINextPriority2.Clear();
            AINextPriority3.Clear();
            foreach (AI_State State_Rate_Set in States_for_AbsoluteInput)
            {
                if (State_Rate_Set.Capacity_enter_condition())
                {
                    if (State_Rate_Set.Enter_condition_priority1())
                        AINextPriority1.Add(State_Rate_Set);
                    if (State_Rate_Set.Enter_condition_priority2())
                        AINextPriority2.Add(State_Rate_Set);
                    if (State_Rate_Set.Enter_condition_priority3())
                        AINextPriority3.Add(State_Rate_Set);
                    AINext.Add(State_Rate_Set);
                }
            }

            if (AINext.Count > 0)
            {
                if (playerMode || _inputManager.PlayerInputting)
                {
                    foreach (AI_State State_Rate_Set in AINext)
                    {
                        if (CheckInput(State_Rate_Set.enterInput))
                        {
                            if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                                 MobileInputsManager.Skillbuttonexplosion(State_Rate_Set.enterInput, State_Rate_Set.splevel);
                            ChangeState(State_Rate_Set.StateKey);
                            return;
                        }
                    }
                }
                else
                {
                    if (AINextPriority1.Count > 0)
                    {
                        RandomStateToStartOff(AINextPriority1);
                        return;
                    }
                    if (AINextPriority2.Count > 0)
                    {
                        RandomStateToStartOff(AINextPriority2);
                        return;
                    }
                    if (AINextPriority3.Count > 0)
                    {
                        RandomStateToStartOff(AINextPriority3);
                        return;
                    }
                }
            }

            if (commandWaitingState == null)
                return;
            state_Dictionary.TryGetValue(commandWaitingState.StateKey, out try_state);
            if (try_state != now_state)//避免战斗待机状态重复进入
            {
                ChangeState(commandWaitingState.StateKey);
            }
        }

        void RandomStateToStartOff(List<AI_State> TransitionsToRun)
        {
            int next = UnityEngine.Random.Range(0, TransitionsToRun.Count);
            if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                MobileInputsManager.Skillbuttonexplosion(TransitionsToRun[next].enterInput, TransitionsToRun[next].splevel);
            ChangeState(TransitionsToRun[next].StateKey);
        }
    }
}