using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//章节这个概念并不存在。这个模块是你提供那么几个关卡的序号，然后它把那几个关卡给读出来。
namespace mainMenu
{
    public class ArcadeManager : MonoBehaviour
    {
        public Canvas _ArcadeCanvas;
        public RectTransform ButtonsContainer;

        [Space(7)]
        [Header("preparingScene")]
        public SingleThreadProcesser mainProcessRunner;

        [Space(7)]
        [Header("StagesButtonManager")]
        public ListPositionCtrl ListPositionCtrl;
        
        [Space(7)]
        [Header("StagesButtonManager")]
        public StageIconObjectBank StageIconObjectBank;
        
        [Space(7)]
        [Header("StageButtonPretab")]
        public ListBox StageButtonPretab;
        
        public static ArcadeManager Instance;

        void Awake()
        {
            Instance = this;
        }
        
        public void LocalTest()
        {
            List<Object> stageScriptableObjects = Resources.LoadAll("stages", typeof(StageScriptableObject)).ToList();
            List<StageScriptableObject> temp = new List<StageScriptableObject>();
            List<ListBox> buttons = new List<ListBox>();
            foreach (Object _object in stageScriptableObjects)
            {
                StageScriptableObject one = (StageScriptableObject)_object;
                temp.Add(one);
                ListBox listBox = Instantiate(StageButtonPretab);
                listBox.listBoxID = one.LocalFightID;
                listBox.transform.SetParent(ButtonsContainer);
                listBox.transform.localScale = new Vector3(1, 1, 1);
                listBox.title.text = one.battleNameCH;
                void testbutton()
                {
                    Debug.Log("关卡："+ one.battleNameCH);
                }
                listBox.button.onClick.AddListener(testbutton);
                buttons.Add(listBox);
            }

            buttons = OrderStagesButtonByNo(buttons);
            
            for (int i = 0; i < buttons.Count; i++)
            {
                Debug.Log(buttons[i].listBoxID+ " hehe ");
            }
            ListPositionCtrl.listBoxes = buttons.ToArray();
            ListPositionCtrl.Initialize();
            StageIconObjectBank.Initialize(temp);
            foreach (ListBox _ListBox in ListPositionCtrl.listBoxes)
            {
                _ListBox.Initialize(ListPositionCtrl);
            }
        }
        
        // 等级升序降序？
        readonly int order = 1;//0:升序 1:降序 //是否按type排序
        List<ListBox> OrderStagesButtonByNo(List<ListBox> originBoxes)
        {
            for (int i = 0; i < originBoxes.Count - 1; i++)
            {
                for (int j = 0; j< originBoxes.Count-1-i; j++)
                {
                    int no1 = originBoxes[j].listBoxID;
                    int no2 = originBoxes[j + 1].listBoxID;
                    if (order == 1 ? no1 > no2 : no1 < no2)
                    {
                        ListBox temp = originBoxes[j];
                        originBoxes[j]=originBoxes[j+1];
                        originBoxes[j + 1] = temp;
                    }
                }
            }
            return originBoxes;
        }
        
        public void Clear()
        {
            ListPositionCtrl.listBoxes = null;
            StageIconObjectBank.Clear();
            foreach (Transform child in ButtonsContainer) 
            {
                Destroy(child.gameObject);
            }
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