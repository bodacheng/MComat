using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharsManager : MonoBehaviour
{
    private IEnumerator _loadingProcess;
    public IEnumerator CreateModelForShowingByCach(IDictionary<int, GameObject> ReferenceDic, int IDinReferenceDic, CharacterDataInfo _CharacterDataInfo)
    {
        _TempNineAndTwo = _CharacterDataInfo._NineAndTwo;
        _TempCharacterResourceInfo = _monstersConfigTable.RowToCharacterResourceInfo(
                                _monstersConfigTable.Find_ID(_CharacterDataInfo.resource_num.ToString()));
        //上面这个应该也是走异步，到时候是个连接数据库流程？

        if (ReferenceDic != null)
        {
            ReferenceDic.TryGetValue(IDinReferenceDic, out _TempModel);
        }
        if (_TempModel == null)
        {
            RuntimeAnimatorController toLoadRuntimeAnimatorController = defaultPools.Instance.getOrLoadRuntimeAnimatorController(_TempCharacterResourceInfo.type);
            AssetBundle modelAsset;
            _loadingProcess = defaultPools.Instance.getABFromCach("charPretabs/" + _TempCharacterResourceInfo.type, _TempCharacterResourceInfo.prefabName);
            yield return _loadingProcess;
            if (_loadingProcess.Current != null)
            {
                modelAsset = (AssetBundle)_loadingProcess.Current;
            }
            else
            {
                Debug.Log("展示用模型" + _TempCharacterResourceInfo.prefabName+ "包读取失败");
                yield break;
            }

            var resultObject = modelAsset.LoadAssetAsync<GameObject>(_TempCharacterResourceInfo.prefabName);
            yield return new WaitWhile(() => resultObject.isDone == false);

            if (resultObject.asset != null)
            {
                Debug.Log("成功从缓存读取了以下模型：" + IDinReferenceDic);
                modelAsset.Unload(false);
            }
            else
            {
                modelAsset.Unload(false);
                Debug.Log("展示用模型GameObject提取失败");
                yield break;
            }
            _TempModel = Instantiate((GameObject)resultObject.asset, Vector3.zero, Quaternion.identity);
            if (ReferenceDic != null)
                ReferenceDic[IDinReferenceDic] = _TempModel;

            _TempDATACENTER = _TempModel.GetComponent<AI_DATA_CENTER>();
            if (_TempDATACENTER)
            {
                _TempModel.GetComponent<Animator>().runtimeAnimatorController = toLoadRuntimeAnimatorController;
            }
        }
        _TempModel.SetActive(true);
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _TempDATACENTER = _TempModel.GetComponent<AI_DATA_CENTER>();
        _TempDATACENTER._CharacterDataInfo = _CharacterDataInfo;
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return (_TempDATACENTER.step1Initialize(_TempCharacterResourceInfo.type, _TempCharacterResourceInfo.BasicMoveSetName,_TempCharacterResourceInfo.personalMagicPack));
    }

    // 脚本信息式
    public IEnumerator CreateCharacterFromABByCach(IDictionary<int, GameObject> ReferenceDic,
                                               int IDinReferenceDic,
                                               CharacterDataInfo _CharacterDataInfo,
                                               string AIScriptName,
                                               zokusei _zokusei,
                                               string personalMagic,
                                               Team team,
                                               Vector3 pos, Quaternion Q
                                              )
    {
        yield return (this.CreateModelForShowingByCach(ReferenceDic, IDinReferenceDic, _CharacterDataInfo));
        GameObject IT;
        ReferenceDic.TryGetValue(IDinReferenceDic, out IT);
        TextAsset AIScriptPrefab = Resources.Load("AIScripts/" + _TempCharacterResourceInfo.type + "/" + AIScriptName) as TextAsset;
        if (AIScriptPrefab != null)
        {
            IT.GetComponent<AI_DATA_CENTER>().step2InitializeByCach(_TempCharacterResourceInfo.type, AIScriptPrefab, _CharacterDataInfo.level, _zokusei, personalMagic);
        }
        if (team == Team.player1)
            IT.GetComponent<AI_DATA_CENTER>().step3Initialize(heroTeamConfig, new playerBattleInfo());
        if (team == Team.player2)
            IT.GetComponent<AI_DATA_CENTER>().step3Initialize(EnemyTeamConfig, new playerBattleInfo());

        addNewMemberToTeamMemberDic(IT.GetComponent<AI_DATA_CENTER>(), team);

        IT.transform.position = pos;
        IT.transform.rotation = Q;
    }
}
