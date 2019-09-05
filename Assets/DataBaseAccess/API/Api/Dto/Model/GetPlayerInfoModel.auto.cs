using System;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ情報取得モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class GetPlayerInfoModel
    {
		/// <summary>
		/// プレーヤレコードID
		/// </summary>
		public string playerId { get; set; }

		/// <summary>
		/// ニックネーム
		/// </summary>
		public string nickname { get; set; }

		/// <summary>
		/// レベル
		/// </summary>
		public int level { get; set; }

		/// <summary>
		/// 経験値
		/// </summary>
		public int experience { get; set; }

		/// <summary>
		/// ダイヤモンド数
		/// </summary>
		public int diamondCount { get; set; }

		/// <summary>
		/// コイン数
		/// </summary>
		public int coinCount { get; set; }
	}
}
