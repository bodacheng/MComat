using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AssetBundleLoader : MonoBehaviour
{
    private IEnumerator ConfigFilesDownLoad()
    {
        AssetBundle readingbundle;
        
        ////////////  下面开始下载并阅读角色配置文件 ////////////
        modelConfigFileMission = new CachDownLoadMission( "Configs","monstersconfig", 0f);
        IEnumerator _loadingProcess = letThisloadMissionBegin(modelConfigFileMission);
        yield return _loadingProcess;
        if (!modelConfigFileMission.downloadfinished)//downloadfinished的赋值机制非常棘手
        {
            Debug.Log("角色配置文件下载失败。");
            yield break;
        }
        _loadingProcess = CachManager.Instance.getABFromCach(modelConfigFileMission.subPath, modelConfigFileMission.filename);
        yield return _loadingProcess;
        if (_loadingProcess .Current != null)
        {
            readingbundle = (AssetBundle)_loadingProcess.Current;
        }
        else
        {
            Debug.Log("角色配置文件对应ab包读取失败");
            yield break;
        }
        AssetBundleRequest loadAsset = readingbundle.LoadAssetAsync<TextAsset>(modelConfigFileMission.filename);
        yield return new WaitWhile(() => loadAsset.isDone == false);
        if (loadAsset.asset != null)
        {
            readingbundle.Unload(false);
        }
        else
        {
            readingbundle.Unload(false);
            Debug.Log("角色配置文件提取失败");
            yield break;
        }
        
        TextAsset CharacterConfigTextFile = (TextAsset)loadAsset.asset;
        MonsterConfigInfos._monstersConfigTable = new monstersConfigTable();
        if (CharacterConfigTextFile != null)
        {
            MonsterConfigInfos._monstersConfigTable.Load(CharacterConfigTextFile);
            MonsterConfigInfos.refreshCharacterResourceInfoDic();
        }
        else{
            Debug.Log("角色配置文件错误。");
            yield break;
        }
        
        ////////////  下面开始下载并阅读技能配置文件 ////////////
        animationConfigFileMission = new CachDownLoadMission( "Configs","skillsconfig", 0f);
        _loadingProcess = letThisloadMissionBegin(animationConfigFileMission);
        yield return _loadingProcess;
        if (!animationConfigFileMission.downloadfinished)//downloadfinished的赋值机制非常棘手
        {
            Debug.Log("技能配置文件下载失败。");
            yield break;
        }
        _loadingProcess = CachManager.Instance.getABFromCach(animationConfigFileMission.subPath, animationConfigFileMission.filename);
        yield return _loadingProcess;
        if (_loadingProcess.Current != null)
        {
            readingbundle = (AssetBundle)_loadingProcess.Current;
        }
        else
        {
            Debug.Log("技能配置文件对应ab包读取失败");
            yield break;
        }
        
        loadAsset = readingbundle.LoadAssetAsync<TextAsset>(animationConfigFileMission.filename);
        yield return new WaitWhile(() => loadAsset.isDone == false);
        if (loadAsset.asset != null)
        {
            readingbundle.Unload(false);
        }
        else
        {
            readingbundle.Unload(false);
            Debug.Log("技能配置文件提取失败");
            yield break;
        }
        TextAsset SkillConfigTextFile = (TextAsset)loadAsset.asset;

        if (SkillConfigTextFile != null)
        {
            SkillsConfigInfos.skillConfigTable.Load(SkillConfigTextFile);
            SkillsConfigInfos.refreshSkillConfigDicForReference();
        }
        else
        {
            Debug.Log("技能配置文件错误。");
            yield break;
        }
    }
}
