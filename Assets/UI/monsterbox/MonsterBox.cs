using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Linq;
using EZObjectPools;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MonsterBox : MonoBehaviour {
    [Space(7)]
    [Header("preparingScene")]
    public preparingScene _preparingScene;

    [Space(7)]
    [Header("monsterboxFilter")]
    public monsterboxFilter _monsterboxFilter;

    [Space(7)]
    [Header("角色属性框")]
    public charIcon noMagic;

    //底下这些我是真看不顺眼
    [Space(7)]
    [Header("宠物栏总RectTransform")]
    public RectTransform MonsterBoxWholeT;
    [Space(2)]
    [Header("宠物栏parent")]
    public RectTransform MonsterBoxContainer;

    private List<charIcon> nowcharIcons = new List<charIcon>();
    private IDictionary<int, charIcon> mainMenuIcons = new Dictionary<int, charIcon>();

    private charIcon targetingIcon;
    private CharacterResourceInfo targetingCharacterResourceInfo;
    private CharacterDataInfo targetingCharacterDataInfo;
    
    void Start () {
        noMagic.gameObject.SetActive(false);
	}

    public void adjustAllIconsSize(int focusingLocalID)
    {
        foreach (KeyValuePair<int, charIcon> icon in mainMenuIcons)
        {
            icon.Value.decideIconSize(focusingLocalID);
        }
    }

    // 从这个函数的名字来看，应该是个产生monsterbox内所有图标的东西。原则上这个玩意如果没有什么新宠物的添加，它是很少加载才对。
    // 难点在于每个monstericon上给予一个什么样的按钮 ，并且这个按钮到底是什么时机下给予。
    // 现在的模型循环利用机制决定：每次运行mymonsterbox，都要执行所有拥有角色的模型建立或确认工作
    // 还有，monsterbox是所有角色CharacterDataInfo的由来，而这个信息现在记载了技能信息，从而可以说这个信息量现在非常大，逻辑出问题也会出现错误。
    // 19.1.3 : monsterbox应该具备能力可以非常灵活的根据检索条件对所有monster进行分类显示，优先显示等等。
    // 这个函数的生成本随着“type”选项卡的整理。
    public IEnumerator monsterIconsGenerate()//icon的生成
    {
        List<CharacterDataInfo> myOwnedChars = AccountCharsSet.ownedChars.ToList();
        foreach (CharacterDataInfo _CharacterDataInfo in myOwnedChars)
        {
            yield return addOneNewIcon(_CharacterDataInfo.localID);
        }
        nowcharIcons.Clear();
        _monsterboxFilter.typeDropDown.ClearOptions();
        List<string> typeList = new List<string>();
        foreach (KeyValuePair<int, charIcon> keyValuePair in mainMenuIcons)
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

    public IEnumerator addOneNewIcon(int localID)
    {
        mainMenuIcons.TryGetValue(localID, out targetingIcon);
        
        targetingCharacterDataInfo = AccountCharsSet.getTheCharacterOfMine(localID);
        targetingCharacterResourceInfo = CharsManager.getCharacterResourceInfo(targetingCharacterDataInfo.resource_num);
        
        if (targetingIcon != null)
        {
            targetingIcon.localId = localID;
            targetingIcon.name = targetingCharacterResourceInfo.prefabName + "_icon";
            targetingIcon._CharacterDataInfo = targetingCharacterDataInfo;
            //targetingIcon._CharacterResourceInfo = _CharacterResourceInfo;
            targetingIcon.gameObject.SetActive(false);
            targetingIcon.transform.SetParent(MonsterBoxWholeT);
            yield return targetingIcon;
            yield break;
        }

        IEnumerator onecoroutine = null;
        switch (defaultPools.Instance.IconLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                onecoroutine = (monsterIconsDic.Instance.findMonsterIconByCach(targetingCharacterDataInfo.resource_num));
                break;
            case ResourceLoadMode.Resource:
                onecoroutine = (monsterIconsDic.Instance.findMonsterIconByResource(targetingCharacterDataInfo.resource_num));
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
        }
        yield return (onecoroutine);
        targetingIcon = Instantiate(noMagic);
        targetingIcon.localId = localID;
        targetingIcon.name = targetingCharacterResourceInfo.prefabName + "_icon";
        targetingIcon._CharacterDataInfo = targetingCharacterDataInfo;
        targetingIcon._CharacterResourceInfo = targetingCharacterResourceInfo;
        targetingIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(targetingCharacterResourceInfo.charResouceNum),targetingCharacterResourceInfo._zokusei);
        targetingIcon.gameObject.SetActive(false);
        targetingIcon.transform.SetParent(MonsterBoxWholeT);
        
        if (mainMenuIcons.ContainsKey(localID))
            mainMenuIcons[localID] = targetingIcon;
        else
            mainMenuIcons.Add(localID, targetingIcon);

        yield return targetingIcon;
    }
    
    public void myMonsterBoxLoad()//!!!
    {
        //_preparingScene.triggerMainProcess(myMonsterBox(_preparingScene.step));
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
            targetingIcon = nowcharIcons[i];
            if (targetingIcon == null)
                Debug.Log("严重错误");

            int localid = targetingIcon._CharacterDataInfo.localID;
            
            targetingIcon.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction action1 = () => {
                _preparingScene.triggerMainProcess(_preparingScene.monsterIconButton(localid));
            };
            targetingIcon.iconButton.onClick.AddListener(action1);

            //if (selectedIDs.Contains(_CharacterDataInfo.localID))
            //{
            //    GameObject selectedFire = Instantiate(selectedMonsterInBoxPrefab);//其实不应该是这样，应该是个额外产生个提示符
            //    selectedFire.transform.SetParent(newbutton.transform);
            //    selectedFire.transform.localPosition = Vector3.zero;
            //    selectedFire.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            //    selectedFire.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //    selectedFire.SetActive(true);
            //}
 
            targetingIcon.transform.SetParent(this.MonsterBoxContainer);
            targetingIcon.gameObject.SetActive(true);
            iconCount++;
        }

        adjustAllIconsSize(-1);
        hangshu = 1 + iconCount / 7;
        MonsterBoxContainer.sizeDelta = new Vector2(MonsterBoxContainer.rect.width, noMagic.GetComponent<RectTransform>().rect.height * hangshu);
        yield break;
    }
}
