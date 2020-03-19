using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//章节这个概念并不存在。这个模块是你提供那么几个关卡的序号，然后它把那几个关卡给读出来。
namespace mainMenu
{
    public class ArcadeManager : MonoBehaviour
    {
        [Space(7)]
        [Header("T")]
        public RectTransform _T;

        [Space(7)]
        [Header("preparingScene")]
        public SingleThreadProcesser mainProcessRunner;

        [Space(7)]
        [Header("这个章节对应关卡号码的列表")]
        public List<string> Chapter1stageIds;

        [Space(7)]
        [Header("StageButton")]
        public stageButton StageButton;

        public static ArcadeManager Instance;

        void Awake()
        {
            Instance = this;
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