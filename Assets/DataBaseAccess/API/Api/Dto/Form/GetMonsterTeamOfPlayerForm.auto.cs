using Api.Dto.Form.Common;

namespace Api.Dto.Form {

	/// <summary>
	/// プレーヤ所有出戦チーム取得フォーム
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/02
	/// </summary>
	public class GetMonsterTeamOfPlayerForm : CertificationForm {

		/// <summary>
		/// プレーヤ所有出戦チームID
		/// </summary>
		public string monsterTeamOfPlayerId { get; set; }

		/// <summary>
		/// チームタイプ
		/// </summary>
		public string teamType { get; set; }
	}
}
