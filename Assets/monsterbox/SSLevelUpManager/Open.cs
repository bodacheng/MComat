using dataAccess;
using mainMenu;
using UnityEngine;

public partial class SSLevelUpManager : MonoBehaviour
{
    private string targetStoneID; 
    public void OpenLevelUpPage(string instanceID)
    {
        targetStoneID = instanceID;
        focusingSSD.RefreshInfo(targetStoneID);
        var renderModel = Stones.GetRenderModel(targetStoneID);
        renderModel._using = true;
        
        var layer = StoneListLayer.Open();
        layer.box.AddFeatureToCells(layer.CellFeature_MAdd);
        RefreshSkillLevelUpModule();
        Stones.HighLight(renderModel._SkillConfig.RECORD_ID);
        gameObject.SetActive(true);
    }
    
    void CloseLevelUpPage()
    {
        var layer = StoneListLayer.Open();
        SKStoneItem renderModel = Stones.GetRenderModel(targetStoneID);
        renderModel._using = false;
        SKStoneItem.SeletedRender(renderModel, SkillStonesBox._Selected);
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
        targetStoneID = null;
        Stones.ResetHighLight();
        gameObject.SetActive(false);
    }
}
