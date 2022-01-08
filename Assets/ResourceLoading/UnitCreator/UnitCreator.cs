using System.Collections;
using UnityEngine;

public partial class UnitCreator {
    
    public static IEnumerator CreateUnit(UnitInfo info)
    {
        IEnumerator getproess = GeneralModelPool.GetModel(info.r_id, false);
        yield return getproess;
        Data_Center _D = (Data_Center)getproess.Current;
        if (_D == null)
        {
            Debug.Log("严重资源类错误");
            yield break;
        }
        UnitConfig unitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(info.r_id));
        yield return _D.Step2Initialize (unitConfig.TYPE, info.set, unitConfig._zokusei, unitConfig.SPECIAL_ZOKUSEI);
        yield return _D;
    }
}