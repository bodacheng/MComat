using Api.Dto.Form.Common;
using System.Collections.Generic;

namespace Api.Dto.Form {

    /// <summary>
    /// ログインフォーム
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    public class RequestRewardForm : NoneCertificationForm {

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string userId { get; set; }
        public FightEventType fightEventType;
        public int eventNum;
        public List<string> StoneOfPlayerIDs;
    }
}
