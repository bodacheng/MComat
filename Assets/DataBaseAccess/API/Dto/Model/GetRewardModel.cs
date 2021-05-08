using System.Collections.Generic;

namespace Api.Dto.Model {

    /// <summary>
    /// スキルストーンガッチャモデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/15
    /// </summary>
    public class GetRewardModel 
    {
        /// <summary>
        /// プレーヤレコードID
        /// </summary>
        public string playerId { get; set; }
        public int Gold;
        public int Diamond;
        
        // 还应该包括竞技场模式下玩家rank的变化才对。。。
        
        public List<StoneOfPlayerInfoModel> stonesToGetExp { get; set; }
    }
}
