using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class OldDebugPreparingProcess : NagareProcess
{
    public OldDebugPreparingProcess(NetFightScene _NetFightScene,DebugManager debugManager)
    {
        this.thisProcessStep = SceneStep.Preparing;
        this.nextProcessStep = SceneStep.Fighting;
        this._NetFightScene = _NetFightScene;
        this.debugManager = debugManager;
    }

    public override bool canEnterNextProcess()
    {
        return _RealTimeGameProcessManager.FightTeam1.IfAllCharsPreparedForBattle() && _RealTimeGameProcessManager.FightTeam2.IfAllCharsPreparedForBattle();
    }
    
    public override void ProcessEnter()
    {
        _NetFightScene.PreparingCanvas.gameObject.SetActive(true);
        _NetFightScene.FightCanvas.gameObject.SetActive(false);
        _NetFightScene._FightOverControl.FightOverCanvas.gameObject.SetActive(false);
        _NetFightScene.StartCoroutine(EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets("defaultmagic"));//我们姑且这样处理debug环境默认魔法问题
        debugManager.debugCharPlacer.SetActive(true);
        _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.approachToCertainDistance,null);//乱写的。原来的模式删除了。
        debugManager.debugModePlayerPlacementStep = 0;        
        debugManager._BoundaryControllByGod.battleRingCenter = Vector3.zero;// 这个逻辑有一定问题，不一定对不对？
    
        if (debugManager.debugMode == DebugMode.ab_mode)
        {
            debugManager.pretabName.gameObject.SetActive(true);
            debugManager.charsOfType.gameObject.SetActive(false);
            debugManager.AIScriptName.gameObject.SetActive(true);
            debugManager.AIScriptsOfType.gameObject.SetActive(false);
        }else{
            debugManager.pretabName.gameObject.SetActive(false);
            debugManager.charsOfType.gameObject.SetActive(true);
            debugManager.AIScriptName.gameObject.SetActive(false);
            debugManager.AIScriptsOfType.gameObject.SetActive(true);
        }
        //_NetFightScene.gameStartButton.gameObject.SetActive(true);
    }
    
    public override void ProcessEnd()
    {
        //考虑一下这个环节。如果双方队伍读取完毕，那loading那个黑幕的就自然消失。
        if (FightLoadError.Instance.FightLoadErrors.Count > 0)
        {
            foreach (string error in FightLoadError.Instance.FightLoadErrors)
            {
                Debug.Log("双方队伍读取后问题： " + error);
            }
            SceneManager.LoadScene(1);
        }
    }

    public override void localUpdate()
    {
        if (debugManager.debugModePlayerPlacementStep == 1 || debugManager.debugModePlayerPlacementStep == 2 || debugManager.debugModePlayerPlacementStep == 3)
        {
            debugManager.placingCharacter();
        }
        //if (_NetFightScene.checkIfEveryTeamHasMember())
        //{
        //    _NetFightScene.gameStartButton.gameObject.SetActive(true);
        //}
        //else
        //{
        //    _NetFightScene.gameStartButton.gameObject.SetActive(false);
        //}
    }
}
