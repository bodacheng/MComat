using UnityEngine;
using dataAccess;
using DG.Tweening;
using mainMenu;

public static class SVCenter
{
    public static void StoneRemoveFromSlotToCell(StoneCell sourceCell, StoneCell boxcell)
    {
        if (sourceCell.cellPhase == StoneCell.CellPhase.NineSlotCell)
        {
            SkillEditLayer skillEditLayer = SkillEditLayer.Open();
            SKStoneItem stone = sourceCell.GetItem();
            if (stone != null && stone.Inherent)
            {
                Debug.Log("固有技能无法移出，返回");
                return;
            }
            // 如果把技能石从9宫格拖到技能背包的一个有石头的格子上，那么就直接把拖动中的技能石先从九宫格拔下来，接着让技能背包自动排序一下
            if (boxcell.GetItem() != null)
            {
                skillEditLayer.StonesBox.ReturnStoneToBox(stone);
            }
            else
            {
                // 如果把技能石从9宫格拖到空技能背包格子上，那就让这个技能石在那个空格子上就可以。
                // 的确这个瞬间可能产生这个技能石所在位置和当前背包显示类型不一致问题，但如果是进行了一个背包自动排序的话，
                // 松手瞬间会有一个技能石“变图案”的错觉。
                boxcell.AddItem(stone);
            }
            skillEditLayer.NineSlot.NineSlotsStatusRefresh();
        }
        else if (sourceCell.cellPhase == StoneCell.CellPhase.SKLevelUpMSlot)
        {
            SKStoneItem stone = sourceCell.GetItem();
            StoneListLayer sl = StoneListLayer.Get();
            sl.ssLevelUper.RefreshSkillLevelUpModule();
            if (boxcell.GetItem() != null)
            {
                sl.box.ReturnStoneToBox(stone);
            }            
            else
            {
                boxcell.AddItem(stone);
            }
        }
    }
    
    public static void MoveItemFromTo(StoneCell from, StoneCell to)
    {
        SKStoneItem item = from.GetItem();
        if (item == null)
            return;
        if (to.cellPhase == StoneCell.CellPhase.NineSlotCell && from.cellPhase == StoneCell.CellPhase.SkillStoneBoxCell)
        {
            SkillEditLayer skillEditLayer = SkillEditLayer.Open();
            if (!CheckIfOtherUnitOkAfterStoneRemove(item))
                return;
            skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(item._SkillConfig.SP_LEVEL, 
            PosCal.GetWorldPos(PreScene.target.FxCamera, to.GetComponent<RectTransform>(), 3), 
            skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.transform);
        }
        
        to.AddItem(item);
        from.UpdateMyItem();
        
        if (from.cellPhase == StoneCell.CellPhase.NineSlotCell || to.cellPhase == StoneCell.CellPhase.NineSlotCell)
        {
            SkillEditLayer skillEditLayer = SkillEditLayer.Open();
            skillEditLayer.NineSlot.NineSlotsStatusRefresh();
        }
        
        if (from.cellPhase == StoneCell.CellPhase.SKLevelUpMSlot || to.cellPhase == StoneCell.CellPhase.SKLevelUpMSlot)
        {
            StoneListLayer sl = StoneListLayer.Get();
            sl.ssLevelUper.RefreshSkillLevelUpModule();
        }
    }
    
    public static void SwapItemFromTo(StoneCell from, StoneCell to)
    {
        SKStoneItem itemFromCell = from.GetItem();
        if (itemFromCell == null)
            return;
            
        // 从技能石盒子取出的石头安装到技能槽，要看如果这个技能石被其他角色使用中的话，那个角色会不会有问题
        if (to.cellPhase == StoneCell.CellPhase.NineSlotCell && from.cellPhase == StoneCell.CellPhase.SkillStoneBoxCell)
        {
            SkillEditLayer skillEditLayer = SkillEditLayer.Get();
            if (!CheckIfOtherUnitOkAfterStoneRemove(itemFromCell))
                return;
            skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(itemFromCell._SkillConfig.SP_LEVEL, 
            PosCal.GetWorldPos(PreScene.target.FxCamera, to.GetComponent<RectTransform>(), 3), 
            skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.transform);
        }
        
        // 把技能石从技能槽拖回技能石盒，如果是固有技能石，连移动也不允许
        if (to.cellPhase == StoneCell.CellPhase.SkillStoneBoxCell && from.cellPhase == StoneCell.CellPhase.NineSlotCell)
        {
            SKStoneItem stone = to.GetItem();
            if (stone.Inherent)
            {
                Debug.Log("固有技能无法移出，返回");
                return;
            }
        }
        
        SwapItems(from, to);
        
        if (from.cellPhase == StoneCell.CellPhase.NineSlotCell || to.cellPhase == StoneCell.CellPhase.NineSlotCell)
        {
            SkillEditLayer skillEditLayer = SkillEditLayer.Get();
            skillEditLayer.NineSlot.NineSlotsStatusRefresh();
        }
        
        if (from.cellPhase == StoneCell.CellPhase.SKLevelUpMSlot || to.cellPhase == StoneCell.CellPhase.SKLevelUpMSlot)
        {
            StoneListLayer sl = StoneListLayer.Get();
            sl.ssLevelUper.RefreshSkillLevelUpModule();
        }
    }
    
    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    static void SwapItems(StoneCell firstCell, StoneCell secondCell)
    {
        firstCell.UpdateMyItem();
        secondCell.UpdateMyItem();
        SKStoneItem firstItem = firstCell.GetItem();                // Get item from first cell
        SKStoneItem secondItem = secondCell.GetItem();              // Get item from second cell
        // Swap items
        if (firstItem != null)
        {
            //firstItem.transform.DOMove(secondCell.transform.position,1f);
            //firstItem.transform.localPosition = Vector3.zero;
            //firstItem.MakeRaycast(true);
            secondCell.AddItem(firstItem);
        }
        if (secondItem != null)
        {
            firstCell.AddItem(secondItem);
            secondItem.transform.position = secondCell.transform.position;
            secondItem.transform.DOMove(firstCell.transform.position,0.5f).OnComplete(() =>
            {
                secondItem.transform.localPosition = Vector3.zero;
            });
        }
    }
    
    // 尝试装载的技能石正被其他角色使用时候，对那个其他角色进行validation检验
    static bool CheckIfOtherUnitOkAfterStoneRemove(SKStoneItem item)
    {
        SkillEditLayer skillEditLayer = SkillEditLayer.Open();
        if (item.Inherent)
        {
            Debug.Log("固有技能无法移出，返回");
            return false;
        }
        if (MyMonsters.CheckExist(Stones.Get(item.instanceId).inUsingMonsterOfPlayerId))
        {
            string monsterPlayerID = Stones.Get(item.instanceId).inUsingMonsterOfPlayerId;
            SkillSet.SkillEditError valR3 = skillEditLayer.NineSlot.CheckEditAfterOneStoneRemoved(monsterPlayerID, item._SkillConfig.RECORD_ID);
            if (valR3 != SkillSet.SkillEditError.Perfect)
            {
                skillEditLayer.NineSlot.ValidationWarn(valR3, monsterPlayerID);
                return false;
            }
        }
        return true;
    }
}
