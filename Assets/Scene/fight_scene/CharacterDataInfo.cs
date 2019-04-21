using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//作为玩家财产，作为敌人信息等存在的表示角色存在的类CharacterDataInfo的构成
//CharacterDataInfo 的各个成员在CharacterDataInfo做不同作用时候可能能用上的成员是不同的。
//[System.Serializable]
[System.Serializable]
public class CharacterDataInfo
{
    public int localID = -1;
    public int resource_num = -1; // 确切的说这个也就是角色的pretab编号，最后也就是数据库里master table的主key。
    public int level = 1;
    public int HP = 500; //通常来说玩家的角色HP和角色level应该有一个清晰的对应关系，而关卡敌人的HP应该是可以自由设置，这个HP必然不会出现在数据库的任何部位。
    public int EXP = 0;
    public NineAndTwo _NineAndTwo;
    public string userd_efined_name;

    //如果是剧情角色，那设置个不可卖呗？读取的时候如果发现不可卖，那就拿着resource_num去资源数据库看看有没有对这个角色的介绍？有的话在画面显示一下故事显得生动些
    public bool favorite = false;

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

    public CharacterDataInfo(int localID, int resource_num,NineAndTwo _NineAndTwo)
    {
        this.localID = localID;
        this.resource_num = resource_num;
        this._NineAndTwo = _NineAndTwo;
        this.favorite = false;
    }

    public void Dissolve()
    {
        localID = -1;
        resource_num = -1;
        _NineAndTwo = null;
    }

    public CharacterDataInfoJson getCharacterDataInfoJson()
    {
        CharacterDataInfoJson characterDataInfoJson = new CharacterDataInfoJson();
        characterDataInfoJson.accountId = "?";
        characterDataInfoJson.monsterId = this.resource_num.ToString();
        characterDataInfoJson.monsterLocalId = this.localID.ToString();
        characterDataInfoJson.canBeDeleted = "?";
        characterDataInfoJson.level = this.level.ToString();
        characterDataInfoJson.exp = this.EXP.ToString();
        characterDataInfoJson.a1Id = this._NineAndTwo.A1skillid.ToString();
        characterDataInfoJson.a2Id = this._NineAndTwo.A2skillid.ToString();
        characterDataInfoJson.a3Id = this._NineAndTwo.A3skillid.ToString();
        characterDataInfoJson.b1Id = this._NineAndTwo.B1skillid.ToString();
        characterDataInfoJson.b2Id = this._NineAndTwo.B2skillid.ToString();
        characterDataInfoJson.b3Id = this._NineAndTwo.B3skillid.ToString();
        characterDataInfoJson.c1Id = this._NineAndTwo.C1skillid.ToString();
        characterDataInfoJson.c2Id = this._NineAndTwo.C2skillid.ToString();
        characterDataInfoJson.c3Id = this._NineAndTwo.C3skillid.ToString();
        return characterDataInfoJson;
    }
}

[System.Serializable]
public class CharacterDataInfoJson
{
    public string accountId;
    public string monsterId;
    public string monsterLocalId;
    public string userd_efined_name;
    public string canBeDeleted;
    public string level;
    public string exp;
    public string a1Id;
    public string a2Id;
    public string a3Id;
    public string b1Id;
    public string b2Id;
    public string b3Id;
    public string c1Id;
    public string c2Id;
    public string c3Id;

    public CharacterDataInfo getCharacterDataInfo()
    {
        try
        {
            CharacterDataInfo characterDataInfo = new CharacterDataInfo();
            characterDataInfo.resource_num = int.Parse(this.monsterId);
            characterDataInfo.localID = int.Parse(this.monsterLocalId);
            characterDataInfo.level = int.Parse(this.level);
            characterDataInfo.EXP = int.Parse(this.exp);
            characterDataInfo.userd_efined_name = userd_efined_name;
            
            NineAndTwo nineAndTwo = new NineAndTwo();
            nineAndTwo.A1skillid = int.Parse(this.a1Id);
            nineAndTwo.A2skillid = int.Parse(this.a2Id);
            nineAndTwo.A3skillid = int.Parse(this.a3Id);
            nineAndTwo.B1skillid = int.Parse(this.b1Id);
            nineAndTwo.B2skillid = int.Parse(this.b2Id);
            nineAndTwo.B3skillid = int.Parse(this.b3Id);
            nineAndTwo.C1skillid = int.Parse(this.c1Id);
            nineAndTwo.C2skillid = int.Parse(this.c2Id);
            nineAndTwo.C3skillid = int.Parse(this.c3Id);
            characterDataInfo._NineAndTwo = nineAndTwo;
            return characterDataInfo;
        }
        catch (Exception e)
        {
            Debug.Log("数据库信息有错误:"+e);
            return null;
        }
    }
}

[System.Serializable]
public class CharacterDataInfoListJsonResponse
{
    public CharacterDataInfoListJsonResponseData data;
}

[System.Serializable]
public class CharacterDataInfoListJsonResponseData
{
    public CharacterDataInfoJson[] list;
}

[System.Serializable]
public class CharacterDataInfoJsonResponse
{
    public CharacterDataInfoJson data;
}
