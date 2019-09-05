using Api.Dto.Model;
using System;
using System.Collections.Generic;

namespace Api.Dto.Model {

	/// <summary>
	/// スキルストーンガッチャモデル
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/15
	/// </summary>
	[Serializable]
	public class SkillStoneGotchaModel {

		/// <summary>
		/// プレーヤレコードID
		/// </summary>
		public string playerId { get; set; }

		/// <summary>
		/// スキルストーンガッチャ情報のリスト
		/// </summary>
		public List<SkillStoneGotchaInfoModel> skillStoneGotchaInfoList { get; set; }
	}
}
