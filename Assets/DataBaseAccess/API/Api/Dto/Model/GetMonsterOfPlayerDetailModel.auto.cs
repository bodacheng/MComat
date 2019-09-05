using System;

namespace Api.Dto.Model {

	/// <summary>
	/// プレーヤ所有モンスター情報詳細取得モデル
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/01
	/// </summary>
	[Serializable]
	public class GetMonsterOfPlayerDetailModel {

		/// <summary>
		/// プレーヤ所有モンスターID
		/// </summary>
		public string monsterOfPlayerId { get; set; }

		/// <summary>
		/// プレーヤレコードID
		/// </summary>
		public string playerId { get; set; }

		/// <summary>
		/// モンスターID
		/// </summary>
		public string monsterId { get; set; }

		/// <summary>
		/// レベル
		/// </summary>
		public int level { get; set; }

		/// <summary>
		/// 経験値
		/// </summary>
		public int experience { get; set; }

		/// <summary>
		/// スキールID(左上)
		/// </summary>
		public string a1_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(中上)
		/// </summary>
		public string a2_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(右上)
		/// </summary>
		public string a3_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(左中)
		/// </summary>
		public string b1_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(中中)
		/// </summary>
		public string b2_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(右中)
		/// </summary>
		public string b3_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(左下)
		/// </summary>
		public string c1_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(中下)
		/// </summary>
		public string c2_skill_stone_record_id { get; set; }

		/// <summary>
		/// スキールID(右下)
		/// </summary>
		public string c3_skill_stone_record_id { get; set; }
	}
}
