using System.Collections;
using dataAccess;
using UnityEngine.SceneManagement;
using mainMenu;
using UnityEngine;
using Json;

public class Starter : MonoBehaviour
{
    public PlayerInfoRefMode ProjectPlayerInfoRefMode;
    public bool enterFrontPageFirst;

    // 启动本地测试模式
    public void BeginLocalTestMode()
    {
        StartCoroutine(_BeginLocalTestMode());
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
    
    IEnumerator _BeginLocalTestMode()
    {
        PlayFabLogin.CustomIDLogin(
            result => {
                //CloudScript.StartCloudHelloWorld();
                AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
                EnterFrontScene();
            },
            fail => {
                Debug.Log("login fail");
            }
        );
        yield break;
    }

    IEnumerator SkillShowerMode()
    {
        DeleteLocalSaveDate();
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        yield return AccountSet.OverrideAccountOnLocalFile();
        yield return MySkillStones.LocalSaveDataGetAllStones();
        yield return AccountCharsSet.LocalSaveDataGetAllCharacters();
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

            LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "arena3V3TeamSet.json", arena3V3TeamSet.text);
            LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "localAccountInfo.json", localAccountInfo.text);
            LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "TeamSet.json", TeamSet.text);

            for (int i = 0; i < stones.Length; i++)
            {
                LocalJson.SaveInfoToJsonFile_persistentDataPath("MyStones", stones[i].name + ".json", ((TextAsset)(stones[i])).text);
            }

            for (int i = 0; i < units.Length; i++)
            {
                LocalJson.SaveInfoToJsonFile_persistentDataPath("AccountCharacterInfos", units[i].name + ".json", ((TextAsset)(units[i])).text);
            }
        }
        else
        {
            DeleteLocalSaveDate();
            yield return AccountSet.OverrideAccountOnLocalFile();
            yield return MySkillStones.LocalSaveDataGetAllStones();
            yield return AccountCharsSet.LocalSaveDataGetAllCharacters();
        }
        StageScriptableObject stage = StageScriptableObject.RandomSkillTestStage(TeamMode.rotation);
        stage._fightEventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }
}
