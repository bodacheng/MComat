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
	}
}
