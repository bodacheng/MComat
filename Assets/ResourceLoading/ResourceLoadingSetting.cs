using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoadingSetting
{
    private static ResourceLoadingSetting instance;
    public static ResourceLoadingSetting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ResourceLoadingSetting();
            }
            return instance;
        }
    }
    
    public ResourceLoadMode ConfigFileLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode ModelLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode IconLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode AnimationLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode MagicLoadingMode = ResourceLoadMode.Resource;
}
