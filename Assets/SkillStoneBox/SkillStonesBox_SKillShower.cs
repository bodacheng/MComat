using UnityEngine;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        // 技能浏览器模式
        public void CellButtonBeheviour_SKillShowMode(StoneCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    SKStoneItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfig != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_stone.SkillStoneOfPlayerId);
                        mainProcessRunner.Run(MemberDetail.target._SkillsPrintOut.SkillShowRunWithPrepare(_stone._SkillConfig.REAL_NAME));
                    }else{
                        _skillStoneDetail.Clear();
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
                button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, _Selected); });
            }
        }
    }
}