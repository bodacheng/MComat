using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个组件更多来说应该起名字叫做关卡阅读器。
public class ChapterPreparePage : MonoBehaviour {

    public preparingScene _preparingScene;

    [Space(7)]
    [Header("Chapter Page")]
    public Text ChapterTitle;
    public RectTransform ChaptersT;
    public GameObject StageButton;

    public void loadChapterPage(Chapter chapter)
    {
        foreach (Transform _chapterT in ChaptersT)
        {
            Destroy(_chapterT.gameObject);
        }

        _preparingScene.trySwitchToStep(MainSceneStep.Chapter,true);
        StageButton.SetActive(false);
        ChapterTitle.text = chapter.ChapterName;
        foreach (StageInspector stage in chapter.StageInspectors)
        {
            GameObject stageBUtton = GameObject.Instantiate(StageButton);
            stageBUtton.SetActive(true);
            if (stageBUtton.GetComponent<Text>() != null)
                stageBUtton.GetComponent<Text>().text = stage.battleNameJPG;
            stageBUtton.transform.SetParent(ChaptersT);
            stageBUtton.transform.localScale = new Vector3(1f, 1f, 1f);

            UnityEngine.Events.UnityAction stageBUttonFeature = () =>
            {
                stage.loadStageByScriptThenGetReadyForIt();
            };
            stageBUtton.GetComponent<Button>().onClick.AddListener(stageBUttonFeature);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
