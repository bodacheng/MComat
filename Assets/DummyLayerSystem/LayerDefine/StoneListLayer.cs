using mainMenu;
using UnityEngine;
using System;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager ssLevelUper;
    public SkillStoneDetail _skillStoneDetail;
    
    public static StoneListLayer Get()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
        }
        return returnValue;
    }
    
    public static StoneListLayer Open()
    {
        StoneListLayer returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(PreScene.target.T,"StoneListLayer") as StoneListLayer;
        returnValue.box.GenerateCells();
        returnValue.box._SkillStoneBoxTabEffectsManager.SwitchZokusei
        (
            Zokusei.blueMagic
        );
        returnValue.box.IniExTabs(PreScene.target.FxCamera);
        returnValue.box.EXTabsFeatureRefresh(true);
        returnValue.box.RestFilter();
        returnValue._skillStoneDetail.Clear();
        return returnValue;
    }

    public static void Close()
    {
        StoneListLayer returnValue = Get();
        if (returnValue != null)
        {
            returnValue._skillStoneDetail.Clear();
            returnValue.box._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("StoneListLayer");
    }
    
    public void CellFeature_StoneShow(StoneCell _Cell)
    {
        void buttonFeature(object sender, System.EventArgs e)
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                StoneCell.SeletedRender(_Cell, SkillStonesBox._Selected);
                _skillStoneDetail.RefreshInfo(_stone.instanceId);
            }else{
                _skillStoneDetail.Clear();
            }
        }
        
        void PressGoToLevelUpPage( object sender, EventArgs e )
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                ssLevelUper.OpenLevelUpPage(_stone.instanceId);
            }
        }
        _Cell.lpGesture.LongPressed += PressGoToLevelUpPage;
        _Cell.pGesture.Pressed += buttonFeature;
        
        _Cell.SetOnDropAction(StoneCell.Install);
    }
    
    public void CellFeature_MAdd(StoneCell _Cell)
    {
        void buttonFeature(object sender, System.EventArgs e)
        {
            StoneCell.SeletedRender(_Cell, SkillStonesBox._Selected);
        }
        
        void doubleClick(object sender, System.EventArgs e)
        {
            Debug.Log(sender + "dj");
            ssLevelUper.AddMaterial(_Cell);
        }
        
        _Cell.pGesture.Pressed += buttonFeature;
        _Cell.tGesture.Tapped += doubleClick;
        _Cell.SetOnDropAction(StoneCell.Install);
        //ssLevelUper.AddMSlotBehaviour(_SkillStoneCell);??  这行代码是个谜
    }
}