using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharsManager : MonoBehaviour
{
    private IEnumerator _loadingProcess;
    public IEnumerator CreateModelForShowingByCach(string monsterId)
    {
        CharacterResourceInfo _TempCharacterResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(monsterId));
        if (_TempCharacterResourceInfo == null)
        {
            Debug.Log("资源号码错误");
            yield break;
        }
        //上面这个应该也是走异步，到时候是个连接数据库流程？
        GameObject _TempModel = null;
        AssetBundle modelAsset;
        _loadingProcess = CachManager.Instance.getABFromCach("CharPretabs/" + _TempCharacterResourceInfo.type, _TempCharacterResourceInfo.REAL_NAME);
        yield return _loadingProcess;
        if (_loadingProcess.Current != null)
        {
            modelAsset = (AssetBundle)_loadingProcess.Current;
        }
        else
        {
            Debug.Log("展示用模型" + _TempCharacterResourceInfo.REAL_NAME+ "包读取失败");
            yield break;
        }

        var resultObject = modelAsset.LoadAssetAsync<GameObject>(_TempCharacterResourceInfo.REAL_NAME);
        yield return new WaitWhile(() => resultObject.isDone == false);

        if (resultObject.asset != null)
        {
            Debug.Log("成功从缓存读取了以下模型：" + monsterId);
            modelAsset.Unload(false);
        }
        else
        {
            modelAsset.Unload(false);
            Debug.Log("展示用模型GameObject提取失败");
            yield break;
        }
        _TempModel = Instantiate((GameObject)resultObject.asset, Vector3.zero, Quaternion.identity);
        _TempModel.SetActive(true);
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        OutsideDataLink _ODL = _TempModel.GetComponent<OutsideDataLink>();
        Data_Center _TempDATACENTER = _ODL._C;
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return (_TempDATACENTER.Step1Initialize(_TempCharacterResourceInfo.type, _TempCharacterResourceInfo.BASIC_MOVEMENT_PACK,_TempCharacterResourceInfo.SPECIAL_ZOKUSEI));
        yield return _TempDATACENTER;
    }

    // 脚本信息式
    public IEnumerator CreateCharacterFromABByCach(CharDataInfo _CharacterDataInfo,string AIScriptName,Zokusei _zokusei,string personalMagic,Team team,Vector3 pos, Quaternion Q)
    {
        //yield return (this.CreateModelForShowingByCach(_CharacterDataInfo.monsterId));
        //GameObject IT;
        //CharacterResourceInfo _TempCharacterResourceInfo = MonsterConfigInfos._monstersConfigTable.RowToCharacterResourceInfo(
        //                                                    MonsterConfigInfos._monstersConfigTable.Find_ID(_CharacterDataInfo.monsterId.ToString()));
        //TextAsset AIScriptPrefab = Resources.Load("AIScripts/" + _TempCharacterResourceInfo.type + "/" + AIScriptName) as TextAsset;
        //OutsideDataLink _ODL = IT.GetComponent<OutsideDataLink>();
        //if (AIScriptPrefab != null)
        //{
        //    _ODL._C.step2InitializeByCach(_TempCharacterResourceInfo.type, AIScriptPrefab, int.Parse(_CharacterDataInfo.level.ToString()), _zokusei, personalMagic);
        //}
        //if (team == Team.player1)
        //    _ODL._C.step3Initialize(heroTeamConfig);
        //if (team == Team.player2)
        //    _ODL._C.step3Initialize(EnemyTeamConfig);

        //addNewMemberToTeamMemberDic(_ODL._C, team, _CharacterDataInfo);

        //IT.transform.position = pos;
        //IT.transform.rotation = Q;
        yield break;
    }
}
