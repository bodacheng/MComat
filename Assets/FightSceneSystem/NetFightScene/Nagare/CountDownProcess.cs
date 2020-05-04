using System.Collections;
using UnityEngine;
using mainMenu;

public class CountDownProcess : NagareProcess
{
    float startTimestamp = 3f;

    public CountDownProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        thisProcessStep = SceneStep.CountDown;
        nextProcessStep = SceneStep.Fighting;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }

    public override void ProcessEnter()
    {
        PreScene.Instance._CameraManager.Assign_Camera(C_Mode.RoundBoundary,null);
        startTimestamp = 3f;
        AutoMoveToNext = false;
        BoundaryControllByGod.target.ChangeMagicRingRadius(20f);
        FightScene.mainProcessRunner.Run(BeforeFightCountDown());
    }
    
    public override void ProcessEnd()
    {
    }
    
    IEnumerator BeforeFightCountDown()
    {
        FightScene.CountDown.gameObject.SetActive(true);
        while (startTimestamp > 0)
        {
            startTimestamp -= Time.deltaTime;
            FightScene.CountDown.text = "" + (1 + (int)(startTimestamp));
            yield return null;
        }
        FightScene.CountDown.gameObject.SetActive(false);
        AutoMoveToNext = true;
        yield break;
    }
}
