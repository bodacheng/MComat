using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AssetBundleLoader : MonoBehaviour
{
    private IEnumerator ModelResourceDownLoad()
    {
        AssetBundle readingbundle;

        foreach (monstersConfigTable.Row row in monstersTable.rowList)
        {
            // 基础动画包下载任务。
            if (!characterTypeAndBasicMoveSets.ContainsKey(row.type))
                characterTypeAndBasicMoveSets.Add(row.type,new List<string>());
            if (!characterTypeAndBasicMoveSets[row.type].Contains(row.basicMoveSet))
            {
                characterTypeAndBasicMoveSets[row.type].Add(row.basicMoveSet);
            }
            
            //模型下载任务
            CachDownLoadMission _oneMission = new CachDownLoadMission( "charPretabs/" + row.type,row.realName, 0f);
            DownLoadMissionDic.Add("charPretabs/" + row.type + "/" + row.realName, _oneMission);//这个key就是副地址，本地其他读取模型的地方也是用的这样的key
        }
        
        yield return downloadingProcess();
    }
}
