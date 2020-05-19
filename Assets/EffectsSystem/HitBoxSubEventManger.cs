using UnityEngine;

public class HitBoxSubEventManger : MonoBehaviour
{
    public Decompositioner _Decompositioner;
    public EventAndTriggerTime _event;
    public string LandedEvent;
    public string fadeEvent;

    float time_count;
    bool start;

    void OnEnable()
    {
        start = true;
    }

    void Update()
    {
        if (start)
        {
            time_count += Time.deltaTime;
            if (time_count > _event.time)
            {
                start = false;
                time_count = 0;
                _Decompositioner.SpecialTriggerEvent(_event.event_name, this);
            }
        }

        if (_Decompositioner.transform.position.y <= 0)
        {
            _Decompositioner.SpecialTriggerEvent(LandedEvent, this);
        }

        if (_Decompositioner._HitBox.weaponHP > 0 && _Decompositioner._HitBox.CurrentHP <= 0)
        {
            _Decompositioner.SpecialTriggerEvent(fadeEvent, this);
        }
    }

    [System.Serializable]
    public class EventAndTriggerTime
    {
        public float time;
        public string event_name;
    }
}