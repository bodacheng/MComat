using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System.Collections;
using dataAccess;

// 贩卖过多的技能石？ 扩张技能石盒？
public class BoxManageHelper : MonoBehaviour
{
    public Canvas HelperCanvas; // 跳出选项，问采取什么策略解决目前技能石盒容量不够问题

    #region 扩张
    public Button ChooseToExpand;

    public Canvas ExpansionT;
    public Button Five; // 扩张5个格
    public Button Ten; // 扩张10个格
    
    // 扩张了格子之后没必要前往某个单独的process，有一个成功提示按说就可以
    public IEnumerator BoxExpansion(int ExpandCount)
    {
        switch (AccountSet._playerinfoReferenceMode)
        {
            case playerInfoRefMode.localTestSaveData:
                AccountSet._AccInfo.Stoneboxsize = AccountSet._AccInfo.Stoneboxsize + ExpandCount;
                yield return AccountSet.SaveCustomerInfo();
                break;
            case playerInfoRefMode.remoteTestPlayer:
                break;
            case playerInfoRefMode.formalVersion:
                break;
        }
        yield break;
    }
    #endregion

    #region 贩卖
    public Button ChooseToSell;
    
    void GoToStoneSell()
    {
        PreScene.Instance.trySwitchToStep(MainSceneStep.SkillStones,true); // 没有单独的技能石贩卖画面所以只能送到这里
    }
    #endregion
}