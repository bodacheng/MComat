using UnityEngine;

// 智慧果实消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    #region 调整目标等级 直接放在按钮上。
    public void PlusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        RefreshSkillLevelUpModule();
        // 消耗coin的显示？
    }
    public void MinusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        RefreshSkillLevelUpModule();
        // 消耗coin的显示？
    }
    #endregion
}
