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
    [SerializeField] Button OpenBtn;
    
    string targetStoneID;
    public string TargetStoneID
    {
        get => targetStoneID; 
        set
        {
            targetStoneID = value;
            var info = Stones.Get(targetStoneID);
            _skillStoneDetail.gameObject.SetActive(info != null);
            OpenBtn.gameObject.SetActive(info != null);
            if (info != null)
            {
                OpenBtn.onClick.RemoveAllListeners();
                OpenBtn.onClick.AddListener(() =>
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
    
    public void CellFeature_StoneShow(StoneCell _Cell)
    {
        void btnFeature()
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
        
        _Cell.btn.AddListener(btnFeature);
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