using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class CharsManager : MonoBehaviour
{
    public IEnumerator CreateModelForShowingByResource(IDictionary<int, GameObject> ReferenceDic, int IDinReferenceDic, CharacterDataInfo _CharacterDataInfo)
    {
        //以上这个信息就包括了全部的“我的角色”信息，下面别的信息都是据此各种由此索引出来的。
        _TempNineAndTwo = _CharacterDataInfo._NineAndTwo;
        if (_TempNineAndTwo == null)
        {
            defaultPools.Instance.FightLoadErrors.Add("九宫格信息加载失败");
            //yield break;
        }

        _TempCharacterResourceInfo = _monstersConfigTable.RowToCharacterResourceInfo(
            _monstersConfigTable.Find_ID(_CharacterDataInfo.resource_num.ToString())
        );

        _TempModel = null;
        if (ReferenceDic != null)
        {
            ReferenceDic.TryGetValue(IDinReferenceDic, out _TempModel);
        }
        if (_TempModel == null)
        {
            RuntimeAnimatorController toLoadRuntimeAnimatorController = defaultPools.Instance.getRuntimeAnimatorController(_TempCharacterResourceInfo.type);
            if (toLoadRuntimeAnimatorController == null)
            {
                Debug.Log("角色控制器读取失败："+ _TempCharacterResourceInfo.type);
                defaultPools.Instance.FightLoadErrors.Add(_TempCharacterResourceInfo.type + "控制器读取失败");
                yield break;
            }

            /// ///////////////////////////////////////////////////////
            var resultObject = Resources.Load("charPretabs/" + _TempCharacterResourceInfo.type + "/" + _TempCharacterResourceInfo.prefabName) as GameObject;
            if (resultObject == null)
            {
                Debug.Log("资源错误："+"charPretabs/" + _TempCharacterResourceInfo.type + "/" + _TempCharacterResourceInfo.prefabName);
                yield break;
            }
            _TempModel = Instantiate((GameObject)resultObject, Vector3.zero, Quaternion.identity);
            if (ReferenceDic != null)
            {
                ReferenceDic[IDinReferenceDic] = _TempModel;
            }
            _TempDATACENTER = _TempModel.GetComponent<AI_DATA_CENTER>();
            if (_TempDATACENTER)
            {
                _TempModel.GetComponent<Animator>().runtimeAnimatorController = toLoadRuntimeAnimatorController;
            }
        }else{
            _TempDATACENTER = _TempModel.GetComponent<AI_DATA_CENTER>();
        }
        _TempModel.SetActive(true);
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _TempDATACENTER._CharacterDataInfo = _CharacterDataInfo;
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return 
            (_TempDATACENTER.step1Initialize(_TempCharacterResourceInfo.type, _TempCharacterResourceInfo.BasicMoveSetName,_TempCharacterResourceInfo.personalMagicPack));
    }
}
