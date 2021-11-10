using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using mainMenu;
using dataAccess;
using System;
using DG.Tweening;

public class ArcadeTop : UILayer
{
    [SerializeField] RectTransform ButtonsContainer;
    [SerializeField] Button JumpToNewStage;
    [SerializeField] ScrollRect _ScrollRect;
    [SerializeField] Scrollbar _Scrollbar;
    [SerializeField] StageButton pretab;
    [SerializeField] HeroIcon UnitIconPrefab;
    [SerializeField] NineForShow _NineForShow;

    int StageCount;
    public static IDictionary<int, StageInfo> ArcadeStages = new Dictionary<int, StageInfo>();

    public StageButton GetStageButton(int stageno)
    {
        return ArcadeStages[stageno]?.stageButton;
    }

    public void IconButtonFeature(HeroIcon heroIcon)
    {
        // 显示模型
        SingleThreadProcesser.backup.RunAsQueued(ModelShower.target.ShowModel(heroIcon._CharConfig.RECORD_ID));
        // 显示技能组
        _NineForShow.ShowStones_DataInfo(heroIcon.unitInfo);
    }

    public void INIArcadeStageButtons()
    {
        List<UnityEngine.Object> stageResources = Resources.LoadAll("StageConfigFiles", typeof(FightInfo)).ToList();
        foreach (UnityEngine.Object _object in stageResources)
        {
            FightInfo one = (FightInfo)_object;
            one.ID = int.Parse(one.name);
            Debug.Log("arcade loaded:" + one.ID);
            one.SetEventType(FightEventType.Quest);
            if (!ArcadeStages.ContainsKey(one.ID))
            {
                one.LoadLocalFightFromScript();
                StageButton newButton = Instantiate(pretab);
                void LoadThisStage()
                {
                    ArcadeStages[one.ID].stageConfig.LoadMyTeam();
                    ArcadeStages[one.ID].stageConfig.team1ID = Account._AccInfo.playerID;
                    PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, ArcadeStages[one.ID].stageConfig, true);
                }
                newButton.button.onClick.AddListener(LoadThisStage);
                newButton.ID = one.ID;
                newButton.text.text = "Stage" + one.ID;
                newButton.name = "Stage" + one.ID;

                for (int i = 0; i < one.fightMembers.EnemySets.GetValues().Count; i++)
                {
                    MonsterIconDic.Get(one.fightMembers.EnemySets.GetValues()[i].r_id);
                }
                
                List<HeroIcon> heroIcons = MemberInfosShow(one.fightMembers.EnemySets.GetValues(), newButton.IconsT);
                for (int i = 0; i < heroIcons.Count; i++)
                {
                    HeroIcon heroIcon = heroIcons[i];
                    void temp()
                    {
                        IconButtonFeature(heroIcon);
                    }
                    heroIcon.iconButton.onClick.RemoveAllListeners();
                    heroIcon.iconButton.onClick.AddListener(temp);
                }
                newButton.MemberIcons = heroIcons;

                StageInfo stageInfo = new StageInfo
                {
                    stageConfig = one,
                    stageButton = newButton
                };
                ArcadeStages.Add(one.ID, stageInfo);
            }
            else
            {
                Debug.Log("重复的Arcade模式关卡ID：" + one.ID);
            }
        }
        StageCount = ArcadeStages.Count;
    }
    
    List<HeroIcon> MemberInfosShow(List<UnitInfo> HeroSets, RectTransform _ShowT)
    {
        foreach (Transform transform in _ShowT)
        {
            Destroy(transform.gameObject);
        }
        List<HeroIcon> icons = new List<HeroIcon>();
        foreach(UnitInfo oneMember in HeroSets)
        {
            icons.Add(HeroIcon.ArrangeHeroIconToT(UnitIconPrefab, oneMember, _ShowT));
        }
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }

    private Action EndDragExtra;

    // 分页排版
    // mode 1: 无限模式 2. 5个关卡算一个章节模式
    public void INIPagingSystem(int mode)
    {
        // 假设有100关，然后按钮应该是越往下拖关卡数越大，才能和JumpToNewest()堆起来
        for (int i = StageCount; i > -1; i--)
        {
            if (!ArcadeStages.ContainsKey(i))
            {
                continue;
            }
            ArcadeStages[i].stageButton.gameObject.SetActive(true);
            ArcadeStages[i].stageButton.gameObject.transform.SetParent(ButtonsContainer);
            ArcadeStages[i].stageButton.gameObject.transform.localScale = Vector3.one;
        }
        VerticalLayoutGroup vGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
        ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x, (pretab.button.GetComponent<RectTransform>().rect.height + vGroup.spacing) * ArcadeStages.Count);

        JumpToNewStage.onClick.RemoveAllListeners();
        Action JumpToButtonFeature = () => { };
        if (mode == 1)
        {
            JumpToButtonFeature = () =>
            {
                JumpTo(CurrentTargetScrollbarValue());
            };
            EndDragExtra = NormalAlignment;
        }
        if (mode == 2)
        {
            JumpToButtonFeature = () =>
            {
                JumpTo(PageD(Account._AccInfo.ArcadeProcess, StageCount));
            };
            EndDragExtra = FiveSetAlignment;
        }
        JumpToNewStage.onClick.AddListener(JumpToButtonFeature.Invoke);
        RefreshRender();
    }

    public void JumpTo(float target)
    {
        DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value = x, target, 0.5f).
            OnComplete((() => { EndDragExtra.Invoke(); }));
    }

    public void RefreshRender()
    {
        foreach (KeyValuePair<int, StageInfo> keyValuePair in ArcadeStages)
        {
            Image buttonImage = keyValuePair.Value.stageButton.GetComponent<Image>();
            Animator buttonAnimator = keyValuePair.Value.stageButton.GetComponent<Animator>();
            if (buttonAnimator != null)
                buttonAnimator.enabled = Account._AccInfo.ArcadeProcess + 1 == keyValuePair.Key;
            if (Account._AccInfo.ArcadeProcess + 1 >= keyValuePair.Key)
            {
                keyValuePair.Value.ChangeColorOfIcons(true);
            }
            else
            {
                keyValuePair.Value.ChangeColorOfIcons(false);
            }
        }
    }

    // 放在滑动区上
    public void DragEnd()
    {
        JumpTo(PageV(_Scrollbar.value, StageCount));
    }

    void FiveSetAlignment()
    {
        // 不可拖向太超前的关卡
        if (_Scrollbar.value > PageD(Account._AccInfo.ArcadeProcess, StageCount) + (float)0.5 / StageCount)
        {
            JumpTo(PageD(Account._AccInfo.ArcadeProcess, StageCount));
        }

        if (!JumpToNewStage.gameObject.activeSelf)
        {
            if (Mathf.Abs(PageD(Account._AccInfo.ArcadeProcess, StageCount) - _Scrollbar.value) > 0.2f)
            {
                JumpToNewStage.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Mathf.Abs(PageD(Account._AccInfo.ArcadeProcess, StageCount) - _Scrollbar.value) <= 0.2f)
            {
                JumpToNewStage.gameObject.SetActive(false);
            }
        }
    }

    // 给关卡号返回应该的scrollbar值。
    // 利用了PageV。具体逻辑虽然混乱但以levelCount = 10做推导后判断应该是无误。
    float PageD(int targetLevel, int levelCount)
    {
        // temp这个值是“每5个关卡所占据的滚动条长度”
        // 如果关卡总数是a，那么显示第a-5到第a个关卡的时候，滚动条value是1
        // 假设有10个关卡，那么滚动条的两个定位节点是0（显示第1到5关）和 1 （显示第6到10关）
        // 不管有多少关，0永远是第0至5关的目标滚动轴值
        // 如果回头时候看不懂这个函数那可以假设有10个关卡，
        // 一步步推导看看什么意思。
        float temp = 0;
        temp = levelCount - 5;
        temp = System.Math.Abs(temp) < 0.001 ? 0f : 5f / (float)temp;

        int tempInt = (int)Mathf.Floor((float)targetLevel / 5); // 整数（对应关卡超过了多少章节）
        int d = targetLevel % 5; //余数
        if (d > 0)
        {
            return PageV(tempInt * temp, levelCount);
        }
        else
        {
            return PageV((tempInt - 1) * temp, levelCount);
        }
    }

    // 滚动轴自动调整功能的辅助计算函数。画面一次显示5个关卡，
    // 那么给出当前滚动轴的值和最大关卡，得到应该调整到的滚动轴值
    // 这个函数显然不能“给出关卡号码，返回应该的滚动轴值）
    float PageV(float currentvalue, int levelCount)
    {
        int i = 0; // 相当于章节，一个章节5个小关
        float nextScrollBarPoint = -9999;

        // temp这个值是“每5个关卡所占据的滚动条长度”
        // 如果关卡总数是a，那么显示第a-5到第a个关卡的时候，滚动条value是1
        // 假设有10个关卡，那么滚动条的两个定位节点是0（显示第1到5关）和 1 （显示第6到10关）
        // 不管有多少关，0永远是第0至5关的目标滚动轴值
        // 如果回头时候看不懂这个函数那可以假设有10个关卡，
        // 一步步推导看看什么意思。

        if (levelCount < 5)
            return 0;

        float temp = 0;
        temp = levelCount - 5;
        temp = System.Math.Abs(temp) < 0.001 ? 0f : 5f / (float)temp;

        do
        {
            i++;
            nextScrollBarPoint = temp * i;
        } while (currentvalue > nextScrollBarPoint);

        float toPre = currentvalue - temp * (i - 1);
        float toNext = temp * i - currentvalue;
        return toNext >= toPre ? temp * (i - 1) : temp * i;
    }

    void NormalAlignment()
    {
        if (!JumpToNewStage.gameObject.activeSelf)
        {
            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) > 0.1f)
            {
                JumpToNewStage.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) <= 0.1f)
            {
                JumpToNewStage.gameObject.SetActive(false);
            }
        }
    }

    float CurrentTargetScrollbarValue()
    {
        float targetScrollbarValue;
        if (Account._AccInfo.ArcadeProcess <= 3)
        {
            targetScrollbarValue = 0;
        }
        else
        {
            VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
            // 重点在于对Scrollbar.value的理解。这个值是scrollview边界目前超出框的长度与可能超出框框最大长度的比值
            targetScrollbarValue =
            ((pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * (Account._AccInfo.ArcadeProcess - 3)) // 分子。如果希望对象关卡不是出现在中间，可调整这个数字。
            / (ButtonsContainer.sizeDelta.y - _ScrollRect.GetComponent<RectTransform>().rect.height); // 分母
        }
        return targetScrollbarValue;
    }
}

// 等级升序降序
//readonly int order = 0;//0:升序 1:降序 //是否按type排序
//List<StageButton> OrderStagesButtonByNo(List<StageButton> originBoxes)
//{
//    for (int i = 0; i < originBoxes.Count - 1; i++)
//    {
//        for (int j = 0; j < originBoxes.Count - 1 - i; j++)
//        {
//            int no1 = originBoxes[j].ID;
//            int no2 = originBoxes[j + 1].ID;
//            if (order == 1 ? no1 > no2 : no1 < no2)
//            {
//                StageButton temp = originBoxes[j];
//                originBoxes[j] = originBoxes[j + 1];
//                originBoxes[j + 1] = temp;
//            }
//        }
//    }
//    return originBoxes;
//}