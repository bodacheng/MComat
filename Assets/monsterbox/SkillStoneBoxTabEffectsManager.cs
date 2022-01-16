using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class SkillStoneBoxTabEffectsManager : MonoBehaviour
    {
        readonly IDictionary<Zokusei, ZokuseiStoneTagsGroup> zokuseiBtnEffects = new Dictionary<Zokusei, ZokuseiStoneTagsGroup>();
        ZokuseiStoneTagsGroup _focusingEffectsGroup;
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
        
        public void StartUp(Zokusei zokusei)
        {
            if (zokuseiBtnEffects.ContainsKey(zokusei))
                return;
            ZokuseiStoneTagsGroup zt = new ZokuseiStoneTagsGroup();
            zt.INI_forSkillStoneBox(zokusei, transform);
            zokuseiBtnEffects.Add(zokusei, zt);
            string path = null;
            switch (zokusei)
            {
                case Zokusei.blueMagic:
                    path = "blueMagic";
                    break;
                case Zokusei.darkMagic:
                    path = "darkMagic";
                    break;
                case Zokusei.greenMagic:
                    path = "greenMagic";
                    break;
                case Zokusei.lightMagic:
                    path = "lightMagic";
                    break;
                case Zokusei.redMagic:
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
            foreach (var VARIABLE in zokuseiBtnEffects)
            {
                VARIABLE.Value.Clear();
            }
        }
        
        public void SwitchZokusei(Zokusei zokusei)
        {
            StartUp(zokusei);
            if (_focusingEffectsGroup != null)
            {
                _focusingEffectsGroup.CloseTagEffects();
            }
            
            if (zokuseiBtnEffects.ContainsKey(zokusei))
            {
                _focusingEffectsGroup = zokuseiBtnEffects[zokusei];
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