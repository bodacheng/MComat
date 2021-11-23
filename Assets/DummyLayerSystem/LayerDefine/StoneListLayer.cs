using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager ssLevelUper;
    public SkillStoneDetail _skillStoneDetail;

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
        
        returnValue._skillStoneDetail.Clear();
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
            returnValue._skillStoneDetail.Clear();
            returnValue.box._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("StoneListLayer");
    }
    
    public void CellFeature_StoneShow(StoneCell _Cell)
    {
        Button button = _Cell.GetComponent<Button>();                
        button.onClick.RemoveAllListeners();
        void buttonFeature(object sender, System.EventArgs e)
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                Debug.Log(_Cell);
                StoneCell.SeletedRender(_Cell, SkillStonesBox._Selected);
                _skillStoneDetail.RefreshInfo(_stone.instanceId);
                ssLevelUper.SetTargetStoneID(_stone.instanceId);
            }else{
                _skillStoneDetail.Clear();
            }
        }
        
        void PressGoToLevelUpPage(object sender, GestureStateChangeEventArgs e)
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                ssLevelUper.OpenLevelUpPage(_stone.instanceId);
            }
        }
        _Cell.lpGesture.StateChanged += PressGoToLevelUpPage;
        _Cell.pGesture.Pressed += buttonFeature;
    }
    
    public void CellFeature_MAdd(StoneCell _SkillStoneCell)
    {
        void buttonFeature(object sender, System.EventArgs e)
        {
            ssLevelUper.AddMaterial(_SkillStoneCell);
            StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected);
        }
        _SkillStoneCell.tGesture.Tapped += buttonFeature;
        //ssLevelUper.AddMSlotBehaviour(_SkillStoneCell);??  这行代码是个谜
    }
}