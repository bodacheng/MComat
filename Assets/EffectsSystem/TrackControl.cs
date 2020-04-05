using UnityEngine;
using DG.Tweening;

public class TrackControl : MonoBehaviour {
    //public List<EventKeyframe> listEventKeyframe = new List<EventKeyframe>();

    public TrackMode _TrackMode;
    float time_counter;
    
    [Space(11)]
    [Header("DefinedTrack")]
    public AnimationCurve xAnimationCurve;
    public AnimationCurve zAnimationCurve;
    public float Z_scale = 1f;
    Matrix4x4 m;

    [Space(11)]
    [Header("Navigation")]
    public Sensor Sensor;
    Vector3 direction;
    Transform navTarget;
    float navRunSpeed;
    
    public void StartOff(Vector3 start,Quaternion startQ,float Z_scale)
    {
        time_counter = 0;
        switch (_TrackMode)
        {
            case TrackMode.DefinedTrack:
                this.Z_scale = Z_scale;
                m = Matrix4x4.TRS(start, startQ, Vector3.one * 1);
            break;
            case TrackMode.Navigation:
                m = Matrix4x4.TRS(start, startQ, Vector3.one * 1);
                direction = m.MultiplyPoint3x4(new Vector3(0,0,1)) - m.MultiplyPoint3x4(new Vector3(0,0,0));
                transform.position = start;
                navRunSpeed = Z_scale;
            break;
        }
    }
    
	void Update()
	{
        time_counter += Time.deltaTime;
        switch(_TrackMode)
        {
            case TrackMode.DefinedTrack:
            transform.position = m.MultiplyPoint3x4(new Vector3(xAnimationCurve.Evaluate( time_counter ), 0, zAnimationCurve.Evaluate( time_counter ) * Z_scale ));
            break;
            case TrackMode.Navigation:
                if (Sensor != null)
                {
                    if (Sensor.GetClosestEnemyColliderInSensorRange() != null)
                    {
                        direction += (Sensor.GetClosestEnemyColliderInSensorRange().transform.position - transform.position).normalized;
                    }
                }
                direction.y = 0;
                direction = direction.normalized;
                transform.position = Vector3.Lerp(transform.position,transform.position + direction * navRunSpeed,Time.deltaTime);
            break;
        }
       
		//foreach( EventKeyframe ekf in listEventKeyframe )
		//{
		//	if( ( ( currentEventKeyframeTime < ekf.time && ekf.time < currentEventKeyframeTime + Time.deltaTime ) ||
		//			( currentEventKeyframeTime == ekf.time ) ) && ekf.functionName != null )
		//	{
		//		gameObject.SendMessage( ekf.functionName );
		//	}
		//}
	}

    public enum TrackMode
    {
        DefinedTrack = 1,
        Navigation = 2
    }
}

public class EventKeyframe
{
    public float time;
    public string functionName;
    
    public void SetValues( float _time, string _name )
    {
        time = _time;
        functionName = _name;
    }
}