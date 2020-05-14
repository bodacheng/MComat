using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class MonsterBox : MonoBehaviour
    {
        public static MonsterBox target;
        
        [Space(7)]
        [Header("monsterboxFilter")]
        public MonsterboxFilter _monsterboxFilter;
        
        [Space(7)]
        [Header("角色属性框")]
        public HeroIcon noMagic;
        
        [Space(7)]
        [Header("选中框")]
        public GameObject selectedFrame;
        
        [Space(7)]
        [Header("宠物栏总RectTransform")]
        public RectTransform MonsterBoxWholeT;
        
        [Space(2)]
        [Header("宠物栏parent")]
        public RectTransform MonsterBoxContainer;

        public static List<string> typeOfMonstersIhave = new List<string>();
        public static readonly IDictionary<string, HeroIcon> mainMenuIcons = new Dictionary<string, HeroIcon>();
        
        void Start()
        {
            noMagic.gameObject.SetActive(false);
            target = this; //放在start是确保每次进入菜单场景都运行
        }

        public void AdjustAllIconsSize(string focusingLocalID)
        {
            foreach (KeyValuePair<string, HeroIcon> icon in mainMenuIcons)
            {
                icon.Value.DecideIconSize(focusingLocalID);
            }
        }

        public static HeroIcon GetCharIcon(string monsterofplayid)
        {
            if (monsterofplayid == null)
                return null;
            mainMenuIcons.TryGetValue(monsterofplayid, out HeroIcon charIcon);
            return charIcon;
        }

        // 从这个函数的名字来看，应该是个产生monsterbox内所有图标的东西。原则上这个玩意如果没有什么新宠物的添加，它是很少加载才对。
        // 难点在于每个monstericon上给予一个什么样的按钮 ，并且这个按钮到底是什么时机下给予。
        // 现在的模型循环利用机制决定：每次运行mymonsterbox，都要执行所有拥有角色的模型建立或确认工作
        // 还有，monsterbox是所有角色CharacterDataInfo的由来，而这个信息现在记载了技能信息，从而可以说这个信息量现在非常大，逻辑出问题也会出现错误。
        // 19.1.3 : monsterbox应该具备能力可以非常灵活的根据检索条件对所有monster进行分类显示，优先显示等等。
        // 这个函数的生成本随着“type”选项卡的整理。
        public static IEnumerator MonsterIconsGenerate()
        {
            foreach (KeyValuePair<string, GetMonsterOfPlayerDetailModel> keyValuePair in AccountCharsSet.AccountCharInfoDic)
            {
                yield return AddOneNewIcon(keyValuePair.Value.monsterOfPlayerId);
            }
            target._monsterboxFilter.RefreshTypeDropDown(typeOfMonstersIhave);
        }

        public static IEnumerator AddOneNewIcon(string monsterOfPlayerId)
        {
            GetMonsterOfPlayerDetailModel targetingCharInfo = AccountCharsSet.Get(monsterOfPlayerId);
            CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(targetingCharInfo.monsterId);
            HeroIcon targetingIcon = GetCharIcon(monsterOfPlayerId);
            if (targetingIcon == null)
            {
                IEnumerator onecoroutine = MonsterIconDic.Instance.LoadAndGet(targetingCharInfo.monsterId);
                yield return onecoroutine;
                targetingIcon = Instantiate(target.noMagic);
                targetingIcon.name = _CharConfig.REAL_NAME + "_icon";
                targetingIcon._MonsterOfPlayerDetailModel = targetingCharInfo;
                targetingIcon._CharConfig = _CharConfig;
                targetingIcon.ChangeIcon(MonsterIconDic.Instance.GetMonsterIconSyn(_CharConfig.RECORD_ID), _CharConfig._zokusei);
                void Select()
                {
                    HeroIcon.Seletedfeature(targetingIcon, target.selectedFrame, 150f);
                }
                targetingIcon.iconButton.onClick.AddListener(Select);
                DicAdd<string, HeroIcon>.Add(mainMenuIcons, monsterOfPlayerId, targetingIcon);
            }
            
            if (!typeOfMonstersIhave.Contains(targetingIcon._CharConfig.TYPE))
            {
                typeOfMonstersIhave.Add(targetingIcon._CharConfig.TYPE);
            }
            yield break;
        }

        public void OnTypeChangeMyMonsterBox()
        {
            PreScene.target.mainProcessRunner.Run(DisplayMonsterIcons());
        }

        //icon的排列，显示   
        public static IEnumerator DisplayMonsterIcons()
        {
            target.MonsterBoxContainer.gameObject.SetActive(true);
            yield return MonsterIconsGenerate();
            foreach (KeyValuePair<string, HeroIcon> keyValuePair in mainMenuIcons)
            {
                keyValuePair.Value.gameObject.SetActive(false);
            }
            List<HeroIcon> nowcharIcons = target._monsterboxFilter.OrderIcons(mainMenuIcons.Values.ToList());
            int hangshu = 1;
            for (int i = 0; i < nowcharIcons.Count; i++)
            {
                HeroIcon _targetingIcon = nowcharIcons[i];
                if (_targetingIcon == null)
                {
                    Debug.Log("严重错误");
                    yield break;
                }
                _targetingIcon.gameObject.SetActive(true);
                _targetingIcon.transform.SetParent(target.MonsterBoxContainer);
                _targetingIcon.transform.localScale = Vector3.one;
                _targetingIcon.transform.localPosition = Vector3.zero;
            }

            //adjustAllIconsSize(null);
            hangshu = 1 + nowcharIcons.Count / 7;
            target.MonsterBoxContainer.sizeDelta = new Vector2(target.MonsterBoxContainer.rect.width, target.noMagic.GetComponent<RectTransform>().rect.height * hangshu);
            yield break;
        }
    }
}