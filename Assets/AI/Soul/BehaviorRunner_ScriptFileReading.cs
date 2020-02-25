using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Linq;
using System.IO;
using System;
using Skill;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        public string AI_States_path; // 我们现在要做的这个游戏完全不牵扯到玩家保存脚本这个事情，但我们自己编辑脚本需要这东西
        public TextAsset usingScript;

        public List<string> PassSkillTypeKeys()//出于初始化的便利而存在的一个函数
        {
            return _States_Incubator?.SkillTypeKeys;
        }

        public void SaveTrans(string chracterType)
        {
            this.SaveStateTransitionInfo(State_Transition_Set_List, AI_States_path, chracterType);
        }

        public string ArrangeScriptPathForPlatfom(string PathInStringOrigin)
        {
            string AI_selected = null;
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                AI_selected = PathInStringOrigin;
            }
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                AI_selected = "/Resources/" + PathInStringOrigin + ".xml";
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                AI_selected = PathInStringOrigin;
            }
            return AI_selected;
        }

        public bool SaveStateTransitionInfo(List<Behavior_Transition_Set> list, string pathAndFileName, string clip_path)
        {
            try
            {
                IDictionary<string, Behavior_Transition_Set> toFormAttackStateList = new Dictionary<string, Behavior_Transition_Set>();
                for (int i = 0; i < list.Count; i++)
                {
                    toFormAttackStateList.Add(list[i].StateKey, list[i]);
                }
                _States_Incubator = new Behaviors_Incubator(empty_State,toFormAttackStateList);

                List<Behavior_Transition_Set> after_list = new List<Behavior_Transition_Set>();
                List<string> alreadyInList = new List<string>();

                foreach (Behavior_Transition_Set s in list)
                {
                    if (!alreadyInList.Contains(s.StateKey) && _States_Incubator.StateIndexList.Contains(s.StateKey))
                    {
                        after_list.Add(s);
                        alreadyInList.Add(s.StateKey);//在保存阶段确保了状态定义不会重复
                    }
                }

                if (!alreadyInList.Contains("Empty"))
                {
                    Behavior_Transition_Set Empty = new Behavior_Transition_Set("Empty",
                                                                          BehaviorType.NONE,
                                                                          0,
                                                                          null,
                                                                          new Behavior_Transition_Set[0], new string[0],
                                                                          Inputs_defined.Null, Inputs_defined.Null,
                                                                          0,
                                                                          0);
                    after_list.Add(Empty);
                    alreadyInList.Add("Empty");
                }

                if (!alreadyInList.Contains("Victory"))
                {
                    Behavior_Transition_Set Victory = new Behavior_Transition_Set("Victory",
                                                                            BehaviorType.NONE,
                                                                            0,
                                                                            null,
                                                                            new Behavior_Transition_Set[0], new string[0],
                                                                            Inputs_defined.Null, Inputs_defined.Null,
                                                                            0,
                                                                            0);
                    after_list.Add(Victory);
                    alreadyInList.Add("Victory");
                }

                if (!alreadyInList.Contains("Death"))
                {
                    Behavior_Transition_Set Death = new Behavior_Transition_Set("Death",
                                                                          BehaviorType.NONE,
                                                                          0,
                                                                          null,
                                                                          new Behavior_Transition_Set[0], new string[0],
                                                                          Inputs_defined.Null, Inputs_defined.Null,
                                                                          0,
                                                                          0);
                    after_list.Add(Death);
                    alreadyInList.Add("Death");
                }

                if (!alreadyInList.Contains("Hit"))
                {
                    Behavior_Transition_Set Hit = new Behavior_Transition_Set("Hit",
                                                                        BehaviorType.NONE,
                                                                        0,
                                                                        null,
                                                                        new Behavior_Transition_Set[0],
                                                                        (new List<string>() { "Hit", "KnockOff" }).ToArray(),
                                                                        Inputs_defined.Null, Inputs_defined.Null,
                                                                        0,
                                                                        0);
                    after_list.Add(Hit);
                    alreadyInList.Add("Hit");
                }

                if (!alreadyInList.Contains("KnockOff"))
                {
                    Behavior_Transition_Set KnockOff = new Behavior_Transition_Set("KnockOff",
                                                                             BehaviorType.NONE,
                                                                             0,
                                                                             null,
                                                                             new Behavior_Transition_Set[0], (new List<string>() { "Hit", "KnockOff" }).ToArray(),
                                                                             Inputs_defined.Null, Inputs_defined.Null,
                                                                             0,
                                                                             0);
                    after_list.Add(KnockOff);
                    alreadyInList.Add("KnockOff");
                }

                List<string> DefaultForceToNums = new List<string>() { "Hit", "KnockOff" };

                foreach (Behavior_Transition_Set s in after_list)
                {
                    List<Behavior_Transition_Set> undefined_CausalStateRateSet = new List<Behavior_Transition_Set>();
                    if (s.Casual_To_Behaviours != null)
                    {
                        foreach (Behavior_Transition_Set rs in s.Casual_To_Behaviours)
                        {
                            if (!alreadyInList.Contains(rs.StateKey))
                            {
                                undefined_CausalStateRateSet.Add(rs);
                            }
                        }
                    }

                    List<Behavior_Transition_Set> casuals_t = s.Casual_To_Behaviours != null ? s.Casual_To_Behaviours.ToList() : new List<Behavior_Transition_Set>();
                    if (undefined_CausalStateRateSet.Any())
                    {
                        foreach (Behavior_Transition_Set _set in undefined_CausalStateRateSet)
                        {
                            casuals_t.Remove(_set);// 把连续状态串里出现的没有定义的状态给删除掉。
                        }
                    }
                    s.Casual_To_Behaviours = casuals_t.ToArray();
                    s.forced_to_state_nums = s.StateKey != "Death" ? DefaultForceToNums.ToArray() : (new string[0]);
                }

                XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Behavior_Transition_Set>));
                FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Create);
                XmlSerializer.Serialize(FileStream, after_list);
                Debug.Log(Application.dataPath + pathAndFileName + " 尝试进行存储");
                FileStream.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.Log("状态迁移信息保存失败");
                Debug.Log(e.ToString());
                return false;
            }
        }

        public void LoadStatesTransition(TextAsset Script)
        {
            if (Script == null)
            {
                Debug.Log("脚本为空，返回");
                return;
            }
            if (now_Behavior != null)
            {
                now_Behavior.AI_State_exit();
            }

            State_Transition_Set_List = AIScriptReading.ReadKongfuBook(this, Script);//这个是一个状态清单，生成状态的是States_Dictionary类。
                                                                                                          //_States_Dictionary = new States_Dictionary(type,this.State_Transition_Set_List);//这一行于7月20号commentout了
            List<BehaviorIndex_With_Behavior> Num_State_List = _States_Incubator.Num_State_List;// 理解整个系统的关键
            Behaviour_Dictionary = new Dictionary<string, Behavior>();
            foreach (BehaviorIndex_With_Behavior s in Num_State_List)
            {
                Behaviour_Dictionary.Add(new KeyValuePair<string, Behavior>(s.num, s.state));
            }
            Behaviour_Transition_Dictionary = new Dictionary<string, Behavior_Transition_Set>();
            List<string> alreadyInList = new List<string>();//7.29 这个环节貌似是现在“同技能没法重复”bug的来源
            foreach (Behavior_Transition_Set _State_Transition_Set in State_Transition_Set_List)
            {
                if (_State_Transition_Set.StateKey != null
                    &&
                    !alreadyInList.Contains(_State_Transition_Set.StateKey)
                    &&
                    _States_Incubator.IfContainsKey(_State_Transition_Set.StateKey))
                {
                    List<Behavior_Transition_Set> new_casual_to = new List<Behavior_Transition_Set>();
                    if (_State_Transition_Set.Casual_To_Behaviours == null)
                    {
                        Debug.Log(Script.name + "脚本的" + _State_Transition_Set.StateKey + "状态自然迁移出错,尝试进行强加");
                        _State_Transition_Set.Casual_To_Behaviours = new_casual_to.ToArray();
                    }
                    foreach (Behavior_Transition_Set _State_Rate_Set in _State_Transition_Set.Casual_To_Behaviours)
                    {
                        if (!_States_Incubator.IfContainsKey(_State_Rate_Set.StateKey))
                        {
                            Debug.Log(Script.name + "脚本中的状态" + _State_Transition_Set.StateKey +
                                      "下存在没有定义的自然迁移状态" + _State_Rate_Set.StateKey + ",从而已经做强行删除处理。");
                        }
                        else
                        {
                            new_casual_to.Add(_State_Rate_Set);
                        }
                    }
                    Behaviour_Transition_Dictionary.Add(
                        new KeyValuePair<string, Behavior_Transition_Set>(
                            _State_Transition_Set.StateKey,
                            _State_Transition_Set
                        )
                    );
                    alreadyInList.Add(_State_Transition_Set.StateKey);
                }
                else
                {
                    if (_State_Transition_Set.StateKey == null)
                    {
                        Debug.Log("脚本中有的状态没有键值");
                    }
                    else
                    {
                        if (!_States_Incubator.IfContainsKey(_State_Transition_Set.StateKey))
                        {
                            Debug.Log("脚本中描写的状态的键值:" + _State_Transition_Set.StateKey + " 不存在于我们的定义");
                        }
                    }
                }
            }
        }

        public List<Behavior_Transition_Set> SortStateTransitionSetList(List<Behavior_Transition_Set> list)
        {
            IDictionary<string, Behavior_Transition_Set> toFormAttackStateList = new Dictionary<string, Behavior_Transition_Set>();
            for (int i = 0; i < list.Count; i++)
            {
                toFormAttackStateList.Add(list[i].StateKey, list[i]);
            }
            _States_Incubator = new Behaviors_Incubator(empty_State,toFormAttackStateList);
            IDictionary<string, Behavior_Transition_Set> stateTransitionSetDictionary = new Dictionary<string, Behavior_Transition_Set>();
            List<Behavior_Transition_Set> setsHaveInitialInput = new List<Behavior_Transition_Set>();
            List<Behavior_Transition_Set> regularStates = new List<Behavior_Transition_Set>();

            bool hasD = false, hasR = false;

            foreach (Behavior_Transition_Set _set in list)
            {

                if (_States_Incubator.StateIndexList.Contains(_set.StateKey))
                {
                    stateTransitionSetDictionary.Add(new KeyValuePair<string, Behavior_Transition_Set>(_set.StateKey, _set));
                }

                if (_set.enterInput != Inputs_defined.Null)
                {
                    hasR |= _set.enterInput == Inputs_defined.Acc;
                    hasD |= _set.enterInput == Inputs_defined.Defend;
                    setsHaveInitialInput.Add(_set);
                }

                if (_set.StateKey == "Controlled" || _set.StateKey == "Hit" || _set.StateKey == "Move")
                {
                    _set.forced_to_state_nums = new string[2] { "Hit", "KnockOff" };
                    regularStates.Add(_set);
                }
                if (_set.StateKey == "Empty" || _set.StateKey == "Death" || _set.StateKey == "Victory")
                {
                    _set.forced_to_state_nums = new string[0] { };
                    regularStates.Add(_set);
                }
                if (_set.StateKey == "KnockOff")
                {
                    _set.forced_to_state_nums = new string[] { "KnockOff" };
                }
            }

            List<Behavior_Transition_Set> knockOFFCasualTransitios = new List<Behavior_Transition_Set>();
            foreach (Behavior_Transition_Set _set in setsHaveInitialInput)
            {
                knockOFFCasualTransitios.Add(_set);
            }

            foreach (Behavior_Transition_Set _set in list)
            {
                if (_set.StateKey == "KnockOff")
                {
                    _set.Casual_To_Behaviours = knockOFFCasualTransitios.ToArray();
                }
            }

            SetStateRatesByAILevel(stateTransitionSetDictionary);

            List<Behavior_Transition_Set> allChuans = new List<Behavior_Transition_Set>();
            foreach (Behavior_Transition_Set _set in setsHaveInitialInput)
            {
                List<Behavior_Transition_Set> chuan = new List<Behavior_Transition_Set>();
                chuan = SearchChuanNext(_set, Inputs_defined.Null, chuan, allChuans, stateTransitionSetDictionary);
            }

            foreach (Behavior_Transition_Set _set in list)
            {
                if (!allChuans.Contains(_set) && !regularStates.Contains(_set) && _set.StateKey != null && _States_Incubator.StateIndexList.Contains(_set.StateKey))
                {
                    allChuans.Add(_set);
                }
            }
            regularStates.AddRange(allChuans);
            //allChuans.AddRange(regularStates);
            return regularStates;
        }

        List<Behavior_Transition_Set> SearchChuanNext(Behavior_Transition_Set _set, Inputs_defined _inputKey,
                                               List<Behavior_Transition_Set> chuan, List<Behavior_Transition_Set> allChuans,
                                               IDictionary<string, Behavior_Transition_Set> stateTransitionSetDictionary)
        {
            if (!chuan.Contains(_set) && !allChuans.Contains(_set))
            {
                chuan.Add(_set);
                allChuans.Add(_set);
            }

            Inputs_defined searching_inputKey = Inputs_defined.Null;
            searching_inputKey = _inputKey == Inputs_defined.Null ? _set.enterInput : _inputKey;

            foreach (Behavior_Transition_Set _rset in _set.Casual_To_Behaviours)
            {
                if (_rset.enterInput == searching_inputKey && _rset.enterInput != Inputs_defined.Null)//也就是说这种“chuan”的逻辑其实是说针对有连续输入命令的，自动迁移逻辑不算。并且在这里并不强调一定是同一输入键的攻击串
                {
                    stateTransitionSetDictionary.TryGetValue(_rset.StateKey, out Behavior_Transition_Set _new);
                    if (_new != null)
                    {
                        if (!chuan.Contains(_new) && !allChuans.Contains(_new))
                        {
                            if (SearchChuanNext(_new, searching_inputKey, chuan, allChuans, stateTransitionSetDictionary) != null)
                            {
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            return chuan;
        }

        bool CheckIfStringInList(string toCheck, List<string> checklist)
        {
            if (checklist == null || toCheck == null)
            {
                return false;
            }
            foreach (string _o in checklist)
            {
                if (toCheck.GetHashCode() == _o.GetHashCode())
                {
                    return true;
                }
            }
            return false;
        }

        List<string> SearchAttackChuanKeyNext(Behavior_Transition_Set _set, Inputs_defined _inputKey, List<string> chuan, IDictionary<string, Behavior_Transition_Set> stateTransitionSetDictionary)
        {
            if (!CheckIfStringInList(_set.StateKey, chuan))
            {
                chuan.Add(_set.StateKey);
            }

            Inputs_defined searching_inputKey = Inputs_defined.Null;
            searching_inputKey = _inputKey == Inputs_defined.Null ? _set.enterInput : _inputKey;
            foreach (Behavior_Transition_Set _rset in _set.Casual_To_Behaviours)
            {
                if (_rset.enterInput == searching_inputKey && _rset.enterInput != Inputs_defined.Null)
                {
                    stateTransitionSetDictionary.TryGetValue(_rset.StateKey, out Behavior_Transition_Set _new);
                    if (_new != null)
                    {
                        if (!CheckIfStringInList(_rset.StateKey, chuan))
                        {
                            if (SearchAttackChuanKeyNext(_new, searching_inputKey, chuan, stateTransitionSetDictionary) != null)
                            {
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("字典中不存在：" + _rset.StateKey);
                    }
                }
            }
            return chuan;
        }

        public void SetStateRatesByAILevel(IDictionary<string, Behavior_Transition_Set> final_dic)
        {
            string myMoveStateKey = null;
            //为什么一定要把这些技能串给提前搜出来？事关随着等级提升“解锁技能的处理”。在我们的系统当中技能的首发概率和连段概率是不一样的
            //但无论首发和连段再不一样，我们想把技能解锁这个概念给突出来。如果一个技能没有解锁，那无论是首发也好连段也好这个技能不应该出现。
            //而且在控制模式下也不应该能发出来
            List<string> attackChuan = new List<string>();
            List<string> Fire1Chuan = new List<string>();
            List<string> Fire2Chuan = new List<string>();

            //第一轮循环所应该做的就是把Attack，Fire1，Fire2这三个系列的技能串儿搜出来。
            foreach (KeyValuePair<string, Behavior_Transition_Set> Transition in final_dic)
            {
                if (Transition.Key == "Move_normal" || Transition.Key == "Move_slow" || Transition.Key == "Move_fast" || Transition.Key == "Test_Move")
                {
                    myMoveStateKey = Transition.Key;
                }

                if (Transition.Value.enterInput == Inputs_defined.Attack)
                {
                    attackChuan = SearchAttackChuanKeyNext(Transition.Value, Inputs_defined.Null, attackChuan, final_dic);
                }
                if (Transition.Value.enterInput == Inputs_defined.Fire1)
                {
                    Fire1Chuan = SearchAttackChuanKeyNext(Transition.Value, Inputs_defined.Null, Fire1Chuan, final_dic);
                }
                if (Transition.Value.enterInput == Inputs_defined.Fire2)
                {
                    Fire2Chuan = SearchAttackChuanKeyNext(Transition.Value, Inputs_defined.Null, Fire2Chuan, final_dic);
                }
                //在这里把三大攻击串给算出来，无非是说他们的主串包含的技能都有啥名字，这个信息不包括他们各自可能出现的首尾循环
            }

            foreach (KeyValuePair<string, Behavior_Transition_Set> Transition in final_dic)
            {
                // 三大首发技能AI模式下概率
                if (Transition.Value.enterInput == Inputs_defined.Attack)
                {
                    List<Behavior_Transition_Set> casual_to_states_now = Transition.Value.Casual_To_Behaviours.ToList();
                    List<Behavior_Transition_Set> casual_to_states_after = new List<Behavior_Transition_Set>();

                    foreach (Behavior_Transition_Set _set in casual_to_states_now)
                    {
                        //接下来这轮分析是整个概率适配系统的关键
                        if (_set.StateKey != myMoveStateKey)
                        {
                            //然后？概率应该适配多少？
                            if (attackChuan.Contains(_set.StateKey) || Fire1Chuan.Contains(_set.StateKey) || Fire2Chuan.Contains(_set.StateKey))
                            {
                                Behavior_Transition_Set _freshNew = new Behavior_Transition_Set(
                                _set.StateKey,
                                _set.stateType,
                                _set.AT,
                                _set.AI_trigger_ranges,
                                _set.can_be_cancelled_to,
                                _set.enterInput, _set.exitInput,
                                _set.SPLevel);
                                casual_to_states_after.Add(_freshNew);
                            }
                        }
                    }
                    Transition.Value.Casual_To_Behaviours = casual_to_states_after.ToArray();
                }

                if (Transition.Value.enterInput == Inputs_defined.Fire1)
                {
                    List<Behavior_Transition_Set> casual_to_states_now = Transition.Value.Casual_To_Behaviours.ToList();
                    List<Behavior_Transition_Set> casual_to_states_after = new List<Behavior_Transition_Set>();

                    foreach (Behavior_Transition_Set _set in casual_to_states_now)
                    {
                        //接下来这轮分析是整个概率适配系统的关键
                        if (_set.StateKey != myMoveStateKey)
                        {
                            //然后？概率应该适配多少？
                            if (attackChuan.Contains(_set.StateKey) || Fire1Chuan.Contains(_set.StateKey) || Fire2Chuan.Contains(_set.StateKey))
                            {
                                Behavior_Transition_Set _freshNew = new Behavior_Transition_Set(
                                    _set.StateKey,
                                    _set.stateType,
                                    _set.AT,
                                    _set.AI_trigger_ranges,
                                    _set.can_be_cancelled_to,
                                    _set.enterInput, _set.exitInput, _set.SPLevel);
                                casual_to_states_after.Add(_freshNew);
                            }
                        }
                    }
                    Transition.Value.Casual_To_Behaviours = casual_to_states_after.ToArray();
                }

                if (Transition.Value.enterInput == Inputs_defined.Fire2)
                {
                    List<Behavior_Transition_Set> casual_to_states_now = Transition.Value.Casual_To_Behaviours.ToList();
                    List<Behavior_Transition_Set> casual_to_states_after = new List<Behavior_Transition_Set>();

                    foreach (Behavior_Transition_Set _set in casual_to_states_now)
                    {
                        //接下来这轮分析是整个概率适配系统的关键
                        if (_set.StateKey != myMoveStateKey)
                        {
                            //然后？概率应该适配多少？
                            if (attackChuan.Contains(_set.StateKey) || Fire1Chuan.Contains(_set.StateKey) || Fire2Chuan.Contains(_set.StateKey))
                            {
                                Behavior_Transition_Set _freshNew = new Behavior_Transition_Set(
                                    _set.StateKey,
                                    _set.stateType,
                                    _set.AT,
                                    _set.AI_trigger_ranges,
                                    _set.can_be_cancelled_to,
                                    _set.enterInput, _set.exitInput,
                                    _set.SPLevel);
                                casual_to_states_after.Add(_freshNew);
                            }
                        }
                    }
                    Transition.Value.Casual_To_Behaviours = casual_to_states_after.ToArray();
                }

                //非首发
                if ((Transition.Value.enterInput != Inputs_defined.Fire2
                     &&
                     Transition.Value.enterInput != Inputs_defined.Fire1
                     && Transition.Value.enterInput != Inputs_defined.Attack)
                   &&
                    (attackChuan.Contains(Transition.Value.StateKey) || Fire1Chuan.Contains(Transition.Value.StateKey) || Fire2Chuan.Contains(Transition.Value.StateKey)))
                {
                    List<Behavior_Transition_Set> casual_to_states_now = Transition.Value.Casual_To_Behaviours.ToList();
                    List<Behavior_Transition_Set> casual_to_states_after = new List<Behavior_Transition_Set>();

                    foreach (Behavior_Transition_Set _set in casual_to_states_now)
                    {
                        //接下来这轮分析是整个概率适配系统的关键
                        if (_set.StateKey != myMoveStateKey
                           &&
                            (attackChuan.Contains(_set.StateKey) || Fire1Chuan.Contains(_set.StateKey) || Fire2Chuan.Contains(_set.StateKey))
                           )//这个环节现在决定了第二次连击后如果有本串之外的选择概率也被修改为了allevel  .
                            //然而，这第二个条件给增加了一个非常明显的限制，那就是接续在任何一个技能后的续技能必须是attack，fire1，fire2主串上的，
                            //比如说角色如果第一招是发出一团火焰，第二招可能是把火焰踢出去或拿着这团火给前方一拳，如果火焰和踢火都是靠attack键发动，火焰圈是靠fire1键接续，那么火焰拳必须是fire1或fire2攻击串
                            //里的一个环节。
                            //如果把这第二个条件去掉将产生以下结果：玩家每在满一个20级周期时解锁了新技能后，如果这个新技能有后续技，那这个后续技也会发动出来，但无法首发（这个可改）
                            //并且任何一个技能的后续技能可以完全独立存在，比如点了attack后，点fire1或fire2可以接续出一个不在fire1，fire2主攻击串上的隐藏技能
                        {
                            Behavior_Transition_Set _freshNew = new Behavior_Transition_Set(
                                    _set.StateKey,
                                    _set.stateType,
                                    _set.AT,
                                    _set.AI_trigger_ranges,
                                    _set.can_be_cancelled_to,
                                    _set.enterInput, _set.exitInput,
                                    _set.SPLevel);
                            casual_to_states_after.Add(_freshNew);
                        }
                    }
                    Transition.Value.Casual_To_Behaviours = casual_to_states_after.ToArray();
                }

                // 移动概率
                if (Transition.Key == "Move_normal"
                    ||
                    Transition.Key == "Move_slow"
                    ||
                    Transition.Key == "Move_fast"
                    ||
                    Transition.Key == "Test_Move")
                {
                    Transition.Value.Casual_To_Behaviours = new Behavior_Transition_Set[] { };
                }
            }
        }
    }
}