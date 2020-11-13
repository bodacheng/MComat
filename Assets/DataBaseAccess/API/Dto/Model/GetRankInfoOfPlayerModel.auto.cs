using System;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ所有モンスター情報詳細取得モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class GetRankInfoOfPlayerModel 
    {
        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public PlayerArenaRankInfo playerArenaRankInfo { get; set; }
    }
}
