using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using dataAccess;

namespace FightScene
{
    public class FightOverControl : MonoBehaviour
    {
        public static FightOverControl target;
        
        [Header("FightOverCanvas")]
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
        
        [Header("RankInfo")]
        public RankInfo rankInfo;
        
        void Awake()
        {
            target = this;
        }
        
        public void Clear()
        {
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(false);
            FightOverCanvas.gameObject.SetActive(false);
            foreach(NineForShow nineForShow in NineForShows)
            {
                nineForShow.ClearCurrent();
            }
        }
        
        // 战斗结束后统计技能石升级情况时的画面显示
        List<NineForShow> NineForShows = new List<NineForShow>();
        public IEnumerator ShowSKillSets(FightTeam fightTeam)
        {
            NineForShows.Clear();
            foreach (Transform child in IconAndSKillShowUISetT) 
            {
                Destroy(child.gameObject);
            }
            
            foreach (KeyValuePair<Data_Center, CharDataInfo> keyValuePair in fightTeam.CharDataInfoRef)
            {
                IconAndSKillShowUISet iconAndSKillShowUISet = Instantiate(IconAndSKillShowUISetPretab);
                SideCharIcon sideCharIcon = fightTeam.GetSideIcon(keyValuePair.Key);
                NineForShow nineForShow = Instantiate(NineForShowPretab);
                NineForShows.Add(nineForShow);
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
            win_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            win_textanimation.gameObject.SetActive(false);
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(true);
        }
        
        // 失败字幕与对应页面加载
        public IEnumerator LoseProcess()
        {
            Step1.gameObject.SetActive(true);
            lose_textanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            lose_textanimation.gameObject.SetActive(false);
            Step1.gameObject.SetActive(false);
            Step2.gameObject.SetActive(true);
        }
        
        public void ShowRewards(int golds, int diamond)
        {
            goldrewards.text = golds.ToString();
            diamondrewards.text = diamond.ToString();
            RewardsTransform.gameObject.SetActive(true);
        }
    }
}