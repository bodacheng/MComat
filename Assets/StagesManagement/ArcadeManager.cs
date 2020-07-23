using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using dataAccess;
using System.Collections;

namespace mainMenu
{
    public class ArcadeManager : MonoBehaviour
    {
        public Canvas _ArcadeCanvas;
        public RectTransform ButtonsContainer;
        
        [Space(7)]
        [Header("ScrollRect")]
        public ScrollRect _ScrollRect;
        
        [Space(7)]
        [Header("ViewScrollBar")]
        public Scrollbar _Scrollbar;
        
        [Space(7)]
        [Header("StageButtonPretab")]
        public StageButton pretab;
        
        public static ArcadeManager target;
        //List<StageButton> stageButtons = new List<StageButton>();

        public static IDictionary<int, StageInfo> ArcadeStages = new Dictionary<int, StageInfo>();
        
        void Awake()
        {
            target = this;
        }
        
        public class StageInfo
        {
            public StageScriptableObject stageConfig;
            public StageButton stageButton;
        }
        
        public StageButton GetStageButton(int stageno)
        {
            return ArcadeStages[stageno]?.stageButton;
        }

        // 原则上这些玩意没有每次都去生成的道理..
        // 而且这个功能可能做一些扩展，比如关卡图标可以搞个特殊一类的
        // 2020523 : 计划根据账户进度选择是否显示隐藏关卡
        public IEnumerator GenerateStageButtons()
        {
            List<Object> stageScriptableObjects = Resources.LoadAll("StageConfigFiles", typeof(StageScriptableObject)).ToList();
            foreach (Object _object in stageScriptableObjects)
            {
                StageScriptableObject one = (StageScriptableObject)_object;
                if (!ArcadeStages.ContainsKey(one.LocalFightID))
                {
                    StageButton newButton = Instantiate(pretab);
                    void LoadThisStage()
                    {
                        FightPreparePage.target.PreLoad(ArcadeStages[one.LocalFightID].stageConfig, TeamSetGameMode.story);
                        PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo,true);
                    }
                    newButton.button.onClick.AddListener(LoadThisStage);
                    newButton.ID = one.LocalFightID;
                    newButton.text.text = "Stage" + one.LocalFightID.ToString();
                    
                    StageInfo stageInfo = new StageInfo
                    {
                        stageConfig = one,
                        stageButton = newButton
                    };
                    ArcadeStages.Add(one.LocalFightID, stageInfo);
                }else{
                    Debug.Log("重复的Arcade模式关卡ID："+ one.LocalFightID);
                }
            }
            
            for (int i = 0; i < 100; i++) // 假设有100关
            {
                if (!ArcadeStages.ContainsKey(i))
                    continue;
                ArcadeStages[i].stageButton.gameObject.SetActive(true);
                ArcadeStages[i].stageButton.gameObject.transform.SetParent(ButtonsContainer);
                ArcadeStages[i].stageButton.gameObject.transform.localScale = Vector3.one;
            }
            VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
            ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x,
            (pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * ArcadeStages.Count);
            yield break;
        }
        
        // Button feature
        public void JumpToNewest()
        {
            JumpTo(AccountSet._AccInfo.ArcadeProcess);
        }
        
        public void JumpTo(int stageNum)
        {
            float targetScrollbarValue;
            if (stageNum <= 3)//5 是现在scrollview里所最多能显示的关卡按钮数量
            {
                targetScrollbarValue = 0;
            }else{
                VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
                // 重点在于对Scrollbar.value的理解。这个值是scrollview边界目前超出框的长度与可能超出框框最大长度的比值
        
                targetScrollbarValue = 
                ((pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * (stageNum - 3)) // 分子。如果希望对象关卡不是出现在中间，可调整这个数字。
                / (ButtonsContainer.sizeDelta.y - _ScrollRect.GetComponent<RectTransform>().rect.height); // 分母
            }
            DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value= x,targetScrollbarValue,0.5f);
        }
        
        public void Clear()
        {
            foreach (Transform child in ButtonsContainer)
            {
                Destroy(child.gameObject);
            }
        }
        
        //// 等级升序降序
        //readonly int order = 0;//0:升序 1:降序 //是否按type排序
        //List<StageButton> OrderStagesButtonByNo(List<StageButton> originBoxes)
        //{
        //    for (int i = 0; i < originBoxes.Count - 1; i++)
        //    {
        //        for (int j = 0; j < originBoxes.Count - 1 - i; j++)
        //        {
        //            int no1 = originBoxes[j].ID;
        //            int no2 = originBoxes[j + 1].ID;
        //            if (order == 1 ? no1 > no2 : no1 < no2)
        //            {
        //                StageButton temp = originBoxes[j];
        //                originBoxes[j] = originBoxes[j + 1];
        //                originBoxes[j + 1] = temp;
        //            }
        //        }
        //    }
        //    return originBoxes;
        //}
    }
}