using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Singleton
{
    public static class UnitIconDic {
    
        static readonly IDictionary<string, Sprite> Dic = new Dictionary<string, Sprite>();
    
        public static async UniTask<Sprite> Load(string recordId, GameObject memoryReleaseTarget = null)
        {
            Dic.TryGetValue(recordId, out Sprite sprite);
            if (sprite == null)
            {
                sprite = await AddressablesLogic.LoadT<Sprite>("unit/" + recordId, memoryReleaseTarget);
            }
            else
            {
                return sprite;
            }
            DicAdd<string, Sprite>.Add(Dic, recordId, sprite);            
            return sprite;
        }
    }
}
