using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        public void CellButtonBeheviour_MAdd(StoneCell _SkillStoneCell)
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
                        SSLevelUpManager.target.AddMaterial(_SkillStoneCell);
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