using Cysharp.Threading.Tasks;
using mainMenu;
using UnityEngine;
using dataAccess;
using DummyLayerSystem;
using UnityEngine.UI;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager levelManager;
    [SerializeField] SkillStoneDetail _skillStoneDetail;
    [SerializeField] Button OpenBtn;
    
    string targetStoneID;
    string TargetStoneID
    {
        get => targetStoneID; 
        set
        {
            targetStoneID = value;
            StoneOfPlayerInfo info = Stones.Get(targetStoneID);
            OpenBtn.gameObject.SetActive(info != null);
            if (info != null)
            {
                OpenBtn.onClick.RemoveAllListeners();
                OpenBtn.onClick.AddListener(() =>
                {
                    levelManager.OpenLevelUpPage(targetStoneID);
                });
            }
        }
    }
    
    public static StoneListLayer Open()
    {
        StoneListLayer returnValue = UILayerLoader.Get<StoneListLayer>();
        if (returnValue != null)
        {
            returnValue.box.GenerateCells();
            return returnValue;
        }
        returnValue = UILayerLoader.Load(PreScene.target.T,"StoneListLayer") as StoneListLayer;
        returnValue.box.IniExTabs();
        returnValue.box.GenerateCells();
        returnValue.box._tabEffects.SwitchZokusei(Element.blueMagic, ()=> returnValue.box.IniExTabsEffects(PreScene.target.FxCamera)).Forget();
        returnValue.box.AddFeatureToCells(returnValue.CellFeature_StoneShow);
        returnValue.box.FilterFeatureRefresh(true);
        returnValue.box.RestFilter();
        returnValue._skillStoneDetail.Clear();
        returnValue.levelManager.INI();
        return returnValue;
    }

    public static void Close()
    {
        var returnValue = UILayerLoader.Get<StoneListLayer>();
        if (returnValue != null)
        {
            returnValue._skillStoneDetail.Clear();
            returnValue.box._tabEffects.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("StoneListLayer");
    }
    
    public void CellFeature_StoneShow(StoneCell _Cell)
    {
        void buttonFeature()
        {
            var _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                StoneCell.SelectedRender(_Cell, SkillStonesBox._Selected);
                _skillStoneDetail.RefreshInfo(_stone.instanceId);
                TargetStoneID = _stone.instanceId;
            }else{
                _skillStoneDetail.Clear();
                TargetStoneID = null;
            }
        }
        
        _Cell.btn.AddListener(buttonFeature);
        _Cell.SetOnDropAction(StoneCell.Install);
    }
    
    public void CellFeature_MAdd(StoneCell _Cell)
    {
        void buttonFeature()
        {
            StoneCell.SelectedRender(_Cell, SkillStonesBox._Selected);
        }
        void doubleClick()
        {
            levelManager.AddMaterialFromCell(_Cell);
        }
        
        _Cell.btn.AddListener(buttonFeature);
        _Cell.btn.AddDoubleClickEvent(doubleClick);
        _Cell.SetOnDropAction(StoneCell.Install);
    }
}