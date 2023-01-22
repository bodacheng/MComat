using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CountDownLayer : UILayer
{
    [Header("CountDownText")]
    public Text CountDown;
    
    float startTimestamp = 3f;
    
    public async UniTask BeforeFightCountDown()
    {
        while (startTimestamp > 0)
        {
            startTimestamp -= Time.deltaTime;
            CountDown.text = "" + (1 + (int)(startTimestamp));
            await UniTask.DelayFrame(0);
        }
    }
}
