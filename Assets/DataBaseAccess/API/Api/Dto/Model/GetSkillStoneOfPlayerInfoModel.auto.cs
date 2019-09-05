using Api.Dto.Model;
using System;
using System.Collections.Generic;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ所有スキルストーン情報取得モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class GetSkillStoneOfPlayerInfoModel {

        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public string playerId { get; set; }

        /// <summary>
        /// プレーヤ所有スキルストーン情報のリスト
        /// </summary>
        public List<SkillStoneOfPlayerInfoModel> skillStoneOfPlayerInfoList { get; set; }
    }
}
