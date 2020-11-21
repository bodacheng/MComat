using UnityEngine;
using UnityEngine.EventSystems;
using mainMenu;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public void ReturnStoneToBox()
    {
        UpdateMyItem();
        SKStoneItem _DragAndDropItem = GetItem();
        if (_DragAndDropItem)
        {
            _DragAndDropItem._using = false;
            //如果尝试归还背包的技能石必杀等级与显示中的一致，则找个当前的空格给放进去就可以。
            if (_DragAndDropItem._SkillConfig.SP_LEVEL == SkillStonesBox.target.GetFocusingExType())
            {
                StoneCell dragAndDropCell = SkillStonesBox.target.GetFirstEmptyCell();
                if (dragAndDropCell != null)
                {
                    dragAndDropCell.AddItem(_DragAndDropItem);
                }
                else
                {
                    Debug.Log("走到这儿的话说明已经是bug了。");
                    _DragAndDropItem.gameObject.transform.SetParent(SkillStonesBox.target.stonesTempContainer);
                }
            }
            else{
                //如果尝试归还背包的技能石必杀等级与显示中的不一致，则直接使其非显示。
                _DragAndDropItem.gameObject.transform.SetParent(SkillStonesBox.target.stonesTempContainer);
            }
        }
        UpdateMyItem();
    }
}
