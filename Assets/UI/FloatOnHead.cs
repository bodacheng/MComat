using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZObjectPools;

public class FloatOnHead : MonoBehaviour
{
    public static PosNum focusingPosNum;
    public Light spotLight;

    //这个模块的改造目标应该是可以根据当前场景的情况自己主动决定显示，而不是其他模块调用函数去主动改变它。
    public enum FloatOnHeadMode : int
    {
        preparingSceneMode = 0,
    }

    private GUISkin custonskin;    
    private CharacterDataInfo _CharacterDataInfo;
    private CharacterResourceInfo _CharacterResourceInfo;
    private preparingScene _preparingScene;
    private CameraManager _CameraManager;
    private PosNum TeamPositionNum;   
    private bool buttonActive;

    void Awake()
    {
    }

    //这个函数与其说是在赋予参数，其实就是点team后一瞬间的产生float按钮操作
    public void applyParametersForTeamMemberEditMode(bool buttonActive,
                                                    preparingScene _preparingScene,
                                                     CharacterDataInfo _CharacterDataInfo, 
                                                     PosNum TeamPositionNum,
                                                     GUISkin custonskin,
                                                     CameraManager _CameraManager)
    {
        this.buttonActive = buttonActive;
        this._preparingScene = _preparingScene;
        this._CharacterDataInfo = _CharacterDataInfo;
        this.TeamPositionNum = TeamPositionNum;
        this.custonskin = custonskin;
        this._CameraManager = _CameraManager;
        if (this._CharacterDataInfo != null)
        {
            this._CharacterResourceInfo = CharsManager._monstersConfigTable.RowToCharacterResourceInfo(
                    CharsManager._monstersConfigTable.Find_ID(_CharacterDataInfo.resource_num.ToString())
            );
        }
        else
        {
            this._CharacterResourceInfo = null;//需要在相应位置槽适配角色
        }
        
    }

	void OnGUI() {
        if (_preparingScene == null)
            return;
		switch (_preparingScene.currentProcess.step)
		{
		    case MainSceneStep.TeamEditFront:
    			Vector3 v = CameraManager._camera.WorldToScreenPoint (transform.position);
                if (this.buttonActive)
                {
                    spotLight.intensity = 6;
                    if (_CharacterResourceInfo != null)
                    {
                        if (GUI.Button(new Rect(v.x - 90, Screen.height - v.y+50, 180, 50), _CharacterResourceInfo.prefabName,custonskin.GetStyle("Button")))
                        {
                            charBodyButton(this.TeamPositionNum);
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(v.x - 90, Screen.height - v.y+50, 180, 50), "点击设置队员"))
                        {
                            charBodyButton(this.TeamPositionNum);
                        }
                    }
                }else{
                    spotLight.intensity = 0;
                }
            break;
            default:
                break;
		}
	}

    public void charBodyButton(PosNum TeamPositionNum)
	{
        if (this.TeamPositionNum == PosNum.none) {

		} else {
            FloatOnHead.focusingPosNum = TeamPositionNum;
            CharacterDataInfo target = _preparingScene.getCurrentOnSetCharInfoByPosNum(FloatOnHead.focusingPosNum);
            _preparingScene._MemberDetail.SetMemberDetailSystemFocusingCharacter(target != null ? target.localID : -1);//确立focusing角色
            _preparingScene.trySwitchToStep(MainSceneStep.TeamEditMonsterDetail, true);
		}
	}
}
