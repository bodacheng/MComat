using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;
using UnityEngine.UI;
//using VRM;

namespace mainMenu
{
    //这个模块首先要有对脚本进行分析的能力。
    // 有这样一点：如果一个九宫格存在相同技能重复登陆，本脚本的功能会出现问题，具体是因为analysisStatesSetDic的机制(以技能key寻找状态从而寻找按钮。)
    public class SkillsPrintOut : MonoBehaviour
    {
        public SkillShowLines skillShowLines;
        public Button skillInfoGamenBackGroundButton;
        public GameObject skillflowparticle;
        private List<GameObject> DisplayingSkillflowparticle = new List<GameObject>();

        public Button normalattackshowbutton, ex1showbutton, ex2showbutton, ex3showbutton;
        public RectTransform attacksT;
        public RectTransform fire1T;
        public RectTransform fire2T;

        [Space(11)]
        [Header("主界面核心模块")]
        public preparingScene _preparingScene;

        [Space(11)]
        [Header("Skill Info")]
        public Text skillName;
        public Text outRange, farRange, nearRange, closeRange;

        [Space(11)]
        [Header("用以技能显示途中调整。与memberdetail模块对应两个变量一样")]
        public Transform MemDetailWatchPos;
        public RectTransform SkillShowT;

        public int focusingResourceNum;
        public Data_Center focusingCharacterData;

        private IDictionary<int, State_Transition_Set> attack_chuan = new Dictionary<int, State_Transition_Set>();
        private IDictionary<int, State_Transition_Set> Fire1_chuan = new Dictionary<int, State_Transition_Set>();
        private IDictionary<int, State_Transition_Set> Fire2_chuan = new Dictionary<int, State_Transition_Set>();
        private IDictionary<State_Transition_Set, Button> StateButtonDic = new Dictionary<State_Transition_Set, Button>();//按理说这个的key值靠skillid是没问题的。

        private List<List<string>> unsualKeyConnects;

        private IDictionary<string, State_Transition_Set> analysisStatesSetDic = new Dictionary<string, State_Transition_Set>();
        private List<State_Transition_Set> analysisStatesList = new List<State_Transition_Set>();
        private List<Vector3[]> _toDrawLines;

        void LateUpdate()
        {
            SkillsPrintOutLateUpdate();
        }
        
        private bool showingSkill = false;
        public bool ifShowingSkill()
        {
            return showingSkill;
        }
       
        public void SkillsPrintOutLateUpdate()
        {
            if (focusingCharacterData != null)
            {
                if (focusingCharacterData.Animation_Manger != null && focusingCharacterData.Animation_Manger.Animator != null && focusingCharacterData.Animation_Manger.gameObject.activeSelf)
                {
                    if (focusingCharacterData.Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over)
                    {
                        SkillShowT.gameObject.SetActive(true);
                        this.focusingCharacterData.Animation_Manger.PlayLayerAnim(null);
                        this.focusingCharacterData.Animation_Manger.setAnimationPlayingStep(AnimationPlaying_Step.unstarted);
                        this.showingSkill = false;
                        //this.focusingCharacterData.blendShapeProxy.setBlendShapeGrdually(new BlendShapeKey("Angry"), 0f, 50);
                    }
                }
            }
        }

        private IDictionary<string, State_Transition_Set> convertStateSetsListToStateTransitionSetDic(List<State_Transition_Set> analysisStatesList)
        {
            analysisStatesSetDic.Clear();
            foreach (State_Transition_Set _set in analysisStatesList)
            {
                if (!analysisStatesSetDic.ContainsKey(_set.StateKey))
                    analysisStatesSetDic.Add(new KeyValuePair<string, State_Transition_Set>(_set.StateKey, _set));
            }
            return analysisStatesSetDic;
        }

        void addShowSkillInfoFeature(Button _button, State_Transition_Set _state_Transition_Set)
        {
            _button.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction showSkillInfo = () =>
            {
                foreach (GameObject _particle in DisplayingSkillflowparticle)
                {
                    Destroy(_particle);
                }

                foreach (KeyValuePair<State_Transition_Set, Button> keyValuePair in StateButtonDic)
                {
                    if (keyValuePair.Value == _button)
                    {
                        Image buttonimage = keyValuePair.Value.GetComponent<Image>();
                        Color color = buttonimage.color;
                        color.a = 1f;
                        buttonimage.color = color;
                        keyValuePair.Value.transform.localScale = Vector3.one * 1.5f;
                    }
                    else
                    {
                        Image buttonimage = keyValuePair.Value.GetComponent<Image>();
                        Color color = buttonimage.color;
                        color.a = 0.3f;
                        buttonimage.color = color;
                        keyValuePair.Value.transform.localScale = Vector3.one * 1f;
                    }
                }

                if (skillName != null)
                    skillName.text = _state_Transition_Set.StateKey;
                if (closeRange != null)
                    closeRange.text = "x";
                if (nearRange != null)
                    nearRange.text = "x";
                if (farRange != null)
                    farRange.text = "x";
                if (outRange != null)
                    outRange.text = "x";

                foreach (behaviorEnterRange _range in _state_Transition_Set.ai_trigger_ranges)
                {
                    switch (_range)
                    {
                        case behaviorEnterRange.inner_range:
                            if (closeRange != null)
                                closeRange.text = "●";
                            break;
                        case behaviorEnterRange.mid_range:
                            if (nearRange != null)
                                nearRange.text = "●";
                            break;
                        case behaviorEnterRange.far_range:
                            if (farRange != null)
                                farRange.text = "●";
                            break;
                        case behaviorEnterRange.out_of_range:
                            if (outRange != null)
                                outRange.text = "●";
                            break;
                    }
                }

                _toDrawLines = new List<Vector3[]>();
                skillShowLines.drawlines(_toDrawLines);

                //下面这些是逻辑核心
                foreach (State_Rate_Set _set in _state_Transition_Set.casual_to_state_Sets)
                {
                    State_Transition_Set _oneCasualTo;
                    analysisStatesSetDic.TryGetValue(_set.AI_State_Number, out _oneCasualTo);
                    Button CasualToButton;
                    StateButtonDic.TryGetValue(_oneCasualTo, out CasualToButton);

                    if (_button != null && CasualToButton != null)
                    {
                        Image buttonimage = CasualToButton.GetComponent<Image>();
                        Color color = buttonimage.color;
                        color.a = 1f;
                        buttonimage.color = color;
                        buildSkillFlowParticle(_button.transform, CasualToButton.transform);
                    }
                }

                ////////超级功能////////
                if (this.focusingCharacterData.Animation_Manger != null)
                {
                    SkillShowT.gameObject.SetActive(false);
                    this.focusingCharacterData.Animation_Manger.animationTrigger(_state_Transition_Set.StateKey);
                }
                else
                {
                    Debug.Log(" 没能锁定角色动画播放器？ ");
                }
                this.showingSkill = true;
            };
            _button.onClick.AddListener(showSkillInfo);
        }

        public IEnumerator skillShowRunWithPreparing(string keyname)
        {
            CharacterResourceInfo _watchingCharacterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(focusingResourceNum);
            //下面这一大片，在资源存在的情况下压根不应该运行        

            if (this.focusingCharacterData.Animation_Manger != null)
            {
                switch (ResourceLoadingSetting.Instance.AnimationLoadingMode)
                {
                    case ResourceLoadMode.CachAB:
                        yield return
                            this.focusingCharacterData.Animation_Manger.preloadPersonalAnim
                            (ResourceLordSceneStarter.BundleURL, _watchingCharacterResourceInfo.type, keyname, _watchingCharacterResourceInfo.personalMagicPack, _watchingCharacterResourceInfo._zokusei);
                        break;
                    case ResourceLoadMode.StreamingAssetAB:
                        yield return
                            this.focusingCharacterData.Animation_Manger.preloadPersonalAnimStreamingAssetMode
                            (_watchingCharacterResourceInfo.type, keyname, _watchingCharacterResourceInfo.personalMagicPack, _watchingCharacterResourceInfo._zokusei);
                        break;
                    case ResourceLoadMode.Resource:
                        yield return
                            this.focusingCharacterData.Animation_Manger.preloadPersonalAnimResourceMode
                            (_watchingCharacterResourceInfo.type, keyname, _watchingCharacterResourceInfo.personalMagicPack, _watchingCharacterResourceInfo._zokusei);
                        break;
                }
                this.showingSkill = true;
                this.focusingCharacterData.Animation_Manger.animationTrigger(keyname);
                //if (this.focusingCharacterData.blendShapeProxy)
                    //this.focusingCharacterData.blendShapeProxy.setBlendShapeGrdually(new BlendShapeKey("Angry"), 1f, 50);
            }
            yield break;
        }

        void buildSkillFlowParticle(Transform startT, Transform endT)
        {
            UIParallelAnimation oneUIAnimation;

            GameObject particle = Instantiate(skillflowparticle);
            DisplayingSkillflowparticle.Add(particle);
            particle.SetActive(true);

            //particle.transform.localScale = new Vector3(1, 1, 1);
            //particle.transform.localPosition = Vector3.zero;
            oneUIAnimation = particle.GetComponent<UIParallelAnimation>();
            oneUIAnimation.moveAnimation = true;
            oneUIAnimation.loop = EasyUIAnimator.Loop.LOOP;

            //Vector3.Scale(attackChuanButtons[i - 1].GetComponent<RectTransform>().localPosition, EasyUIAnimator.UIAnimator.InvertedScreenDimension);
            particle.transform.SetParent(startT);
            particle.transform.localPosition = Vector3.zero;
            Vector3 S = particle.transform.position;

            particle.transform.SetParent(endT);
            particle.transform.localPosition = Vector3.zero;
            Vector3 F = particle.transform.position;

            particle.transform.SetParent(attacksT);

            if (oneUIAnimation.useScreenValues)
            {
                oneUIAnimation.start[0] = S;
                oneUIAnimation.final[0] = F;
            }
            else
            {
                oneUIAnimation.start[0] = Vector3.Scale(S
                                                        //+ Vector3.right * (newShow.GetComponent<RectTransform>().rect.width)
                                                        , EasyUIAnimator.UIAnimator.InvertedScreenDimension);
                oneUIAnimation.final[0] = Vector3.Scale(F
                                                        //- Vector3.right * (newShow.GetComponent<RectTransform>().rect.width)
                                                        , EasyUIAnimator.UIAnimator.InvertedScreenDimension);
            }

            oneUIAnimation.Play();

            if (_toDrawLines != null)
            {
                Vector3[] oneLineSet = new Vector3[] { oneUIAnimation.start[0], oneUIAnimation.final[0] };
                _toDrawLines.Add(oneLineSet);
                skillShowLines.drawlines(_toDrawLines);
            }
        }

        public void SkillsPrintGamenRefresh(CharacterDataInfo _watchingCharInfo)//根据锁定的技能组，角色，来打印出所有技能按钮，以及背景按钮。
        {
            CharacterResourceInfo _watchingCharacterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(_watchingCharInfo.monsterId);
            skillInfoGamenBackGroundButton.onClick.RemoveAllListeners();
            if (_watchingCharInfo != null && _watchingCharInfo._NineAndTwo != null)
            {
                sKillScriptReader(_watchingCharacterResourceInfo.type,
                                  _watchingCharInfo._NineAndTwo,
                                  _watchingCharacterResourceInfo.getPassiveSkillConfigs(),
                                  _watchingCharInfo._NineAndTwo.level);
            }

            UnityEngine.Events.UnityAction backGroundButtonforRefresh = () =>
            {
                SkillsPrintGamenRefresh(_watchingCharInfo);
            };
            skillInfoGamenBackGroundButton.onClick.AddListener(backGroundButtonforRefresh);
        }

        //从这个环节看，只要AIStateRunner模块有一个把九宫格信息转成最终技能组的函数，就能和SkillsPrintOut模块接轨
        private Button newShow;
        public void sKillScriptReader(string type, NineAndTwo nineAndTwo, passiveSkillConfigs passiveSkillConfigs, int AI_level)
        {
            skillName.text = "";

            foreach (GameObject _particle in DisplayingSkillflowparticle)
                Destroy(_particle);

            foreach (Transform child in attacksT)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in fire1T)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in fire2T)
            {
                Destroy(child.gameObject);
            }

            this.focusingCharacterData.AIStateRunner.FormFightingSetsByNineAndTwo(type, nineAndTwo, AI_level);
            analysisStatesList.Clear();
            analysisStatesList = this.focusingCharacterData.AIStateRunner.State_Transition_Set_List;
            analysisStatesSetDic.Clear();
            analysisStatesSetDic = this.convertStateSetsListToStateTransitionSetDic(analysisStatesList);

            StateButtonDic.Clear();

            attack_chuan.Clear();
            Fire1_chuan.Clear();
            Fire2_chuan.Clear();

            unsualKeyConnects = new List<List<string>>();
            _toDrawLines = new List<Vector3[]>();
            skillShowLines.ClearDrawingLines();

            //////////////////////////

            attack_chuan = nineAndTwo.getAttackChuan();

            for (int i = 1; i < 4; i++)
            {
                if (attack_chuan[i] == null)
                {
                    continue;
                }

                if (attack_chuan[i].SPLevel == 1)
                {
                    newShow = Instantiate(ex1showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.blue;
                }
                else if (attack_chuan[i].SPLevel == 2)
                {
                    newShow = Instantiate(ex2showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.red;
                }
                else if (attack_chuan[i].SPLevel == 3)
                {
                    newShow = Instantiate(ex3showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.white;
                }
                else
                {
                    newShow = Instantiate(normalattackshowbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.yellow;
                }

                addShowSkillInfoFeature(newShow, attack_chuan[i]);
                StateButtonDic.Add(new KeyValuePair<State_Transition_Set, Button>(attack_chuan[i], newShow));

                //newShow.GetComponent<Text>().text = attack_chuan[i].StateKey;
                newShow.name = attack_chuan[i].StateKey;
                newShow.transform.SetParent(attacksT);
                newShow.transform.localScale = new Vector3(1, 1, 1);
                newShow.transform.localPosition = Vector3.zero;
                newShow.transform.localPosition = Vector3.zero + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
            }

            ///////////////////////

            unsualKeyConnects = new List<List<string>>();
            Fire1_chuan = nineAndTwo.getFire1Chuan();

            for (int i = 1; i < 4; i++)
            {
                if (Fire1_chuan[i] == null)
                {
                    continue;
                }

                if (Fire1_chuan[i].SPLevel == 1)
                {
                    newShow = Instantiate(ex1showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.blue;
                }
                else if (Fire1_chuan[i].SPLevel == 2)
                {
                    newShow = Instantiate(ex2showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.red;
                }
                else if (Fire1_chuan[i].SPLevel == 3)
                {
                    newShow = Instantiate(ex3showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.white;
                }
                else
                {
                    newShow = Instantiate(normalattackshowbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.yellow;
                }

                addShowSkillInfoFeature(newShow, Fire1_chuan[i]);
                StateButtonDic.Add(new KeyValuePair<State_Transition_Set, Button>(Fire1_chuan[i], newShow));
                //newShow.GetComponent<Text>().text = attack_chuan[i].StateKey;
                newShow.name = Fire1_chuan[i].StateKey;
                newShow.transform.SetParent(fire1T);
                newShow.transform.localScale = new Vector3(1, 1, 1);
                newShow.transform.localPosition = Vector3.zero;
                newShow.transform.localPosition = Vector3.zero + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
            }

            unsualKeyConnects = new List<List<string>>();
            Fire2_chuan = nineAndTwo.getFire2Chuan();

            for (int i = 1; i < 4; i++)
            {
                if (Fire2_chuan[i] == null)
                {
                    continue;
                }

                if (Fire2_chuan[i].SPLevel == 1)
                {
                    newShow = Instantiate(ex1showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.blue;
                }
                else if (Fire2_chuan[i].SPLevel == 2)
                {
                    newShow = Instantiate(ex2showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.red;
                }
                else if (Fire2_chuan[i].SPLevel ==3)
                {
                    newShow = Instantiate(ex3showbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.white;
                }
                else
                {
                    newShow = Instantiate(normalattackshowbutton);
                    newShow.GetComponentInChildren<Image>().color = Color.yellow;
                }

                addShowSkillInfoFeature(newShow, Fire2_chuan[i]);
                StateButtonDic.Add(new KeyValuePair<State_Transition_Set, Button>(Fire2_chuan[i], newShow));
                //newShow.GetComponent<Text>().text = attack_chuan[i].StateKey;
                newShow.name = Fire2_chuan[i].StateKey;
                newShow.transform.SetParent(fire2T);
                newShow.transform.localScale = new Vector3(1, 1, 1);
                newShow.transform.localPosition = Vector3.zero;
                newShow.transform.localPosition = Vector3.zero + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
            }
        }

        //首先三个基础按键的连按结果必须要显示出来，如果存在非连按键接续，那么只要把这个组给记录下来，在画出连按键后再画个线其实就可以。
        //那么考虑到这个东西还有这样丰富的功能，单纯一个返回值可能得不到所有我们需要的东西.我们从中引入了unsualKeyConnects来记录非寻常连接技能
        List<State_Transition_Set> searchChuanNextAlreadyUseless(State_Transition_Set _set,
                                                   inputs_defined _inputKey,
                                                   List<string> _keyChuan,
                                                   List<State_Transition_Set> chuan,
                                                   IDictionary<string, State_Transition_Set> stateTransitionSetDictionary)
        {
            if (!_keyChuan.Contains(_set.StateKey))
            {
                State_Transition_Set freshNew =
                new State_Transition_Set(
                    _set.StateKey,
                    _set.stateType,
                    _set.AT,
                    _set.ai_trigger_ranges,
                    _set.casual_to_state_Sets,
                    _set.forced_to_state_nums,
                    _set.enterInput,
                    _set.exitInput,
                    _set.SPLevel,
                    _set.skillEmergentLevel,
                    _set.rarelevel
                );
                chuan.Add(freshNew);
                _keyChuan.Add(freshNew.StateKey);
            }

            inputs_defined searching_inputKey = inputs_defined.Null;
            if (_inputKey == inputs_defined.Null)
            {
                searching_inputKey = _set.enterInput;
            }
            else
            {
                searching_inputKey = _inputKey;
            }

            foreach (State_Rate_Set _rset in _set.casual_to_state_Sets)
            {
                if (_rset.enterInput != inputs_defined.Null)
                {
                    if (_rset.enterInput == searching_inputKey)//也就是说这种“chuan”的逻辑其实是说针对有连续输入命令的，自动迁移逻辑不算。并且在这里并不强调一定是同一输入键的攻击串
                    {
                        State_Transition_Set _new = null;
                        stateTransitionSetDictionary.TryGetValue(_rset.AI_State_Number, out _new);
                        if (_new != null)
                        {
                            if (!_keyChuan.Contains(_new.StateKey))
                            {
                                if (searchChuanNextAlreadyUseless(_new, searching_inputKey, _keyChuan, chuan, stateTransitionSetDictionary) != null)
                                {
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                Debug.Log(_set.StateKey + "状态后产生首尾循环");
                                unsualKeyConnects.Add(new List<string> { _set.StateKey, _rset.AI_State_Number });//首尾循环我们看作是种特殊连接
                            }
                        }
                    }
                    else
                    {
                        //非寻常攻击串
                        if (unsualKeyConnects != null)
                        {
                            unsualKeyConnects.Add(new List<string> { _set.StateKey, _rset.AI_State_Number });
                        }
                    }
                }
            }
            return chuan;
        }
    }
}