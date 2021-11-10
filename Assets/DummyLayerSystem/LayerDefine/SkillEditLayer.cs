using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class SkillEditLayer : UILayer
{
    [Space(11)]
    [Header("技能展示器模式切换角色按钮")]
    public Button charSwitcher;
    
    public TheNineSlot NineSlot;
    public SkillStonesBox StonesBox;
    
    public static SkillEditLayer Open()
    {
        UILayer l = UILayerLoader.Get("SkillEditLayer");
        SkillEditLayer returnValue;
        if (l != null)
        {
            returnValue = l as SkillEditLayer;
            return returnValue;
        }
        l = UILayerLoader.Load(PreScene.target.T,"SkillEditLayer") as SkillEditLayer;
        returnValue = l as SkillEditLayer;
        returnValue.NineSlot.StartUp();
        returnValue.StonesBox.GenerateCells();
        returnValue.StonesBox._SkillStoneBoxTabEffectsManager.StartUp();
        returnValue.StonesBox._skillStoneDetail.Clear();
        SkillStonesBox.target = returnValue.StonesBox;
        returnValue.charSwitcher.gameObject.SetActive(FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow);
        return returnValue;
    }

    public static void Close()
    {
        UILayerLoader.Remove("SkillEditLayer");
    }
}
