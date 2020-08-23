using UnityEngine;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        public void CellButtonBeheviour_STStoneShow(StoneCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    SKStoneItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfig != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_stone._SkillConfig, _stone.SkillStoneOfPlayerId);
                        SSLevelUpManager.target.RefreshSkillLevelUpModule();
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