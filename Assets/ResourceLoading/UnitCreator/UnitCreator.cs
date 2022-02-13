using System.Collections;
using UnityEngine;

public partial class UnitCreator {
    
    public static IEnumerator CreateUnit(UnitInfo info)
    {
        IEnumerator get = GeneralModelPool.GetModel(info.r_id, false);
        yield return get;
        Data_Center _D = (Data_Center)get.Current;
        if (_D == null)
        {
            Debug.Log("严重资源类错误");
            yield break;
        }
        var unitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(info.r_id));
        yield return _D.Step2Initialize (unitConfig.TYPE, info.set, info.level, unitConfig._zokusei, unitConfig.SPECIAL_ZOKUSEI);
        yield return _D;
    }
}