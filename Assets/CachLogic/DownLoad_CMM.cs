using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceDownLoad : MonoBehaviour
{
    // 初始热更新所还欠缺的环节
    // 1.两个配置文件下载到内存后广播至整个程序的问题
    // 2.容量总结。没有办法估算容量说明我们靠两个主表配置文件来做下载统计是不成熟的。不过我难道可以先远程读一下容量再开始下载。。？
    // 3.下载进程显示。
    // 4.下载错误总结. 进入主程序文件审核。
    // 5.主程运行中文件检查，重下载。
    IEnumerator DownloadingProcess()
    {
        foreach (KeyValuePair<string, CachDownLoadMission> _keyvalue in DownLoadMissionDic)
        {
            yield return LetThisloadMissionBegin(_keyvalue.Value);
        }
        DownLoadMissionDic.Clear();
        yield break;
    }

    IEnumerator LetThisloadMissionBegin(CachDownLoadMission _CachDownLoadMission)
    {
        IEnumerator task;
        if (_CachDownLoadMission != null)
        {
            task = CachManager.Instance.DownloadAndCacheExactFile(BundleURL + "/" + _CachDownLoadMission.subPath, _CachDownLoadMission.filename);
            yield return task;
            _CachDownLoadMission.downloadfinished = task.Current != null;
        }
        else
        {
            Debug.Log("下载任务建立错误");
        }
    }
}
