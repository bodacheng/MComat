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
        [Header("ViewScroll")]
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
            List<Object> stageScriptableObjects = Resources.LoadAll("stages", typeof(StageScriptableObject)).ToList();
            foreach (Object _object in stageScriptableObjects)
            {
                StageScriptableObject one = (StageScriptableObject)_object;
                StageButton newButton = Instantiate(pretab);
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
            float value = (float)(int.Parse(_StageNoInput.text)) / (float)stageButtons.Count;
            Debug.Log("to value:"+value);
            DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value= x,value,1f);
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