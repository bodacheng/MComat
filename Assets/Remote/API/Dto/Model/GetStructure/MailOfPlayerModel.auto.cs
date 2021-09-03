using System;

namespace dataAccess
{
    /// <summary>
    /// プレーヤ所有モンスター情報詳細取得モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class MailOfPlayerModel
    {
        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public string playerId { get; set; }

        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public string mailId { get; set; }

        public string title { get; set; }

        public string message { get; set; }

        public string presentlifeRemain { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public string read { get; set; }
    }
}