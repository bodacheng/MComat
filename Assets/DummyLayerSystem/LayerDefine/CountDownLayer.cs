using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownLayer : UILayer
{
    [Space(11)]
    [Header("CountDownText")]
    public Text CountDown;
    
    float startTimestamp = 3f;
    
    public IEnumerator BeforeFightCountDown()
    {
        while (startTimestamp > 0)
        {
            startTimestamp -= Time.deltaTime;
            CountDown.text = "" + (1 + (int)(startTimestamp));
            yield return null;
        }
    }
}
