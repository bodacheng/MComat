using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharsManager : MonoBehaviour
{
    public IEnumerator CreateModelForShowingByStreamingAssets(string monsterId)
    {
        //主要就是上面这个环节不太舒服，考虑如果换成scriptableobject能不能简单些。如果换成那东西...比方说这个信息都是作为textasset保存在CharacterDataInfo里。。。这样就不会出现各种混乱的各平台地址写法问题                                                            
        CharacterResourceInfo _TempCharacterResourceInfo = monstersConfigTable.Instance.RowToCharacterResourceInfo(monstersConfigTable.Instance.Find_RECORD_ID(monsterId.ToString()));
        if (_TempCharacterResourceInfo == null)
        {
            Debug.Log("资源号码错误");
            yield break;
        }
        //下面这一步其实牵扯到了大量的运算，到底怎么改造我们再研究。我们现在把展示角色的AI也给加载出来...究竟是图什么呢...
        GameObject _TempModel = null;
        OutsideDataLink ODL;
        Data_Center _TempDATACENTER;
        RuntimeAnimatorController toLoadRuntimeAnimatorController = AnimationResourceLoader.Instance.getRuntimeAnimatorController(_TempCharacterResourceInfo.type);
        if (toLoadRuntimeAnimatorController == null)
        {
            FightLoadError.Instance.FightLoadErrors.Add(_TempCharacterResourceInfo.type + "控制器读取失败");
            yield break;
        }
        AssetBundle modelAsset;
        IEnumerator enumerator = CachManager.Instance.getABFromStreamingAssets("charPretabs/" + _TempCharacterResourceInfo.type, _TempCharacterResourceInfo.REAL_NAME);
        yield return enumerator;
        modelAsset = (AssetBundle)enumerator.Current;
        if (modelAsset == null)
        {
            FightLoadError.Instance.FightLoadErrors.Add("没能读取到角色包。:" + _TempCharacterResourceInfo.type + "/" + _TempCharacterResourceInfo.REAL_NAME);
            yield break;
        }
        
        var resultObject = modelAsset.LoadAssetAsync<GameObject>(_TempCharacterResourceInfo.REAL_NAME);
        yield return new WaitWhile(() => resultObject.isDone == false);

        if (resultObject != null)
        {
            modelAsset.Unload(false);
        }
        else
        {
            FightLoadError.Instance.FightLoadErrors.Add(modelAsset.name + "包里没有" + _TempCharacterResourceInfo.REAL_NAME + "这个资源");
            modelAsset.Unload(false);
            yield break;
        }
        if (resultObject.asset == null)
        {
            FightLoadError.Instance.FightLoadErrors.Add(_TempCharacterResourceInfo.REAL_NAME + "pretab不存在");
            yield break;
        }

        /// ///////////////////////////////////////////////////////
        _TempModel = Instantiate((GameObject)resultObject.asset, Vector3.zero, Quaternion.identity);
        ODL = _TempModel.GetComponent<OutsideDataLink>();
        _TempDATACENTER = ODL._C;
        _TempDATACENTER.animator.runtimeAnimatorController = toLoadRuntimeAnimatorController;

        _TempModel.SetActive(true);
        ODL = _TempModel.GetComponent<OutsideDataLink>();
        _TempDATACENTER = ODL._C;
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return (_TempDATACENTER.step1Initialize(_TempCharacterResourceInfo.type, _TempCharacterResourceInfo.BASIC_MOVEMENT_PACK,_TempCharacterResourceInfo.SPECIAL_ZOKUSEI));
        yield return _TempDATACENTER;
    }

    /////////////// For Debug  //////////////
    public IEnumerator CreateCharacterFromABByStreamingAssets(CharacterDataInfo _CharacterDataInfo,
                                                              string AIScriptName,
                                                              zokusei _zokusei,
                                                              string personalMagic,
                                                              Team team,
                                                              Vector3 pos, Quaternion Q)
    {
        //yield return (this.CreateModelForShowingByStreamingAssets(_CharacterDataInfo.monsterId));
        //GameObject IT;
        //OutsideDataLink ODL;
        //ReferenceDic.TryGetValue(IDinReferenceDic, out IT);
        //ODL = IT.GetComponent<OutsideDataLink>();
        //CharacterResourceInfo _TempCharacterResourceInfo = MonsterConfigInfos._monstersConfigTable.RowToCharacterResourceInfo(
        //                                                    MonsterConfigInfos._monstersConfigTable.Find_ID(_CharacterDataInfo.monsterId.ToString()));
        //TextAsset AIScriptPrefab = Resources.Load("AIScripts/" + _TempCharacterResourceInfo.type + "/" + AIScriptName) as TextAsset;
        //if (AIScriptPrefab != null)
        //{
        //    ODL._C.step2InitializeByStreamingAssets(_TempCharacterResourceInfo.type, AIScriptPrefab, _CharacterDataInfo.level,_zokusei, personalMagic);
        //}
        //if (team == Team.player1)
        //     ODL._C.step3Initialize(heroTeamConfig);
        //if (team == Team.player2)
        //     ODL._C.step3Initialize(EnemyTeamConfig);

        //addNewMemberToTeamMemberDic( ODL._C, team,_CharacterDataInfo);

        //IT.transform.position = pos;
        //IT.transform.rotation = Q;
        yield break;
    }
}
