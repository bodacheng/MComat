using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using DG.Tweening;
using mainMenu;
using UnityEngine.UI;
using Skill;

public class GachaRender : MonoBehaviour
{
    public static GachaRender target;
    
    #region 动画的跳过以及加速
    public Button Skip;
    public Button SpeedOnce;
    bool starfallAnimEnd = false;
    bool oneStarfallAnimEnd = false;
    Coroutine starFallAnimWholeProcess;
    Coroutine starFallAnimOneProcess;
    #endregion
    
    #region 星空球
    // 抽卡画面背景观摩用相机，在星空球内部
    public Camera Camera;
    public Transform SkyLightCenter;
    public float SkySphereRadius = 650;
    #endregion
    
    #region 屏幕星星飞入位置
    public RectTransform starWaitPos1, starWaitPos2, starWaitPos3, starWaitPos4, starWaitPos5, starWaitPos6, starWaitPos7, starWaitPos8, starWaitPos9;
    List<RectTransform> WaitPos = new List<RectTransform>();
    #endregion
    
    List<Decompositioner> stoneFallingModels = new List<Decompositioner>();
    readonly List<Decompositioner> stoneStartFlashModels = new List<Decompositioner>();
    List<Vector3> nineslotScreenPos = new List<Vector3>();
    List<IEnumerator> enumerators = new List<IEnumerator>();
    
    void Awake()
    {
        target = this;
        Skip.onClick.AddListener(SkipStarFallAnim);
        SpeedOnce.onClick.AddListener(SpeedOneGochaAnim);
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
    }
    
    // 必须使用时候即时运行因为里面几个决定位置的运算要考虑当前相机位置等
    void PosDecide()
    {
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

    // 清理相关特效等等
    public void Reset()
    {
        starfallAnimEnd = false;
        oneStarfallAnimEnd = false;
        for (int i = 0; i < stoneFallingModels.Count; i++)
        {
            stoneFallingModels[i].EnergyRessolve();
        }
        for (int i = 0; i < stoneStartFlashModels.Count; i++)
        {
            stoneStartFlashModels[i].EnergyRessolve();
        }
        stoneFallingModels.Clear();
        stoneStartFlashModels.Clear();
        GachaManager.target.NineForShow.ClearGochaEffects();
    }

    #region 星星下落动画
    // 整个星星下落动画
    public IEnumerator StarFallAnim(List<StoneOfPlayerInfo> results)
    {
        starfallAnimEnd = false;
        Reset();
        
        if (results != null)
        foreach (StoneOfPlayerInfo stoneinfo in results)
        {
            starFallAnimOneProcess = StartCoroutine(OneStarFallAnim());
            while(!oneStarfallAnimEnd)
                yield return new WaitForSeconds(0.1f);
            void StarComing(StoneOfPlayerInfo stone)
            {
                Vector3 targetPos = GetRandomStarPos();
                Vector3 flashPos = ScreenPositionCal.Cal(3, Camera, targetPos, 300f);

                SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(stone.skillId);
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
                Decompositioner Star = EffectsManager.GenerateEffect(fallingstarname, FightGlobalSetting.EffectPathDefine(Zokusei.Null), targetPos, Quaternion.identity, null);
                Decompositioner flash = EffectsManager.GenerateEffect(fallingstarexplosionname, FightGlobalSetting.EffectPathDefine(Zokusei.Null), flashPos, Quaternion.identity, null);
                stoneFallingModels.Add(Star);
                stoneStartFlashModels.Add(flash);
                Camera.transform.DOLookAt(Star.transform.position, 1f);
                Star.transform.DOMoveY(-600, 30f);
            }
            StarComing(stoneinfo);
        }
        starfallAnimEnd = true;
    }
    
    // 跳过整个星星下落动画
    void SkipStarFallAnim()
    {
        if (starFallAnimWholeProcess != null)
        {
            StopCoroutine(starFallAnimWholeProcess);
        }
        SpeedOnce.gameObject.SetActive(false);
        starfallAnimEnd = true;
    }
    
    // 一个星星下落动画
    IEnumerator OneStarFallAnim()
    {
        oneStarfallAnimEnd = false;
        yield return new WaitForSecondsRealtime(1f);
        oneStarfallAnimEnd = true;
    }
    
    // 加速一个星星下落动画
    void SpeedOneGochaAnim()
    {
        if (starFallAnimOneProcess != null)
        {
            StopCoroutine(starFallAnimOneProcess);
        }
        oneStarfallAnimEnd = true;
    }
    #endregion
    
    #region 星星集中到屏幕动画
    public IEnumerator StarSortAnim(List<StoneOfPlayerInfo> results)
    {
        for (int i = 0; i < results.Count; i++)
        {
            IEnumerator enumerator = GachaManager.target.NineForShow.OneStoneGochaAnim(results[i], WaitPos[i].position, nineslotScreenPos[i]);
            enumerators.Add(enumerator);
            StartCoroutine(enumerator);
        }
        yield return new WaitForSecondsRealtime(2f);
    }
    #endregion
    
    // Gotcha总过程 点击画面的话进入下一个星星
    public IEnumerator GotchaAnimProcess(List<StoneOfPlayerInfo> results)
    {
        CameraManager._camera.gameObject.SetActive(false);
        Camera.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        PosDecide();
        SpeedOnce.gameObject.SetActive(true);
        GachaRender.target.Skip.gameObject.SetActive(true);
        starFallAnimWholeProcess = StartCoroutine (StarFallAnim(results));
        
        while(!starfallAnimEnd)
            yield return new WaitForSeconds(0.1f);
        
        SpeedOnce.gameObject.SetActive(false);
        Reset();
        GachaRender.target.Skip.gameObject.SetActive(false);
        yield return StarSortAnim(results);
        GachaManager.target.NineForShow.GochaResultShow(results);
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
