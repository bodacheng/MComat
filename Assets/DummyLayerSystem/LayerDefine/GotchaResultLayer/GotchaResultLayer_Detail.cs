using mainMenu;
using Skill;
using UnityEngine;

public partial class GotchaResultLayer : UILayer
{
    [SerializeField] SkillStoneDetail _stoneDetail;

    public void ShowDetail(string skillId)
    {
        _stoneDetail.gameObject.SetActive(true);
         SkillConfig sc = SkillConfigTable.GetSkillConfig(skillId);
        _stoneDetail.RefreshInfo(sc);
    }

    void ClearDetail()
    {
        _stoneDetail.gameObject.SetActive(false);
        _stoneDetail.Clear();
    }
}
