using System.Collections.Generic;

namespace dataAccess
{

    /// <summary>
    /// モンスタースキルストーン配置フォーム
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    public class SkillStoneLevelUpForm
    {
        /// <summary>
        /// 升级对象技能石
        /// </summary>
        public string targetStoneID { get; set; }
        
        /// <summary>
        /// 目标提升等级。其实原则上说肯定不应该由客户端提供这个数值
        /// 但要么是playfab的设计问题要么是我们对playfab有知识盲区
        /// 单靠cloundscript计算出应该的等级极端麻烦，
        /// 所以我们简化处理。
        /// </summary>
        public string addLevel { get; set; }
        
        /// <summary>
        /// 材料技能石
        /// </summary>
        public string M1Stone { get; set; }
        public string M2Stone { get; set; }
        public string M3Stone { get; set; }
        public string M4Stone { get; set; }
    }
}