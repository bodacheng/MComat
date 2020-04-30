using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

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

    [Header("NineForShow")]
    public RectTransform NinesT;
    public NineForShow NineForShowPretab;
    
    [Header("Rewards")]
    public RectTransform RewardsTransform;
    public Text goldrewards;
    public Text diamondrewards;

    public ReactiveProperty<bool> CanGotoSummary { get; set; } = new ReactiveProperty<bool>(false);

    readonly UnityEngine.Events.UnityAction ReturnToMainMenu = () =>
    {
        SceneManager.LoadScene(1);
    };

    //重新开战意味着所有资源重新加载？
    readonly UnityEngine.Events.UnityAction RestartGame = () =>
    {
        SceneManager.LoadScene(FightSceneNote.Instance.nextBattle.BattleGroundID);
    };
    
    public IEnumerator ShowSKillSets(List<CharDataInfo> TeamMembers)
    {
        for (int i = 0; i < TeamMembers.Count; i++)
        {
            NineForShow nineForShow = Instantiate(NineForShowPretab);
            yield return nineForShow.ShowStones(TeamMembers[i]._NineAndTwo);
            nineForShow.gameObject.SetActive(true);
            nineForShow.transform.SetParent(NinesT);
            nineForShow.transform.localPosition = Vector3.zero;
            nineForShow.transform.localScale = Vector3.one;
        }
    }
    
    public IEnumerator WINProcess()
    {
        CanGotoSummary.Value = false;
        FightOverCanvas.gameObject.SetActive(true);
        PlayAgain.onClick.RemoveAllListeners();
        PlayAgain.onClick.AddListener(RestartGame);
        ReturnToMainMenuWin.onClick.RemoveAllListeners();
        ReturnToMainMenuWin.onClick.AddListener(ReturnToMainMenu);
        
        LoseRectTransform.gameObject.SetActive(false);
        WinRectTransform.gameObject.SetActive(true);
        
        win_textanimation.transform.position = _CameraManager.transform.position + _CameraManager.transform.forward * 5f;
        win_textanimation.transform.rotation = _CameraManager.transform.rotation;
        win_textanimation.transform.SetParent( _CameraManager.transform);
        win_textanimation.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        CanGotoSummary.Value = true;
        yield break;
    }
    
    public IEnumerator LoseProcess()
    {
        CanGotoSummary.Value = false;
        FightOverCanvas.gameObject.SetActive(true);
        TryAgain.onClick.RemoveAllListeners();
        TryAgain.onClick.AddListener(RestartGame);
        ReturnToMainMenuLose.onClick.RemoveAllListeners();
        ReturnToMainMenuLose.onClick.AddListener(ReturnToMainMenu);
        
        WinRectTransform.gameObject.SetActive(false);
        LoseRectTransform.gameObject.SetActive(true);
        
        lose_textanimation.transform.position = _CameraManager.transform.position + _CameraManager.transform.forward * 5f;
        lose_textanimation.transform.rotation = _CameraManager.transform.rotation;
        lose_textanimation.transform.SetParent( _CameraManager.transform);
        lose_textanimation.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        CanGotoSummary.Value = true;
        yield break;
    }

    public IEnumerator ShowRewards(int golds,int diamond)
    {
        goldrewards.text = golds.ToString();
        diamondrewards.text = diamond.ToString();
        RewardsTransform.gameObject.SetActive(true);
        yield break;
    }
}
