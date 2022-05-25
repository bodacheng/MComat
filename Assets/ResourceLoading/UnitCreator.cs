using System.Collections;
using Singleton;
using UnityEngine;

public class UnitCreator {
    
    public static IEnumerator CreateUnit(UnitInfo info)
    {
        var get = GeneralModelPool.GetModel(info.r_id);
        yield return get;
        var _D = (Data_Center)get.Current;
        if (_D == null)
        {
            Debug.Log("严重资源类错误");
            yield break;
        }
        var unitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(info.r_id));
        yield return _D.Step2Initialize (unitConfig.TYPE, info.set, info.level, unitConfig.element, unitConfig.SPECIAL_ZOKUSEI);
        yield return _D;
    }
}