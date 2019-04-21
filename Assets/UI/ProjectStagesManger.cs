using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectStagesManger : MonoBehaviour {

    public QuestPreparePage _QuestPreparePage;
    public ChapterPreparePage _ChapterPreparePage;
    public List<Season> seasons;

    void Awake()
    {
        AllStagesButtonINI();
    }

    public void showThisSeasonGamen(int focusingSeasonNum)
    {
        foreach (Season season in seasons)
        {
            if (season.SeasonNum == focusingSeasonNum)
                season.gameObject.SetActive(true);
            else
                season.gameObject.SetActive(false);
        }
    }

    public void AllStagesButtonINI()
    {
        foreach (Season season in seasons)
        {
            foreach (Chapter C in season.Chapters)
            {
                C._ChapterPreparePage = _ChapterPreparePage;
                foreach (StageInspector StageInspector in C.StageInspectors)
                {
                    StageInspector._QuestPreparePage = _QuestPreparePage;
                }
                C.ButtonINI();
            }
        }
    }
}
