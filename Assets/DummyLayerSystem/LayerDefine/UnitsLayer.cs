using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using dataAccess;

namespace mainMenu
{
    public class UnitsLayer : UILayer
    {
        [Header("filter")]
        [SerializeField] UnitFilter filter;
        
        [Header("角色属性框")]
        [SerializeField] HeroIcon noMagic;
        
        [Header("选中框")]
        [SerializeField] GameObject selectedFrame;
        
        [Header("宠物栏parent")]
        [SerializeField] RectTransform unitBoxContainer;
        
        readonly List<string> _typeOfUnitsIHave = new ();
        
        readonly IDictionary<string, HeroIcon> heroIcons = new Dictionary<string, HeroIcon>();
        string selected_InstanceID;

        private Action<string> onClick;
        public Action<string> OnClick => onClick;
        
        HeroIcon GetUnitIcon(string instanceID)
        {
            if (instanceID == null)
                return null;
            heroIcons.TryGetValue(instanceID, out var unitIcon);
            return unitIcon;
        }
        
        public void SetUnitsIconOnClick(Action<string> onClick)
        {
            this.onClick = onClick;
            foreach (var kv in heroIcons)
            {
                kv.Value.iconButton.onClick.RemoveAllListeners();
                kv.Value.iconButton.onClick.AddListener(()=> { onClick.Invoke(kv.Key); });
            }
        }
        
        public void CancelSelect()
        {
            selected_InstanceID = null;
            HeroIcon.SelectedFeature(null, selectedFrame, 1f);
        }

        public void Select(string instanceID)
        {
            var targetingIcon = GetUnitIcon(instanceID);
            HeroIcon.SelectedFeature(targetingIcon, selectedFrame, 1f);
            selected_InstanceID = instanceID;
        }

        public string GetSelect()
        {
            return selected_InstanceID;
        }

        void AddUnitIcon(string instanceID, bool clearBtnFeature)
        {
            var unitInfo = dataAccess.Units.Get(instanceID);
            var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
            if (unitConfig == null)
            {
                Debug.Log("unit ID:"+ unitInfo.r_id + " doesnt exist in this version");
                return;
            }
            
            var targetingIcon = GetUnitIcon(instanceID);
            if (targetingIcon == null)
            {
                targetingIcon = Instantiate(noMagic);
                targetingIcon.name = unitConfig.REAL_NAME + "_icon";
                targetingIcon.ChangeIcon(unitInfo);
                DicAdd<string, HeroIcon>.Add(heroIcons, instanceID, targetingIcon);
            }
            if (clearBtnFeature)
                targetingIcon.iconButton.onClick.RemoveAllListeners();
            if (!_typeOfUnitsIHave.Contains(unitConfig.TYPE))
            {
                _typeOfUnitsIHave.Add(unitConfig.TYPE);
            }
        }
        
        public void OnTypeChangeMyMonsterBox()
        {
            DisplayUnitIcons(dataAccess.Units.Dic, false);
        }
        
        void UnitIconsGenerate(IDictionary<string, UnitInfo> dic, bool clearButtonFeature)
        {
            selected_InstanceID = null;
            foreach (var keyValuePair in dic)
            {
                AddUnitIcon(keyValuePair.Value.id, clearButtonFeature);
            }
            filter.RefreshTypeDropDown(_typeOfUnitsIHave);
        }

        public void DisableLackSkillUnitIcon()
        {
            foreach (var kv in heroIcons)
            {
                if (kv.Value.unitInfo != null && Stones.GetEquippingStones(kv.Key).Count == 9)
                {
                    kv.Value.LightOn();
                }
                else
                {
                    kv.Value.Grey();
                }
            }
        }
        
        Action displayUnitIconsAfterAction;

        public void SetDisplayUnitIconsAfterAction(Action a)
        {
            this.displayUnitIconsAfterAction = a;
        }
        
        //icon的排列，显示   
        public void DisplayUnitIcons(IDictionary<string, UnitInfo> dic, bool clearBtnFeature)
        {
            unitBoxContainer.gameObject.SetActive(true);
            UnitIconsGenerate(dic, clearBtnFeature);
            foreach (var keyValuePair in heroIcons)
            {
                keyValuePair.Value.gameObject.SetActive(false);
            }
            var icons = filter.OrderIcons(heroIcons.Values.ToList());
            var row = 1;
            for (var i = 0; i < icons.Count; i++)
            {
                var _targetingIcon = icons[i];
                if (_targetingIcon == null)
                {
                    Debug.Log("严重错误");
                    return;
                }
                _targetingIcon.gameObject.SetActive(true);
                _targetingIcon.transform.SetParent(unitBoxContainer);
                _targetingIcon.transform.localScale = Vector3.one;
                _targetingIcon.transform.localPosition = Vector3.zero;
            }
            
            row = 1 + icons.Count / 7;
            unitBoxContainer.sizeDelta = new Vector2(unitBoxContainer.rect.width, noMagic.GetComponent<RectTransform>().rect.height * row);
            displayUnitIconsAfterAction?.Invoke();
        }

        public void ForceClickUnit(string unitRId)
        {
            foreach (var iconKV in heroIcons)
            {
                iconKV.Value.iconButton.interactable = unitRId == iconKV.Value.unitConfig.RECORD_ID;
            }
        }
    }
}