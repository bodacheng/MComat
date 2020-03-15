using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//章节这个概念并不存在。这个模块是你提供那么几个关卡的序号，然后它把那几个关卡给读出来。
namespace mainMenu
{
    public class ChaptersManager : MonoBehaviour
    {
        [Space(7)]
        [Header("preparingScene")]
        public PreScene _preparingScene;
        public SingleThreadProcesser mainProcessRunner;

        [Space(7)]
        [Header("章节标题")]
        public Text title;

        [Space(7)]
        [Header("ChaptersT")]
        public RectTransform ChapterInfoT;
        public RectTransform ChaptersT;

        [Space(7)]
        [Header("这个章节对应关卡号码的列表")]
        public List<string> Chapter1stageIds;

        [Space(7)]
        [Header("StageButton")]
        public stageButton StageButton;

        public void OpenChapter1()
        {
            title.text = "第一章：蘑菇大冒险";
            mainProcessRunner.TriggerMainProcess(loadChapterPage(Chapter1stageIds));
        }

        public void clearStagesButtons()
        {
            foreach (Transform _chapterT in ChaptersT)
                Destroy(_chapterT.gameObject);
        }

        public IEnumerator loadChapterPage(List<string> stagesIDs)
        {
            clearStagesButtons();
            foreach (string stageid in stagesIDs)
            {
                StageScriptableObject stageScriptableObject = Resources.Load("stages/" + stageid, typeof(ScriptableObject)) as StageScriptableObject;
                if (stageScriptableObject == null)
                {
                    Debug.Log("没找到关卡信息：" + stageid.ToString());
                    continue;
                }
                stageButton stageBUtton = Instantiate(StageButton);
                stageBUtton.gameObject.SetActive(true);
                stageBUtton.title.text = stageScriptableObject.battleNameJPG;
                stageBUtton.transform.SetParent(ChaptersT);
                stageBUtton.transform.localScale = new Vector3(1f, 1f, 1f);

                UnityEngine.Events.UnityAction stageBUttonFeature = () =>
                {
                    mainProcessRunner.TriggerMainProcess(_preparingScene._QuestPreparePage.LoadStageByScriptThenGetReadyForIt(stageScriptableObject));
                };
                stageBUtton.button.onClick.AddListener(stageBUttonFeature);
                if (stageScriptableObject.StageButtonSprite)
                    stageBUtton.buttonImage.sprite = stageScriptableObject.StageButtonSprite;
            }
            yield break;
        }
    }
}