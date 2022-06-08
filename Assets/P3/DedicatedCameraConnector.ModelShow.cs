using UnityEngine;
using System.Collections;
using dataAccess;
using mainMenu;
using Singleton;

namespace Cocone.ProjectP3
{
    public partial class DedicatedCameraConnector : MonoBehaviour
    {
        public void ShowMyModel(string instanceID)
        {
            StartCoroutine(_ShowMyModel(instanceID));
        }
        
        GameObject _model;
        Data_Center focusingC;
        public Data_Center FocusingC => focusingC;
        
        string FocusRId;
        bool IfShowingSkill = false;
        
        IEnumerator _ShowMyModel(string instanceID)
        {
            var info = MyMonsters.Get(instanceID);
            var p = _ShowModel(info?.r_id);
            yield return p;
            yield return p.Current;
        }
        
        public void ShowModel(string recordID)
        {
            StartCoroutine(_ShowModel(recordID));
        }
        
        IEnumerator _ShowModel(string recordID) 
        {
            PopupLayer.Loading(">", PreScene.target.T);
            if (_model != null)
            {
                DestroyImmediate(_model);
                _model = null;
            }
            if (recordID == null)
            {
                yield break;
            }
            
            var p = GeneralModelPool.GetModel(recordID, transform);
            yield return p;
            if (p.Current == null)
            {
                Debug.Log("模型错误");
                yield break;
            }
            
            var _dataCenter = p.Current;
            if (_dataCenter == null)
            {
                Debug.Log("模型错误");
                focusingC = null;
                FocusRId = null;
                _model = null;
                yield break;
            }
            focusingC = (Data_Center)_dataCenter;
            focusingC.WholeT.SetParent(transform); // 尽量确保模型总与图层一起被摧毁
            FocusRId = recordID;
            focusingC.Animation_Manger.AnimatorRef.applyRootMotion = true;
            
            // 这个短暂变色是为了掩盖一些模型刚加载瞬间有些渲染没到位的尴尬。比如裙子摇晃 
            focusingC._ShaderManager.FlatColorForAShortTime(Color.black, 0f, 1f);
            
            _model = focusingC.WholeT.gameObject;
            _model.SetActive(true);
            
            Initialize(false,_model.transform, transform, PreScene.target.FxCamera);
            ItemDetailStartDirection(0,0,0);
            
            PopupLayer.Close();
            yield return _model;
        }

        public void SkillShowRun(string skillName)
        {
            StartCoroutine(SkillShowRunWithPrepare(skillName));
        }
        
        IEnumerator SkillShowRunWithPrepare(string skillName)
        {
            var unitConfig = Units.GetUnitConfig(FocusRId);
            if (unitConfig == null)
                yield break;
            
            if (focusingC.Animation_Manger != null)
            {
                yield return focusingC.Animation_Manger.PreloadPersonalAnimResourceMode(unitConfig.TYPE, skillName, unitConfig.SPECIAL_ZOKUSEI, unitConfig.element);
                IfShowingSkill = true;
                focusingC.Animation_Manger.AnimationTrigger(skillName, true, 0.05f);
            }
        }
        
        void SkillsPrintOutLateUpdate()
        {
            if (focusingC != null && focusingC.Animation_Manger != null && focusingC.WholeT.gameObject.activeSelf)
            {
                if (focusingC.Animation_Manger.GetBool("in_transition") == false && 
                    focusingC.Animation_Manger.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
                {
                    focusingC.Animation_Manger.PlayLayerAnim(null, true, 0.05f);
                    IfShowingSkill = false;
                }
            }
        }
    }
}