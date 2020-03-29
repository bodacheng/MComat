using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;
using Skill;

// 11.13号思考这样几个问题：
// 1.玩家的等级与CellLimit之间的制约关系怎么实现
// 2.从数据库阅读拥有技能石的函数在哪
// 3.当石头的数量超过了格子数量时候所进行的validation在哪。
// 4.有财产类的安全隐患吗。

// 18.1.6
// 这个模块缺乏这些函数：添加新技能石头(与技能石头盒子的画面配合？)
// 消耗某技能石头

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        public static SkillStonesBox Instance;
        
        [Space(5)]
        [Header("进程器")]
        public SingleThreadProcesser mainProcessRunner;

        [Header("画面主模块parent")]
        public RectTransform SkillBoxCanvas;
        public RectTransform BoxWholeT, BoxT, stonesTempContainer;

        [Space(7)]
        [Header("type按钮")]
        public Dropdown types;
        public Button NormalTab;
        public Button EX1Tab;
        public Button EX2Tab;
        public Button EX3Tab;

        [Space(7)]
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _SkillStoneBoxTabEffectsManager;

        [Space(7)]
        [Header("技能石头删除区域")]
        public DragAndDropCell DeleteArea;

        [Space(7)]
        [Header("攻击范围限定")]
        public Toggle closeCheckBox;
        public Toggle nearCheckBox;
        public Toggle farCheckBox;
        public Toggle outRangeCheckBox;

        [Space(7)]
        [Header("技能石详细")]
        public SkillStoneDetail _skillStoneDetail;
       
        [Header("fxcamera")]
        public Camera fxCamera;

        IDictionary<int, DragAndDropCell> CellsDictionary = new Dictionary<int, DragAndDropCell>();//Cell这个东西我每次进入场景重新生成一次就可以。
        string focusingtype = "human";
        int focusingExType;
        SkillStoneSlot DeleteSkillStoneSlot;

        void Awake()
        {
            Instance = this;
        }
        
        public IEnumerator StartUp(int stoneboxsize)
        {
            yield return _SkillStoneBoxTabEffectsManager.StartUp();
            DeleteArea.cellPhase = DragAndDropCell.CellPhase.DeleteArea;
            DeleteSkillStoneSlot = new SkillStoneSlot(-1, null, DeleteArea);
            Debug.Log("技能石盒子容量为"+stoneboxsize);
            GenerateCells(stoneboxsize);
        }
        
        public string GetFocusingType()
        {
            return focusingtype;
        }
        public void SetFocusingType(string type)
        {
            focusingtype = type;
        }
        public int GetFocusingExType()
        {
            return focusingExType;
        }

        public void NormalTabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 0;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void EX1TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 1;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void EX2TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 2;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void EX3TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 3;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }
        
        // 功能系。刷新技能石陈列界面。这里应该包括一个特殊功能，就是展示Tutorial模式下临时可用的那些石头
        public IEnumerator EXTabsFeatureRefresh(bool viewingMode)
        {
            if (viewingMode)
            {
                types.gameObject.SetActive(true);
                types.ClearOptions();
                foreach (string Rname in MonstersConfigTable.Instance.GetTypeList())
                {
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData
                    {
                        text = Rname
                    };
                    types.options.Add(m_NewData);
                }
            }
            else
            {
                types.gameObject.SetActive(false);
            }
            closeCheckBox.onValueChanged.RemoveAllListeners();
            closeCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            nearCheckBox.onValueChanged.RemoveAllListeners();
            nearCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            farCheckBox.onValueChanged.RemoveAllListeners();
            farCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            yield break;
        }

        void RangeCheckBoxOnValueChanged()
        {
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void TypeDropDownBehaviour()// 直接放在type下拉按钮上的功能
        {
            string targetType = types.options[types.value].text.Clone() as string;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(EXTabsFeatureRefresh(true));
        }

        public IEnumerator ArrangeSkillStonesToBox()
        {
            yield return ArrangeSkillStonesToBox(GetFocusingType(), GetFocusingExType(), closeCheckBox.isOn, nearCheckBox.isOn, farCheckBox.isOn, outRangeCheckBox.isOn, TheNineSlot.Instance.GetUsingStonesId());
        }
        
        // stoneviewScrollRect 应该在这个函数里扮演一个作用。
        public IEnumerator ArrangeSkillStonesToBox(string type, int exType, bool close, bool near, bool far, bool outrange, List<string> UsingStoneIDs)
        {
            foreach (KeyValuePair<int, DragAndDropCell> cellPair in CellsDictionary)
            {
                // 下面第一行（UpdateMyItem）至关重要。技能石box往往和九宫格一起显示，readANineAndTwo函数如果和arrangeSkillStonesToBox配合运行，
                // 都是前者在前，决定好在九宫格里显示的角色装备中石头是啥，先放在那里。这个时间点上技能石背包里的格子还没有断开和那几个石头的连接。如果你不UpdateMyItem一下，
                // 它会把已经放到九宫格里的石头给拔下来扔进stonesTempContainer。
                cellPair.Value.UpdateMyItem();
                DragAndDropItem dragAndDropItem = cellPair.Value.GetItem();
                if (dragAndDropItem != null)
                {
                    dragAndDropItem.transform.SetParent(stonesTempContainer);
                }
                cellPair.Value.UpdateMyItem(); // 被拔下石头的格子需要把使用中角色头像关闭。单纯的通过null化物体的parent不会让Cell组件所记录的“放置中item”撤销
            }
            List<String> SkillStonesOfTypeAndExType = MySkillStonesReader.TargetStonesFromOfAccount(type, exType, close, near, far, outrange);
            if (SkillStonesOfTypeAndExType.Count > AccountSet.Instance._PlayerAccountInfo.Stoneboxsize)
            {
                Debug.Log("错误：待显示技能石数量超过了盒子容量");
                yield break;
            }
            Debug.Log("本次显示技能石的总数量（包括九宫格内）：" + SkillStonesOfTypeAndExType.Count);
            
            int cellindex = 0;
            for (int i = 0; i < SkillStonesOfTypeAndExType.Count; i++)
            {
                if (UsingStoneIDs != null)
                {
                    if (!UsingStoneIDs.Contains(SkillStonesOfTypeAndExType[i]))
                    {
                        CellsDictionary.TryGetValue(cellindex, out DragAndDropCell _SkillStoneCell);
                        _SkillStoneCell.AddItem(MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]]);
                        _SkillStoneCell.image.color = !AccountCharsSet.CheckIfContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(SkillStonesOfTypeAndExType[i]).inUsingMonsterOfPlayerId) ? Color.white : Color.yellow;
                        cellindex++;
                    }
                    else
                    {
                        CellsDictionary.TryGetValue(cellindex, out DragAndDropCell _SkillStoneCell);
                        _SkillStoneCell.UpdateMyItem();
                        Debug.Log("有使用中的技能石头，直接跳过这一格");
                    }
                }
                else
                {
                    MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]].GetComponent<Image>().color = Color.white;
                    CellsDictionary.TryGetValue(cellindex, out DragAndDropCell _SkillStoneCell);
                    _SkillStoneCell.AddItem(MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]]); //！！！！！这个环节会销毁被覆盖的石头。
                    _SkillStoneCell.image.color = !AccountCharsSet.CheckIfContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(SkillStonesOfTypeAndExType[i]).inUsingMonsterOfPlayerId) ? Color.white : Color.yellow;
                    cellindex++;
                }
            }
            yield break;
        }
        
        public IEnumerator GenerateOneStone(SkillStoneOfPlayerInfoModel one)
        {
            SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(one.skillId);
            if (_SkillConfig == null)
            {
                Debug.Log("巨大问题,技能id似乎未定义："+one.skillId);
                yield break;
            }
            if (MySkillStonesReader.mySkillStonesDataDic.ContainsKey(one.skillStoneOfPlayerId))
            {
                MySkillStonesReader.mySkillStonesDataDic[one.skillStoneOfPlayerId] = one;
            }else{
                MySkillStonesReader.mySkillStonesDataDic.Add(one.skillStoneOfPlayerId, one);
            }
            yield return GenerateOneStoneModel(one.skillStoneOfPlayerId);
        }
        
        public IEnumerator GenerateOneStoneModel(string skillStoneOfPlayerId)
        {
            if (MySkillStonesReader.mySkillStonesObjectsDic.ContainsKey(skillStoneOfPlayerId))
            {
                if (MySkillStonesReader.mySkillStonesObjectsDic[skillStoneOfPlayerId] != null)
                {
                    yield break;
                }
            }
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(skillStoneOfPlayerId);
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillStoneOfPlayerInfoModel.skillId);
            IEnumerator process = null;
            switch (ResourceLoadingSetting.IconLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    process = (SkillIconsDic.Instance.FindSkillIconByCach(MySkillStonesReader.mySkillStonesDataDic[skillStoneOfPlayerId].skillId));
                    break;
                case ResourceLoadMode.Resource:
                    process = (SkillIconsDic.Instance.FindSkillIconByResource(MySkillStonesReader.mySkillStonesDataDic[skillStoneOfPlayerId].skillId));
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    break;
            }
            yield return (process);
            GameObject Icon = (GameObject)process.Current;
            if (Icon == null)
                Icon = Instantiate(SkillIconsDic.Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL));
            DragAndDropItem item = Icon.GetComponent<DragAndDropItem>();
            if (item == null)
            {
                item = Icon.AddComponent<DragAndDropItem>();
            }

            if (!MySkillStonesReader.mySkillStonesObjectsDic.ContainsKey(skillStoneOfPlayerId))
                MySkillStonesReader.mySkillStonesObjectsDic.Add(skillStoneOfPlayerId, item);
            else
                 MySkillStonesReader.mySkillStonesObjectsDic[skillStoneOfPlayerId] = item;

            item._SkillConfigOfSkillStone = SkillConfigTable.GetSkillConfigByID(MySkillStonesReader.mySkillStonesDataDic[skillStoneOfPlayerId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfigOfSkillStone.TYPE + "_" + item._SkillConfigOfSkillStone.REAL_NAME;
            item.SkillStoneOfPlayerId = skillStoneOfPlayerId;
            item.gameObject.transform.SetParent(stonesTempContainer);           
        }
        
        Vector2 buttonAnchorPosition;
        Vector2 true_buttonAnchorPosition;
        Vector3 buttonWorldPosition;
        readonly int worldSpaceConvertMode = 1;// 1: canvas screen space 2: UI元素在左下角？忘了
        public Vector3 ButtonEffectInFxCameraWorldSpace(Camera fxcamera, GameObject UI_thing, float z_offset)
        {
            switch (worldSpaceConvertMode)
            {
                case 1:
                    buttonWorldPosition = UI_thing.transform.position;
                    buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, buttonWorldPosition.z + z_offset);
                break;
                case 2:
                    buttonAnchorPosition = UI_thing.GetComponent<RectTransform>().transform.position;
                    true_buttonAnchorPosition = new Vector2(buttonAnchorPosition.x, buttonAnchorPosition.y);
                    buttonWorldPosition = fxcamera.ScreenToWorldPoint(true_buttonAnchorPosition);
                    buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, fxcamera.transform.position.z + z_offset);
                break;
            }
            return buttonWorldPosition;
        }
    }
}