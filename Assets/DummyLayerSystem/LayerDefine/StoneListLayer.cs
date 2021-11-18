using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class StoneListLayer : UILayer
{
    public SkillStonesBox box;
    public SSLevelUpManager ssLevelUper;
    
    [Space(10)] 
    [Header("FX Camera")] 
    public Camera fxCamera;
    
    public static StoneListLayer Open()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
            return returnValue;
        }
        l = UILayerLoader.Load(PreScene.target.T,"StoneListLayer") as StoneListLayer;
        returnValue = l as StoneListLayer;
        returnValue.box.GenerateCells();
        returnValue.box._SkillStoneBoxTabEffectsManager.StartUp();
        returnValue.box.IniExTabs(returnValue.fxCamera);
        returnValue.box.EXTabsFeatureRefresh(true);
        returnValue.box.RestFilter();
        returnValue.box._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            returnValue.box.NormalTab.transform,
            returnValue.box.EX1Tab.transform,
            returnValue.box.EX2Tab.transform,
            returnValue.box.EX3Tab.transform, 
            Zokusei.blueMagic
        );
        
        returnValue.box._skillStoneDetail.Clear();
        
        return returnValue;
    }

    public static void Close()
    {
        UILayer l = UILayerLoader.Get("StoneListLayer");
        StoneListLayer returnValue;
        if (l != null)
        {
            returnValue = l as StoneListLayer;
            GameObject.Destroy(returnValue.fxCamera);
            returnValue.box._skillStoneDetail.Clear();
            returnValue.box._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("StoneListLayer");
    }
    
    float pressingSeconds;
    SingleAssignmentDisposable pressCount;
    bool pressStart = false;
    public void CellButtonBeheviour_STStoneShow(StoneCell _SkillStoneCell)
    {
        Button button = _SkillStoneCell.GetComponent<Button>();                
        button.onClick.RemoveAllListeners();
        void buttonFeature()
        {
            SKStoneItem _stone = _SkillStoneCell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                box._skillStoneDetail.RefreshInfo(_stone.instanceId);
                SSLevelUpManager.target.SetTargetStoneID(_stone.instanceId);
            }else{
                box._skillStoneDetail.Clear();
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
                                {
                                    SSLevelUpManager.target.OpenLevelUpPage(_stone.instanceId);
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
    
    float lastclicktime;
    public void CellButtonBeheviour_MAdd(StoneCell _SkillStoneCell)
    {
        Button button = _SkillStoneCell.GetComponent<Button>();
        if (button != null)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            trigger.triggers.Clear();
                
            void buttonFeature()
            {
                if (Time.time - lastclicktime < 0.25f) // double click
                {
                    SSLevelUpManager.target.AddMaterial(_SkillStoneCell);
                }
                lastclicktime = Time.time;
            }
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(buttonFeature);
            button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected); });
            SSLevelUpManager.target.AddMSlotBehaviour(_SkillStoneCell);
        }
    }
}
