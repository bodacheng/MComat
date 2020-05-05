using UnityEngine;

public class ResourceSetting : MonoBehaviour
{
    public ResourceLoadMode ConfigFileLoadingMode;
    public ResourceLoadMode ModelLoadingMode;
    public ResourceLoadMode AnimationLoadingMode;
    public ResourceLoadMode MagicLoadingMode;
    public ResourceLoadMode IconLoadingMode;
    public ResourceLoadMode bgmAndCvs;
}

public enum ResourceLoadMode
{
    CachAB = 1,
    StreamingAssetAB = 2,
    Resource = 3
}

public enum playerInfoRefMode
{
    localTestSaveData = 1,
    remoteTestPlayer = 2,
    formalVersion = 3,
}
