using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStoneBoxTabEffectsManager : MonoBehaviour
{
    IDictionary<zokusei, zokuseiSkillStoneTagsGroup> zokuseiButtonEffects = new Dictionary<zokusei, zokuseiSkillStoneTagsGroup>();
    zokuseiSkillStoneTagsGroup _focusingButtonEffectsGroup;
    
    public IEnumerator startUp()
    {
        zokuseiSkillStoneTagsGroup red = new zokuseiSkillStoneTagsGroup();
        zokuseiSkillStoneTagsGroup blue = new zokuseiSkillStoneTagsGroup();
        zokuseiSkillStoneTagsGroup green = new zokuseiSkillStoneTagsGroup();
        zokuseiSkillStoneTagsGroup dark = new zokuseiSkillStoneTagsGroup();
        zokuseiSkillStoneTagsGroup _light = new zokuseiSkillStoneTagsGroup();

        red.INI_forSkillStoneBox(zokusei.redMagic);
        blue.INI_forSkillStoneBox(zokusei.blueMagic);
        green.INI_forSkillStoneBox(zokusei.greenMagic);
        dark.INI_forSkillStoneBox(zokusei.darkMagic);
        _light.INI_forSkillStoneBox(zokusei.lightMagic);

        zokuseiButtonEffects.Add(zokusei.redMagic,red);
        zokuseiButtonEffects.Add(zokusei.blueMagic,blue);
        zokuseiButtonEffects.Add(zokusei.greenMagic,green);
        zokuseiButtonEffects.Add(zokusei.darkMagic,dark);
        zokuseiButtonEffects.Add(zokusei.lightMagic,_light);
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
            RefreshTagEffect(normaltagpos,EX.normal);
            RefreshTagEffect(ex1tagpos,EX.EX1);
            RefreshTagEffect(ex2tagpos,EX.EX2);
            RefreshTagEffect(ex3tagpos,EX.EX3);
        }else{
            Debug.Log("见鬼了。检查手机控制器渲染模块加载顺序");
        }
    }
    
    private void RefreshTagEffect(Vector3 pos,EX sp_level)//按钮切换也可以在这里做文章
    {
        _focusingButtonEffectsGroup.refreshforbuttonForSkillStoneBox(sp_level,pos);
    }
}
