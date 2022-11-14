using dataAccess;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public partial class SSLevelUpManager : MonoBehaviour
{
    private string targetInstanceID;
    
    public void OpenLevelUpPage(string instanceID)
    {
        targetInstanceID = instanceID;
        focusingSSD.RefreshInfo(targetInstanceID);
        var renderModel = Stones.GetRenderModel(targetInstanceID);
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
        var layer = UILayerLoader.Load<StoneListLayer>();
        layer.Setup();
        var renderModel = Stones.GetRenderModel(targetInstanceID);
        if (renderModel == null)
        {
            Debug.Log("Logic Error:"+ targetInstanceID);
            return;
        }
        
        renderModel._using = false;
        SKStoneItem.SelectedRender(renderModel, SkillStonesBox._Selected);
        focusingSSD.RefreshInfo(renderModel.instanceId);
        
        foreach (var t in MaterialSlots)
        {
            if (t.GetItem() != null)
            {
                layer.box.ReturnStoneToBox(t.GetItem());
            }
        }
        
        layer.box.AddFeatureToCells(layer.CellFeature_StoneShow);
        RefreshSkillLevelUpModule();
        targetInstanceID = null;
        Stones.ResetHighLight();
        gameObject.SetActive(false);
    }
}
