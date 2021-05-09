using System.Collections;
using UnityEngine;

public partial class CharsManager : MonoBehaviour
{
    public IEnumerator CreateModelForShowingByResource(string monsterId)
    {
        //以上这个信息就包括了全部的“我的角色”信息，下面别的信息都是据此各种由此索引出来的。
        Data_Center _TempDATACENTER;
        CharConfig _TempCharacterResourceInfo = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(monsterId));
        if (_TempCharacterResourceInfo == null)
        {
            Debug.Log("资源号码错误");
            yield break;
        }
        GameObject _TempModel = null;

        /// ///////////////////////////////////////////////////////
        var resultObject = Resources.Load("CharPretabs/" + _TempCharacterResourceInfo.TYPE + "/" + _TempCharacterResourceInfo.REAL_NAME) as GameObject;
        if (resultObject == null)
        {
            Debug.Log("资源错误："+"CharPretabs/" + _TempCharacterResourceInfo.TYPE + "/" + _TempCharacterResourceInfo.REAL_NAME);
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
        _TempModel.SetActive(true);
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _TempDATACENTER.Zokusei = _TempCharacterResourceInfo._zokusei;
        yield return (_TempDATACENTER.Step1Initialize(_TempCharacterResourceInfo.TYPE, _TempCharacterResourceInfo.BASIC_MOVEMENT_PACK,_TempCharacterResourceInfo.SPECIAL_ZOKUSEI));
        yield return _TempDATACENTER;
    }
}
