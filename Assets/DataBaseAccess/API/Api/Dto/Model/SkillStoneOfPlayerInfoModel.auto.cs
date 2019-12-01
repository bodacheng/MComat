using System;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ所有スキルストーン情報モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class SkillStoneOfPlayerInfoModel {

        /// <summary>
        /// プレーヤ所有スキルストーンID
        /// </summary>
        public string skillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキルレコードID
        /// </summary>
        public string skillId { get; set; }

        /// <summary>
        /// 使用中のプレーヤ所有モンスターID
        /// </summary>
        public string inUsingMonsterOfPlayerId { get; set; }
        
        /// <summary>
        /// 使用中のプレーヤ所有モンスターID
        /// </summary>
        public string inUsingSkillSlot { get; set; }
    }
}
