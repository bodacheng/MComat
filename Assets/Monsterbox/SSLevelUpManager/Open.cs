using dataAccess;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public partial class SSLevelUpManager : MonoBehaviour
{
    public void OpenLevelUpPage()
    {
        focusingSSD.RefreshInfo(_stoneListLayer.TargetStoneID);
        var renderModel = Stones.GetRenderModel(_stoneListLayer.TargetStoneID);
        renderModel._using = true;
        
        var layer = UILayerLoader.Get<StoneListLayer>();
        //layer.Setup();
        layer.box.AddFeatureToCells(layer.CellFeature_MAdd);
        RefreshSkillLevelUpModule();
        Stones.HighLight(renderModel._SkillConfig.RECORD_ID);
        gameObject.SetActive(true);
    }
    
    void CloseLevelUpPage()
    {
        var layer = UILayerLoader.Get<StoneListLayer>();
        //layer.Setup();
        var renderModel = Stones.GetRenderModel(_stoneListLayer.TargetStoneID);
        if (renderModel == null)
        {
            Debug.Log("Logic Error:"+ _stoneListLayer.TargetStoneID);
            return;
        }
        
        renderModel._using = false;
        SKStoneItem.SelectedRender(renderModel, SkillStonesBox._Selected);
        focusingSSD.RefreshInfo(renderModel.instanceId);
        
        foreach (var t in _materialSlots)
        {
            if (t.GetItem() != null)
            {
                layer.box.ReturnStoneToBox(t.GetItem());
            }
        }
        
        layer.box.AddFeatureToCells(layer.CellFeature_StoneShow);
        RefreshSkillLevelUpModule();
        Stones.ResetHighLight();
        gameObject.SetActive(false);
    }
}
