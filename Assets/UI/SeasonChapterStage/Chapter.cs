using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个元件就是放在Chapter按钮上的。
public class Chapter : MonoBehaviour {

    public string unlockConditionStageID;

    public string ChapterName;
    public ChapterPreparePage _ChapterPreparePage;
    public List<StageInspector> StageInspectors;

    public Button ChapterButton;

    public void ButtonINI()
    {
        UnityEngine.Events.UnityAction buttonFeature = () =>
        {
            Debug.Log("Chapter按钮已经按下");
            _ChapterPreparePage.loadChapterPage(this);
        };

        if (ChapterButton != null)
        {
            ChapterButton.onClick.RemoveAllListeners();
            ChapterButton.onClick.AddListener(buttonFeature);
        }
    }
}
