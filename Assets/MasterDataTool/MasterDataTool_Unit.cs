using System.Collections.Generic;
using UnityEngine;
using Skill;

public partial class MasterDataTool
{
    // 由Resource文件夹更新角色配置文件信息所需要的工作应该有如下：
    // 首先，同prefabName 允许在数据库存在复数个条目。比如外观一样的红色魔法暴龙和蓝色魔法暴龙，他们可以prefabName一样但charResouceNum不同。
    // 但首次自动生成配置文件，系统只会为新的资源添加一条对应条目，并且条目具体信息是默认的，非常空，一定需要手动设置。
    // 如果要有一样的资源不同的条目这种情况，必定是手动添加的结果。
    // 如果数据库里存在条目在Resource下检测不到对应资源。。。。那这样的条目会被删除。原先的ID会被新添加的资源对应的新条目补空档。
    // 系统不会对旧条目中自己手动填写的任何具体定义做更新，只会根据资源的有无来决定条目的追加与删除与否。

    public void UnitsConfigFileGenerate(string path, TextAsset textAsset, string[] chartypes)
    {
        if (textAsset != null)
        {
            Units.Load(textAsset);
        }
        List<int> AllDeletedRecordsIDs = new List<int>();
        List<string> existedUnitUnitRID = new List<string>();
        List<UnitConfig> AllNewUnitConfigsAllTypes = new List<UnitConfig>();
        foreach (string chartype in chartypes)
        {
            List<string> currentAllRealNamesOfResourceFolder = new List<string>();
            List<UnitConfig> CharConfigsOfOldConfigFileOFtype = Units.RowToConfigList(Units.FindAll_MONSTER_TYPE(chartype));
            List<string> keySonnCharacterRealNames = new List<string>();
            foreach (UnitConfig oneConfig in CharConfigsOfOldConfigFileOFtype)
            {
                if (!keySonnCharacterRealNames.Contains(oneConfig.REAL_NAME))
                {
                    keySonnCharacterRealNames.Add(oneConfig.REAL_NAME);
                }
                else
                {
                    //什么也不做。允许。
                }
                existedUnitUnitRID.Add(oneConfig.RECORD_ID);
            }

            Object[] pretabResources = Resources.LoadAll("CharPretabs/" + chartype);
            foreach (Object charPretab in pretabResources)
            {
                if (!currentAllRealNamesOfResourceFolder.Contains(charPretab.name))
                    currentAllRealNamesOfResourceFolder.Add(charPretab.name);

                if (keySonnCharacterRealNames.Contains(charPretab.name))
                {
                    continue;//不对原来就存在的资源对应条目做更改。
                }

                GameObject character = charPretab as GameObject;
                if (character.GetComponent<OutsideDataLink>() == null)
                {
                    Debug.Log(chartype + "资源" + charPretab.name + "丢失必要组件，不是一个正常角色资源");
                    continue;
                }

                UnitConfig unitConfig = new UnitConfig
                {
                    RECORD_ID = "-1",
                    TYPE = chartype,
                    REAL_NAME = charPretab.name,
                    showNameEN = null,
                    showNameCN = null,
                    showNameJP = null
                };

                OutsideDataLink outsideDataLink = character.GetComponent<OutsideDataLink>();
                switch (outsideDataLink._C.zokusei)
                {
                    case Zokusei.blueMagic:
                        unitConfig._zokusei = Zokusei.blueMagic;
                        break;
                    case Zokusei.redMagic:
                        unitConfig._zokusei = Zokusei.redMagic;
                        break;
                    case Zokusei.greenMagic:
                        unitConfig._zokusei = Zokusei.greenMagic;
                        break;
                    case Zokusei.darkMagic:
                        unitConfig._zokusei = Zokusei.darkMagic;
                        break;
                    case Zokusei.lightMagic:
                        unitConfig._zokusei = Zokusei.lightMagic;
                        break;
                }
                unitConfig.SPECIAL_ZOKUSEI = null; //这个只能后加把。。
                unitConfig.BASIC_MOVEMENT_PACK = "warrior";//我感觉这个应该起名字叫做basic。每个type起码有一个叫这个的。
                unitConfig.MoveType = MoveType.Move_normal;
                unitConfig.RushType = RushType.RushBack;
                unitConfig.DEFENDABLE_FLAG = true;
                unitConfig.InstructionCH = null;
                unitConfig.InstructionEN = null;
                unitConfig.InstructionJP = null;
                unitConfig.RARITY_LEVEL = 1;

                AllNewUnitConfigsAllTypes.Add(unitConfig);
            }

            //旧版本有的keyname可是Resource文件夹下没有的
            List<string> ResourceNamesShouldDeletedFromConfig = new List<string>();
            foreach (string keyname in keySonnCharacterRealNames)
            {
                if (!currentAllRealNamesOfResourceFolder.Contains(keyname))
                    ResourceNamesShouldDeletedFromConfig.Add(keyname);
            }

            foreach (string keyname in ResourceNamesShouldDeletedFromConfig)
            {
                List<Units.Row> toDeleteRows = Units.FindAll_TYPE_REALNAME(chartype, keyname);
                foreach (Units.Row row in toDeleteRows)
                {
                    if (!AllDeletedRecordsIDs.Contains(int.Parse(row.RECORD_ID)))
                    {
                        Debug.Log("这是一个要删除的ID" + int.Parse(row.RECORD_ID));
                        AllDeletedRecordsIDs.Add(int.Parse(row.RECORD_ID));
                    }
                    else
                        Debug.Log("原monstersConfigTable似乎有重复ID，而且似乎还是因为资源缺失要删除的条目。。");
                    Units.rowList.Remove(row);
                }
            }
        }

        foreach (UnitConfig characterResourceInfo in AllNewUnitConfigsAllTypes)
        {
            if (AllDeletedRecordsIDs.Count > 0)
            {
                characterResourceInfo.RECORD_ID = "new";
                Debug.Log(characterResourceInfo.REAL_NAME + "的 ID： " + characterResourceInfo.RECORD_ID);
                AllDeletedRecordsIDs.RemoveAt(0);
                existedUnitUnitRID.Add(characterResourceInfo.RECORD_ID);
            }
            else
            {
                characterResourceInfo.RECORD_ID = "new";
                existedUnitUnitRID.Add(characterResourceInfo.RECORD_ID);
            }
            Units.Row newRow = Units.ConfigToRow(characterResourceInfo);
            if (newRow != null && newRow.REAL_NAME != null)
                Units.rowList.Add(newRow);
        }
        Units.SaveByCurrentRows(Application.dataPath + "/" + path != null ? path : "mst_monster");
    }
}
