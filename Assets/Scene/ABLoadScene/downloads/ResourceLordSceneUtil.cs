using System.Collections;
using System.Collections.Generic;
using dataAccess;
using UnityEngine.SceneManagement;
using mainMenu;
using Gs2.Weave.Login;
using Gs2.Weave.Credential;
using Weave.Core.Runtime;
using UnityEngine;
using UnityEditor;
using System.IO;
// AssetBundle cache checker & loader with caching
// worsk by loading .manifest file from server and parsing hash string from it
// 资源下载策略：角色模型和技能动画先读取配置文件再根据配置文件一个个请求资源。
// controller的话从上面的步骤里搞一个type统计，从所有type里找对应的controller组件
// 魔法包直接代码引导去下那6个大包。
// 所有资源应该进行一个文件数量统计和容量统计。下载过程中应该是前台有一个动画告诉已经下载到多少。
// 还有个问题。。。我们看了下主场景里startup函数。。。发现的确很多加载性的东西分布在主场景的很多模块。。。
// 所以我们这么想，这个scene只负责资源加载而不负责信息加载。

// 一上来是要load所有的ab包，所有ab包包括什么呢 
// 魔法特效，所有角色，所有角色动画，所有角色controller文件，所有技能脚本，所有音乐包。
// 以上这些如果在load过程里出了任何问题，则应该终止程序运行。这些方法写在哪里都可以只要在头画面里运行就行。
// 然后如果程序运行途中这些下载完了的数据在读取时候出错，那怎么办？不管任何时候， 直接弹回资源确认画面。

public partial class ResourceLordSceneUtil : MonoBehaviour
{
    public PlayerInfoRefMode ProjectPlayerInfoRefMode;

    public bool enterFrontPageFirst;
    
    [Space(7)]
    [Header("资源读取设置")]
    public ResourceSetting _ResourceSetting;
    public ConfigFileManager _ConfigFileManager;
    
    [Space(7)]
    [Header("assetBundleURL。根据服务器可能有变化")]
    public string assetBundleURL = "http://18.218.70.129/ios";
    public static string BundleURL = "http://18.218.70.129/ios";
    IDictionary<string, CachDownLoadMission> DownLoadMissionDic = new Dictionary<string, CachDownLoadMission>();
    IDictionary<string, List<string>> CharTypeCodeAndBasicMoveSets = new Dictionary<string, List<string>>();//key是typecode，值是所有基础动画包的名字
    CachDownLoadMission modelConfigFileMission;
    CachDownLoadMission animationConfigFileMission;
    
    public bool DProcessFinished { get; set; }

    /// <summary>
    /// GS2 相关
    /// </summary>
    /// 
    public me_LoginDirector loginDirector;
    public CredentialDirector credentialDirector;

    public void OnCreateGs2Client(Gs2Client client)
    {
        Debug.Log("SceneDirector::OnCreateGs2Client");
        me_LoginDirector._myclient = client;
        StartCoroutine(loginDirector.Run(client.Client, new PlayerPrefsAccountRepository()));
    }

    public void OnCreateGameSession(Gs2GameSession session)// login
    {
        Debug.Log("SceneDirector::OnCreateGameSession");
        me_LoginDirector._mysession = session;
    }

    void Start()
    {
        DProcessFinished = false;
        BundleURL = assetBundleURL;
    }

    // 测试中的远程模式
    public void BeginRemoteTestMode()
    {
        StartCoroutine(Gs2Login());
    }
    IEnumerator Gs2Login()
    {
        me_LoginDirector.loginFinished = false;
        yield return credentialDirector.Run();
        while (!me_LoginDirector.loginFinished)
        {
            yield return null;
        }
        AccountSet.ReferenceMode = PlayerInfoRefMode.remoteTestPlayer;
        EnterFrontScene();
    }

    public void BeginLocalTestMode()
    {
        StartCoroutine(_BeginLocalTestMode());
    }
    
    public void StartNewLocalTestMode()
    {
        StartCoroutine(_StartNewLocalTestMode());
    }
    
    public void ToMainScene()
    {
        StartCoroutine(ToMainSceneDirectly());
    }
    
    IEnumerator _BeginLocalTestMode()
    {
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        EnterFrontScene();
        yield break;
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
    
    public void DeleteLocalSaveDate()
    {
        LocalJson.DeleteAllUnderFolder(Application.persistentDataPath);
        LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/AccountCharacterInfos");
        LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/MyStones");
    }
    
    IEnumerator ToMainSceneDirectly()
    {
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        yield return AccountSet.OverrideAccountOnLocalFile();
        yield return MySkillStonesReader.LocalSaveDataGetAllStones();
        yield return AccountCharsSet.LocalSaveDataGetAllCharacters();
        SceneManager.LoadScene(1);
    }

    bool forShow = true;
    IEnumerator _StartNewLocalTestMode()
    {
        AccountSet.ReferenceMode = PlayerInfoRefMode.localTestSaveData;
        if (forShow)
        {
            DeleteLocalSaveDate();
            CopyFileTo("Assets/Resources/TestSaveData", Application.persistentDataPath, null);
            CopyFileTo("Assets/Resources/TestSaveData/AccountCharacterInfos", Application.persistentDataPath + "/AccountCharacterInfos", null);
            CopyFileTo("Assets/Resources/TestSaveData/MyStones", Application.persistentDataPath + "/MyStones", null);
        }
        else
        {
            DeleteLocalSaveDate();
            yield return AccountSet.OverrideAccountOnLocalFile();
            yield return MySkillStonesReader.LocalSaveDataGetAllStones();
            yield return AccountCharsSet.LocalSaveDataGetAllCharacters();
        }
        StageScriptableObject stage = StageScriptableObject.RandomSkillTestStage(TeamMode.rotation);
        stage._fightEventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }

    void CopyFileTo(string sourceDir, string backupDir, string extension)
    {
        if (!Directory.Exists(backupDir))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(backupDir);
        }
        string[] picList = Directory.GetFiles(sourceDir, "*" + extension != null ? ("." + extension) : string.Empty);
        foreach (string f in picList)
        {
            // Remove path from the file name.
            string fName = f.Substring(sourceDir.Length + 1);

            // Use the Path.Combine method to safely append the file name to the path.
            // Will overwrite if the destination file already exists.
            File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
        }
    }
    
    public IEnumerator ResourcePrepareProcess()
    {
        ResourceLoadingSetting.ConfigFileLoadingMode = _ResourceSetting.ConfigFileLoadingMode;
        ResourceLoadingSetting.AnimationLoadingMode = _ResourceSetting.AnimationLoadingMode;
        ResourceLoadingSetting.MagicLoadingMode = _ResourceSetting.MagicLoadingMode;
        ResourceLoadingSetting.ModelLoadingMode = _ResourceSetting.ModelLoadingMode;
        ResourceLoadingSetting.IconLoadingMode = _ResourceSetting.IconLoadingMode;
        
        LoadingCanvas.target.DarkOff(0.5f);
        LoadingCanvas.target.TurnOnProcessDescription(true);
        LoadingCanvas.target.NowProcess("正在加载资源", 0);
        
        switch (ResourceLoadingSetting.ConfigFileLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return ConfigFilesDownLoad();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                yield return MonstersConfigTable.Instance.LoadMonstersConfig();
                yield return SkillConfigTable.LoadAllSkillConfigs();
                PowerEstimateTable.Load();
                
                LevelExpConfig.LoadLevelExpConfig();
                LoadingCanvas.target.NowProcess("正在加载资源", 0.3f);
                break;
        }
        
        switch (ResourceLoadingSetting.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return ModelResourceDownLoad();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                //Resource模式下模型都是现加载。
                break;
        }
        LoadingCanvas.target.NowProcess("正在加载资源", 0.6f);
        
        // CharTypeCodeAndBasicMoveSets 记录了角色配置文件所出现的所有角色type以及出现的所有基础动画包的名字。
        foreach (MonstersConfigTable.Row row in MonstersConfigTable.Instance.rowList)
        {
            if (!CharTypeCodeAndBasicMoveSets.ContainsKey(row.MONSTER_TYPE))
                CharTypeCodeAndBasicMoveSets.Add(row.MONSTER_TYPE,new List<string>());
            if (!CharTypeCodeAndBasicMoveSets[row.MONSTER_TYPE].Contains(row.BASIC_MOVEMENT_PACK))
            {
                CharTypeCodeAndBasicMoveSets[row.MONSTER_TYPE].Add(row.BASIC_MOVEMENT_PACK);
            }
        }

        switch (ResourceLoadingSetting.AnimationLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return characterComponentsDownload();
                yield return AnimationResourceDownLoad();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                // 测试版本要把所有动画片段全加载，不能像正式版那样按照角色技能分个加载，原因是动画片段地址机理不同。
                int i = 0;
                foreach (string type in _ConfigFileManager.chartypes)
                {
                    AnimationResourceLoader.Instance.PrepareAllAttackAnimationClipsByTypeFromResourceAndPutItIntoDic(type);
                    i++;
                    LoadingCanvas.target.NowProcess("正在加载资源", i/_ConfigFileManager.chartypes.Length);
                    yield return null;
                }
                break;
        }
        
        switch (ResourceLoadingSetting.MagicLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                EffectsDownLoadByCach();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                break;
        }
        LoadingCanvas.target.NowProcess("正在加载资源", 1f);
        LoadingCanvas.target.TurnOnProcessDescription(false);
        DProcessFinished = true;
    }

    // 初始热更新所还欠缺的环节
    // 1.两个配置文件下载到内存后广播至整个程序的问题
    // 2.容量总结。没有办法估算容量说明我们靠两个主表配置文件来做下载统计是不成熟的。不过我难道可以先远程读一下容量再开始下载。。？
    // 3.下载进程显示。
    // 4.下载错误总结. 进入主程序文件审核。
    // 5.主程运行中文件检查，重下载。
    IEnumerator DownloadingProcess()
    {
        foreach (KeyValuePair<string, CachDownLoadMission> _keyvalue in DownLoadMissionDic)
        {
            yield return LetThisloadMissionBegin(_keyvalue.Value);
        }
        DownLoadMissionDic.Clear();
        yield break;
    }
    
    IEnumerator LetThisloadMissionBegin(CachDownLoadMission _CachDownLoadMission)
    {
        IEnumerator task;
        if (_CachDownLoadMission != null)
        {
            task = CachManager.Instance.DownloadAndCacheExactFile(BundleURL + "/" +_CachDownLoadMission.subPath, _CachDownLoadMission.filename);
            yield return task;
            _CachDownLoadMission.downloadfinished = task.Current != null;
        }
        else{
            Debug.Log("下载任务建立错误");
        }
    }
}
