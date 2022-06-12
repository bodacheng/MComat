using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
    public static class UnitIconDic {
    
        static readonly IDictionary<string, Sprite> Dic = new Dictionary<string, Sprite>();
    
        public static Sprite Load(string unit_id)
        {
            Dic.TryGetValue(unit_id, out Sprite Sprite);
            if (Sprite == null)
            {
                Sprite = AddressablesLogic.LoadT<Sprite>("unit/" + unit_id);
            }
            else
            {
                return Sprite;
            }
            DicAdd<string, Sprite>.Add(Dic, unit_id, Sprite);            
            return Sprite;
        }
    }
}
