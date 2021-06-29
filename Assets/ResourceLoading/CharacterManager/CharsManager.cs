using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

public partial class CharsManager : MonoBehaviour {

    public static CharsManager target;
    
    void Start()
    {
        target = this;
    }
    
    //这些都是中间变量
    public IEnumerator CreateCharacter(CharDataInfo _CharDataInfo)
    {
        IEnumerator getproess = GeneralModelPool.GetModel(_CharDataInfo.r_id, false);
        yield return getproess;
        Data_Center _TempDATACENTER = (Data_Center)getproess.Current;
        if (_TempDATACENTER == null)
        {
            Debug.Log("严重资源类错误");
            yield break;
        }
        CharConfig charConfig = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(_CharDataInfo.r_id));
        yield return _TempDATACENTER.Step2Initialize (charConfig.TYPE, _CharDataInfo.set, charConfig._zokusei, charConfig.SPECIAL_ZOKUSEI);
        yield return _TempDATACENTER;
    }
}