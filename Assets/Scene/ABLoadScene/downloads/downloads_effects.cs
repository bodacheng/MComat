using System.Collections;
using UnityEngine;

public partial class ResourceLordSceneUtil : MonoBehaviour
{
    IEnumerator EffectsDownLoadByCach()
    {
        CachDownLoadMission defaultMagic = new CachDownLoadMission("Magics", "defaultmagic", 0);
        CachDownLoadMission redMagic = new CachDownLoadMission("Magics", "redmagic", 0);
        CachDownLoadMission greenMagic = new CachDownLoadMission("Magics", "greenmagic", 0);
        CachDownLoadMission blueMagic = new CachDownLoadMission("Magics", "bluemagic", 0);
        CachDownLoadMission darkMagic = new CachDownLoadMission("Magics", "darkmagic", 0);
        CachDownLoadMission lightMagic = new CachDownLoadMission("Magics", "lightmagic", 0);
        
        DownLoadMissionDic.Add("Magics/defaultmagic", defaultMagic);
        DownLoadMissionDic.Add("Magics/redmagic", redMagic);
        DownLoadMissionDic.Add("Magics/greenmagic", greenMagic);
        DownLoadMissionDic.Add("Magics/bluemagic", blueMagic);
        DownLoadMissionDic.Add("Magics/darkmagic", darkMagic);
        DownLoadMissionDic.Add("Magics/lightmagic", lightMagic);
        
        yield return DownloadingProcess();
    }
}
