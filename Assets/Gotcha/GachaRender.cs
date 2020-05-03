using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using DG.Tweening;

public class GachaRender : MonoBehaviour
{
    public Camera Camera;
    public Transform SkyLightCenter;
    public float SkySphereRadius = 650;
    
    public static GachaRender target;
    
    void Awake()
    {
        target = this;
    }
    
    public IEnumerator TenGotchaAnimProcess(List<SkillStoneOfPlayerInfoModel> results)
    {
        CameraManager._camera.gameObject.SetActive(false);
        Camera.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        
        foreach (SkillStoneOfPlayerInfoModel stoneinfo in results)
        {
            Decompositioner Star = EffectAndHurtObjectLoading.Instance.GenerateEffect("gachastar", FightGlobalSetting.EffectPathDefine(Zokusei.Null), GetRandomStarPos(), Quaternion.identity, null);
            Camera.transform.DOLookAt(Star.transform.position,1f);
            Star.transform.DOMoveY(-600,30f);// 星星下坠
            yield return new WaitForSecondsRealtime(1f);
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
