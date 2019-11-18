using UnityEngine;

public class DanMuTest : MonoBehaviour {
    //public List<EventKeyframe> listEventKeyframe = new List<EventKeyframe>();
    public AnimationCurve xAnimationCurve;
    public AnimationCurve zAnimationCurve;
    public float scale = 1f;
    
    private Vector3 startPoint;
    private Quaternion startquaternion;
	
    private Matrix4x4 m;
    private float time_counter;

    public void StartOff(Vector3 start,Quaternion startQ)
    {
        time_counter = 0;
        this.startPoint = start;
        this.startquaternion = startQ;
        m = Matrix4x4.TRS(startPoint, startquaternion, Vector3.one * 1);
    }
    
	void Update()
	{
        time_counter += Time.deltaTime;
        transform.position = m.MultiplyPoint3x4(new Vector3(xAnimationCurve.Evaluate( time_counter ) * scale, 0, zAnimationCurve.Evaluate( time_counter ) * scale ));

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