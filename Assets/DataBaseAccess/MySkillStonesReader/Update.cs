using System.Collections;
using UnityEngine;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        // 更新存档数据
        public static IEnumerator Update(string stoneOfPlayerID)
        {
            if (!Dic.ContainsKey(stoneOfPlayerID) || Dic[stoneOfPlayerID] == null)
            {
                Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + stoneOfPlayerID);
                yield break;
            }
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    Update_Json(Dic[stoneOfPlayerID]);
                break;
                case PlayerInfoRefMode.remoteTestPlayer:
                break;
                case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
    }
}