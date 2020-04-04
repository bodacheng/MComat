using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//章节这个概念并不存在。这个模块是你提供那么几个关卡的序号，然后它把那几个关卡给读出来。
namespace mainMenu
{
    public class ArcadeManager : MonoBehaviour
    {
        public Canvas _ArcadeCanvas;
        public RectTransform ButtonsContainer;
        
        [Space(7)]
        [Header("Stage no input")]
        public InputField _StageNoInput;

        [Space(7)]
        [Header("ScrollRect")]
        public ScrollRect _ScrollRect;
           
        [Space(7)]
        [Header("ViewScrollBar")]
        public Scrollbar _Scrollbar;

        [Space(7)]
        [Header("StageButtonPretab")]
        public StageButton pretab;

        [Space(7)]
        [Header("preparingScene")]
        public SingleThreadProcesser mainProcessRunner;
                        
        public static ArcadeManager Instance;

        List<StageButton> stageButtons = new List<StageButton>();
        
        void Awake()
        {
            Instance = this;
        }
        
        public void LocalTest()
        {
            List<Object> stageScriptableObjects = Resources.LoadAll("StageConfigFiles", typeof(StageScriptableObject)).ToList();
            foreach (Object _object in stageScriptableObjects)
            {
                StageScriptableObject one = (StageScriptableObject)_object;
                StageButton newButton = Instantiate(pretab);

                void LoadThisStage()
                {
                    mainProcessRunner.Run(QuestPreparePage.Instance.LoadStageByScriptThenGetReadyForIt(one));
                }

                newButton.button.onClick.AddListener(LoadThisStage);
                newButton.ID = one.LocalFightID;
                stageButtons.Add(newButton);
                newButton.text.text = "Stage" + one.LocalFightID.ToString();
            }
            stageButtons = OrderStagesButtonByNo(stageButtons);
            for (int i = 0; i < stageButtons.Count; i++)
            {
                stageButtons[i].transform.SetParent(ButtonsContainer);
            }
            VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
            ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x,
            (pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * stageButtons.Count);
        }
        
        public void JumpTo()
        {
            int targetNum = int.Parse(_StageNoInput.text);
            float targetScrollbarValue;
            if (targetNum <= 3)//5 是现在scrollview里所最多能显示的关卡按钮数量
            {
                targetScrollbarValue = 0;
            }else{
                VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
                // 重点在于对Scrollbar.value的理解。这个值是scrollview边界目前超出框的长度与可能超出框框最大长度的比值
                
                targetScrollbarValue = 
                ((pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * (targetNum - 3)) // 分子。如果希望对象关卡不是出现在中间，可调整这个数字。
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
        
        // 等级升序降序？
        readonly int order = 0;//0:升序 1:降序 //是否按type排序
        List<StageButton> OrderStagesButtonByNo(List<StageButton> originBoxes)
        {
            for (int i = 0; i < originBoxes.Count - 1; i++)
            {
                for (int j = 0; j < originBoxes.Count - 1 - i; j++)
                {
                    int no1 = originBoxes[j].ID;
                    int no2 = originBoxes[j + 1].ID;
                    if (order == 1 ? no1 > no2 : no1 < no2)
                    {
                        StageButton temp = originBoxes[j];
                        originBoxes[j] = originBoxes[j + 1];
                        originBoxes[j + 1] = temp;
                    }
                }
            }
            return originBoxes;
        }
        
        //public IEnumerator LoadChapterPage(List<string> stagesIDs)
        //{
        //    foreach (string stageid in stagesIDs)
        //    {
        //        StageScriptableObject stageScriptableObject = Resources.Load("stages/" + stageid, typeof(ScriptableObject)) as StageScriptableObject;
        //        if (stageScriptableObject == null)
        //        {
        //            Debug.Log("没找到关卡信息：" + stageid);
        //            continue;
        //        }
        //        stageButton stageButton = Instantiate(StageButton);
        //        stageButton.gameObject.SetActive(true);
        //        stageButton.title.text = stageScriptableObject.battleNameJPG;
        //        stageButton.transform.SetParent(ChaptersT);
        //        stageButton.transform.localScale = new Vector3(1f, 1f, 1f);
        //        void StageBUttonFeature()
        //        {
        //            mainProcessRunner.TriggerMainProcess(QuestPreparePage.Instance.LoadStageByScriptThenGetReadyForIt(stageScriptableObject));
        //        }
        //        stageButton.button.onClick.AddListener(StageBUttonFeature);
        //        if (stageScriptableObject.StageButtonSprite)
        //        {
        //            stageButton.buttonImage.sprite = stageScriptableObject.StageButtonSprite;
        //        }
        //    }
        //    yield break;
        //}
    }
}