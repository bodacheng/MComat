using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class gotchaManager : MonoBehaviour
{
    public preparingScene preparingScene;
    public RectTransform gotchaCanvas;
    
    public void gotchaButtonFeature()
    {
        preparingScene.mainProcessRunner.triggerMainProcess(MySkillStonesReader.Instance.StoneGotcha());        
    }
}
