using UnityEngine;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        float lastclicktime;
        public void CellButtonBeheviour_EditCharSkill(StoneCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    if (Time.time - lastclicktime < 0.25f) // double click
                    {
                        if (TheNineSlot.target.GetFocusingStoneSlot() != null)
                        {
                            StoneCell.Install(_SkillStoneCell, TheNineSlot.target.GetFocusingStoneSlot()._DragAndDropCell);
                        }
                    }
                    lastclicktime = Time.time;
                    SKStoneItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfig != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_stone.SkillStoneOfPlayerId);
                    }else{
                        _skillStoneDetail.Clear();
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
                button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected); });
            }
        }
    }
}