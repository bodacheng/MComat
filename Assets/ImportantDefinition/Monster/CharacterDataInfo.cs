using dataAccess;
using Api.Dto.Model;

// CharacterDataInfo 的各个成员在CharacterDataInfo做不同作用时候可能能用上的成员是不同的。
// 这个类本身可以看作是AccountCharacterInfo的“实际体”，它也可以序列化，也可以保存。
// GetMonsterOfPlayerDetailModel 更侧重玩家信息，以及与远程的交互，而这个类更侧重在游戏里代表一个角色系统性信息。

[System.Serializable]
public class CharacterDataInfo
{
    public string monsterOfPlayerId = "-1";
    public int monsterId = -1;
    public int level = 1;
    public int HP = 500; //通常来说玩家的角色HP和角色level应该有一个清晰的对应关系，而关卡敌人的HP应该是可以自由设置，这个HP必然不会出现在数据库的任何部位。    
    public NineAndTwo _NineAndTwo;//

    public CharacterDataInfo Clone()
    {
        return (CharacterDataInfo)MemberwiseClone();
    }

    public CharacterDataInfo DeepCopy()
    {
        CharacterDataInfo Copy = this.Clone();
        Copy._NineAndTwo = Copy._NineAndTwo.DeepCopy();
        return Copy;
    }

    public CharacterDataInfo()
    {
    }

    public CharacterDataInfo(string localID, int resource_num,NineAndTwo _NineAndTwo)
    {
        this.monsterOfPlayerId = localID;
        this.monsterId = resource_num;
        this._NineAndTwo = _NineAndTwo;
    }

    public void Dissolve()
    {
        monsterOfPlayerId = (-1).ToString();
        monsterId = -1;
        _NineAndTwo = null;
    }

    public GetMonsterOfPlayerDetailModel getCharacterDataInfoJson()
    {
        GetMonsterOfPlayerDetailModel characterDataInfoJson = new GetMonsterOfPlayerDetailModel();
        characterDataInfoJson.playerId = "1";
        characterDataInfoJson.monsterId = this.monsterId.ToString();
        characterDataInfoJson.monsterOfPlayerId = this.monsterOfPlayerId;
        //characterDataInfoJson.experience = 
        characterDataInfoJson.level = this.level;
        characterDataInfoJson.a1_skill_stone_record_id = this._NineAndTwo.A1skillid;
        characterDataInfoJson.a2_skill_stone_record_id = this._NineAndTwo.A2skillid;
        characterDataInfoJson.a3_skill_stone_record_id = this._NineAndTwo.A3skillid;
        characterDataInfoJson.b1_skill_stone_record_id = this._NineAndTwo.B1skillid;
        characterDataInfoJson.b2_skill_stone_record_id = this._NineAndTwo.B2skillid;
        characterDataInfoJson.b3_skill_stone_record_id = this._NineAndTwo.B3skillid;
        characterDataInfoJson.c1_skill_stone_record_id = this._NineAndTwo.C1skillid;
        characterDataInfoJson.c2_skill_stone_record_id = this._NineAndTwo.C2skillid;
        characterDataInfoJson.c3_skill_stone_record_id = this._NineAndTwo.C3skillid;
        return characterDataInfoJson;
    }
}