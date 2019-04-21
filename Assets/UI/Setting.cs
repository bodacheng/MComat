using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//主界面那里应该通过这些指标来决定defaultPools里的设置信息。
public class Setting : MonoBehaviour {
    public playerinfoReferenceMode _playerinfoReferenceMode;
    public ResourceLoadMode ConfigFileLoadingMode;
    public ResourceLoadMode ModelLoadingMode;
    public ResourceLoadMode AnimationLoadingMode;
    public ResourceLoadMode MagicLoadingMode;
    public ResourceLoadMode IconLoadingMode;
}

public enum ResourceLoadMode
{
    CachAB = 1,
    StreamingAssetAB = 2,
    Resource = 3
}

public enum playerinfoReferenceMode
{
    localTestSaveData = 1,
    remoteTestPlayer = 2,
    formalVersion = 3,
}
