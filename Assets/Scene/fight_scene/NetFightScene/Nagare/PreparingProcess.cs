using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PreparingProcess : NagareProcess
{
    public PreparingProcess(NetFightScene _NetFightScene)
    {
        this.thisProcessStep = SceneStep.Preparing;
        this.nextProcessStep = SceneStep.StoryBeforeFight;
        this._NetFightScene = _NetFightScene;
    }

    public override bool canEnterNextProcess()
    {
        return _NetFightScene.ifLoadStageFinished() && _NetFightScene._CharSetManager.ifAllCharsPreparedForBattle();
    }
    
    public override void ProcessEnter()
    {
        defaultPools.Instance.FightLoadErrors.Clear();
        //应该在这里打开黑幕，直到preparing阶段完成，黑幕消失
        switch (_NetFightScene._SceneMode)
        {
            case SceneMode.MyPetsFight:
                if (GoingToLoadFight.Instance.nextBattle != null)
                {
                    _NetFightScene._DebugManager.debugCharPlacer.SetActive(false);
                    _NetFightScene.RunFightSceneProcess(_NetFightScene.loadGame(GoingToLoadFight.Instance.nextBattle, false));//这个环节的完成flag就是ifLoadStageFinished()
                }
            break;
            case SceneMode.QuestFight:
                if (GoingToLoadFight.Instance.nextBattle != null)
                {
                    _NetFightScene._DebugManager.debugCharPlacer.SetActive(false);
                    _NetFightScene.RunFightSceneProcess(_NetFightScene.loadGame(GoingToLoadFight.Instance.nextBattle, true));//这个环节的完成flag就是ifLoadStageFinished()
                }
            break;
        }
        if (_NetFightScene.Team2StandPoints != null)
            _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.WatchOverCamera, _NetFightScene.Team2StandPoints.ToList());
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.resetLoadStageFinishedFlag();
        if (defaultPools.Instance.FightLoadErrors.Count > 0)
        {
            foreach (string error in defaultPools.Instance.FightLoadErrors)
                Debug.Log("双方队伍读取后问题： " + error);
            
            SceneManager.LoadScene(1);//也就是说这个地方是为了阻止进入下一步呗？
        }
        defaultPools.Instance.FightLoadErrors.Clear();
    }

    public override void localUpdate()
    {
    }
}
