using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        float pressingSeconds;
        bool pressStart;
        SingleAssignmentDisposable pressCount;
        
        public void CellButtonBeheviour_STStoneShow(StoneCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();                
            button.onClick.RemoveAllListeners();
            void buttonFeature()
            {
                SKStoneItem _stone = _SkillStoneCell.GetItem();
                if (_stone != null && _stone._SkillConfig != null)
                {
                    _skillStoneDetail.RefreshInfo(_stone.SkillStoneOfPlayerId);
                    SSLevelUpManager.target.SetTargetStoneID(_stone.SkillStoneOfPlayerId);
                }else{
                    _skillStoneDetail.Clear();
                }
            }
            
            void PressGoToLevelUpPage2()
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
                                        SSLevelUpManager.target.OpenLevelUpPage(_stone.SkillStoneOfPlayerId);
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
                    PressGoToLevelUpPage2();
                    StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected);
                }
            } );
            up.callback.AddListener( (eventData) => { pressStart = false; } );
            
            trigger.triggers.Clear();
            trigger.triggers.Add(enter);
            trigger.triggers.Add(up);
            
            //button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected); });
        }
    }
}