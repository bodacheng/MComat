using System;

namespace dataAccess
{
    /// <summary>
    /// プレーヤ所有スキルストーン情報モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class StoneOfPlayerInfo
    {
        /// <summary>
        /// プレーヤ所有スキルストーンID
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// スキルレコードID
        /// </summary>
        public string skillId { get; set; }
        
        public int level { get; set; }
        
        /// <summary>
        /// 使用中のプレーヤ所有モンスターID
        /// </summary>
        public string unitInstanceId { get; set; }
        
        /// <summary>
        /// 装备的位置槽。从1到9为A1到C3
        /// </summary>
        public string slot { get; set; }
        
        /// <summary>
        /// 是否为角色原生技能
        /// </summary>
        public string Inherent { get; set; }
        
        public void ExpUpForStoneShow(float formerExp, float newExp)
        {
            SKStoneItem SKStone = Stones.GetRenderModel(InstanceId);
            SKStone.LevelUpShow(formerExp, newExp);
        }
        
        public StoneOfPlayerInfo Clone()
        {
            StoneOfPlayerInfo info = new StoneOfPlayerInfo()
            {
                InstanceId = this.InstanceId,
                skillId = this.skillId,
                Inherent = this.Inherent
            };
            return info;
        }
    }
}