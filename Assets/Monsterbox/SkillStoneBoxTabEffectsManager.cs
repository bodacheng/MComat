using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace mainMenu
{
    public class SkillStoneBoxTabEffectsManager : MonoBehaviour
    {
        readonly IDictionary<Element, ElementStoneTagsGroup> _BtnEffects = new Dictionary<Element, ElementStoneTagsGroup>();
        ElementStoneTagsGroup _focusingEffectsGroup;
        ParticleSystem triggerExplosion0;

        public async UniTask StartUp(Element element, CancellationToken ct = default)
        {
            if (_BtnEffects.ContainsKey(element))
                return;
            var zt = new ElementStoneTagsGroup();
            await zt.INI_forSkillStoneBox(element, transform);
            ct.ThrowIfCancellationRequested();
            _BtnEffects.Add(element, zt);
            var path = FightGlobalSetting.EffectPathDefine(element);
            triggerExplosion0 = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion0.prefab");
            ct.ThrowIfCancellationRequested();
            triggerExplosion0.transform.SetParent(transform);
        }
        
        public void CloseShowingTagEffects()
        {
            _focusingEffectsGroup?.CloseTagEffects();
            foreach (var kv in _BtnEffects)
            {
                kv.Value.Clear();
            }
        }

        public void SetSelectedTabPos(int ex)
        {
            _focusingEffectsGroup?.SetSelectedTabPos(ex);
        }
        
        public async UniTask SwitchElement(Element element, Action refreshTabEffects, CancellationToken ct)
        {
            ProgressLayer.Loading(">", PreScene.target.T);
            await StartUp(element, ct);
            ct.ThrowIfCancellationRequested();
            _focusingEffectsGroup?.CloseTagEffects();
            if (_BtnEffects.ContainsKey(element))
            {
                _focusingEffectsGroup = _BtnEffects[element];
            }else{
                Debug.Log("fatal error element tags");
            }
            refreshTabEffects.Invoke();
            ProgressLayer.Close();
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