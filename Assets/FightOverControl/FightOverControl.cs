using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

namespace FightScene
{
    public class FightOverControl : MonoBehaviour
    {
        public Canvas FightOverCanvas;
        
        [Header("WIN")]
        public GameObject win_textanimation;

        [Header("LOSE")]
        public GameObject lose_textanimation;

        [Header("技能与角色头像T")]
        public RectTransform Step1;
        public RectTransform Step2;
        public RectTransform IconAndSKillShowUISetT;
        
        [Header("IconWithSkillShow")]
        public IconAndSKillShowUISet IconAndSKillShowUISetPretab;
        
        [Header("NineForShow")]
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
        
        public static FightOverControl target;
        
        void Awake()
        {
            target = this;
        }
        
        //重新开战意味着所有资源重新加载？
        readonly UnityEngine.Events.UnityAction RestartGame = () =>
        {
            SceneManager.LoadScene(2);
        };
        
        // 战斗结束后统计技能石升级情况时的画面显示
        public IEnumerator ShowSKillSets(FightTeam fightTeam)
        {
            foreach (Transform child in IconAndSKillShowUISetT) 
            {
                Destroy(child.gameObject);
            }
            
            foreach (KeyValuePair<Data_Center, CharDataInfo> keyValuePair in fightTeam.CharDataInfoRef)
            {
                IconAndSKillShowUISet iconAndSKillShowUISet = Instantiate(IconAndSKillShowUISetPretab);
                SideCharIcon sideCharIcon = fightTeam.GetSideIcon(keyValuePair.Key);
                NineForShow nineForShow = Instantiate(NineForShowPretab);
                iconAndSKillShowUISet.Set(sideCharIcon, nineForShow);
                iconAndSKillShowUISet.transform.SetParent(IconAndSKillShowUISetT);
                iconAndSKillShowUISet.transform.localPosition = Vector3.zero;
                iconAndSKillShowUISet.transform.localScale = Vector3.one;
                
                yield return nineForShow.ShowStones_Acc(keyValuePair.Value.monsterOfPlayerId);
            }
        }
        
        // 胜利字幕与对应页面加载
        public IEnumerator WINProcess()
        {
            Step1.gameObject.SetActive(true);
            CanGotoSummary.Value = false;
            win_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            win_textanimation.gameObject.SetActive(false);
            CanGotoSummary.Value = true;
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(true);
        }
        
        // 失败字幕与对应页面加载
        public IEnumerator LoseProcess()
        {
            Step1.gameObject.SetActive(true);
            CanGotoSummary.Value = false;
            lose_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            lose_textanimation.gameObject.SetActive(false);
            CanGotoSummary.Value = true;
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(true);
        }
        
        public IEnumerator ShowRewards(int golds, int diamond)
        {
            goldrewards.text = golds.ToString();
            diamondrewards.text = diamond.ToString();
            RewardsTransform.gameObject.SetActive(true);
            yield break;
        }
    }
}