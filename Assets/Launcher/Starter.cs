using dataAccess;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Json;
using mainMenu;

public partial class Starter : MonoBehaviour
{
    public PlayerInfoRefMode ProjectPlayerInfoRefMode;
    public bool enterFrontPageFirst;

    // 启动本地测试模式
    public void BeginLocalTestMode()
    {
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        EnterFrontScene();
    }

    // 启动本地测试模式（新存档）
    public void StartNewLocalTestMode()
    {
        PlayFabLogin.CustomIDLogin(
            result =>
            {
                StartCoroutine(_StartNewLocalTestMode());
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    // 启动技能浏览器模式
    public void ToSkillShowerMode()
    {
        StartCoroutine(SkillShowerMode());
    }

    IEnumerator SkillShowerMode()
    {
        DeleteLocalSaveDate();
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        MySkillStones.LocalSaveDataGetAllStones();
        yield return MyMonsters.LocalSaveDataGetAllCharacters();
        SceneManager.LoadScene(1);
    }

    // 进入开头画面
    void EnterFrontScene()
    {
        if (enterFrontPageFirst)
        {
            StageScriptableObject stage = StageScriptableObject.RandomSkillTestStage(TeamMode.rotation);
            stage._fightEventType = FightEventType.Screensaver;
            FightLoad.Go(stage);
        }else{
            MainMenuNote.goingtostep = MainSceneStep.FrontPage;
            SceneManager.LoadScene(1);
        }
    }

    // 删除本地存档
    public void DeleteLocalSaveDate()
    {
        LocalJson.DeleteAllUnderFolder(Application.persistentDataPath);
        LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/AccountCharacterInfos");
        LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/MyStones");
    }

    /// <summary>
    /// 是否使用提前准备好的存档文件，即Resources/TestSaveData下的各本地存档
    /// </summary>
    bool UseBackUpData = false;
    IEnumerator _StartNewLocalTestMode()
    {
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        if (UseBackUpData)
        {
            DeleteLocalSaveDate();

            TextAsset arena3V3TeamSet = Resources.Load("TestSaveData/arena3V3TeamSet") as TextAsset;
            TextAsset localAccountInfo = Resources.Load("TestSaveData/localAccountInfo") as TextAsset;
            TextAsset TeamSet = Resources.Load("TestSaveData/TeamSet") as TextAsset;

            Object[] stones = Resources.LoadAll("TestSaveData/MyStones", typeof(TextAsset));
            Object[] units = Resources.LoadAll("TestSaveData/AccountCharacterInfos", typeof(TextAsset));

            LocalJson.SaveToJsonFile_persistentDataPath(null, "arena3V3TeamSet.json", arena3V3TeamSet.text);
            LocalJson.SaveToJsonFile_persistentDataPath(null, "localAccountInfo.json", localAccountInfo.text);
            LocalJson.SaveToJsonFile_persistentDataPath(null, "TeamSet.json", TeamSet.text);

            for (int i = 0; i < stones.Length; i++)
            {
                LocalJson.SaveToJsonFile_persistentDataPath("MyStones", stones[i].name + ".json", ((TextAsset)(stones[i])).text);
            }

            for (int i = 0; i < units.Length; i++)
            {
                LocalJson.SaveToJsonFile_persistentDataPath("AccountCharacterInfos", units[i].name + ".json", ((TextAsset)(units[i])).text);
            }
        }
        else
        {
            DeleteLocalSaveDate();
            MySkillStones.LocalSaveDataGetAllStones();
            yield return MyMonsters.LocalSaveDataGetAllCharacters();
        }
        StageScriptableObject stage = StageScriptableObject.RandomSkillTestStage(TeamMode.rotation);
        stage._fightEventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }

    // 启动网络模式
    public void BeginNetMode()
    {
        PlayFabLogin.CustomIDLogin(
            result => {
                Debug.Log( " 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken );
                AccountSet._AccInfo = new PlayerAccountInfo();
                AccountSet._AccInfo.playerID = result.PlayFabId;
                //CloudScript.GrantStonesTest();
                AccountSet.ReferenceMode = PlayerInfoRefMode.remoteTestPlayer;
                EnterFrontScene();
            },
            fail => {
                Debug.Log("login fail");
            }
        );
    }
}
