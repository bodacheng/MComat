using Api.Dto.Form.Common;

namespace Api.Dto.Form {

	/// <summary>
	/// プレーヤ所有出戦チーム配置フォーム
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/02
	/// </summary>
	public class SetMonsterTeamOfPlayerForm : CertificationForm {

        public string teamType{ get; set; }
        
		/// <summary>
		/// プレーヤ所有出戦チームID
		/// </summary>
		public string monsterTeamOfPlayerId { get; set; }

		/// <summary>
		/// プレーヤ所有モンスターID(前)
		/// </summary>
		public string fMonsterOfPlayerId { get; set; }

		/// <summary>
		/// プレーヤ所有モンスターID(後)
		/// </summary>
		public string bMonsterOfPlayerId { get; set; }

		/// <summary>
		/// プレーヤ所有モンスターID(左)
		/// </summary>
		public string lMonsterOfPlayerId { get; set; }

		/// <summary>
		/// プレーヤ所有モンスターID(右)
		/// </summary>
		public string rMonsterOfPlayerId { get; set; }
	}
}
