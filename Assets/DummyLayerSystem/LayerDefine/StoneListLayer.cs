using mainMenu;
using UnityEngine;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager ssLevelUper;
    
    public static StoneListLayer Open()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
            SkillStonesBox.target = returnValue.box;
            return returnValue;
        }
        l = UILayerLoader.Load(PreScene.target.T,"StoneListLayer") as StoneListLayer;
        returnValue = l as StoneListLayer;
        returnValue.box.GenerateCells();
        returnValue.box._SkillStoneBoxTabEffectsManager.StartUp();
        returnValue.box._skillStoneDetail.Clear();
        SkillStonesBox.target = returnValue.box;
        return returnValue;
    }

    public static void Close()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
            SkillStonesBox.target = returnValue.box;
            GameObject.Destroy(returnValue.box.fxCamera);
            returnValue.box._skillStoneDetail.Clear();
            returnValue.box._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("StoneListLayer");
    }
}
