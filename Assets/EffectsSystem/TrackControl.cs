using UnityEngine;

public class TrackControl : MonoBehaviour {
    //public List<EventKeyframe> listEventKeyframe = new List<EventKeyframe>();
    public AnimationCurve xAnimationCurve;
    public AnimationCurve zAnimationCurve;
    public float Z_scale = 1f;

    Vector3 startPoint;
    Quaternion startquaternion;
    Matrix4x4 m;
    float time_counter;

    public void StartOff(Vector3 start,Quaternion startQ,float Z_scale)
    {
        time_counter = 0;
        this.Z_scale = Z_scale;
        startPoint = start;
        startquaternion = startQ;
        m = Matrix4x4.TRS(startPoint, startquaternion, Vector3.one * 1);
    }
    
	void Update()
	{
        time_counter += Time.deltaTime;
        transform.position = m.MultiplyPoint3x4(new Vector3(xAnimationCurve.Evaluate( time_counter ), 0, zAnimationCurve.Evaluate( time_counter ) * Z_scale ));

		//foreach( EventKeyframe ekf in listEventKeyframe )
		//{
		//	if( ( ( currentEventKeyframeTime < ekf.time && ekf.time < currentEventKeyframeTime + Time.deltaTime ) ||
		//			( currentEventKeyframeTime == ekf.time ) ) && ekf.functionName != null )
		//	{
		//		gameObject.SendMessage( ekf.functionName );
		//	}
		//}
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