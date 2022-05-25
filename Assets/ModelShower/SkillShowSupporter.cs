using System.Collections;
using mainMenu;

public static class SkillShowSupporter
{
    public static string FocusRId;
    public static Data_Center FocusingC;
    public static bool IfShowingSkill = false;
    
    public static IEnumerator SkillShowRunWithPrepare(string skillName)
    {
        var unitConfig = Units.GetUnitConfig(FocusRId);
        if (unitConfig == null)
            yield break;
        //下面这一大片，在资源存在的情况下压根不应该运行            
        if (FocusingC.Animation_Manger != null)
        {
            yield return FocusingC.Animation_Manger.PreloadPersonalAnimResourceMode(unitConfig.TYPE, skillName, unitConfig.SPECIAL_ZOKUSEI, unitConfig.element);
            IfShowingSkill = true;
            FocusingC.Animation_Manger.AnimationTrigger(skillName, true, 0.05f);
        }
    }
    
    public static void SkillsPrintOutLateUpdate()
    {
        if (FocusingC != null)
        {
            if (FocusingC.Animation_Manger != null && FocusingC.WholeT.gameObject.activeSelf)
            {
                if (FocusingC.Animation_Manger.GetBool("in_transition") == false && 
                    FocusingC.Animation_Manger.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
                {
                    FocusingC.Animation_Manger.PlayLayerAnim(null, true, 0.05f);
                    IfShowingSkill = false;
                    PreScene.target._CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
                }
            }
        }
    }
}
