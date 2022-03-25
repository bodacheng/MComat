using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class SkillStoneBoxTabEffectsManager : MonoBehaviour
    {
        readonly IDictionary<Element, ElementStoneTagsGroup> _elementBtnEffects = new Dictionary<Element, ElementStoneTagsGroup>();
        ElementStoneTagsGroup _focusingEffectsGroup;
        GameObject triggerExplosionPrefab;
        ParticleSystem triggerExplosion0;
        
        static GameObject Marker;
        
        void Awake()
        {
            if (Marker == null)
            {
                Marker = new GameObject("ObjectPoolsContainer");
                //UnityEngine.Object.DontDestroyOnLoad(Marker);
            }
        }
        
        public void StartUp(Element element)
        {
            if (_elementBtnEffects.ContainsKey(element))
                return;
            ElementStoneTagsGroup zt = new ElementStoneTagsGroup();
            zt.INI_forSkillStoneBox(element, transform);
            _elementBtnEffects.Add(element, zt);
            string path = null;
            switch (element)
            {
                case Element.blueMagic:
                    path = "blueMagic";
                    break;
                case Element.darkMagic:
                    path = "darkMagic";
                    break;
                case Element.greenMagic:
                    path = "greenMagic";
                    break;
                case Element.lightMagic:
                    path = "lightMagic";
                    break;
                case Element.redMagic:
                    path = "redMagic";
                    break;
                default:
                    path = "lightMagic";
                    break;
            }
            triggerExplosionPrefab = Resources.Load("essentialUIElements/buttonEffects/"+ path + "/explosion0", typeof(GameObject)) as GameObject;
            triggerExplosion0 = Instantiate(triggerExplosionPrefab).GetComponent<ParticleSystem>();
            triggerExplosion0.transform.SetParent(Marker.transform);
        }
        
        public void CloseShowingZokuseiTagEffects()
        {
            if (_focusingEffectsGroup != null)
                _focusingEffectsGroup.CloseTagEffects();
            foreach (var VARIABLE in _elementBtnEffects)
            {
                VARIABLE.Value.Clear();
            }
        }
        
        public void SwitchZokusei(Element element)
        {
            StartUp(element);
            if (_focusingEffectsGroup != null)
            {
                _focusingEffectsGroup.CloseTagEffects();
            }
            
            if (_elementBtnEffects.ContainsKey(element))
            {
                _focusingEffectsGroup = _elementBtnEffects[element];
            }else{
                Debug.Log("见鬼了。检查手机控制器渲染模块加载顺序");
            }
        }
        
        public void RefreshTagEffect(Vector3 pos, int sp_level)//按钮切换也可以在这里做文章
        {
            _focusingEffectsGroup.RefreshSTBoxEffects(sp_level, pos);
        }
        
        public void RefreshSlotEffect(int slotNum ,Vector3 pos, int sp_level)//按钮切换也可以在这里做文章
        {
            _focusingEffectsGroup.RefreshSlotEffects(slotNum, sp_level, pos);
        }
        
        public void SkillButtonExplosion(int splevel, Vector3 targetPOS, Transform parent)
        {
            var pressedExplosion = _focusingEffectsGroup.btnPressedEffects.ContainsKey(splevel) ?
            _focusingEffectsGroup.btnPressedEffects[splevel] : triggerExplosion0;
            pressedExplosion.gameObject.name = "UIexplosion" + splevel;
            pressedExplosion.transform.position = targetPOS;
            pressedExplosion.Play();
            pressedExplosion.transform.SetParent(parent);
        }
    }
}