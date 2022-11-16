using System.Threading;
using Cysharp.Threading.Tasks;
using mainMenu;
using UnityEngine;
using dataAccess;
using UnityEngine.UI;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager levelManager;
    [SerializeField] SkillStoneDetail _skillStoneDetail;
    [SerializeField] Button openPowerUpBtn;
    
    string _targetStoneID;
    public string TargetStoneID
    {
        get => _targetStoneID; 
        set
        {
            _targetStoneID = value;
            var info = Stones.Get(_targetStoneID);
            _skillStoneDetail.gameObject.SetActive(info != null);
            openPowerUpBtn.gameObject.SetActive(Stones.StoneCanLevelUp(_targetStoneID));
            if (info != null)
            {
                openPowerUpBtn.onClick.RemoveAllListeners();
                openPowerUpBtn.onClick.AddListener(() =>
                {
                    levelManager.OpenLevelUpPage();
                });
            }
        }
    }
    
    public void Setup()
    {
        var cts = new CancellationTokenSource();
        ReturnLayer.AddUniTaskCancel(cts);
        
        box.IniExTabs();
        box.GenerateCells();
        box._tabEffects.SwitchElement(Element.blueMagic, 
            ()=> box.IniExTabsEffects(PreScene.target.FxCamera),
            cts.Token).Forget();
        box.AddFeatureToCells(CellFeature_StoneShow);
        box.FilterFeatureRefresh(true);
        box.RestFilter();
        _skillStoneDetail.Clear();
        levelManager.INI();
    }
    
    public override void OnDestroy()
    {
        _skillStoneDetail.Clear();
        box._tabEffects.CloseShowingTagEffects();
    }
    
    public void CellFeature_StoneShow(StoneCell cell)
    {
        void BtnFeature()
        {
            var stone = cell.GetItem();
            if (stone != null && stone._SkillConfig != null)
            {
                StoneCell.SelectedRender(cell, SkillStonesBox._Selected);
                _skillStoneDetail.RefreshInfo(stone.instanceId);
                TargetStoneID = stone.instanceId;
            }else{
                _skillStoneDetail.Clear();
                TargetStoneID = null;
            }
        }
        
        cell.btn.ActivateHold = false;
        cell.btn.ActivateDoubleClick = false;
        
        cell.btn.SetListener(BtnFeature);
        cell.SetOnDropAction(StoneCell.Install);
    }
    
    public void CellFeature_MAdd(StoneCell cell)
    {
        void BtnFeature()
        {
            StoneCell.SelectedRender(cell, SkillStonesBox._Selected);
        }
        void DoubleClick()
        {
            levelManager.AddMaterialFromCell(cell);
        }
        
        cell.btn.SetListener(BtnFeature);
        cell.btn.ActivateDoubleClick = true;
        cell.btn.onDoubleClick.AddListener(DoubleClick);
        cell.SetOnDropAction(StoneCell.Install);
    }
}