using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine;

public partial class SSLevelUpManager : MonoBehaviour
{
    private string targetStoneID; 
    public void OpenLevelUpPage(string instanceID)
    {
        if (string.IsNullOrEmpty(instanceID))
            return;
        Debug.Log("open:"+ instanceID);
        targetStoneID = instanceID;
        focusingSSD.RefreshInfo(targetStoneID);
        SKStoneItem targetStone = Stones.GetRenderModel(targetStoneID);
        targetStone._using = true;
        
        StoneListLayer layer = StoneListLayer.Open();
        // layer.box.RestFilter();
        // layer.box.rares = new List<int> { 0, 1, 2 };
        layer.box.AddFeatureToCells(layer.CellFeature_MAdd);
        RefreshSkillLevelUpModule();
        gameObject.SetActive(true);
    }
    
    void CloseLevelUpPage()
    {
        StoneListLayer layer = StoneListLayer.Open();
        SKStoneItem targetStone = Stones.GetRenderModel(targetStoneID);
        if (targetStone != null)
        {
            targetStone._using = false;
            SKStoneItem.SeletedRender(targetStone, SkillStonesBox._Selected);
            focusingSSD.RefreshInfo(targetStone.instanceId);
        }
        // layer.box.rares = new List<int> { 0, 1, 2 ,3, 4, 5};
        // layer.box.RestFilter();
        
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            if (MaterialSlots[i].GetItem() != null)
            {
                layer.box.ReturnStoneToBox(MaterialSlots[i].GetItem());
            }
        }
        
        layer.box.AddFeatureToCells(layer.CellFeature_StoneShow);
        RefreshSkillLevelUpModule();
        Debug.Log("close:"+ targetStoneID);
        targetStoneID = null;
        
        gameObject.SetActive(false);
    }
}
