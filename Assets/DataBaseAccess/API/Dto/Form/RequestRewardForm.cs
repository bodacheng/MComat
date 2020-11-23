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
        /// 根据clear的是什么关卡，可能获得不同的报酬
        /// 并且用在厂上的所有技能石也会根据关卡获得相应的经验点数
        /// </summary>
        public string userId { get; set; }
        public FightEventType fightEventType;
        public int eventNum;// arcade模式这个是关卡号码，竞技场模式这个是对手的id
        public List<string> StoneOfPlayerIDs;
    }
}
