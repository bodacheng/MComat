using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

// 功能：
// 1.读取所有攻击类型文件进行配置文件生成。

public class ConfigFileManager : MonoBehaviour {

    public string[] chartypes;
    public TextAsset CharacterConfigTextFile;
    public string MonstersConfigFilePath;
    public TextAsset SkillConfigTextFile;
    public string SkillConfigFilePath;

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "All Characters"))
        {
            AccountCharsSet.Instance.localSaveDataGetAllCharacters(Application.persistentDataPath + "/" + "myownedCharsJson.json");
        }
        if (GUI.Button(new Rect(0, 50, 100, 50), "All stones"))
        {
            MySkillStonesReader.loadAllSkillConfigFromConfigFile();
            List<int> mystones = new List<int>();
            foreach (KeyValuePair<int, SkillConfig> _pair in MySkillStonesReader.SkillConfigDicForReference)
            {
                mystones.Add(_pair.Value.id);
            }
            MySkillStonesReader.Instance.saveMySkillStones();
        }
    }

        //<UnityEngine.Object> basicAnimsObject = Resources.LoadAll(animPath + "/BasicPack/" + basicPackName, typeof(AnimationClip)).ToList();

        // 以下这个函数对技能表的更新机制企划如下：
        // 首先读取现有配置文件，获取现有的所有条目。然后，读取resource文件夹，会按type顺序拿现有条目和resource进行比较。
        // 配置文件允许在一个type下存在相同keyname的复数个条目(同一个动画不同攻击类型)，
        // 但，为了管理思路清晰我们不允许Resource文件夹下出现同名资源，确切的说不允许一个type下，某一个攻击类型的动画片段在另一个攻击类型的文件夹里有同名动画片段
        // 一旦发现上述情况将中断配置文件更新。
        // 如果，检测出现有的配置文件中记载的某一个条目的keyname值找不到对应type的Resource文件夹资源，那么这个条目会被记载下来进行删除。
        // 如果有复数个同keyname条目找不到对应type的Resource文件夹资源，这几个条目都会被删除。
        // 上述情况下被删除的条目，他们原先的ID会被记录下来，然后就是重点：如果有新的条目加入，新条目会顶替被删除的旧条目换上这些ID
        // 而新条目的自动追加原则是：查看Resource文件夹下有没有配置文件里所有条目不存在的keyname(对应type下)，有这样的新资源，就给配置文件追加一个条目。
        // 因此————1. 配置文件中如果同type下一个keyname有复数个条目，只能是我们手动所为 2. 配置文件里没“基本状态”的定义，只有技能状态定义。
        // 基本状态是角色加载时候函数给根据九宫格记载给添加的，不参考数据库或配置文件。
        // 3.如果一个keyname的资源，已经在相应type下的配置文件信息中有一个条目，那么，假设你更改了这个资源在Resource文件夹下的位置，比如从GR移动到了GM，
        // 那么系统将不会根据它在Resource文件夹下位置的变动来更新它在配置文件对应条目里的信息，原来是怎么定义的就是怎么定义的，除非找不到这个资源把它条目删了。
        // 也就是说这个配置文件更新函数的重点只是根据资源的有无来决定是不是添加初始条目或删除旧条目，技能详细定义你自己去改，它不会胡乱给你改。
        // 4. ID的“填补机制”是建立在原有条目对应资源缺失,并且在一次更新操作的前提下。只有那些找不到资源了的条目的旧ID才会被新资源对应的新条目代替ID。假设你在某资源存在的时候删了它条目，然后重新更新一次配置文件，
        // 会发现被补上的条目ID是最新（最大）值。这一点无论是角色Config还是SkillConfig都是一样的。
    public void SkillConfigFileUpdate(string path, TextAsset textAsset)
    {
        SkillConfigTable skillConfigTable = new SkillConfigTable();
        if (textAsset != null)
            skillConfigTable.Load(textAsset);

        List<int> KisoonIDs = new List<int>();
        List<int> AllDeletedRecordsIDs = new List<int>();
        List<SkillConfig> AllNewSkillConfigsOfAllTypes = new List<SkillConfig>();

        foreach (string chartype in chartypes)
        {
            List<SkillConfig> skillConfigsOfOldConfigFileOFtype = skillConfigTable.RowsToSkillConfigList(
                skillConfigTable.FindAll_type(chartype)
            );

            List<string> KisonnRecourdsOFKeyNames = new List<string>(); //这个type的角色旧config文件中所有条目的keyname list。

            foreach (SkillConfig skillConfig in skillConfigsOfOldConfigFileOFtype)
            {
                if (!KisonnRecourdsOFKeyNames.Contains(skillConfig.keyName))//注意看，同一个key名在数据库可能不止一个条目。比如一个发波动画定义了两种攻击             
                    KisonnRecourdsOFKeyNames.Add(skillConfig.keyName);

                if (!KisoonIDs.Contains(skillConfig.id))
                    KisoonIDs.Add(skillConfig.id);
                else{
                    Debug.Log("极端致命错误：原SkillConfigTable似乎有重复ID,操作中止。");
                    return;
                }
            }

            //这个首先是确保了本地资源重名的情况下不会被重复登陆
            List<string> currentAllkeyNamesOfResourceFolder = new List<string>();

            ///////// 接下来索引该type现在resource文件夹下的资源。如果是已经存在的
            List<SkillConfig> newSkillConfigsOfType = new List<SkillConfig>();

            // 下面这一大堆针对各个攻击片段文件夹的循环，意思是说，原则上一发现一个新动画片段，那么只针对它添加一个技能条目。如果你想针对一个动画片段添加其他类型的攻击技能那只能手动
            // 并且如果旧的config文件中已经有针对一个动画资源的技能状态定义，那么以旧定义为准，不会去动它，即便你换了某个动画片段的Resource文件夹位置。
            // 你如果真换了某个动画片段的Resource文件夹位置，那意味着你可能本来觉得它是个GR类攻击，那后来觉得做GM攻击更合适，那你只能手动去数据库文件做相应更改，这个更新操作不会替你做这个事情。
            // 这个系列的状态角色必须有个叫做“dash”的冲刺动作
            UnityEngine.Object[] GAttackStateResources = Resources.LoadAll("Animations/" + chartype + "/G_Attack_State", typeof(AnimationClip));
            foreach (UnityEngine.Object _anim in GAttackStateResources)
            {
                if (!currentAllkeyNamesOfResourceFolder.Contains(_anim.name))
                    currentAllkeyNamesOfResourceFolder.Add(_anim.name);
                else
                {
                    Debug.Log("本地" + chartype + "type" + "出现重名情况：" + _anim.name + "考虑改资源文件名。skill表更新操作将中断。");
                    return;
                }

                if (!KisonnRecourdsOFKeyNames.Contains(_anim.name))
                {
                    SkillConfig OneConfig = new SkillConfig();
                    OneConfig.id = -1;
                    OneConfig.type = chartype;
                    OneConfig.keyName = _anim.name;
                    OneConfig.ShowName = "unknown";
                    OneConfig.SPLevel = EX.normal;
                    switch (OneConfig.SPLevel)
                    {
                        case EX.normal:
                            OneConfig.SkillPoint = 10;//实际的SKillPoint可能会根据玩家角色等级进行一些加成
                            break;
                        case EX.EX1:
                            OneConfig.SkillPoint = -20;
                            break;
                        case EX.EX2:
                            OneConfig.SkillPoint = -30;
                            break;
                        case EX.EX3:
                            OneConfig.SkillPoint = -50;
                            break;
                        default:
                            OneConfig.SkillPoint = 0;
                            break;
                    }
                    OneConfig.stateType = stateType.GR;
                    OneConfig.ai_trigger_ranges = new behaviorEnterRange[1] { behaviorEnterRange.inner_range };
                    OneConfig.canAirTrigger = false;
                    OneConfig.skillEmergentLevel = skillEmergentLevel.level2;
                    OneConfig.rarelevel = 1;

                    newSkillConfigsOfType.Add(OneConfig);
                }else{
                    //那么代表这个资源对应的条目已经在原先的config文件里已经有了，保留原先设定，不添加。
                }
            }

            UnityEngine.Object[] GAttackStateStayResources = Resources.LoadAll("Animations/" + chartype + "/G_Attack_State_Stay", typeof(AnimationClip));//这个系列的状态角色必须有个叫做“Rush”的冲刺动作
            foreach (UnityEngine.Object _anim in GAttackStateStayResources)
            {
                if (!currentAllkeyNamesOfResourceFolder.Contains(_anim.name))
                    currentAllkeyNamesOfResourceFolder.Add(_anim.name);
                else
                {
                    Debug.Log("本地" + chartype + "type" + "出现重名情况：" + _anim.name + "考虑改资源文件名。skill表更新操作将中断。");
                    return;
                }

                if (!KisonnRecourdsOFKeyNames.Contains(_anim.name))
                {
                    SkillConfig OneConfig = new SkillConfig();
                    OneConfig.id = -1;
                    OneConfig.type = chartype;
                    OneConfig.keyName = _anim.name;
                    OneConfig.ShowName = "unknown";
                    OneConfig.SPLevel = EX.normal;
                    switch (OneConfig.SPLevel)
                    {
                        case EX.normal:
                            OneConfig.SkillPoint = 10;//实际的SKillPoint可能会根据玩家角色等级进行一些加成
                            break;
                        case EX.EX1:
                            OneConfig.SkillPoint = -20;
                            break;
                        case EX.EX2:
                            OneConfig.SkillPoint = -30;
                            break;
                        case EX.EX3:
                            OneConfig.SkillPoint = -50;
                            break;
                        default:
                            OneConfig.SkillPoint = 0;
                            break;
                    }
                    OneConfig.stateType = stateType.GI;
                    OneConfig.ai_trigger_ranges = new behaviorEnterRange[3] { behaviorEnterRange.inner_range, behaviorEnterRange.mid_range, behaviorEnterRange.far_range};
                    OneConfig.canAirTrigger = false;
                    OneConfig.skillEmergentLevel = skillEmergentLevel.level2;
                    OneConfig.rarelevel = 1;

                    newSkillConfigsOfType.Add(OneConfig);
                }else{

                }
            }

            UnityEngine.Object[] GMResources = Resources.LoadAll("Animations/" + chartype + "/GMStates", typeof(AnimationClip));
            foreach (UnityEngine.Object _anim in GMResources)
            {
                if (!currentAllkeyNamesOfResourceFolder.Contains(_anim.name))
                    currentAllkeyNamesOfResourceFolder.Add(_anim.name);
                else
                {
                    Debug.Log("本地" + chartype + "type" + "出现重名情况：" + _anim.name + "考虑改资源文件名。skill表更新操作将中断。");
                    return;
                }

                if (!KisonnRecourdsOFKeyNames.Contains(_anim.name))
                {
                    SkillConfig OneConfig = new SkillConfig();
                    OneConfig.id = -1;
                    OneConfig.type = chartype;
                    OneConfig.keyName = _anim.name;
                    OneConfig.ShowName = "unknown";
                    OneConfig.SPLevel = EX.normal;
                    switch (OneConfig.SPLevel)
                    {
                        case EX.normal:
                            OneConfig.SkillPoint = 10;//实际的SKillPoint可能会根据玩家角色等级进行一些加成
                            break;
                        case EX.EX1:
                            OneConfig.SkillPoint = -20;
                            break;
                        case EX.EX2:
                            OneConfig.SkillPoint = -30;
                            break;
                        case EX.EX3:
                            OneConfig.SkillPoint = -50;
                            break;
                        default:
                            OneConfig.SkillPoint = 0;
                            break;
                    }
                    OneConfig.stateType = stateType.GM;
                    OneConfig.ai_trigger_ranges = new behaviorEnterRange[2] { behaviorEnterRange.mid_range, behaviorEnterRange.inner_range };
                    OneConfig.canAirTrigger = false;
                    OneConfig.skillEmergentLevel = skillEmergentLevel.level2;
                    OneConfig.rarelevel = 1;

                    newSkillConfigsOfType.Add(OneConfig);
                }
            }

            AllNewSkillConfigsOfAllTypes.AddRange(newSkillConfigsOfType);

            //旧版本有的keyname可是Resource文件夹下没有的
            List<string> ShouldDeletedFromConfig = new List<string>();
            foreach(string keyname in KisonnRecourdsOFKeyNames)
            {
                if (!currentAllkeyNamesOfResourceFolder.Contains(keyname))
                    ShouldDeletedFromConfig.Add(keyname);
            }

            foreach(string keyname in ShouldDeletedFromConfig)
            {
                List<SkillConfigTable.Row> toDeleteRows = skillConfigTable.FindAll_type_keyName(chartype, keyname);//注意看，同一个key名在数据库可能不止一个条目。比如一个发波动画定义了两种攻击
                foreach (SkillConfigTable.Row row in toDeleteRows)
                {
                    if (!AllDeletedRecordsIDs.Contains(int.Parse(row.ID)))
                    {
                        Debug.Log("这是一个要删除的ID"+ int.Parse(row.ID));
                        AllDeletedRecordsIDs.Add(int.Parse(row.ID));
                    }
                    else
                        Debug.Log("原SkillConfigTable似乎有重复ID，而且似乎还是因为资源缺失要删除的条目。。");

                    skillConfigTable.rowList.Remove(row);
                }
            }
        }

        /////////  TYpe内处理循环结束  ////////
        foreach (SkillConfig newSkillConfig in AllNewSkillConfigsOfAllTypes)
        {
            if (AllDeletedRecordsIDs.Count > 0)
            {
                newSkillConfig.id = AllDeletedRecordsIDs[0];
                Debug.Log(newSkillConfig.keyName + "de 新ID： "+ newSkillConfig.id);
                AllDeletedRecordsIDs.RemoveAt(0);
                KisoonIDs.Add(newSkillConfig.id);
            }
            else{
                newSkillConfig.id = MaxOfIntList(KisoonIDs) + 1;
                KisoonIDs.Add(newSkillConfig.id);
            }
            SkillConfigTable.Row newRow = skillConfigTable.SkillConfigToRow(newSkillConfig);
            if (newRow != null && newRow.keyName != null)
                skillConfigTable.rowList.Add(newRow);
        }
        skillConfigTable.SaveByCurrentRows(Application.dataPath + "/" + path);
    }

    public int MaxOfIntList(List<int> List)
    {
        int i = -999;

        if (List.Count == 0)
        {
            return 0;
        }
        if (List.Count == 1)
        {
            return List[0];
        }
        if (List.Count > 1)
        {
            i = List[0];
            for (int y = 0; y < List.Count; y++)
            {
                if (i < List[y])
                {
                    i = List[y];
                }
            }
            return i;
        }
        return i;
    }

    // 由Resource文件夹更新角色配置文件信息所需要的工作应该有如下：
    // 首先，同prefabName 允许在数据库存在复数个条目。比如外观一样的红色魔法暴龙和蓝色魔法暴龙，他们可以prefabName一样但charResouceNum不同。
    // 但首次自动生成配置文件，系统只会为新的资源添加一条对应条目，并且条目具体信息是默认的，非常空，一定需要手动设置。
    // 如果要有一样的资源不同的条目这种情况，必定是手动添加的结果。
    // 如果数据库里存在条目在Resource下检测不到对应资源。。。。那这样的条目会被删除。原先的ID会被新添加的资源对应的新条目补空档。
    // 系统不会对旧条目中自己手动填写的任何具体定义做更新，只会根据资源的有无来决定条目的追加与删除与否。
    public void CharsConfigFileGenerate(string path, TextAsset textAsset)
    {
        monstersConfigTable monstersTable = new monstersConfigTable();
        if (textAsset != null)
            monstersTable.Load(textAsset);
            
        List<int> AllDeletedRecordsIDs = new List<int>();
        List<int> kisoonCharacterResourceInfoRID = new List<int>();
        List<CharacterResourceInfo> AllNewCharacterConfigsOfAllTypes = new List<CharacterResourceInfo>();

        foreach (string chartype in chartypes)
        {
            List<string> currentAllPrefabNamesOfResourceFolder = new List<string>();
            List<CharacterResourceInfo> CharConfigsOfOldConfigFileOFtype = monstersTable.RowToCharacterResourceInfoList(
                monstersTable.FindAll_type(chartype)
            );

            List<string> keySonnCharacterPrefabNames = new List<string>();
            foreach (CharacterResourceInfo oneConfig in CharConfigsOfOldConfigFileOFtype)
            {
                if (!keySonnCharacterPrefabNames.Contains(oneConfig.prefabName))
                {
                    keySonnCharacterPrefabNames.Add(oneConfig.prefabName);
                }else{
                    //什么也不做。允许。
                }
                if (!kisoonCharacterResourceInfoRID.Contains(oneConfig.charResouceNum))
                {
                    kisoonCharacterResourceInfoRID.Add(oneConfig.charResouceNum);
                }else{
                    Debug.Log("致命错误，原COnfig设置中ID重复："+ oneConfig.charResouceNum);
                    return;
                }
            }

            UnityEngine.Object[] pretabResources = Resources.LoadAll("charPretabs/" + chartype);
            foreach (UnityEngine.Object charPretab in pretabResources)
            {
                if (!currentAllPrefabNamesOfResourceFolder.Contains(charPretab.name))
                    currentAllPrefabNamesOfResourceFolder.Add(charPretab.name);

                if (keySonnCharacterPrefabNames.Contains(charPretab.name))
                {
                    continue;//不对原来就存在的资源对应条目做更改。
                }

                GameObject character = charPretab as GameObject;
                if (character.GetComponent<Data_Center>() == null
                   ||
                    character.GetComponent<BO_Ani_E>() == null
                    ||
                    character.GetComponent<AIStateRunner>() == null
                    ||
                    character.GetComponent<BO_Health>() == null
                   )
                {
                    Debug.Log(chartype+"资源"+ charPretab.name + "丢失必要组件，不是一个正常角色资源");
                    continue;
                }

                CharacterResourceInfo _CharacterResourceInfo = new CharacterResourceInfo();
                _CharacterResourceInfo.charResouceNum = -1;
                _CharacterResourceInfo.type = chartype;
                _CharacterResourceInfo.prefabName = charPretab.name;
                _CharacterResourceInfo.showNameEN = null;
                _CharacterResourceInfo.showNameCN = null;
                _CharacterResourceInfo.showNameJP = null;
                switch (character.GetComponent<Data_Center>().Zokusei)
                {
                    case zokusei.blueMagic:
                        _CharacterResourceInfo._zokusei = zokusei.blueMagic;
                        break;
                    case zokusei.redMagic:
                        _CharacterResourceInfo._zokusei = zokusei.redMagic;
                        break;
                    case zokusei.greenMagic:
                        _CharacterResourceInfo._zokusei = zokusei.greenMagic;
                        break;
                    case zokusei.darkMagic:
                        _CharacterResourceInfo._zokusei = zokusei.darkMagic;
                        break;
                    case zokusei.lightMagic:
                        _CharacterResourceInfo._zokusei = zokusei.lightMagic;
                        break;
                }
                _CharacterResourceInfo.personalMagicPack = null;//这个只能后加把。。
                _CharacterResourceInfo.BasicMoveSetName = "warrior";//我感觉这个应该起名字叫做basic。每个type起码有一个叫这个的。
                _CharacterResourceInfo.moveType = MoveType.Mode1;
                _CharacterResourceInfo.rushType = RushType.RushBack;
                _CharacterResourceInfo.canDefend = true;
                _CharacterResourceInfo.instructionCH = null;
                _CharacterResourceInfo.instructionEN = null;
                _CharacterResourceInfo.instructionJP = null;
                _CharacterResourceInfo.rarelevel = 1;

                AllNewCharacterConfigsOfAllTypes.Add(_CharacterResourceInfo);
            }

            //旧版本有的keyname可是Resource文件夹下没有的
            List<string> ResourceNamesShouldDeletedFromConfig = new List<string>();
            foreach (string keyname in keySonnCharacterPrefabNames)
            {
                if (!currentAllPrefabNamesOfResourceFolder.Contains(keyname))
                    ResourceNamesShouldDeletedFromConfig.Add(keyname);
            }

            foreach (string keyname in ResourceNamesShouldDeletedFromConfig)
            {
                List<monstersConfigTable.Row> toDeleteRows
                                        = monstersTable.FindAll_type_realName(chartype, keyname);//注意看，同一个key名在数据库可能不止一个条目。比如一个发波动画定义了两种攻击
                foreach (monstersConfigTable.Row row in toDeleteRows)
                {
                    if (!AllDeletedRecordsIDs.Contains(int.Parse(row.id)))
                    {
                        Debug.Log("这是一个要删除的ID" + int.Parse(row.id));
                        AllDeletedRecordsIDs.Add(int.Parse(row.id));
                    }
                    else
                        Debug.Log("原monstersConfigTable似乎有重复ID，而且似乎还是因为资源缺失要删除的条目。。");

                    monstersTable.rowList.Remove(row);
                }
            }
        }

        foreach (CharacterResourceInfo characterResourceInfo in AllNewCharacterConfigsOfAllTypes)
        {
            if (AllDeletedRecordsIDs.Count > 0)
            {
                characterResourceInfo.charResouceNum = AllDeletedRecordsIDs[0];
                Debug.Log(characterResourceInfo.prefabName + "的 ID： " + characterResourceInfo.charResouceNum);
                AllDeletedRecordsIDs.RemoveAt(0);
                kisoonCharacterResourceInfoRID.Add(characterResourceInfo.charResouceNum);
            }
            else
            {
                characterResourceInfo.charResouceNum = MaxOfIntList(kisoonCharacterResourceInfoRID) + 1;
                kisoonCharacterResourceInfoRID.Add(characterResourceInfo.charResouceNum);
            }
            monstersConfigTable.Row newRow = monstersTable.CharacterResourceInfoToRow(characterResourceInfo);
            if (newRow != null && newRow.realName != null)
                monstersTable.rowList.Add(newRow);
        }
        monstersTable.SaveByCurrentRows(Application.dataPath + "/" + path);
    }
}
