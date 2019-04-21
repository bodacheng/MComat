using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Playables;
using System.Xml;
using System.Xml.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StageInspector : MonoBehaviour {

    public string LocalFightID;

    public string battleNameENG;
    public string battleNameJPG;
    public string battleNameCH;

    [SerializeField] public PlayableAsset beforefightstory;
    public TextAsset Script;
    public LocalFight _LocalFight;

    public QuestPreparePage _QuestPreparePage;

    public void loadStageByScriptThenGetReadyForIt()
    {
        loadOneLocalFightByScript();

        _LocalFight._team1positionLocalCharKeySet = TeamSet.Instance._positionLocalCharKeySet4V4Mode;
        _LocalFight.team1members = TeamSet.Instance.myTeamMembersByEntryMemberNum(_LocalFight.EntryMemberNum);

        Stage stage = new Stage();
        stage._LocalFight = _LocalFight;
        stage.battleNameCH = this.battleNameCH;
        stage.battleNameENG = this.battleNameENG;
        stage.battleNameJPG = this.battleNameJPG;
        stage.beforefightstory = this.beforefightstory;
        _QuestPreparePage._preparingScene.triggerMainProcess(_QuestPreparePage.getReadyToBattle(stage,SceneMode.QuestFight));
    }

    public LocalFight loadOneLocalFightByScript()
    {
        if (Script == null)
            return null;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalFight));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    _LocalFight = serializer.Deserialize(textReader) as LocalFight;
                }
            }
            else
            {
                var reader = new System.IO.StringReader(Script.text);
                _LocalFight = serializer.Deserialize(reader) as LocalFight;
                Debug.Log("貌似已经成功读取了4V4模式随机战斗信息");
            }
            _LocalFight.generateTeamPosAndFixLocalIDmaybe();
            return _LocalFight;
        }
        catch (Exception e)
        {
            Debug.Log("4V4战斗信息读取失败");
            Debug.Log(e.ToString());
            return null;
        }
    }
}
