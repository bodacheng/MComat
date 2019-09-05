using Api.Dto.Model;
using System;
using System.Collections.Generic;

namespace Api.Dto.Model {

	/// <summary>
	/// プレーヤ所有出戦チーム取得モデル
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/02
	/// </summary>
	[Serializable]
	public class GetMonsterTeamOfPlayerModel {

		/// <summary>
		/// プレーヤ所有出戦チームリスト
		/// </summary>
		public List<MonsterTeamOfPlayerModel> monsterTeamOfPlayerList { get; set; }
	}
}
