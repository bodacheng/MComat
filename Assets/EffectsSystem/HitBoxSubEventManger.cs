using UnityEngine;
using UniRx;

public class HitBoxSubEventManger : MonoBehaviour
{
    public Decompositioner _Decompositioner;
    public EventAndTriggerTime _event;
    public string LandedEvent;
    public string fadeEvent;
    
    float time_count;

    void OnEnable()
    {
        time_count = 0;        
        if (!string.IsNullOrEmpty(_event.event_name))
        {
            var clockEvent = new SingleAssignmentDisposable();
            clockEvent.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (time_count > _event.time)
                    {
                        _Decompositioner.SpecialTriggerEvent(_event.event_name, this);
                        clockEvent.Dispose();
                    }
                    if (!gameObject.activeSelf)
                    {
                        clockEvent.Dispose();
                    }
                }
            );
        }
        
        if (!string.IsNullOrEmpty(LandedEvent))
        {
            var landEvent = new SingleAssignmentDisposable();
            landEvent.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (_Decompositioner.transform.position.y <= 0)
                    {
                        _Decompositioner.SpecialTriggerEvent(LandedEvent, this);
                        landEvent.Dispose();
                    }
                    if (!gameObject.activeSelf)
                    {
                        landEvent.Dispose();
                    }
                }
            );
        }
        
        if (!string.IsNullOrEmpty(fadeEvent))
        {
            var fadedEvent = new SingleAssignmentDisposable();
            fadedEvent.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (_Decompositioner._HitBox.weaponHP > 0 && _Decompositioner._HitBox.CurrentHP <= 0)
                    {
                        _Decompositioner.SpecialTriggerEvent(fadeEvent, this);
                        fadedEvent.Dispose();
                    }
                    if (!gameObject.activeSelf)
                    {
                        fadedEvent.Dispose();
                    }
                }
            );
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