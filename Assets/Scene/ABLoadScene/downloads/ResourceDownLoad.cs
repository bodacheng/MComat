using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public partial class ResourceDownLoad : MonoBehaviour
{
    [Space(7)]
    [Header("资源读取设置")]
    public ConfigureOptions _ResourceSetting;
    public LocalMasterDataTool _ConfigFileManager;

    [Space(7)]
    [Header("assetBundleURL。根据服务器可能有变化")]
    public string assetBundleURL = "http://18.218.70.129/ios";
    public static string BundleURL = "http://18.218.70.129/ios";
    IDictionary<string, CachDownLoadMission> DownLoadMissionDic = new Dictionary<string, CachDownLoadMission>();

    /// <summary>
    /// key: typecode
    /// value : 所有基础动画包的名字
    /// </summary>
    IDictionary<string, List<string>> CharTypeCodeAndBasicMoveSets = new Dictionary<string, List<string>>();
    CachDownLoadMission modelConfigFileMission;
    CachDownLoadMission animationConfigFileMission;

    public bool DProcessFinished { get; set; }

    void Start()
    {
        DProcessFinished = false;
        BundleURL = assetBundleURL;
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
                MonstersConfigTable.LoadMonstersConfig();
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
        foreach (MonstersConfigTable.Row row in MonstersConfigTable.rowList)
        {
            if (!CharTypeCodeAndBasicMoveSets.ContainsKey(row.MONSTER_TYPE))
                CharTypeCodeAndBasicMoveSets.Add(row.MONSTER_TYPE, new List<string>());
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
                    LoadingCanvas.target.NowProcess("正在加载资源", i / _ConfigFileManager.chartypes.Length);
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
}
