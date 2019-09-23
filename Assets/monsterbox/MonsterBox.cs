using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class MonsterBox : MonoBehaviour
    {
        [Space(7)]
        [Header("preparingScene")]
        public preparingScene _preparingScene;

        [Space(7)]
        [Header("monsterboxFilter")]
        public monsterboxFilter _monsterboxFilter;

        [Space(7)]
        [Header("角色属性框")]
        public charIcon noMagic;

        [Space(7)]
        [Header("选中框")]
        public GameObject selectedFrame;

        //底下这些我是真看不顺眼
        [Space(7)]
        [Header("宠物栏总RectTransform")]
        public RectTransform MonsterBoxWholeT;
        [Space(2)]
        [Header("宠物栏parent")]
        public RectTransform MonsterBoxContainer;

        public static GameObject _selectedFrame;

        private List<charIcon> nowcharIcons = new List<charIcon>();
        private IDictionary<string, charIcon> mainMenuIcons = new Dictionary<string, charIcon>();

        private charIcon targetingIcon;
        private CharacterResourceInfo targetingCharacterResourceInfo;
        private GetMonsterOfPlayerDetailModel targetingCharacterDataInfo;
        private List<string> typeList = new List<string>();

        void Awake()
        {
            _selectedFrame = selectedFrame;
        }

        void Start()
        {
            noMagic.gameObject.SetActive(false);
        }

        public void adjustAllIconsSize(string focusingLocalID)
        {
            foreach (KeyValuePair<string, charIcon> icon in mainMenuIcons)
            {
                icon.Value.decideIconSize(focusingLocalID);
            }
        }
        
        public charIcon getCharIcon(string monsterofplayid)
        {
            if (monsterofplayid == null)
                return null;
            charIcon charIcon;
            mainMenuIcons.TryGetValue(monsterofplayid,out charIcon);
            return charIcon;
        }

        // 从这个函数的名字来看，应该是个产生monsterbox内所有图标的东西。原则上这个玩意如果没有什么新宠物的添加，它是很少加载才对。
        // 难点在于每个monstericon上给予一个什么样的按钮 ，并且这个按钮到底是什么时机下给予。
        // 现在的模型循环利用机制决定：每次运行mymonsterbox，都要执行所有拥有角色的模型建立或确认工作
        // 还有，monsterbox是所有角色CharacterDataInfo的由来，而这个信息现在记载了技能信息，从而可以说这个信息量现在非常大，逻辑出问题也会出现错误。
        // 19.1.3 : monsterbox应该具备能力可以非常灵活的根据检索条件对所有monster进行分类显示，优先显示等等。
        // 这个函数的生成本随着“type”选项卡的整理。
        public IEnumerator monsterIconsGenerate()//icon的生成
        {
            foreach (KeyValuePair<string, MonsterOfPlayerListModel> keyValuePair in AccountCharsSet.accountCharacterInfoListObjectsDictionary)
            {
                //Debug.Log("下面以以下值作为monsterOfPlayerId来进行角色详细信息的寻找与图标构成等："+keyValuePair.Value.monsterOfPlayerId);
                if (keyValuePair.Value.monsterOfPlayerId != null)
                    yield return addOneNewIcon(keyValuePair.Value.monsterOfPlayerId);
                else
                    Debug.Log("角色列表中存在奇怪数据。monsterid" + keyValuePair.Value.monsterId + ",monsterOfPlayerId为null");
            }
            nowcharIcons.Clear();
            //_monsterboxFilter.typeDropDown.ClearOptions();
            foreach (KeyValuePair<string, charIcon> keyValuePair in mainMenuIcons)
            {
                nowcharIcons.Add(keyValuePair.Value);
                if (!typeList.Contains(keyValuePair.Value._CharacterResourceInfo.type))
                {
                    typeList.Add(keyValuePair.Value._CharacterResourceInfo.type);
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData();
                    m_NewData.text = keyValuePair.Value._CharacterResourceInfo.type;
                    _monsterboxFilter.typeDropDown.options.Add(m_NewData);
                }
            }
        }

        public IEnumerator addOneNewIcon(string monsterOfPlayerId)
        {
            targetingIcon = getCharIcon(monsterOfPlayerId);
            IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(monsterOfPlayerId);
            yield return getchar;
            targetingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (targetingCharacterDataInfo == null)
            {
                Debug.Log("读取角色信息严重错误monsterOfPlayerId:" + monsterOfPlayerId);
                yield break;
            }
            targetingCharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(targetingCharacterDataInfo.monsterId);
            if (targetingCharacterResourceInfo == null)
            {
                Debug.Log("严重错误，无法找到对应角色信息。monsterid:" + targetingCharacterDataInfo.monsterId);
                yield break;
            }

            if (targetingIcon != null)
            {
                targetingIcon.name = targetingCharacterResourceInfo.REAL_NAME + "_icon";
                targetingIcon.AccountCharacterInfo = targetingCharacterDataInfo;
                targetingIcon._CharacterResourceInfo = targetingCharacterResourceInfo;
                targetingIcon.gameObject.SetActive(false);
                yield return targetingIcon;
                yield break;
            }

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
            yield return (onecoroutine);
            targetingIcon = Instantiate(noMagic);
            targetingIcon.AccountCharacterInfo = targetingCharacterDataInfo;
            targetingIcon.name = targetingCharacterResourceInfo.REAL_NAME + "_icon";
            targetingIcon._CharacterResourceInfo = targetingCharacterResourceInfo;
            targetingIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(targetingCharacterResourceInfo.RECORD_ID), targetingCharacterResourceInfo._zokusei);
            targetingIcon.gameObject.SetActive(false);
            targetingIcon.transform.SetParent(MonsterBoxWholeT);

            if (mainMenuIcons.ContainsKey(monsterOfPlayerId))
                mainMenuIcons[monsterOfPlayerId] = targetingIcon;
            else
                mainMenuIcons.Add(monsterOfPlayerId, targetingIcon);
            yield return targetingIcon;
            yield break;
        }
        
        public void onTypeChangeMyMonsterBox()
        {
            this._preparingScene.mainProcessRunner.triggerMainProcess(myMonsterBox());
        }

        //icon的排列，显示   
        public IEnumerator myMonsterBox()
        {
            this.MonsterBoxContainer.gameObject.SetActive(true);
            yield return monsterIconsGenerate();
            nowcharIcons = _monsterboxFilter.orderIcons(nowcharIcons);
            int iconCount = 0;
            int hangshu = 1;
            for (int i = 0; i < nowcharIcons.Count; i++)
            {
                charIcon _targetingIcon = nowcharIcons[i];
                if (_targetingIcon == null)
                    Debug.Log("严重错误");
                string monsterOfPlayerId = _targetingIcon.AccountCharacterInfo.monsterOfPlayerId;
                _targetingIcon.iconButton.onClick.RemoveAllListeners();
                UnityEngine.Events.UnityAction action1 = () =>
                {
                    charIcon.Seletedfeature(_targetingIcon, _selectedFrame);
                    _preparingScene.mainProcessRunner.triggerMainProcess(_preparingScene.monsterIconButton(monsterOfPlayerId));
                };
                _targetingIcon.iconButton.onClick.AddListener(action1);
                _targetingIcon.transform.localScale = Vector3.one;
                _targetingIcon.transform.SetParent(this.MonsterBoxContainer);
                _targetingIcon.gameObject.SetActive(true);
                iconCount++;
            }

            //adjustAllIconsSize(null);
            hangshu = 1 + iconCount / 7;
            MonsterBoxContainer.sizeDelta = new Vector2(MonsterBoxContainer.rect.width, noMagic.GetComponent<RectTransform>().rect.height * hangshu);
            yield break;
        }
    }
}