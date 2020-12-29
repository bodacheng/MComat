using System.Collections;
using UnityEngine;

public partial class ResourceDownLoad : MonoBehaviour
{
    private IEnumerator AnimationResourceDownLoad()
    {
        foreach (SkillConfigTable.Row row in SkillConfigTable.rowList)
        {
            CachDownLoadMission _oneMission = new CachDownLoadMission("animClips/" + row.USEABLE_MONSTER_TYPE + "/skills", row.REAL_NAME, 0f);
            DownLoadMissionDic.Add("animClips/" + row.USEABLE_MONSTER_TYPE + "/skills/" + row.REAL_NAME, _oneMission);//本地读取每个技能的key也是这个key。
        }
        yield return DownloadingProcess();
    }
}
