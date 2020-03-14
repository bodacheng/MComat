using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Skill;

[Serializable]
public class CharacterResourceInfo
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
        PassiveSkillConfigs passiveSkillConfigs = new PassiveSkillConfigs(this.moveType,this.DEFENDABLE_FLAG,this.rushType);
        return passiveSkillConfigs;
    }

    public CharacterDataInfo GetASampleCharacterDataInfo(string localID)
    {
        CharacterDataInfo characterDataInfo = new CharacterDataInfo
        {
            monsterOfPlayerId = localID,
            ResourceID = RECORD_ID, // 确切的说这个也就是角色的pretab编号，最后也就是数据库里master table的主key。
            level = 1,
            HP = 500, //通常来说玩家的角色HP和角色level应该有一个清晰的对应关系，而关卡敌人的HP应该是可以自由设置，这个HP必然不会出现在数据库的任何部位。
            _NineAndTwo = null
        };
        return characterDataInfo;
    }
}

//namespace UnityEngine.UI
//{
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
//}
	
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