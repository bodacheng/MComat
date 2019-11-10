using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UniRx;

// 战斗前剧情？对话？姑且把这个环节给表示出来
// 我们的最初版本不会有那么多废话，但这个模块起码代表载入战斗后双方开火前的环节
// 这样一个流程：不是对方是4个角色左右？那么相机应该是把这四个角色给过一遍，在角色身上显示一下这个角色的等级和名字？
// 实际上原则上一场战斗应该可以提前确定对方阵容详细信息，这个信息如果在主界面可以看，那进入战斗后还有必要显示的那么详细？
// 那么我们决定在进入正式战斗前不需要任何确认按键了，但我们希望把镜头的控制也交给这个模块。所以关于这个处理的对象。。localfight？还要有个参数是看哪个是boss？
// 我觉得其实可以把这个东西给做成个可编辑的东西，比如说不是五个随机关卡？那我们可以决定在第五关的位置上强调位置0上的敌人，给个特写，来放大显示位置0上角色的名字
// 为什么做出这个东西来，是考虑到将来可能会在这个环节里构写剧情对话

public class FightTalksRunner : MonoBehaviour
{
    public RPGTalk RPGTalk; //这个元件里有对应的ui.text？应该是在storyCanvas下面。这两个部件应该是在场景里给布置好，检测到哪个元件缺失的话那不播放剧情就行。
    public PlayableDirector playableDirector;
    public CameraManager _CameraManager;
    public Text CountDown;

    public ReactiveProperty<bool> playersStartOff { get; set; } = new ReactiveProperty<bool>(false);
    
    private float startTimestamp = 3f;
    private int step = -1;
    
    public int Step
    {
        set
        {
            step = value;
            switch (step)
            {
                case 0:
                    startTimestamp = 3f;
                    this._CameraManager.Assign_Camera(Camera_Mode_Num.RoundBoundary,null);
                    StartCoroutine(BeforeFightCountDown());
                break;
                case 1:
                    playersStartOff.Value = true;
                break;
            }
        }
        get
        {
            return step;
        }
    }

    void Start()
    {
        Step = -1;
    }

    public void RunStoryTimeLine(PlayableAsset playableAsset)
    {
        playableDirector.playableAsset = playableAsset;
        //必须设法在timeline条上设置RPGTalk轨道对应的RPGTalk。
        //foreach (PlayableBinding item in playableDirector.playableAsset.outputs)
        //{
        //    if (item.streamName == "RPGTalkTrack")
        //        playableDirector.SetGenericBinding(item.sourceObject, RPGTalk.gameObject);//为对话轨道设置场景里的对话管理器。
        //}
        playableDirector.Play();      
        //RPGTalk.PlayNext();        
    }
        
    private IEnumerator BeforeFightCountDown()
    {
        while (startTimestamp > 0)
        {
            startTimestamp -= Time.deltaTime;
            CountDown.text = "" + (1 + (int)(startTimestamp));
            yield return null;
        }
        CountDown.gameObject.SetActive(false);
        Step = 1;
        yield break;
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
