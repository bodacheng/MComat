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
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                void buttonFeature()
                {
                    SKStoneItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfig != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_stone.SkillStoneOfPlayerId);
                        if (SSLevelUpManager.target != null)
                            SSLevelUpManager.target.RefreshSkillLevelUpModule();
                    }else{
                        _skillStoneDetail.Clear();
                    }
                }
                
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
                                        SSLevelUpManager.target.OpenLevelUpPage();
                                        SSLevelUpManager.target._MSkillStoneDetail.Clear();
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
                
                //button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected); });
            }
        }
    }
}