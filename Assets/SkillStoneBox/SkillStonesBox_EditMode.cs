using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        float lastclicktime;
        public void CellButtonBeheviour_EditCharSkill(StoneCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            void buttonFeature()
            {
                if (Time.time - lastclicktime < 0.25f) // double click
                {
                    if (TheNineSlot.target.GetFocusingStoneSlot() != null)
                    {
                        StoneCell.Install(_SkillStoneCell, TheNineSlot.target.GetFocusingStoneSlot()._DragAndDropCell);
                    }
                }
                lastclicktime = Time.time;
                SKStoneItem _stone = _SkillStoneCell.GetItem();
                if (_stone != null && _stone._SkillConfig != null)
                {
                    _skillStoneDetail.RefreshInfo(_stone.SkillStoneOfPlayerId);
                }else{
                    _skillStoneDetail.Clear();
                }
            }
            
            // 前往技能石升级画面
            void PressGoToLevelUpPage()
            {
                pressCount = new SingleAssignmentDisposable
                {
                    Disposable = Observable.EveryUpdate().Subscribe(_ =>
                        {
                            if (pressStart)
                            {
                                pressingSeconds += Time.deltaTime;
                                if (pressingSeconds > 1f)
                                {
                                    pressingSeconds = 0;
                                    pressStart = false;
                                    SKStoneItem _stone = _SkillStoneCell.GetItem();
                                    if (_stone != null && _stone._SkillConfig != null)
                                    {
                                        PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList, _stone.SkillStoneOfPlayerId, true);
                                    }
                                }
                            }
                            if (!pressStart)
                            {
                                pressingSeconds = 0;
                                if (!pressCount.IsDisposed)
                                {
                                    pressCount.Dispose();
                                }
                            }
                        }
                    )
                };
            }
            
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry enter = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            EventTrigger.Entry up = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            enter.callback.AddListener((eventData) => {
                if (!pressStart)
                {
                    pressStart = true;
                    buttonFeature();
                    PressGoToLevelUpPage();
                    StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected);
                }
            } );
            up.callback.AddListener( (eventData) => { pressStart = false; } );
            
            trigger.triggers.Clear();
            trigger.triggers.Add(enter);
            trigger.triggers.Add(up);
        }
    }
}