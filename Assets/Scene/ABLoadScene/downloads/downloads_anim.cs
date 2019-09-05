using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AssetBundleLoader : MonoBehaviour
{
    private IEnumerator AnimationResourceDownLoad()
    {
        foreach (SkillConfigTable.Row row in SkillsConfigInfos.skillConfigTable.rowList)
        {
            CachDownLoadMission _oneMission = new CachDownLoadMission("animClips/" + row.type + "/skills", row.keyName, 0f);
            DownLoadMissionDic.Add("animClips/" + row.type + "/skills/" + row.keyName, _oneMission);//本地读取每个技能的key也是这个key。
        }
        yield return downloadingProcess();
    }
}
