using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        // 技能浏览器模式
        public void CellButtonBeheviour_StoneMergeMode(StoneCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                EventTrigger trigger = button.GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                
                void buttonFeature()
                {
                    if (Time.time - lastclicktime < 0.25f) // double click
                    {
                        StoneMergeManger.target.AddMaterial(_SkillStoneCell);
                    }
                    lastclicktime = Time.time;
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
                button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, _Selected); });
                SSLevelUpManager.target.AddMSlotBehaviour(_SkillStoneCell);
            }
        }
    }
}