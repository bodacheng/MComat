using Api.Dto.Model;
using System;
using System.Collections.Generic;

namespace Api.Dto.Model {

	/// <summary>
	/// プレーヤ所有モンスター情報一覧取得モデル
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/01
	/// </summary>
	[Serializable]
	public class GetMonsterOfPlayerListModel {

		/// <summary>
		/// プレーヤレコードID
		/// </summary>
		public string playerId { get; set; }

		/// <summary>
		/// プレーヤ所有モンスター情報リスト
		/// </summary>
		public List<GetMonsterOfPlayerDetailModel> monsterOfPlayerList { get; set; }
	}
}
