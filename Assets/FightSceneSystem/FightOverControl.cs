using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightOverControl : MonoBehaviour {

    public Canvas FightOverCanvas;
    public CameraManager _CameraManager;

    [Header("WIN")]
    public RectTransform WinRectTransform;
    public GameObject win_textanimation;
    public Button PlayAgain;
    public Button ReturnToMainMenuWin;

    [Header("LOSE")]
    public RectTransform LoseRectTransform;
    public GameObject lose_textanimation;
    public Button TryAgain;
    public Button ReturnToMainMenuLose;
    
    [Header("Rewards")]
    public RectTransform RewardsTransform;
    public Text goldrewards;
    public Text diamondrewards;

    private bool canGotoSummary = false;
    
    public bool ifCanGotoSummary()
    {
        return canGotoSummary;
    }

    UnityEngine.Events.UnityAction returnToMainMenu = () =>
    {
        SceneManager.LoadScene(1);
    };

    //重新开战意味着所有资源重新加载？
    UnityEngine.Events.UnityAction restartGame = () =>
    {
        SceneManager.LoadScene(FightSceneNote.Instance.nextBattle.BattleGroundID);
    };

    public IEnumerator WINProcess()
    {
        canGotoSummary = false;
        FightOverCanvas.gameObject.SetActive(true);
        PlayAgain.onClick.RemoveAllListeners();
        PlayAgain.onClick.AddListener(restartGame);
        ReturnToMainMenuWin.onClick.RemoveAllListeners();
        ReturnToMainMenuWin.onClick.AddListener(returnToMainMenu);

        LoseRectTransform.gameObject.SetActive(false);
        WinRectTransform.gameObject.SetActive(true);

        win_textanimation.transform.position = _CameraManager.transform.position + _CameraManager.transform.forward * 5f;
        win_textanimation.transform.rotation = _CameraManager.transform.rotation;
        win_textanimation.transform.SetParent( _CameraManager.transform);
        win_textanimation.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        canGotoSummary = true;
        yield break;
    }

    public IEnumerator LoseProcess()
    {
        canGotoSummary = false;
        FightOverCanvas.gameObject.SetActive(true);
        TryAgain.onClick.RemoveAllListeners();
        TryAgain.onClick.AddListener(restartGame);
        ReturnToMainMenuLose.onClick.RemoveAllListeners();
        ReturnToMainMenuLose.onClick.AddListener(returnToMainMenu);

        WinRectTransform.gameObject.SetActive(false);
        LoseRectTransform.gameObject.SetActive(true);

        lose_textanimation.transform.position = _CameraManager.transform.position + _CameraManager.transform.forward * 5f;
        lose_textanimation.transform.rotation = _CameraManager.transform.rotation;
        lose_textanimation.transform.SetParent( _CameraManager.transform);
        lose_textanimation.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        canGotoSummary = true;
        yield break;
    }

    public IEnumerator showRewards(int golds,int diamond,List<int> skillstones)
    {
        goldrewards.text = golds.ToString();
        diamondrewards.text = diamond.ToString();
        RewardsTransform.gameObject.SetActive(true);
        yield break;
    }
}
