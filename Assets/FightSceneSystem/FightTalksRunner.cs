using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UniRx;

public class FightTalksRunner : MonoBehaviour
{
    public RPGTalk RPGTalk; //这个元件里有对应的ui.text？应该是在storyCanvas下面。这两个部件应该是在场景里给布置好，检测到哪个元件缺失的话那不播放剧情就行。
    public PlayableDirector playableDirector;
    public Text CountDown;
    public ReactiveProperty<bool> PlayersStartOff { get; set; } = new ReactiveProperty<bool>(false);

    public static FightTalksRunner target;
    
    void Awake()
    {
        target = this;
    }
}

//public void RunBeforeFight(List<Data_Center> myTeamTs)
//{
//    timeCounter += Time.deltaTime;
//    if (step == -1)
//        step = 0;
    
//    //第一轮重复执行其实只需要一次的代码貌似也没啥办法
//    List<float> xzrosoff = new List<float>(){-30,-20,20,-30};
//    List<Transform> allMuTs = new List<Transform>();
//    if (step < myTeamTs.Count)
//    {
//        if (timeCounter > 0.6f)
//        {
//            timeCounter = 0f;
//            if (step < myTeamTs.Count)
//            {
//                CountDown.text = step.ToString();
//                _CameraManager.Assign_Camera(Camera_Mode_Num.WatchOver, new List<Transform>() { myTeamTs[step].gameObject.transform });
//                allMuTs.Add(myTeamTs[step].gameObject.transform);
//                _CameraManager.current_Camera_Mode.SetWatchOverModeParas(7,5, xzrosoff[step], 0,5f);
//            }
//            step += 1;
//            if (step == myTeamTs.Count)
//            {
//                _CameraManager.Assign_Camera(Camera_Mode_Num.WatchOver, allMuTs);
//                _CameraManager.current_Camera_Mode.SetWatchOverModeParas(25,14, 0, 0, 5f);
//                step += 1;
//            }
//            timeCounter = 0f;
//        }
//    }
//    if (step == myTeamTs.Count + 1 && timeCounter < 0.6f)
//    {
//        allMuTs.Clear();
//        foreach (Data_Center _one in myTeamTs)
//        {
//            allMuTs.Add(_one.transform);
//        }
//        _CameraManager.Assign_Camera(Camera_Mode_Num.WatchOver, allMuTs);
//        _CameraManager.current_Camera_Mode.SetWatchOverModeParas(25, 14, -180, 0, 5f);
//        step += 1;
//    }

//    if (step == myTeamTs.Count + 2)
//    {
//        //开始倒计时
//        LocalCountDown();
//    }
//    LocalCountDown();
//}
