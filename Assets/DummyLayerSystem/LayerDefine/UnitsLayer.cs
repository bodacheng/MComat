using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public class UnitsLayer : UILayer
    {
        [Space(7)]
        [Header("monsterboxFilter")]
        public MonsterboxFilter _monsterboxFilter;
        
        [Space(7)]
        [Header("角色属性框")]
        public HeroIcon noMagic;
        
        [Space(7)]
        [Header("选中框")]
        public GameObject selectedFrame;
        
        [Space(2)]
        [Header("宠物栏parent")]
        public RectTransform MonsterBoxContainer;

        readonly List<string> typeOfUnitsIhave = new List<string>();
        readonly IDictionary<string, HeroIcon> mainMenuIcons = new Dictionary<string, HeroIcon>();
        string selectingInstanceID;
        
        public static UnitsLayer Open()
        {
            return UILayerLoader.Load(PreScene.target.T,"UnitsLayer") as UnitsLayer;
        }

        public static void Close()
        {
            UILayerLoader.Remove("UnitsLayer");
        }
        
        public HeroIcon GetCharIcon(string instanceID)
        {
            if (instanceID == null)
                return null;
            mainMenuIcons.TryGetValue(instanceID, out HeroIcon charIcon);
            return charIcon;
        }

        public void SetUnitsIconOnClick(Action<string> a)
        {
            foreach (var kv in mainMenuIcons)
            {
                kv.Value.iconButton.onClick.RemoveAllListeners();
                kv.Value.iconButton.onClick.AddListener(()=> { a.Invoke(kv.Key); });
            }
        }
        
        public void CancelSelect()
        {
            selectingInstanceID = null;
            HeroIcon.Seletedfeature(null, selectedFrame, 150f);
        }

        public void Select(string monsterOfPlayerId)
        {
            HeroIcon targetingIcon = GetCharIcon(monsterOfPlayerId);
            HeroIcon.Seletedfeature(targetingIcon, selectedFrame, 150f);
            selectingInstanceID = monsterOfPlayerId;
        }

        public string GetSelect()
        {
            return selectingInstanceID;
        }

        public void AddOneNewIcon(string instanceID, bool clearButtonFeature)
        {
            UnitInfo targetingCharInfo = MyMonsters.Get(instanceID);
            CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(targetingCharInfo.r_id);
            if (_CharConfig == null)
            {
                Debug.Log("MonsterID:"+ targetingCharInfo.r_id + " doesnt exist in this version");
                return;
            }

            HeroIcon targetingIcon = GetCharIcon(instanceID);
            if (targetingIcon == null)
            {
                MonsterIconDic.Get(_CharConfig.RECORD_ID);
                targetingIcon = Instantiate(noMagic);
                targetingIcon.name = _CharConfig.REAL_NAME + "_icon";
                targetingIcon._CharConfig = _CharConfig;
                targetingIcon.ChangeIcon(MonsterIconDic.Get(_CharConfig.RECORD_ID), _CharConfig._zokusei);
                DicAdd<string, HeroIcon>.Add(mainMenuIcons, instanceID, targetingIcon);
            }
            if (clearButtonFeature)
                targetingIcon.iconButton.onClick.RemoveAllListeners();
            if (!typeOfUnitsIhave.Contains(targetingIcon._CharConfig.TYPE))
            {
                typeOfUnitsIhave.Add(targetingIcon._CharConfig.TYPE);
            }
        }

        public void OnTypeChangeMyMonsterBox()
        {
            DisplayMonsterIcons(false);
        }

        // 从这个函数的名字来看，应该是个产生monsterbox内所有图标的东西。原则上这个玩意如果没有什么新宠物的添加，它是很少加载才对。
        // 难点在于每个monstericon上给予一个什么样的按钮 ，并且这个按钮到底是什么时机下给予。
        // 现在的模型循环利用机制决定：每次运行mymonsterbox，都要执行所有拥有角色的模型建立或确认工作
        // 还有，monsterbox是所有角色CharacterDataInfo的由来，而这个信息现在记载了技能信息，从而可以说这个信息量现在非常大，逻辑出问题也会出现错误。
        // 19.1.3 : monsterbox应该具备能力可以非常灵活的根据检索条件对所有monster进行分类显示，优先显示等等。
        // 这个函数的生成本随着“type”选项卡的整理。
        public void MonsterIconsGenerate(bool clearButtonFeature)
        {
            selectingInstanceID = null;
            foreach (KeyValuePair<string, UnitInfo> keyValuePair in MyMonsters.Dic)
            {
                AddOneNewIcon(keyValuePair.Value.id, clearButtonFeature);
            }
            _monsterboxFilter.RefreshTypeDropDown(typeOfUnitsIhave);
        }

        //icon的排列，显示   
        public void DisplayMonsterIcons(bool clearButtonFeature)
        {
            MonsterBoxContainer.gameObject.SetActive(true);
            MonsterIconsGenerate(clearButtonFeature);
            foreach (KeyValuePair<string, HeroIcon> keyValuePair in mainMenuIcons)
            {
                keyValuePair.Value.gameObject.SetActive(false);
            }
            List<HeroIcon> nowcharIcons = _monsterboxFilter.OrderIcons(mainMenuIcons.Values.ToList());
            int hangshu = 1;
            for (int i = 0; i < nowcharIcons.Count; i++)
            {
                HeroIcon _targetingIcon = nowcharIcons[i];
                if (_targetingIcon == null)
                {
                    Debug.Log("严重错误");
                    return;
                }
                _targetingIcon.gameObject.SetActive(true);
                _targetingIcon.transform.SetParent(MonsterBoxContainer);
                _targetingIcon.transform.localScale = Vector3.one;
                _targetingIcon.transform.localPosition = Vector3.zero;
            }

            //adjustAllIconsSize(null);
            hangshu = 1 + nowcharIcons.Count / 7;
            MonsterBoxContainer.sizeDelta = new Vector2(MonsterBoxContainer.rect.width, noMagic.GetComponent<RectTransform>().rect.height * hangshu);
        }
    }
}