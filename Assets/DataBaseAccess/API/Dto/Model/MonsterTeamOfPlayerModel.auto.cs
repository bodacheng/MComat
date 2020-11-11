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
    public class MonsterTeamOfPlayerModel
    {
        /// <summary>
        /// プレーヤ所有出戦チームID
        /// </summary>
        public string monsterTeamOfPlayerId { get; set; }

        /// <summary>
        /// チームタイプ
        /// </summary>
        public string teamType { get; set; }

        /// <summary>
        /// プレーヤ所有モンスターID(前)
        /// </summary>
        public string fMonsterOfPlayerId { get; set; }

        /// <summary>
        /// プレーヤ所有モンスターID(後)
        /// </summary>
        public string bMonsterOfPlayerId { get; set; }

        /// <summary>
        /// プレーヤ所有モンスターID(左)
        /// </summary>
        public string lMonsterOfPlayerId { get; set; }

        /// <summary>
        /// プレーヤ所有モンスターID(右)
        /// </summary>
        public string rMonsterOfPlayerId { get; set; }

        public PosKeySet ToPosKeySet()
        {
            PosKeySet PosKeySet = new PosKeySet();
            List<PosNumWithLocalKey> posNumWithLocalKeys = new List<PosNumWithLocalKey>
            {
                new PosNumWithLocalKey(0, bMonsterOfPlayerId),
                new PosNumWithLocalKey(1, lMonsterOfPlayerId),
                new PosNumWithLocalKey(2, rMonsterOfPlayerId),
                new PosNumWithLocalKey(3, fMonsterOfPlayerId)
            };
            PosKeySet.PosNumsWithLocalKeys = posNumWithLocalKeys.ToArray();
            return PosKeySet;
        }
    }
}
