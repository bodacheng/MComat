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
        [Space(7)]
        [Header("monsterboxFilter")]
        public monsterboxFilter _monsterboxFilter;
        static monsterboxFilter monsterboxFilter;

        [Space(7)]
        [Header("角色属性框")]
        public charIcon noMagic;
        static charIcon NoMagic;

        [Space(7)]
        [Header("选中框")]
        public GameObject selectedFrame;
        static GameObject _selectedFrame;

        [Space(7)]
        [Header("宠物栏总RectTransform")]
        public RectTransform MonsterBoxWholeT;
        static RectTransform _MonsterBoxWholeT;
        
        [Space(2)]
        [Header("宠物栏parent")]
        public RectTransform MonsterBoxContainer;
        static RectTransform _MonsterBoxContainer;

        static IDictionary<string, charIcon> mainMenuIcons = new Dictionary<string, charIcon>();
        static List<string> typeList = new List<string>();

        void Awake()
        {
            _selectedFrame = selectedFrame;
            monsterboxFilter = _monsterboxFilter;
            _MonsterBoxWholeT = MonsterBoxWholeT;
            NoMagic = noMagic;
            _MonsterBoxContainer = MonsterBoxContainer;
        }

        void Start()
        {
            NoMagic.gameObject.SetActive(false);
        }

        public void AdjustAllIconsSize(string focusingLocalID)
        {
            foreach (KeyValuePair<string, charIcon> icon in mainMenuIcons)
            {
                icon.Value.decideIconSize(focusingLocalID);
            }
        }
        
        public static charIcon GetCharIcon(string monsterofplayid)
        {
            if (monsterofplayid == null)
                return null;
            mainMenuIcons.TryGetValue(monsterofplayid, out charIcon charIcon);
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
            foreach (KeyValuePair<string, MonsterOfPlayerListModel> keyValuePair in AccountCharsSet.AccountCharacterInfoListObjectsDictionary)
            {
                if (keyValuePair.Value.monsterOfPlayerId != null)
                    yield return AddOneNewIcon(keyValuePair.Value.monsterOfPlayerId);
                else
                {
                    Debug.Log("角色列表中存在奇怪数据。monsterid" + keyValuePair.Value.monsterId + ", monsterOfPlayerId为null");
                }
            }
        }

        public static IEnumerator AddOneNewIcon(string monsterOfPlayerId)
        {
            charIcon targetingIcon = GetCharIcon(monsterOfPlayerId);
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo(monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel targetingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (targetingCharacterDataInfo == null)
            {
                Debug.Log("读取角色信息严重错误monsterOfPlayerId:" + monsterOfPlayerId);
                yield break;
            }
            CharacterResourceInfo targetingCharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(targetingCharacterDataInfo.monsterId);
            if (targetingCharacterResourceInfo == null)
            {
                Debug.Log("严重错误，无法找到对应角色信息。monsterid:" + targetingCharacterDataInfo.monsterId);
                yield break;
            }
            if (targetingIcon == null)
            {
                IEnumerator onecoroutine = null;
                switch (ResourceLoadingSetting.Instance.IconLoadingMode)
                {
                    case ResourceLoadMode.CachAB:
                        onecoroutine = (monsterIconsDic.Instance.findMonsterIconByCach(targetingCharacterDataInfo.monsterId));
                        break;
                    case ResourceLoadMode.Resource:
                        onecoroutine = (monsterIconsDic.Instance.findMonsterIconByResource(targetingCharacterDataInfo.monsterId));
                        break;
                    case ResourceLoadMode.StreamingAssetAB:
                        break;
                }
                yield return onecoroutine;
                targetingIcon = Instantiate(NoMagic);
                targetingIcon.name = targetingCharacterResourceInfo.REAL_NAME + "_icon";
                targetingIcon.AccountCharacterInfo = targetingCharacterDataInfo;
                targetingIcon._CharacterResourceInfo = targetingCharacterResourceInfo;
                targetingIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(targetingCharacterResourceInfo.RECORD_ID), targetingCharacterResourceInfo._zokusei);
                if (mainMenuIcons.ContainsKey(monsterOfPlayerId))
                {
                    mainMenuIcons[monsterOfPlayerId] = targetingIcon;
                    Debug.Log("重复出现的角色id？"+monsterOfPlayerId);
                }
                else
                {
                    mainMenuIcons.Add(monsterOfPlayerId, targetingIcon);
                }
            }
            
            // 下面的环节重新加载type下拉表
            foreach (KeyValuePair<string, charIcon> keyValuePair in mainMenuIcons)
            {
                if (!typeList.Contains(keyValuePair.Value._CharacterResourceInfo.type))
                {
                    typeList.Add(keyValuePair.Value._CharacterResourceInfo.type);
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData
                    {
                        text = keyValuePair.Value._CharacterResourceInfo.type
                    };
                    monsterboxFilter.typeDropDown.options.Add(m_NewData);
                }
            }
            yield break;
        }

        public void OnTypeChangeMyMonsterBox()
        {
            preparingScene.Instance.mainProcessRunner.triggerMainProcess(DisplayMonsterIcons());
        }

        //icon的排列，显示   
        public static IEnumerator DisplayMonsterIcons()
        {
            Debug.Log("here");
            _MonsterBoxContainer.gameObject.SetActive(true);
            List<charIcon> nowcharIcons = monsterboxFilter.OrderIcons(mainMenuIcons.Values.ToList());
            int hangshu = 1;
            for (int i = 0; i < nowcharIcons.Count; i++)
            {
                charIcon _targetingIcon = nowcharIcons[i];
                if (_targetingIcon == null)
                {
                    Debug.Log("严重错误");
                    yield break;
                }
                string monsterOfPlayerId = _targetingIcon.AccountCharacterInfo.monsterOfPlayerId;
                _targetingIcon.iconButton.onClick.RemoveAllListeners();
                void action1()
                {
                    charIcon.Seletedfeature(_targetingIcon, _selectedFrame);
                    preparingScene.Instance.mainProcessRunner.triggerMainProcess(preparingScene.Instance.MonsterIconButton(monsterOfPlayerId));
                }
                _targetingIcon.iconButton.onClick.AddListener(action1);
                Debug.Log(_targetingIcon.AccountCharacterInfo.monsterId);
                _targetingIcon.gameObject.SetActive(true);
                _targetingIcon.transform.SetParent(_MonsterBoxContainer);
                _targetingIcon.transform.localScale = Vector3.one;
                _targetingIcon.transform.localPosition = Vector3.zero;
            }

            //adjustAllIconsSize(null);
            hangshu = 1 + nowcharIcons.Count / 7;
            _MonsterBoxContainer.sizeDelta = new Vector2(_MonsterBoxContainer.rect.width, NoMagic.GetComponent<RectTransform>().rect.height * hangshu);
            yield break;
        }
    }
}