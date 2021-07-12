using Api.Dto.Form.Common;

namespace Api.Dto.Form
{
    /// <summary>
    /// モンスタースキルストーン配置フォーム
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    public class SkillStoneMergeForm : CertificationForm
    {        
        /// <summary>
        /// 材料技能石
        /// </summary>
        public string M1Stone { get; set; }
        public string M2Stone { get; set; }
        public string M3Stone { get; set; }
        public string M4Stone { get; set; }
        public string M5Stone { get; set; }        
    }
}