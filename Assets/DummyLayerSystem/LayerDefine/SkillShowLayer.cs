using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill;
using System.Linq;
using DG.Tweening;
using UniRx;

namespace mainMenu
{
    // 这个模块首先要有对脚本进行分析的能力。
    // 如果一个九宫格存在相同技能重复登陆，本脚本的功能会出现问题，具体是因为analysisStatesSetDic的机制(以技能key寻找状态从而寻找按钮。)
    public class SkillShowLayer : UILayer
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] Button PageBGBtn;
        [SerializeField] GameObject flowParticle;
        [SerializeField] Button SkillButton;
        [SerializeField] RectTransform attacksT;
        [SerializeField] RectTransform fire1T;
        [SerializeField] RectTransform fire2T;

        [Space(11)]
        [Header("Runner")]
        [SerializeField] SingleThreadProcesser runner;

        [Space(11)]
        [Header("Skill Info")]
        [SerializeField] SkillStoneDetail _skillStoneDetail;
        
        public SkillStoneBoxTabEffectsManager EffectsManager;
        
        List<GameObject> floatingMarks = new List<GameObject>();
        IDictionary<string, Button> StateButtonDic = new Dictionary<string, Button>();
        IDictionary<string, SkillEntity> analysisSKList = new Dictionary<string, SkillEntity>();
        
        public static SkillShowLayer Open()
        {
            UILayer l = UILayerLoader.Get("SkillShowLayer");
            SkillShowLayer returnValue;
            if (l != null)
            {
                returnValue = l as SkillShowLayer;
                return returnValue;
            }
            l = UILayerLoader.Load(PreScene.target.T,"SkillShowLayer") as SkillShowLayer;
            returnValue = l as SkillShowLayer;
            return returnValue;
        }
        
        void LateUpdate()
        {
            SkillShowSupporter.SkillsPrintOutLateUpdate();
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
                runner.RunAsQueued(SkillShowSupporter.SkillShowRunWithPrepare(_SE.REAL_NAME));                
                SkillShowSupporter.IfShowingSkill = true;
                
                // 这个就是强行把技能盒子附带的那个点击触效给拿过来用了。
                EffectsManager.SkillButtonExplosion(_SE.SP_LEVEL, _button.transform.position, transform);
            }
            _button.onClick.AddListener(showSkillInfo);
        }
        
        // 生成技能迁移表示符
        void BuildSkillFlowParticle(Transform startT, Transform endT)
        {
            GameObject particle = Instantiate(flowParticle);
            floatingMarks.Add(particle);
            particle.SetActive(true);
            particle.transform.SetParent(startT);
            particle.transform.position = startT.position;
            particle.transform.localScale = Vector3.one;
            particle.transform.DOMove(endT.position,1f).OnComplete(DestroyFloatingMarks);
        }
        
        // 技能按钮渲染与处理
        List<GameObject> renderPs = new List<GameObject>();
        async void RenderButton(Zokusei zokusei, GameObject button, int splevel)
        {
            await Observable.TimerFrame(5);
            GameObject t = ZokuseiStoneTagsGroup.CreateOneButtonIcon(zokusei,splevel);
            t.layer = 5;//UI Layer
            foreach (Transform _t in t.transform)
            {
                _t.gameObject.layer = 5;
            }

            t.transform.position = 
                PosCal.GetWorldPos(PreScene.target.FxCamera, 
                    PosCal.ConvertAnchorPos(button.GetComponent<RectTransform>().anchoredPosition, Vector2.one, Vector2.zero )
                    , 20f);
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
        public void SkillsPrintPageRefresh(UnitInfo _watchingCharInfo)
        {
            UnitConfig unitConfig = Units.GetUnitConfig(_watchingCharInfo.r_id);
            PageBGBtn.onClick.RemoveAllListeners();
            if (_watchingCharInfo != null && _watchingCharInfo.set != null)
            {
                SkillScriptReader(_watchingCharInfo.set, unitConfig._zokusei);
            }
            void BFBtnForRefresh()
            {
                SkillsPrintPageRefresh(_watchingCharInfo);
            }
            PageBGBtn.onClick.AddListener(BFBtnForRefresh);
        }

        // 打印出技能显示画面
        void SkillScriptReader(SkillSet nineAndTwo, Zokusei zokusei)
        {
            EffectsManager.StartUp(zokusei);
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
                
                newShow.transform.SetParent(transform);
                Vector3 pos = attacksT.transform.position;
                
                newShow.transform.position = pos + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
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
                
                newShow.transform.SetParent(transform);
                Vector3 pos = fire1T.transform.position;
                newShow.transform.position = pos + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
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
                
                newShow.transform.SetParent(transform);
                Vector3 pos = fire2T.transform.position;
                newShow.transform.position = pos + Vector3.right * newShow.GetComponent<RectTransform>().rect.width * (i - 1) + Vector3.right * 200f * (i - 1);
                newShow.gameObject.SetActive(true);
                RenderButton(zokusei,newShow.gameObject,Fire2_chuan[i].SP_LEVEL);
            }
        }

        #region 表情测试相关
        public void Face_CloseEye()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("close_eye");
            }
        }
        public void Face_Suprise()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("suprise");
            }
        }
        public void Face_Angry()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("angry");
            }
        }
        public void Face_Pain()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("hurt");
            }
        }
        public void Face_Smile()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("smile");
            }
        }
        public void Face_Evil()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("evil");
            }
        }
        public void Face_Ferocious()
        {
            if (SkillShowSupporter.focusingC != null)
            {
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("face_reset");
                SkillShowSupporter.focusingC.Animation_Manger.SetTrigger("ferocious");
            }
        }
        #endregion
    }
}