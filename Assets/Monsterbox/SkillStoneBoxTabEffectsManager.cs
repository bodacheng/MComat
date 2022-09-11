using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace mainMenu
{
    public class SkillStoneBoxTabEffectsManager : MonoBehaviour
    {
        readonly IDictionary<Element, ElementStoneTagsGroup> _BtnEffects = new Dictionary<Element, ElementStoneTagsGroup>();
        ElementStoneTagsGroup _focusingEffectsGroup;
        ParticleSystem triggerExplosion0;

        public async UniTask StartUp(Element element)
        {
            if (_BtnEffects.ContainsKey(element))
                return;
            var zt = new ElementStoneTagsGroup();
            await zt.INI_forSkillStoneBox(element, transform);
            _BtnEffects.Add(element, zt);
            var path = FightGlobalSetting.EffectPathDefine(element);
            triggerExplosion0 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion0.prefab");
            triggerExplosion0.transform.SetParent(transform);
        }
        
        public void CloseShowingZokuseiTagEffects()
        {
            if (_focusingEffectsGroup != null)
                _focusingEffectsGroup.CloseTagEffects();
            foreach (var VARIABLE in _BtnEffects)
            {
                VARIABLE.Value.Clear();
            }
        }

        public void SetSelectedTabPos(int ex)
        {
            _focusingEffectsGroup?.SetSelectedTabPos(ex);
        }
        
        public async UniTask SwitchZokusei(Element element, Action refreshTabEffects)
        {
            await StartUp(element);
            if (_focusingEffectsGroup != null)
            {
                _focusingEffectsGroup.CloseTagEffects();
            }
            
            if (_BtnEffects.ContainsKey(element))
            {
                _focusingEffectsGroup = _BtnEffects[element];
            }else{
                Debug.Log("见鬼了。检查手机控制器渲染模块加载顺序");
            }
            
            refreshTabEffects.Invoke();
        }
        
        public void RefreshTagEffect(Vector3 pos, int sp_level)//按钮切换也可以在这里做文章
        {
            _focusingEffectsGroup.RefreshSTBoxEffects(sp_level, pos);
        }
        
        public void RefreshSlotEffect(int slotNum ,Vector3 pos, int sp_level)//按钮切换也可以在这里做文章
        {
            _focusingEffectsGroup.RefreshSlotEffects(slotNum, sp_level, pos, transform);
        }
        
        public void SkillButtonExplosion(int spLevel, Vector3 targetPOS, Transform parent)
        {
            var pressedExplosion = _focusingEffectsGroup.btnPressedEffects.ContainsKey(spLevel) ?
            _focusingEffectsGroup.btnPressedEffects[spLevel] : triggerExplosion0;
            pressedExplosion.gameObject.name = "UIexplosion" + spLevel;
            pressedExplosion.transform.position = targetPOS;
            pressedExplosion.Play();
            pressedExplosion.transform.SetParent(parent);
        }
    }
}