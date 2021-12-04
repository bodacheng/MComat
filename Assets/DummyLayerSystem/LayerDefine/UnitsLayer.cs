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
        
        public HeroIcon GetUnitIcon(string instanceID)
        {
            if (instanceID == null)
                return null;
            mainMenuIcons.TryGetValue(instanceID, out HeroIcon unitIcon);
            return unitIcon;
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
            HeroIcon.SelectedFeature(null, selectedFrame, 150f);
        }

        public void Select(string instanceID)
        {
            HeroIcon targetingIcon = GetUnitIcon(instanceID);
            HeroIcon.SelectedFeature(targetingIcon, selectedFrame, 150f);
            selectingInstanceID = instanceID;
        }

        public string GetSelect()
        {
            return selectingInstanceID;
        }

        void AddOneNewIcon(string instanceID, bool clearButtonFeature)
        {
            UnitInfo unitInfo = MyMonsters.Get(instanceID);
            CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(unitInfo.r_id);
            if (_CharConfig == null)
            {
                Debug.Log("MonsterID:"+ unitInfo.r_id + " doesnt exist in this version");
                return;
            }

            HeroIcon targetingIcon = GetUnitIcon(instanceID);
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
            DisplayUnitIcons(false);
        }
        
        void UnitIconsGenerate(bool clearButtonFeature)
        {
            selectingInstanceID = null;
            foreach (KeyValuePair<string, UnitInfo> keyValuePair in MyMonsters.Dic)
            {
                AddOneNewIcon(keyValuePair.Value.id, clearButtonFeature);
            }
            _monsterboxFilter.RefreshTypeDropDown(typeOfUnitsIhave);
        }
        
        //icon的排列，显示   
        public void DisplayUnitIcons(bool clearButtonFeature)
        {
            MonsterBoxContainer.gameObject.SetActive(true);
            UnitIconsGenerate(clearButtonFeature);
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