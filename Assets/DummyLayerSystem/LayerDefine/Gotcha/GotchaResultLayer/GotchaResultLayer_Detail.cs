using mainMenu;
using UnityEngine;

public partial class GotchaResultLayer : UILayer
{
    [SerializeField] RectTransform stoneDetailT;
    [SerializeField] SkillStoneDetail _stoneDetail;

    public void ShowDetail(string skillId)
    {
        stoneDetailT.gameObject.SetActive(true);
         var sc = SkillConfigTable.GetSkillConfig(skillId);
         Debug.Log("skill config:"+ sc);
        _stoneDetail.RefreshInfo(sc);
    }

    void ClearDetail()
    {
        stoneDetailT.gameObject.SetActive(false);
        _stoneDetail.Clear();
    }
}
