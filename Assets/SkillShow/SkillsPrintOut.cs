using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill;
using System.Linq;
using DG.Tweening;

namespace mainMenu
{
    // 这个模块首先要有对脚本进行分析的能力。
    // 如果一个九宫格存在相同技能重复登陆，本脚本的功能会出现问题，具体是因为analysisStatesSetDic的机制(以技能key寻找状态从而寻找按钮。)
    public class SkillsPrintOut : MonoBehaviour
    {
        public Button skillInfoGamenBackGroundButton;
        public GameObject skillflowparticle;
        List<GameObject> floatingMarks = new List<GameObject>();
        
        public Button SkillButton;
        public RectTransform attacksT;
        public RectTransform fire1T;
        public RectTransform fire2T;

        [Space(11)]
        [Header("Runner")]
        public SingleThreadProcesser runner;

        [Space(11)]
        [Header("Skill Info")]
        public SkillStoneDetail _skillStoneDetail;
        
        public string focusCharConfigID;
        public Data_Center focusingC;
        
        IDictionary<string, Button> StateButtonDic = new Dictionary<string, Button>();
        IDictionary<string, SkillEntity> analysisSKList = new Dictionary<string, SkillEntity>();
        
        void LateUpdate()
        {
            SkillsPrintOutLateUpdate();
        }
        
        // 清理技能迁移表示符
        void DestroyFloatingMarks()
        {
            foreach (GameObject _particle in floatingMarks)
            {
                Destroy(_particle);
            }
            floatingMarks.Clear();
        }
        
        public bool IfShowingSkill { get; private set; } = false;
        public void SkillsPrintOutLateUpdate()
        {
            if (focusingC != null)
            {
                if (focusingC.Animation_Manger != null && focusingC.Animation_Manger.Animator != null && focusingC.WholeT.gameObject.activeSelf)
                {
                    if (focusingC.Animation_Manger.Animator.GetBool("in_transition") == false && focusingC.Animation_Manger.Animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
                    {
                        //SkillShowT.gameObject.SetActive(true);
                        focusingC.Animation_Manger.PlayLayerAnim(null, true, 0.05f);
                        IfShowingSkill = false;
                        PreScene.target._CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
                    }
                }
            }
        }
        
        // 列表变字典
        IDictionary<string, SkillEntity> LToD(List<SkillEntity> _analysisStatesList)
        {
            analysisSKList.Clear();
            foreach (SkillEntity _set in _analysisStatesList)
            {
                if (_set == null)
                {
                    continue;
                }
                if (!analysisSKList.ContainsKey(_set.REAL_NAME))
                {
                    analysisSKList.Add(_set.REAL_NAME, _set);
                }
            }
            return analysisSKList;
        }
        
        // 为打印出的技能按钮添加功能
        void AddShowSkillInfoFeature(Button _button, SkillEntity _SE)
        {
            _button.onClick.RemoveAllListeners();
            void showSkillInfo()
            {
                DestroyFloatingMarks();
                _skillStoneDetail.RefreshInfo(_SE);
                foreach (string _set in _SE.CasualTo)
                {
                    analysisSKList.TryGetValue(_set, out SkillEntity _oneCasualTo);
                    if (_oneCasualTo != null)
                    {
                        StateButtonDic.TryGetValue(_oneCasualTo.REAL_NAME, out Button CasualToButton);
                        if (_button != null && CasualToButton != null)
                        {
                            BuildSkillFlowParticle(_button.transform, CasualToButton.transform);
                        }
                    }
                }

                //////// 超级功能 ////////
                runner.RunAsQueued(SkillShowRunWithPrepare(_SE.REAL_NAME));                
                IfShowingSkill = true;
                
                // 这个就是强行把技能盒子附带的那个点击触效给拿过来用了。
                SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(_SE.SP_LEVEL, _button.transform.position, transform);
            }
            _button.onClick.AddListener(showSkillInfo);
        }
        
        // 九宫格内技能显示功能
        public IEnumerator SkillShowRunWithPrepare(string keyname)
        {
            CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(focusCharConfigID);
            //下面这一大片，在资源存在的情况下压根不应该运行            
            if (focusingC.Animation_Manger != null)
            {
                switch (ResourceLoadingSetting.AnimationLoadingMode)
                {
                    case ResourceLoadMode.CachAB:
                        yield return focusingC.Animation_Manger.PreloadPersonalAnim(ResourceDownLoad.BundleURL, _CharConfig.TYPE, keyname, _CharConfig.SPECIAL_ZOKUSEI, _CharConfig._zokusei);
                    break;
                    case ResourceLoadMode.StreamingAssetAB:
                        yield return focusingC.Animation_Manger.PreloadPersonalAnimStreamingAssetMode(_CharConfig.TYPE, keyname, _CharConfig.SPECIAL_ZOKUSEI, _CharConfig._zokusei);
                    break;
                    case ResourceLoadMode.Resource:
                        yield return focusingC.Animation_Manger.PreloadPersonalAnimResourceMode(_CharConfig.TYPE, keyname, _CharConfig.SPECIAL_ZOKUSEI, _CharConfig._zokusei);
                    break;
                }
                IfShowingSkill = true;
                focusingC.Animation_Manger.AnimationTrigger(keyname, true, 0.05f);
            }
        }
        
        // 生成技能迁移表示符
        void BuildSkillFlowParticle(Transform startT, Transform endT)
        {
            GameObject particle = Instantiate(skillflowparticle);
            floatingMarks.Add(particle);
            particle.SetActive(true);
            particle.transform.SetParent(startT);
            particle.transform.position = startT.position;
            particle.transform.localScale = Vector3.one;
            particle.transform.DOMove(endT.position,1f).OnComplete(DestroyFloatingMarks);
        }
        
        // 技能按钮渲染与处理
        List<GameObject> renderPs = new List<GameObject>();
        void RenderButton(Zokusei zokusei, GameObject button, int splevel)
        {
            GameObject t = ZokuseiStoneTagsGroup.CreateOneButtonIcon(zokusei,splevel);
            t.layer = 5;//UI Layer
            foreach (Transform _t in t.transform)
            {
                _t.gameObject.layer = 5;
            }
            t.transform.position = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, button.GetComponent<RectTransform>(), 5f);
            renderPs.Add(t);
        }
        // 离开技能展示画面的时候必须要清除掉不要的特效
        public void ClearRenderPs()
        {
            foreach (GameObject _t in renderPs)
            {
                Destroy(_t);
            }
            renderPs.Clear();
        }
        
        //根据锁定的技能组，角色，来打印出所有技能按钮，以及背景按钮。
        public void SkillsPrintGamenRefresh(UnitInfo _watchingCharInfo)
        {
            CharConfig CharConfig = MonstersConfigTable.GetCharConfig(_watchingCharInfo.r_id);
            skillInfoGamenBackGroundButton.onClick.RemoveAllListeners();
            if (_watchingCharInfo != null && _watchingCharInfo.set != null)
            {
                SkillScriptReader(_watchingCharInfo.set, CharConfig._zokusei);
            }
            void backGroundButtonforRefresh()
            {
                SkillsPrintGamenRefresh(_watchingCharInfo);
            }
            skillInfoGamenBackGroundButton.onClick.AddListener(backGroundButtonforRefresh);
        }

        // 打印出技能显示画面
        public void SkillScriptReader(SkillSet nineAndTwo, Zokusei zokusei)
        {
            DestroyFloatingMarks();
            ClearRenderPs();
            _skillStoneDetail.RefreshInfo((SkillEntity)null);
            
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
            
            List<SkillEntity> SkillEntity_List = new List<SkillEntity>();
            SkillEntity_List.AddRange(nineAndTwo.GetAttack1Chuan().Values.ToList());
            SkillEntity_List.AddRange(nineAndTwo.GetAttack2Chuan().Values.ToList());
            SkillEntity_List.AddRange(nineAndTwo.GetAttack3Chuan().Values.ToList());
            analysisSKList = LToD(SkillEntity_List);
            StateButtonDic.Clear();
            
            IDictionary<int, SkillEntity> attack_chuan = nineAndTwo.GetAttack1Chuan();
            for (int i = 1; i < 4; i++)
            {
                if (attack_chuan[i] == null)
                {
                    continue;
                }
                Button newShow = Instantiate(SkillButton);
                AddShowSkillInfoFeature(newShow, attack_chuan[i]);
                StateButtonDic.Add(attack_chuan[i].REAL_NAME, newShow);
                newShow.name = attack_chuan[i].REAL_NAME;
                newShow.transform.SetParent(attacksT);
                newShow.transform.localScale = new Vector3(1, 1, 1);
                newShow.transform.localPosition = Vector3.zero;
                newShow.transform.localPosition = Vector3.zero + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
                RenderButton(zokusei,newShow.gameObject,attack_chuan[i].SP_LEVEL);
            }

            IDictionary<int, SkillEntity> Fire1_chuan = nineAndTwo.GetAttack2Chuan();
            for (int i = 1; i < 4; i++)
            {
                if (Fire1_chuan[i] == null)
                {
                    continue;
                }
                Button newShow = Instantiate(SkillButton);
                AddShowSkillInfoFeature(newShow, Fire1_chuan[i]);
                StateButtonDic.Add(Fire1_chuan[i].REAL_NAME, newShow);
                newShow.name = Fire1_chuan[i].REAL_NAME;
                newShow.transform.SetParent(fire1T);
                newShow.transform.localScale = new Vector3(1, 1, 1);
                newShow.transform.localPosition = Vector3.zero;
                newShow.transform.localPosition = Vector3.zero + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
                RenderButton(zokusei,newShow.gameObject,Fire1_chuan[i].SP_LEVEL);
            }
             
            IDictionary<int, SkillEntity> Fire2_chuan = nineAndTwo.GetAttack3Chuan();
            for (int i = 1; i < 4; i++)
            {
                if (Fire2_chuan[i] == null)
                {
                    continue;
                }
                Button newShow = Instantiate(SkillButton);
                AddShowSkillInfoFeature(newShow, Fire2_chuan[i]);
                StateButtonDic.Add(Fire2_chuan[i].REAL_NAME, newShow);
                newShow.name = Fire2_chuan[i].REAL_NAME;
                newShow.transform.SetParent(fire2T);
                newShow.transform.localScale = new Vector3(1, 1, 1);
                newShow.transform.localPosition = Vector3.zero;
                newShow.transform.localPosition = Vector3.zero + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
                RenderButton(zokusei,newShow.gameObject,Fire2_chuan[i].SP_LEVEL);
            }
        }
                
        // 表情测试相关
        public void Face_CloseEye()
        {
            if (focusingC != null)
            {
                if (focusingC != null)
                {
                    focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                    focusingC.Animation_Manger.Animator.SetTrigger("close_eye");
                }
            }
        }
        public void Face_Suprise()
        {
            if (focusingC != null)
            {
                focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                focusingC.Animation_Manger.Animator.SetTrigger("suprise");
            }
        }
        public void Face_Angry()
        {
            if (focusingC != null)
            {
                focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                focusingC.Animation_Manger.Animator.SetTrigger("angry");
            }
        }
        public void Face_Pain()
        {
            if (focusingC != null)
            {
                focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                focusingC.Animation_Manger.Animator.SetTrigger("hurt");
            }
        }
        public void Face_Smile()
        {
            if (focusingC != null)
            {
                focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                focusingC.Animation_Manger.Animator.SetTrigger("smile");
            }
        }
        public void Face_Evil()
        {
            if (focusingC != null)
            {
                focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                focusingC.Animation_Manger.Animator.SetTrigger("evil");
            }
        }
        public void Face_Ferocious()
        {
            if (focusingC != null)
            {
                focusingC.Animation_Manger.Animator.SetTrigger("face_reset");
                focusingC.Animation_Manger.Animator.SetTrigger("ferocious");
            }
        }
    }
}