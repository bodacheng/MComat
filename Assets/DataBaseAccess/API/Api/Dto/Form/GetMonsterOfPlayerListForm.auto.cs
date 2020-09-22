using Api.Dto.Form.Common;

namespace Api.Dto.Form {

	/// <summary>
	/// プレーヤ所有モンスター情報詳細取得フォーム
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/01
	/// </summary>
	public class GetMonsterOfPlayerListForm : CertificationForm {

        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public string playerId { get; set; }
	}
}
