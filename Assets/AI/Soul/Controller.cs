using Inputs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Soul
{
    public class Controller : MonoBehaviour
    {
        public static bool playerMode;        
        public InputManager _inputManager = new InputManager();//_inputManager.refreshSPLevelButtonsInfo在本脚本中的两处出现，一个是在状态运行引擎函数的“next state”判断处，        
        List<Behavior_Transition_Set> finalDecisions = new List<Behavior_Transition_Set>();
        // 满足了触发条件的情况对策因果组
        IDictionary<KeyValuePair<string, string>, int> Triggerd = new Dictionary<KeyValuePair<string, string>, int>();// 果，因，优先级
        Behavior try_Behavior;
        
        public void SetPlayerMode(bool result)
        {
            playerMode = result;
            _inputManager.PlayerInputting = false;
        }

        public void Control(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions)
        {
            finalDecisions.Clear();
            Triggerd.Clear();
            if (playerMode || _inputManager.PlayerInputting)
            {
                bool exitCommandFufilled = true;
                exitCommandFufilled = runner.CurrentBehaviorTransitionSet.exitInput == Inputs_defined.Null || CheckInput(runner.CurrentBehaviorTransitionSet.exitInput);
                if (!exitCommandFufilled)
                    return;
                for (int i = 0; i < avaliable_casual_Transitions.Count; i++)
                {
                    if ((avaliable_casual_Transitions[i].enterInput != Inputs_defined.Null && CheckInput(avaliable_casual_Transitions[i].enterInput))|| avaliable_casual_Transitions[i].enterInput == Inputs_defined.Null)
                    {
                        finalDecisions.Add(avaliable_casual_Transitions[i]);
                    }
                }
            }else{
                if (runner.GetNowState().Strategic_exit_condition())
                {
                    for (int i = 0; i < avaliable_casual_Transitions.Count; i++)
                    {
                        runner.Behaviour_Dictionary.TryGetValue(avaliable_casual_Transitions[i].StateKey, out try_Behavior);
                        List<string> Conditions = runner.RespondAndCondition[avaliable_casual_Transitions[i].StateKey];
                        for (int y = 0; y < Conditions.Count; y++)
                        {
                            if (try_Behavior.CheckTriggerCondition(Conditions[y]))//条件满足
                            {
                                // 查看情况与行为反应的优先度
                                foreach (KeyValuePair<KeyValuePair<string, string>, int> keyValuePair in runner.RespondAndConditionPriority)
                                {
                                    if (keyValuePair.Key.Key == try_Behavior.StateKey && keyValuePair.Key.Value == Conditions[y])//这个检索机制应该找点什么给取代掉
                                    {
                                        //Debug.Log("状态"+try_Behavior.StateKey + "遇到了情况："+Conditions[y] +"从而可能要触发");
                                        Triggerd.Add(keyValuePair);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (Triggerd.Count > 0)
                {
                    List<KeyValuePair<string,string>> minkeys = Triggerd.Keys.Select(x => new { x, y = Triggerd[x] }).GroupBy(x => x.y).OrderBy(x => x.Key).First().Select(x => x.x).ToList();
                    for (int x = 0; x < minkeys.Count;x++)
                    {
                        //Debug.Log("AI甄选的最佳决定："+minkeys[x].Key);
                        finalDecisions.Add(runner.Behaviour_Transition_Dictionary[minkeys[x].Key]);
                    }
                }
            }

            if (finalDecisions.Count > 0)//本质上说控制模式下finalDecisions的长度基本只能是1或0，而AI模式下则在这里进一步牵扯到多个选项做选择的问题
            {
                if (playerMode || _inputManager.PlayerInputting)
                {
                    if (MobileInputsManager.target.watchingInputManger == _inputManager)
                    {
                        MobileInputsManager.Skillbuttonexplosion(finalDecisions[0].enterInput, finalDecisions[0].SPLevel);
                    }
                    runner.ChangeState(finalDecisions[0].StateKey);
                }else{//这里虽然是随机但是毕竟随机的这几个选项在优先级上是相同的。
                    int random = Random.Range(0,finalDecisions.Count);
                    if (MobileInputsManager.target.watchingInputManger == _inputManager)
                    {
                        MobileInputsManager.Skillbuttonexplosion(finalDecisions[random].enterInput, finalDecisions[random].SPLevel);
                    }
                    runner.ChangeState(finalDecisions[random].StateKey);
                }
                return;
            }
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
    }
}

