using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class SkillStoneBoxTabEffectsManager : MonoBehaviour
    {
        IDictionary<Zokusei, ZokuseiStoneTagsGroup> zokuseiButtonEffects = new Dictionary<Zokusei, ZokuseiStoneTagsGroup>();
        ZokuseiStoneTagsGroup _focusingButtonEffectsGroup;
        GameObject triggerExplosionPretab0;
        ParticleSystem triggerExplosion0;
        
        public IEnumerator StartUp()
        {
            ZokuseiStoneTagsGroup red = new ZokuseiStoneTagsGroup();
            ZokuseiStoneTagsGroup blue = new ZokuseiStoneTagsGroup();
            ZokuseiStoneTagsGroup green = new ZokuseiStoneTagsGroup();
            ZokuseiStoneTagsGroup dark = new ZokuseiStoneTagsGroup();
            ZokuseiStoneTagsGroup _light = new ZokuseiStoneTagsGroup();
            ZokuseiStoneTagsGroup _default = new ZokuseiStoneTagsGroup();
            
            red.INI_forSkillStoneBox(Zokusei.redMagic,transform);
            blue.INI_forSkillStoneBox(Zokusei.blueMagic,transform);
            green.INI_forSkillStoneBox(Zokusei.greenMagic,transform);
            dark.INI_forSkillStoneBox(Zokusei.darkMagic,transform);
            _light.INI_forSkillStoneBox(Zokusei.lightMagic,transform);
            _default.INI_forSkillStoneBox(Zokusei.Null,transform);
            
            zokuseiButtonEffects.Add(Zokusei.redMagic,red);
            zokuseiButtonEffects.Add(Zokusei.blueMagic,blue);
            zokuseiButtonEffects.Add(Zokusei.greenMagic,green);
            zokuseiButtonEffects.Add(Zokusei.darkMagic,dark);
            zokuseiButtonEffects.Add(Zokusei.lightMagic,_light);
            zokuseiButtonEffects.Add(Zokusei.Null,_default);
            
            triggerExplosionPretab0 = Resources.Load("essentialUIElements/buttonEffects/lightMagic/explosion0", typeof(GameObject)) as GameObject;
            triggerExplosion0 = Instantiate(triggerExplosionPretab0).GetComponent<ParticleSystem>();
            
            yield break;
        }
        
        public void CloseShowingZokuseiTagEffects()
        {
            if (_focusingButtonEffectsGroup != null)
                _focusingButtonEffectsGroup.Close_skillstoneboxtageffects();
        }
        
        public void SwitchZokuseiButtons(Vector3 normaltagpos, Vector3 ex1tagpos, Vector3 ex2tagpos, Vector3 ex3tagpos, Zokusei zokusei)
        {
            if (_focusingButtonEffectsGroup != null)
            {
                _focusingButtonEffectsGroup.Close_skillstoneboxtageffects();
            }
            
            if (zokuseiButtonEffects.ContainsKey(zokusei))
            {
                _focusingButtonEffectsGroup = zokuseiButtonEffects[zokusei];
                RefreshTagEffect(normaltagpos,0);
                RefreshTagEffect(ex1tagpos,1);
                RefreshTagEffect(ex2tagpos,2);
                RefreshTagEffect(ex3tagpos,3);
            }else{
                Debug.Log("见鬼了。检查手机控制器渲染模块加载顺序");
            }
        }

        void RefreshTagEffect(Vector3 pos, int sp_level)//按钮切换也可以在这里做文章
        {
            _focusingButtonEffectsGroup.RefreshSTBoxEffects(sp_level, pos);
        }

        public void SkillButtonExplosion(int splevel, Vector3 targetPOS)
        {
            ParticleSystem pressedExplosion = _focusingButtonEffectsGroup.buttonPressedEffects.ContainsKey(splevel) ?
            _focusingButtonEffectsGroup.buttonPressedEffects[splevel] : triggerExplosion0;
            pressedExplosion.transform.position = targetPOS;
            pressedExplosion.Play();
        }
    }
}