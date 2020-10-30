using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using DG.Tweening;
using mainMenu;

public class GachaRender : MonoBehaviour
{
    public Camera Camera;
    public Transform SkyLightCenter;
    public float SkySphereRadius = 650;

    #region 
    public RectTransform starWaitPos1, starWaitPos2, starWaitPos3, starWaitPos4, starWaitPos5, starWaitPos6, starWaitPos7, starWaitPos8, starWaitPos9;
    List<RectTransform> WaitPos = new List<RectTransform>();
    #endregion

    public static GachaRender target;

    List<Decompositioner> stoneFallingModels = new List<Decompositioner>();
    List<Decompositioner> screenStarModels = new List<Decompositioner>();
    List<Vector3> nineslotScreenPos = new List<Vector3>();
    
    void Awake()
    {
        target = this;
    }
    
    // 必须使用时候即时运行因为里面几个决定位置的运算要考虑当前相机位置等
    void PosDecide()
    {
        WaitPos.Clear();
        WaitPos.Add(starWaitPos1);
        WaitPos.Add(starWaitPos2);
        WaitPos.Add(starWaitPos3);
        WaitPos.Add(starWaitPos4);
        WaitPos.Add(starWaitPos5);
        WaitPos.Add(starWaitPos6);
        WaitPos.Add(starWaitPos7);
        WaitPos.Add(starWaitPos8);
        WaitPos.Add(starWaitPos9);
        
        // 星星落入格子
        Vector3 A1screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.A1T.GetComponent<RectTransform>(), 5f);
        Vector3 A2screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.A2T.GetComponent<RectTransform>(), 5f);
        Vector3 A3screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.A3T.GetComponent<RectTransform>(), 5f);
        Vector3 B1screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.B1T.GetComponent<RectTransform>(), 5f);
        Vector3 B2screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.B2T.GetComponent<RectTransform>(), 5f);
        Vector3 B3screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.B3T.GetComponent<RectTransform>(), 5f);
        Vector3 C1screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.C1T.GetComponent<RectTransform>(), 5f);
        Vector3 C2screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.C2T.GetComponent<RectTransform>(), 5f);
        Vector3 C3screenpos = ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, GachaManager.target.NineForShow.C3T.GetComponent<RectTransform>(), 5f);
        nineslotScreenPos.Clear();
        nineslotScreenPos.Add(A1screenpos);
        nineslotScreenPos.Add(A2screenpos);
        nineslotScreenPos.Add(A3screenpos);
        nineslotScreenPos.Add(B1screenpos);
        nineslotScreenPos.Add(B2screenpos);
        nineslotScreenPos.Add(B3screenpos);
        nineslotScreenPos.Add(C1screenpos);
        nineslotScreenPos.Add(C2screenpos);
        nineslotScreenPos.Add(C3screenpos);
    }
    
    public IEnumerator TenGotchaAnimProcess(List<SkillStoneOfPlayerInfoModel> results)
    {
        CameraManager._camera.gameObject.SetActive(false);
        Camera.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        PosDecide();
        
        // 星星下坠
        foreach (SkillStoneOfPlayerInfoModel stoneinfo in results)
        {
            Decompositioner Star = EffectsManager.GenerateEffect("gachastar", FightGlobalSetting.EffectPathDefine(Zokusei.Null), GetRandomStarPos(), Quaternion.identity, null);
            stoneFallingModels.Add(Star);
            Camera.transform.DOLookAt(Star.transform.position,1f);
            Star.transform.DOMoveY(-600, 30f);
            yield return new WaitForSecondsRealtime(1f);
        }
        
        // 屏幕星星
        for (int i = 0; i < results.Count; i++)
        {
            Decompositioner screenStar = EffectsManager.GenerateEffect("normal_test_screenstar", FightGlobalSetting.EffectPathDefine(Zokusei.Null), WaitPos[i].position, Quaternion.identity, null);
            screenStarModels.Add(screenStar);
        }
        
        if (results.Count == 1)
        {
            screenStarModels[0].transform.DOMove(nineslotScreenPos[4], 2f);
            yield return new WaitForSeconds(2f);
            EffectsManager.GenerateEffect("screenStarExplostionTest", FightGlobalSetting.EffectPathDefine(Zokusei.Null), nineslotScreenPos[4], Quaternion.identity, null);
            yield return GachaManager.target.NineForShow.ShowStones
            (
                "-1", "-1", "-1",
                "-1", results[0] != null ? results[0].skillId : "-1", "-1",
                "-1", "-1", "-1"
            );
        }
        else if (results.Count == 9)
        {
            screenStarModels[0].transform.DOMove(nineslotScreenPos[0], 2f);
            screenStarModels[1].transform.DOMove(nineslotScreenPos[1], 2f);
            screenStarModels[2].transform.DOMove(nineslotScreenPos[2], 2f);
            screenStarModels[3].transform.DOMove(nineslotScreenPos[3], 2f);
            screenStarModels[4].transform.DOMove(nineslotScreenPos[4], 2f);
            screenStarModels[5].transform.DOMove(nineslotScreenPos[5], 2f);
            screenStarModels[6].transform.DOMove(nineslotScreenPos[6], 2f);
            screenStarModels[7].transform.DOMove(nineslotScreenPos[7], 2f);
            screenStarModels[8].transform.DOMove(nineslotScreenPos[8], 2f);
            yield return new WaitForSeconds(2f);
            
            // 星星爆炸
            for (int i = 0; i < results.Count; i++)
            {
                EffectsManager.GenerateEffect("screenStarExplostionTest", FightGlobalSetting.EffectPathDefine(Zokusei.Null), nineslotScreenPos[i], Quaternion.identity, null);
            }
            
            yield return GachaManager.target.NineForShow.ShowStones
            (
                results[0] != null ? results[0].skillId : null,
                results[1] != null ? results[1].skillId : null,
                results[2] != null ? results[2].skillId : null,
                results[3] != null ? results[3].skillId : null,
                results[4] != null ? results[4].skillId : null,
                results[5] != null ? results[5].skillId : null,
                results[6] != null ? results[6].skillId : null,
                results[7] != null ? results[7].skillId : null,
                results[8] != null ? results[8].skillId : null
            );
        }
        
        CameraManager._camera.gameObject.SetActive(true);
        Camera.gameObject.SetActive(false);
    }
    
    Vector3 GetRandomStarPos()
    {
        float xzDisFromCenter = Random.Range(0, SkySphereRadius * 2 / 3);
        Vector3 temp = SkyLightCenter.transform.position + (Vector3.forward * Random.Range(0, 100) + Vector3.right * Random.Range(0, 100)).normalized * xzDisFromCenter;
        float tempheight = Mathf.Sqrt(Mathf.Pow(SkySphereRadius, 2) - Mathf.Pow(xzDisFromCenter, 2));
        Vector3 finalPos = temp + (int)(tempheight - 10) * Vector3.up;
        return finalPos;
    }
}
