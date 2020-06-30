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
        /// 等级，限制为1到100
        /// </summary>
        string exp { get; set; }
        public float EXP {
            set {
                if (exp != null)
                    ExpUpForStoneShow(float.Parse(exp), value);
                exp = value.ToString();
            }
            get { return float.Parse(exp);}
        }
        
        /// <summary>
        /// 使用中のプレーヤ所有モンスターID
        /// </summary>
        public string inUsingMonsterOfPlayerId { get; set; }
        
        /// <summary>
        /// 使用中のプレーヤ所有モンスターID
        /// </summary>
        public string inUsingSkillSlot { get; set; }
        
        /// <summary>
        /// 是否为角色原生技能
        /// </summary>
        public string Inherent { get; set; }
        
        public int GetLevel()
        {
            return ExpToLevel(EXP);
        }
        
        public static int ExpToLevel(float Exp)
        {
            return (int)((Exp / 10) + 1);
        }
                
        public void ExpUpForStoneShow(float formerExp, float newExp)
        {
            SKStoneItem SKStone = MySkillStonesReader.GetRenderModel(skillStoneOfPlayerId);
            SKStone.LevelUpShow(formerExp, newExp);
        }
    }
}