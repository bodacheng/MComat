using System;
using System.Collections.Generic;

namespace Api.Dto.Model
{
    /// <summary>
    /// プレーヤ所有出戦チームモデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/02
    /// </summary>
    [Serializable]
    public class TeamPos
    {
        /// <summary>
        /// プレーヤ所有モンスターID(前)
        /// </summary>
        public string f { get; set; }

        /// <summary>
        /// プレーヤ所有モンスターID(左)
        /// </summary>
        public string l { get; set; }

        /// <summary>
        /// プレーヤ所有モンスターID(右)
        /// </summary>
        public string r { get; set; }

        public PosKeySet ToPosKeySet()
        {
            PosKeySet PosKeySet = new PosKeySet();
            List<PosNumWithLocalKey> posNumWithLocalKeys = new List<PosNumWithLocalKey>
            {
                new PosNumWithLocalKey(1, l),
                new PosNumWithLocalKey(2, r),
                new PosNumWithLocalKey(0, f)
            };
            PosKeySet.PosNumsWithLocalKeys = posNumWithLocalKeys.ToArray();
            return PosKeySet;
        }
    }
}
