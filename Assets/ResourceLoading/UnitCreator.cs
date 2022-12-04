using Cysharp.Threading.Tasks;
using Singleton;
using UnityEngine;

public class UnitCreator {
    
    public static async UniTask<Data_Center> CreateUnit(UnitInfo info)
    {
        var _D = await GeneralModelPool.GetModel(info.r_id);
        if (_D == null)
        {
            Debug.Log("严重资源类错误");
            return _D;
        }
        var unitConfig = Units.RowToUnitConfigInfo(Units.Find_RECORD_ID(info.r_id));
        await _D.Step2Initialize (unitConfig.TYPE, unitConfig.element, info.set);
        return _D;
    }
}