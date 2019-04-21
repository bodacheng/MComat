using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightOverControl : MonoBehaviour {

    public Canvas FightOverCanvas;

    [Header("WIN")]
    public RectTransform WinRectTransform;
    public Button PlayAgain;
    public Button ReturnToMainMenuWin;

    [Header("LOSE")]
    public RectTransform LoseRectTransform;
    public Button TryAgain;
    public Button ReturnToMainMenuLose;

    UnityEngine.Events.UnityAction returnToMainMenu = () =>
    {
        SceneManager.LoadScene(1);
    };

    //重新开战意味着所有资源重新加载？
    UnityEngine.Events.UnityAction restartGame = () =>
    {
        SceneManager.LoadScene(GoingToLoadFight.Instance.nextBattle._LocalFight.BattleGroundID);
    };

    public IEnumerator WINProcess()
    {
        PlayAgain.onClick.RemoveAllListeners();
        PlayAgain.onClick.AddListener(restartGame);
        ReturnToMainMenuWin.onClick.RemoveAllListeners();
        ReturnToMainMenuWin.onClick.AddListener(returnToMainMenu);

        LoseRectTransform.gameObject.SetActive(false);
        WinRectTransform.gameObject.SetActive(true);
        yield break;
    }

    public IEnumerator LoseProcess()
    {
        TryAgain.onClick.RemoveAllListeners();
        TryAgain.onClick.AddListener(restartGame);
        ReturnToMainMenuLose.onClick.RemoveAllListeners();
        ReturnToMainMenuLose.onClick.AddListener(returnToMainMenu);

        WinRectTransform.gameObject.SetActive(false);
        LoseRectTransform.gameObject.SetActive(true);
        yield break;
    }
}
