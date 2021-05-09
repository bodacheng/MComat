using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceDownLoad : MonoBehaviour
{
    private IEnumerator ModelResourceDownLoad()
    {
        foreach (MonstersConfigTable.Row row in MonstersConfigTable.rowList)
        {            
            //模型下载任务
            CachDownLoadMission _oneMission = new CachDownLoadMission( "CharPretabs/" + row.MONSTER_TYPE,row.REAL_NAME, 0f);
            DownLoadMissionDic.Add("CharPretabs/" + row.MONSTER_TYPE + "/" + row.REAL_NAME, _oneMission);//这个key就是副地址，本地其他读取模型的地方也是用的这样的key
        }
        yield return DownloadingProcess();
    }
}
