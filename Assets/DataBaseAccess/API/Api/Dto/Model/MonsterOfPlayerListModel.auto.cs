using System;

namespace Api.Dto.Model {

	/// <summary>
	/// プレーヤ所有モンスター情報モデル
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/01
	/// </summary>
	[Serializable]
	public class MonsterOfPlayerListModel {

		/// <summary>
		/// プレーヤ所有モンスターID
		/// </summary>
		public string monsterOfPlayerId { get; set; }

		/// <summary>
		/// モンスターID
		/// </summary>
		public string monsterId { get; set; }
	}
}
