using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharsManager : MonoBehaviour
{
    public IEnumerator CreateModelForShowingByStreamingAssets(IDictionary<int, GameObject> ReferenceDic, int IDinReferenceDic, CharacterDataInfo _CharacterDataInfo)
    {
        //以上这个信息就包括了全部的“我的角色”信息，下面别的信息都是据此各种由此索引出来的。
        _TempNineAndTwo = _CharacterDataInfo._NineAndTwo;
        if (_TempNineAndTwo == null)
        {
            defaultPools.Instance.FightLoadErrors.Add("九宫格信息加载失败");
        }

        //主要就是上面这个环节不太舒服，考虑如果换成scriptableobject能不能简单些。如果换成那东西...比方说这个信息都是作为textasset保存在CharacterDataInfo里。。。这样就不会出现各种混乱的各平台地址写法问题
        _TempCharacterResourceInfo = _monstersConfigTable.RowToCharacterResourceInfo(
            _monstersConfigTable.Find_ID(_CharacterDataInfo.resource_num.ToString())
        );
        //下面这一步其实牵扯到了大量的运算，到底怎么改造我们再研究。我们现在把展示角色的AI也给加载出来...究竟是图什么呢...
        if (ReferenceDic != null)
        {
            ReferenceDic.TryGetValue(IDinReferenceDic, out _TempModel);
        }
        if (_TempModel == null)
        {
            RuntimeAnimatorController toLoadRuntimeAnimatorController = defaultPools.Instance.getOrLoadRuntimeAnimatorController(_TempCharacterResourceInfo.type);
            if (toLoadRuntimeAnimatorController == null)
            {
                defaultPools.Instance.FightLoadErrors.Add(_TempCharacterResourceInfo.type + "控制器读取失败");
                yield break;
            }
            AssetBundle modelAsset;
            yield return (defaultPools.Instance.getABFromStreamingAssets("charPretabs/" + _TempCharacterResourceInfo.type, _TempCharacterResourceInfo.prefabName));
            if (defaultPools.Instance.readingBundle != null)
            {
                modelAsset = defaultPools.Instance.readingBundle;
            }
            else
            {
                defaultPools.Instance.FightLoadErrors.Add("没能读取到角色包。:" + _TempCharacterResourceInfo.type + "/" + _TempCharacterResourceInfo.prefabName);
                yield break;
            }
            var resultObject = modelAsset.LoadAssetAsync<GameObject>(_TempCharacterResourceInfo.prefabName);
            yield return new WaitWhile(() => resultObject.isDone == false);

            if (resultObject != null)
            {
                modelAsset.Unload(false);
            }
            else
            {
                defaultPools.Instance.FightLoadErrors.Add(modelAsset.name + "包里没有" + _TempCharacterResourceInfo.prefabName + "这个资源");
                modelAsset.Unload(false);
                yield break;
            }
            if (resultObject.asset == null)
            {
                defaultPools.Instance.FightLoadErrors.Add(_TempCharacterResourceInfo.prefabName + "pretab不存在");
                yield break;
            }

            /// ///////////////////////////////////////////////////////
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
        _TempDATACENTER = _TempModel.GetComponent<AI_DATA_CENTER>();
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _TempDATACENTER._CharacterDataInfo = _CharacterDataInfo;
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return (_TempDATACENTER.
                                    step1Initialize(_TempCharacterResourceInfo.type, _TempCharacterResourceInfo.BasicMoveSetName,_TempCharacterResourceInfo.personalMagicPack));
    }

    /////////////// For Debug  //////////////
    public IEnumerator CreateCharacterFromABByStreamingAssets(IDictionary<int, GameObject> ReferenceDic,
                                                              int IDinReferenceDic,
                                                              CharacterDataInfo _CharacterDataInfo,
                                                              string AIScriptName,
                                                              zokusei _zokusei,
                                                              string personalMagic,
                                                              Team team,
                                                              Vector3 pos, Quaternion Q)
    {
        yield return (this.CreateModelForShowingByStreamingAssets(ReferenceDic, IDinReferenceDic,_CharacterDataInfo));
        GameObject IT;
        ReferenceDic.TryGetValue(IDinReferenceDic, out IT);
        TextAsset AIScriptPrefab = Resources.Load("AIScripts/" + _TempCharacterResourceInfo.type + "/" + AIScriptName) as TextAsset;
        if (AIScriptPrefab != null)
        {
            IT.GetComponent<AI_DATA_CENTER>().step2InitializeByStreamingAssets(_TempCharacterResourceInfo.type, AIScriptPrefab, _CharacterDataInfo.level,
                                                                               _zokusei, personalMagic);
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
