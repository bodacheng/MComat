using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class SkillStoneBoxTabEffectsManager : MonoBehaviour
    {
        public SkillStonesBox skillStonesBox;
        IDictionary<zokusei, zokuseiSkillStoneTagsGroup> zokuseiButtonEffects = new Dictionary<zokusei, zokuseiSkillStoneTagsGroup>();
        zokuseiSkillStoneTagsGroup _focusingButtonEffectsGroup;
        GameObject triggerExplosionPretab0;
        ParticleSystem triggerExplosion0;
        
        public IEnumerator startUp()
        {
            zokuseiSkillStoneTagsGroup red = new zokuseiSkillStoneTagsGroup();
            zokuseiSkillStoneTagsGroup blue = new zokuseiSkillStoneTagsGroup();
            zokuseiSkillStoneTagsGroup green = new zokuseiSkillStoneTagsGroup();
            zokuseiSkillStoneTagsGroup dark = new zokuseiSkillStoneTagsGroup();
            zokuseiSkillStoneTagsGroup _light = new zokuseiSkillStoneTagsGroup();
    
            red.INI_forSkillStoneBox(zokusei.redMagic,transform);
            blue.INI_forSkillStoneBox(zokusei.blueMagic,transform);
            green.INI_forSkillStoneBox(zokusei.greenMagic,transform);
            dark.INI_forSkillStoneBox(zokusei.darkMagic,transform);
            _light.INI_forSkillStoneBox(zokusei.lightMagic,transform);
    
            zokuseiButtonEffects.Add(zokusei.redMagic,red);
            zokuseiButtonEffects.Add(zokusei.blueMagic,blue);
            zokuseiButtonEffects.Add(zokusei.greenMagic,green);
            zokuseiButtonEffects.Add(zokusei.darkMagic,dark);
            zokuseiButtonEffects.Add(zokusei.lightMagic,_light);
            triggerExplosionPretab0 = Resources.Load("essentialUIElements/buttonEffects/lightMagic/explosion0", typeof(GameObject)) as GameObject;
            triggerExplosion0 = Object.Instantiate(triggerExplosionPretab0).GetComponent<ParticleSystem>();
            yield break;
        }
        
        public void closeShowingZokuseiTagEffects()
        {
            if (_focusingButtonEffectsGroup != null)
                _focusingButtonEffectsGroup.close_skillstoneboxtageffects();
        }
        
        public void switchZokuseiButtons(Vector3 normaltagpos,Vector3 ex1tagpos,Vector3 ex2tagpos,Vector3 ex3tagpos, zokusei zokusei)
        {
            if (_focusingButtonEffectsGroup != null)
                _focusingButtonEffectsGroup.close_skillstoneboxtageffects();
            
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
        
        private void RefreshTagEffect(Vector3 pos,int sp_level)//按钮切换也可以在这里做文章
        {
            _focusingButtonEffectsGroup.refreshforbuttonForSkillStoneBox(sp_level,pos);
        }
        
        public void skillbuttonexplosion(Vector3 targetPOS)
        {
            triggerExplosion0.transform.position = targetPOS;
            triggerExplosion0.Play();            
        }
    }
}