using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Skill;

[Serializable]
public class CharConfig
{
    public string RECORD_ID;//monsterTable ID
    public string type;
    public string REAL_NAME;//monsterTable realName
    public string showNameEN;//monsterTable showNameEN
    public string showNameCN;
    public string showNameJP;
    public Zokusei _zokusei = Zokusei.lightMagic;
    public string SPECIAL_ZOKUSEI;
    public string BASIC_MOVEMENT_PACK = "basic_anim";//monsterTable BasicMoveSet
    public MoveType moveType = MoveType.Mode1;//monsterTable moveType
    public RushType rushType = RushType.RushBack;//monsterTable accSKill
    public bool DEFENDABLE_FLAG = true;
    public string instructionEN;
    public string instructionCH;
    public string instructionJP;
    public int RARITY_LEVEL = 3;

    public PassiveSkillConfigs GetPassiveSkillConfigs()
    {
        PassiveSkillConfigs passiveSkillConfigs = new PassiveSkillConfigs(moveType, DEFENDABLE_FLAG, rushType);
        return passiveSkillConfigs;
    }

    public CharDataInfo GetTestCharConfig(string localID)
    {
        CharDataInfo characterDataInfo = new CharDataInfo
        {
            monsterOfPlayerId = localID,
            ResourceID = RECORD_ID, // 确切的说这个也就是角色的pretab编号，最后也就是数据库里master table的主key。
            _NineAndTwo = null
        };
        return characterDataInfo;
    }
}