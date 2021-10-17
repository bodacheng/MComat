using System.Collections;
using UnityEngine;

public class FightResultAnimLayer : UILayer
{
    [Header("WIN")]
    [SerializeField] private GameObject win_textanimation;
    
    [Header("LOSE")]
    [SerializeField] private GameObject lose_textanimation;
    
    // 胜利字幕与对应页面加载
    public IEnumerator WINProcess()
    {
        win_textanimation.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        win_textanimation.gameObject.SetActive(false);
    }
        
    // 失败字幕与对应页面加载
    public IEnumerator LoseProcess()
    {
        lose_textanimation.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        lose_textanimation.gameObject.SetActive(false);
    }
}
