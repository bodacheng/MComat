using UnityEngine;

public class HitBoxSubEventManger : MonoBehaviour
{
    [System.Serializable]
    public class EventAndTriggerTime
    {
        public float time;
        public string event_name;
    }

    public Decompositioner _Decompositioner;
    public EventAndTriggerTime _event;
    
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
                _Decompositioner.SpecialTriggerEvent(_event.event_name,this);
            }
        }
    }
}