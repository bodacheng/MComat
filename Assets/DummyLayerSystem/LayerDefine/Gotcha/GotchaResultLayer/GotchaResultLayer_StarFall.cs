using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using DG.Tweening;
using Skill;

public partial class GotchaResultLayer : UILayer
{
    // 整个星星下落动画
    IEnumerator StarFallAnim(List<StoneOfPlayerInfo> results)
    {
        Reset();
        
        if (results != null)
        foreach (var info in results)
        {
            StarFall(info);
            starFallAnimOneProcess = StartCoroutine(WaitForOneStarFall());
            while(!_oneStarFallen)
                yield return new WaitForSeconds(0.1f);
        }
        _starFallen = true;
    }
    
    async void StarFall(StoneOfPlayerInfo stone)
    {
        Vector3 targetPos = StarsFall.target.GetRandomStarPos();
        Vector3 forwardOfCamera = targetPos - StarsFall.target._camera.transform.position;
        Vector3 flashPos = StarsFall.target._camera.transform.position + forwardOfCamera.normalized * 200;
        
        SkillConfig skillConfig = SkillConfigTable.GetSkillConfig(stone.SkillId);
        string fallingstarname = "";
        string fallingstarexplosionname = "";
        switch(skillConfig.SP_LEVEL) // 这里应该是rarelevel
        {
            case 0:
                fallingstarname = "gachastar0";
                fallingstarexplosionname = "screenStarExplostionTest0";
            break;
            case 1:
                fallingstarname = "gachastar1";
                fallingstarexplosionname = "screenStarExplostionTest1";
            break;
            case 2:
                fallingstarname = "gachastar2";
                fallingstarexplosionname = "screenStarExplostionTest2";
            break;
            case 3:
                fallingstarname = "gachastar3";
                fallingstarexplosionname = "screenStarExplostionTest3";
            break;
        }
        Decomposition Star = await AddressablesLogic.LoadTOnObject<Decomposition>(fallingstarname);
        Star.transform.position = targetPos;
        Decomposition flash = await AddressablesLogic.LoadTOnObject<Decomposition>(fallingstarexplosionname);
        flash.transform.position = flashPos;
        stoneFallingModels.Add(Star);
        stoneStartFlashModels.Add(flash);
        StarsFall.target._camera.transform.DOLookAt(Star.transform.position, 1f);
        Star.transform.DOMoveY(-600, 30f);
    }
    
    // 跳过整个星星下落动画
    void SkipStarFallAnim()
    {
        if (starFallAnimWholeProcess != null)
        {
            StopCoroutine(starFallAnimWholeProcess);
        }
        SpeedOnce.gameObject.SetActive(false);
        _starFallen = true;
    }
    
    // 一个星星下落动画
    IEnumerator WaitForOneStarFall()
    {
        _oneStarFallen = false;
        yield return new WaitForSecondsRealtime(1f);
        _oneStarFallen = true;
    }
    
    // 加速一个星星下落动画
    void SpeedOneGotchaAnim()
    {
        if (starFallAnimOneProcess != null)
        {
            StopCoroutine(starFallAnimOneProcess);
        }
        _oneStarFallen = true;
    }
}
