using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// AssetBundle cache checker & loader with caching
// worsk by loading .manifest file from server and parsing hash string from it

// 资源下载策略：角色模型和技能动画先读取配置文件再根据配置文件一个个请求资源。
// controller的话从上面的步骤里搞一个type统计，从所有type里找对应的controller组件
// 魔法包直接代码引导去下那6个大包。

// 所有资源应该进行一个文件数量统计和容量统计。下载过程中应该是前台有一个动画告诉已经下载到多少。

//还有个问题。。。我们看了下主场景里startup函数。。。发现的确很多加载性的东西分布在主场景的很多模块。。。
//所以我们这么想，这个scene只负责资源加载而不负责信息加载。

public class CachDownLoadMission
{
    public string filename;
    public string subPath;
    public float filesize;
    public bool downloadfinished = false;

    public CachDownLoadMission(string subPath,string filename, float filesize)
    {
        this.filename = filename;
        this.subPath = subPath;
        this.filesize = filesize;
        this.downloadfinished = false;
    }
}

//Resources.UnloadUnusedAssets();
public partial class AssetBundleLoader : MonoBehaviour
{
    [Header("资源读取设置")]
    public Setting _Setting;
    public ConfigFileManager _ConfigFileManager;
    
    [Space(7)]
    [Header("LoadingProcess")]
    public LoadingCanvas _LoadingCanvas;
    
    public string assetBundleURL = "http://18.218.70.129/ios";
    public static string BundleURL = "http://18.218.70.129/ios";

    public IDictionary<string, CachDownLoadMission> DownLoadMissionDic = new Dictionary<string, CachDownLoadMission>();
    public IDictionary<string, List<string>> characterTypeAndBasicMoveSets = new Dictionary<string, List<string>>();//key是type，值是所有基础动画包的名字

    private CachDownLoadMission modelConfigFileMission;
    private CachDownLoadMission animationConfigFileMission;

    private TextAsset CharacterConfigTextFile;
    private TextAsset SkillConfigTextFile;
    
    private monstersConfigTable monstersTable;
    private SkillConfigTable skillConfigTable;

    private IEnumerator _loadingProcess;
    private bool startupsucessed = false;
    
    void Start()
    {
        BundleURL = assetBundleURL;
        //StartCoroutine(DownloadAndCacheExactFile(assetBundleURL,"MagicsAB/darkmagic"));
        //StartCoroutine(justTryToLoadABFromCache(assetBundleURL,"MagicsAB","darkmagic"));
        StartCoroutine(StartUpProcess());
    }
    
    public IEnumerator StartUpProcess()
    {
        AccountSet.Instance._playerinfoReferenceMode = _Setting._playerinfoReferenceMode;
        defaultPools.Instance.ConfigFileLoadingMode = _Setting.ConfigFileLoadingMode;
        defaultPools.Instance.AnimationLoadingMode = _Setting.AnimationLoadingMode;//确认动画资源读取模式
        defaultPools.Instance.MagicLoadingMode = _Setting.MagicLoadingMode;//确认动画资源读取模式
        defaultPools.Instance.ModelLoadingMode = _Setting.ModelLoadingMode;//确认模型资源读取模式
        defaultPools.Instance.IconLoadingMode = _Setting.IconLoadingMode;//确认模型资源读取模式
        
        _LoadingCanvas.Loading_Canvas.gameObject.SetActive(true);
        _LoadingCanvas.turnOnProcessDescription(true);
        _LoadingCanvas.nowProcess("正在加载资源",0);
        
        
        switch (defaultPools.Instance.ConfigFileLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return ConfigFilesDownLoad();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                break;
        }
        
        switch (defaultPools.Instance.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return ModelResourceDownLoad();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                break;
        }

        switch (defaultPools.Instance.AnimationLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return AnimationResourceDownLoad();
                yield return characterComponentsDownload();//这个是建立在ModelResourceDownLoad()流程中顺便做好了characterTypeAndBasicMoveSets
                break;
            case ResourceLoadMode.StreamingAssetAB:
                yield return (defaultPools.Instance.PrepareMagicFromStreamingAssets("defaultmagic"));
                break;
            case ResourceLoadMode.Resource:
                //这些的存在是出于测试版本(Resource)的角色画面详细的技能表示功能，正式版不是ResourceLoadMode.Resource所以不起作用。
                //测试版本要按着文件夹把所有动画片段全加载，不能像正式版那样按照角色技能分个加载，原因是动画片段地址机理不同。
                int i = 0;
                foreach (string type in _ConfigFileManager.chartypes)
                {
                    defaultPools.Instance.prepareAllAttackAnimationClipsByTypeFromResourceAndPutItIntoDic(type);
                    i++;
                    _LoadingCanvas.nowProcess("正在加载资源",i/_ConfigFileManager.chartypes.Length);
                }
                break;
        }
                
        switch (defaultPools.Instance.MagicLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                EffectsDownLoadByCach();
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                break;
        }
        
        SceneManager.LoadScene(1);
    }

    // 初始热更新所还欠缺的环节
    // 1.两个配置文件下载到内存后广播至整个程序的问题
    // 2.容量总结。没有办法估算容量说明我们靠两个主表配置文件来做下载统计是不成熟的。不过我难道可以先远程读一下容量再开始下载。。？
    // 3.下载进程显示。
    // 4.下载错误总结. 进入主程序文件审核。
    // 5.主程运行中文件检查，重下载。
    IEnumerator downloadingProcess()
    {
        foreach (KeyValuePair<string, CachDownLoadMission> _keyvalue in DownLoadMissionDic)
        {
            yield return letThisloadMissionBegin(_keyvalue.Value);
        }
        DownLoadMissionDic.Clear();
    }

    public IEnumerator letThisloadMissionBegin(CachDownLoadMission _CachDownLoadMission)
    {
        IEnumerator task;
        if (_CachDownLoadMission != null)
        {
            task = defaultPools.Instance.DownloadAndCacheExactFile(assetBundleURL + "/" +_CachDownLoadMission.subPath, _CachDownLoadMission.filename);
            yield return task;
            if (task.Current != null)
                _CachDownLoadMission.downloadfinished = true;
            else
                _CachDownLoadMission.downloadfinished = false;
        }else{
            Debug.Log("下载任务建立错误");
        }
    }

    // 一上来是要load所有的ab包，所有ab包包括什么呢 
    //  魔法特效，所有角色，所有角色动画，所有角色controller文件，所有技能脚本，所有音乐包。
    // 以上这些如果在load过程里出了任何问题，则应该终止程序运行。这些方法写在哪里都可以只要在头画面里运行就行。
    // 然后如果程序运行途中这些下载完了的数据在读取时候出错，那怎么办？不管任何时候， 直接弹回资源确认画面。
}
