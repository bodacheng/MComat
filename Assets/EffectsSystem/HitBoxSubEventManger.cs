using UnityEngine;
using UniRx;

public class HitBoxSubEventManger : MonoBehaviour
{
    public Decomposition decomposition;
    public EventAndTriggerTime _event;
    public string LandedEvent;
    public string fadeEvent;
    float time_count;
    
    SingleAssignmentDisposable clockEvent, landEvent, fadedEvent;
    
    void OnDestroy()
    {
        if (clockEvent != null && !clockEvent.IsDisposed)
            clockEvent.Dispose();
        if (landEvent != null && !landEvent.IsDisposed)
            landEvent.Dispose();
        if (fadedEvent != null && !fadedEvent.IsDisposed)
            fadedEvent.Dispose();
    }
    
    void OnDisable()
    {
        if (clockEvent != null && !clockEvent.IsDisposed)
            clockEvent.Dispose();
        if (landEvent != null && !landEvent.IsDisposed)
            landEvent.Dispose();
        if (fadedEvent != null && !fadedEvent.IsDisposed)
            fadedEvent.Dispose();
    }

    void OnEnable()
    {
        time_count = 0;        
        if (!string.IsNullOrEmpty(_event.event_name))
        {
            clockEvent = new SingleAssignmentDisposable();
            clockEvent.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (time_count > _event.time)
                    {
                        decomposition.SpecialTriggerEvent(_event.event_name, this);
                        clockEvent.Dispose();
                    }
                    if (!gameObject.activeSelf)
                    {
                        clockEvent.Dispose();
                    }
                }
            );
            SingleAssignmentDisposableCleaner.Add(clockEvent);
        }
        
        if (!string.IsNullOrEmpty(LandedEvent))
        {
            landEvent = new SingleAssignmentDisposable();
            landEvent.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (decomposition.transform.position.y <= 0)
                    {
                        decomposition.SpecialTriggerEvent(LandedEvent, this);
                        decomposition.Phase = -1;
                        landEvent.Dispose();
                    }
                    if (!gameObject.activeSelf)
                    {
                        landEvent.Dispose();
                    }
                }
            );
            SingleAssignmentDisposableCleaner.Add(landEvent);
        }
        
        if (!string.IsNullOrEmpty(fadeEvent))
        {
            fadedEvent = new SingleAssignmentDisposable();
            fadedEvent.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (decomposition._HitBox.weaponHP > 0 && decomposition._HitBox.CurrentHP <= 0)
                    {
                        decomposition.SpecialTriggerEvent(fadeEvent, this);
                        fadedEvent.Dispose();
                    }
                    if (!gameObject.activeSelf)
                    {
                        fadedEvent.Dispose();
                    }
                }
            );
            SingleAssignmentDisposableCleaner.Add(fadedEvent);
        }
    }
    
    void Update()
    {
        time_count += Time.deltaTime;
    }
    
    [System.Serializable]
    public class EventAndTriggerTime
    {
        public float time;
        public string event_name;
    }
}