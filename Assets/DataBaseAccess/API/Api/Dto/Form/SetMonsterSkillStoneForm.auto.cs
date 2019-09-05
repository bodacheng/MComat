using Api.Dto.Form.Common;

namespace Api.Dto.Form {

    /// <summary>
    /// モンスタースキルストーン配置フォーム
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    public class SetMonsterSkillStoneForm : CertificationForm {

        /// <summary>
        /// プレーヤ所有モンスターID
        /// </summary>
        public string monsterOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(左上)
        /// </summary>
        public string a1SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(中上)
        /// </summary>
        public string a2SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(右上)
        /// </summary>
        public string a3SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(左中)
        /// </summary>
        public string b1SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(中中)
        /// </summary>
        public string b2SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(右中)
        /// </summary>
        public string b3SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(左下)
        /// </summary>
        public string c1SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(中下)
        /// </summary>
        public string c2SkillStoneOfPlayerId { get; set; }

        /// <summary>
        /// スキールID(右下)
        /// </summary>
        public string c3SkillStoneOfPlayerId { get; set; }
    }
}