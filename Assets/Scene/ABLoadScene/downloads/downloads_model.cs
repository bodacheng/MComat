using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceLordSceneStarter : MonoBehaviour
{
    private IEnumerator ModelResourceDownLoad()
    {
        foreach (monstersConfigTable.Row row in monstersConfigTable.Instance.rowList)
        {            
            //模型下载任务
            CachDownLoadMission _oneMission = new CachDownLoadMission( "charPretabs/" + row.MONSTER_TYPE_CODE,row.REAL_NAME, 0f);
            DownLoadMissionDic.Add("charPretabs/" + row.MONSTER_TYPE_CODE + "/" + row.REAL_NAME, _oneMission);//这个key就是副地址，本地其他读取模型的地方也是用的这样的key
        }
        yield return downloadingProcess();
    }
}
