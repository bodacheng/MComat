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
        public string ItemId { get; set; }
        
        public string ItemInstanceId { get; set; }

        public string title { get; set; }

        public string message { get; set; }

        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool read { get; set; } = false;
    }
}