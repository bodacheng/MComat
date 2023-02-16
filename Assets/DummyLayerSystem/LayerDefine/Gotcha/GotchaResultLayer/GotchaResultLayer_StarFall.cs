using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using dataAccess;
using DG.Tweening;

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
        var targetPos = StarsFall.target.GetRandomStarPos();
        var forwardOfCamera = targetPos - StarsFall.target.Camera.transform.position;
        var flashPos = StarsFall.target.Camera.transform.position + forwardOfCamera.normalized * 200;
        
        var skillConfig = SkillConfigTable.GetSkillConfig(stone.SkillId);
        var fallingstarname = "";
        var fallingstarexplosionname = "";
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
        var star = await AddressablesLogic.LoadTOnObject<ParticleSystem>(fallingstarname);
        star.gameObject.name = fallingstarname;
        star.transform.position = targetPos;
        
        var flash = await AddressablesLogic.LoadTOnObject<ParticleSystem>(fallingstarexplosionname);
        flash.gameObject.name = fallingstarexplosionname;
        flash.transform.position = StarsFall.target.GetEffectCenter();
        flash.transform.DOMove(flashPos, 1);
        
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        stoneFallingModels.Add(star);
        stoneStartFlashModels.Add(flash);
        StarsFall.target.Camera.transform.DOLookAt(star.transform.position, 1f);
        star.transform.DOMoveY(-600, 30f);
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
}
