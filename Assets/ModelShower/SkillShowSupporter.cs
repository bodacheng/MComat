using System.Collections;
using mainMenu;

public static class SkillShowSupporter
{
    public static string focusRId;
    public static Data_Center focusingC;
    public static bool IfShowingSkill = false;
    
    public static IEnumerator SkillShowRunWithPrepare(string keyname)
    {
        var unitConfig = Units.GetUnitConfig(focusRId);
        //下面这一大片，在资源存在的情况下压根不应该运行            
        if (focusingC.Animation_Manger != null)
        {
            switch (ResourceLoadingSetting.AnimationLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    yield return focusingC.Animation_Manger.PreloadPersonalAnim(ResourceDownLoad.BundleURL, unitConfig.TYPE, keyname, unitConfig.SPECIAL_ZOKUSEI, unitConfig._zokusei);
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    yield return focusingC.Animation_Manger.PreloadPersonalAnimStreamingAssetMode(unitConfig.TYPE, keyname, unitConfig.SPECIAL_ZOKUSEI, unitConfig._zokusei);
                    break;
                case ResourceLoadMode.Resource:
                    yield return focusingC.Animation_Manger.PreloadPersonalAnimResourceMode(unitConfig.TYPE, keyname, unitConfig.SPECIAL_ZOKUSEI, unitConfig._zokusei);
                    break;
            }
            IfShowingSkill = true;
            focusingC.Animation_Manger.AnimationTrigger(keyname, true, 0.05f);
        }
    }
    
    public static void SkillsPrintOutLateUpdate()
    {
        if (focusingC != null)
        {
            if (focusingC.Animation_Manger != null && focusingC.WholeT.gameObject.activeSelf)
            {
                if (focusingC.Animation_Manger.GetBool("in_transition") == false && 
                    focusingC.Animation_Manger.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
                {
                    focusingC.Animation_Manger.PlayLayerAnim(null, true, 0.05f);
                    IfShowingSkill = false;
                    PreScene.target._CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
                }
            }
        }
    }
}
