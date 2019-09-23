using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class CharsManager : MonoBehaviour
{
    public IEnumerator CreateModelForShowingByResource(string monsterId)
    {
        //以上这个信息就包括了全部的“我的角色”信息，下面别的信息都是据此各种由此索引出来的。
        Data_Center _TempDATACENTER;
        CharacterResourceInfo _TempCharacterResourceInfo = monstersConfigTable.Instance.RowToCharacterResourceInfo(monstersConfigTable.Instance.Find_RECORD_ID(monsterId.ToString()));
        if (_TempCharacterResourceInfo == null)
        {
            Debug.Log("资源号码错误");
            yield break;
        }
        GameObject _TempModel = null;
        RuntimeAnimatorController toLoadRuntimeAnimatorController = AnimationResourceLoader.Instance.getRuntimeAnimatorController(_TempCharacterResourceInfo.type);
        if (toLoadRuntimeAnimatorController == null)
        {
            Debug.Log("角色控制器读取失败："+ _TempCharacterResourceInfo.type);
            FightLoadError.Instance.FightLoadErrors.Add(_TempCharacterResourceInfo.type + "控制器读取失败");
            yield break;
        }

        /// ///////////////////////////////////////////////////////
        var resultObject = Resources.Load("charPretabs/" + _TempCharacterResourceInfo.type + "/" + _TempCharacterResourceInfo.REAL_NAME) as GameObject;
        if (resultObject == null)
        {
            Debug.Log("资源错误："+"charPretabs/" + _TempCharacterResourceInfo.type + "/" + _TempCharacterResourceInfo.REAL_NAME);
            yield break;
        }
        _TempModel = Instantiate((GameObject)resultObject, Vector3.zero, Quaternion.identity);
        OutsideDataLink _ODL = _TempModel.GetComponent<OutsideDataLink>();
        if (_ODL == null)
        {
            yield return null;
            yield break;
        }
        _TempDATACENTER = _ODL._C;
        if (_TempDATACENTER)
        {
            _TempModel.GetComponent<Animator>().runtimeAnimatorController = toLoadRuntimeAnimatorController;
        }
        
        _TempModel.SetActive(true);
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return (_TempDATACENTER.step1Initialize(_TempCharacterResourceInfo.type, _TempCharacterResourceInfo.BASIC_MOVEMENT_PACK,_TempCharacterResourceInfo.SPECIAL_ZOKUSEI));
        yield return _TempDATACENTER;
    }
}
