using System;
using System.Collections.Generic;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ所有モンスター情報詳細取得モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class GetMailsOfPlayerModel {
    
        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public string playerId { get; set; }
        
        public List<MailOfPlayerModel> myMailList = new List<MailOfPlayerModel>();
    }
}
