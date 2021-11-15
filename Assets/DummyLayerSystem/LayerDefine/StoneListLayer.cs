using mainMenu;
using UnityEngine;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager ssLevelUper;
    
    [Space(10)] 
    [Header("FX Camera")] 
    public Camera fxCamera;
    
    public static StoneListLayer Open()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
            return returnValue;
        }
        l = UILayerLoader.Load(PreScene.target.T,"StoneListLayer") as StoneListLayer;
        returnValue = l as StoneListLayer;
        returnValue.box.GenerateCells();
        returnValue.box._SkillStoneBoxTabEffectsManager.StartUp();
        returnValue.box.IniExTabs(returnValue.fxCamera);
        returnValue.box.EXTabsFeatureRefresh(true);
        returnValue.box.RestFilter();
        returnValue.box._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            returnValue.box.NormalTab.transform,
            returnValue.box.EX1Tab.transform,
            returnValue.box.EX2Tab.transform,
            returnValue.box.EX3Tab.transform, 
            Zokusei.blueMagic
        );
        
        returnValue.box._skillStoneDetail.Clear();
        
        return returnValue;
    }

    public static void Close()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
            GameObject.Destroy(returnValue.fxCamera);
            returnValue.box._skillStoneDetail.Clear();
            returnValue.box._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("StoneListLayer");
    }
}
