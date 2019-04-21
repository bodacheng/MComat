using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

[Serializable]
public class CharacterResourceInfo
{
	public int charResouceNum;//monsterTable ID
    public string type = null;
    public string prefabName;//monsterTable realName
    public string showNameEN;//monsterTable showNameEN
    public string showNameCN;
    public string showNameJP;
    public zokusei _zokusei = zokusei.lightMagic;
    public string personalMagicPack;
    public string BasicMoveSetName = "basic_anim";//monsterTable BasicMoveSet
    public MoveType moveType = MoveType.Mode1;//monsterTable moveType
    public RushType rushType = RushType.RushBack;//monsterTable accSKill
    public bool canDefend = true;
	public string instructionEN;
    public string instructionCH;
    public string instructionJP;
    public int rarelevel = 3;

    public passiveSkillConfigs getPassiveSkillConfigs()
    {
        passiveSkillConfigs passiveSkillConfigs = new passiveSkillConfigs(this.moveType,this.canDefend,this.rushType);
        return passiveSkillConfigs;
    }

    public CharacterDataInfo getASampleCharacterDataInfo(int localID)
    {
        CharacterDataInfo characterDataInfo = new CharacterDataInfo();
        characterDataInfo.localID = localID;
        characterDataInfo.resource_num = charResouceNum; // 确切的说这个也就是角色的pretab编号，最后也就是数据库里master table的主key。
        characterDataInfo.level = 1;
        characterDataInfo.HP = 500; //通常来说玩家的角色HP和角色level应该有一个清晰的对应关系，而关卡敌人的HP应该是可以自由设置，这个HP必然不会出现在数据库的任何部位。
        characterDataInfo.EXP = 0;
        characterDataInfo._NineAndTwo = null;
        return characterDataInfo;
    }
}

[Serializable] 
public class passiveSkillConfigs
{
    public SkillConfig MConfig;
    public SkillConfig DConfig;
    public SkillConfig RConfig;

    public passiveSkillConfigs(MoveType moveType,bool hasDefend,RushType RStyle)
    {
        switch(moveType)
        {
            case MoveType.Mode1:
                this.MConfig = new SkillConfig
                    (
                        -1, null, "Move_normal","normal move", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            case MoveType.Mode2:
                this.MConfig = new SkillConfig
                    (
                        -1, null, "Move_slow", "normal move", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            case MoveType.Mode3:
                this.MConfig = new SkillConfig
                    (
                        -1, null, "Move_fast", "normal move", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            case MoveType.Test:
                this.MConfig = new SkillConfig
                    (
                        -1, null, "Test_Move", "测试用移动状态(角色站着不动)", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            default:
                this.MConfig = new SkillConfig
                    (
                        -1, null, "Move_normal", "normal move", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
        }

        if (hasDefend)
        {
            this.DConfig = new SkillConfig
                    (
                        -1, null, "Defend", "防衛", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
        }else{
            this.DConfig = null;
        }

        switch (RStyle)
        {
            case RushType.Jump:
                this.RConfig = new SkillConfig
                    (
                        -1, null, "Jump", "Jump", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            case RushType.Rush:
                this.RConfig = new SkillConfig
                    (
                        -1, null, "Rush", "Rush", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            case RushType.RushBack:
                this.RConfig = new SkillConfig
                    (
                        -1, null, "RushBack", "RushBack", 0, stateType.NONE, null, EX.normal, false, skillEmergentLevel.none
                    );
                break;
            case RushType.None:
                this.RConfig = null;
                break;
        }
    }
}

namespace UnityEngine.UI
{
	//public class CharResourceDataBase : ScriptableObject {
 //       public string type;
	//	public CharacterResourceInfo[] chars;

	//	/// <summary>
	//	/// Gets the specified ItemInfo by ID.
	//	/// </summary>
	//	/// <returns>The ItemInfo or NULL if not found.</returns>
	//	/// <param name="ID">The item ID.</param>
	//	public CharacterResourceInfo GetByID(int ID)
	//	{
	//		for (int i = 0; i < this.chars.Length; i++)
	//		{
 //               if (this.chars[i].charResouceNum == ID)
 //               {
 //                   return this.chars[i];
 //               }else{
 //               }					
	//		}

	//		return null;
	//	}

 //       public CharacterResourceInfo GetByPrefabName(string prefabName)
 //       {
 //           foreach (CharacterResourceInfo _char in chars)
 //           {

 //               if (_char.prefabName.GetHashCode() == prefabName.GetHashCode())
 //               {
 //                   return _char;
 //               }
 //           }
 //           return null;
 //       }

	//	public CharacterResourceInfo getRandomChar()
	//	{
 //           foreach (CharacterResourceInfo _info in chars)
 //           {
 //               if (_info == null)
 //               {
 //                   Debug.Log("角色数据库中不允许空值存在，操作停止");
 //                   return null;
 //               }
 //           }
 //           CharacterResourceInfo info = chars[Random.Range(0, chars.Length)];
 //           return info;
	//	}

 //       public List<int> getAllResourceNums()
 //       {
 //           List<int> _nums = new List<int>();
 //           foreach(CharacterResourceInfo _char in chars)
 //           {
 //               if (!_nums.Contains(_char.charResouceNum))
 //                   _nums.Add(_char.charResouceNum);
 //           }
 //           return _nums;
 //       }

 //       public List<string> getAllResourceNames()
 //       {
 //           //IDictionary<int, string> ResourceNumWithName = new Dictionary<int,string>();
 //           List<string> _nums = new List<string>();
 //           foreach (CharacterResourceInfo _char in chars)
 //           {
 //               if (_char != null)
 //               {
 //                   if (!_nums.Contains(_char.prefabName))
 //                       _nums.Add(_char.prefabName);
 //               }else
 //               {
 //                   Debug.Log(_char + "为设置pretab");
 //               }
 //           }
 //           return _nums;
 //       }
	//}
}
	
//public class CharDatabaseEditor
//{
//	#if UNITY_EDITOR
//	private static string GetSavePath()
//	{
//		return EditorUtility.SaveFilePanelInProject("New item database", "New item database", "asset", "Create a new item database.");
//	}
		
//	[MenuItem("Assets/Create/Databases/CharResourceDataBase")]
//	public static void CreateDatabase()
//	{
//		string assetPath = GetSavePath();
//		CharResourceDataBase asset = ScriptableObject.CreateInstance("CharResourceDataBase") as CharResourceDataBase;  //scriptable object
//		AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
//		AssetDatabase.Refresh();
//	}
//	#endif
//}