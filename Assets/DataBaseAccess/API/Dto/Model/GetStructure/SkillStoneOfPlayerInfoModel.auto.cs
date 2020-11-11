using System;
using dataAccess;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ所有スキルストーン情報モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class SkillStoneOfPlayerInfoModel
    {
        /// <summary>
        /// プレーヤ所有スキルストーンID
        /// </summary>
        public string skillStoneOfPlayerId { get; set; }
        
        /// <summary>
        /// スキルレコードID
        /// </summary>
        public string skillId { get; set; }
        
        /// <summary>
        /// 经验值
        /// </summary>
        string exp { get; set; }
        public int EXP {
            set {
                if (exp != null)
                    ExpUpForStoneShow(float.Parse(exp), value);
                exp = value.ToString();
            }
            get { return int.Parse(exp);}
        }
        
        /// <summary>
        /// 突破等级上限次数
        /// </summary>
        string breakthrough { get; set; }
        public int BreakThrough
        {
            set => breakthrough = value.ToString();
            get => int.Parse(breakthrough);
        }
        
        /// <summary>
        /// 使用中のプレーヤ所有モンスターID
        /// </summary>
        public string inUsingMonsterOfPlayerId { get; set; }
        
        /// <summary>
        /// 装备的位置槽。从1到9为A1到C3
        /// </summary>
        public string inUsingSkillSlot { get; set; }
        
        /// <summary>
        /// 是否为角色原生技能
        /// </summary>
        public string Inherent { get; set; }
        
        public void ExpUpForStoneShow(float formerExp, float newExp)
        {
            SKStoneItem SKStone = MySkillStonesReader.GetRenderModel(skillStoneOfPlayerId);
            SKStone.LevelUpShow(formerExp, newExp);
        }
        
        public int GetLevel()
        {
            return LevelExpConfig.GetCurrentInfo(EXP).currentLevel;
        }
    }
}