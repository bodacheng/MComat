using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using DG.Tweening;

public class GachaManager : MonoBehaviour
{
    public Camera Camera;
    public Transform SkyLightCenter;
    public float SkySphereRadius = 1950f;

    public static GachaManager target;
    
    void Awake()
    {
        target = this;
    }
    
    public IEnumerator Process()
    {
        Camera.transform.position = SkyLightCenter.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.up, Vector3.up);
        transform.rotation = rotation; //  相机面朝天
        yield return new WaitForSecondsRealtime(2f);
        List<SkillStoneOfPlayerInfoModel> TenTimesGotcha = SkillConfigTable.TenTimesGotcha("human");      
        foreach (SkillStoneOfPlayerInfoModel stoneinfo in TenTimesGotcha)
        {
            Decompositioner Star = EffectAndHurtObjectLoading.Instance.GenerateEffect("long_effect", FightGlobalSetting.EffectPathDefine(Zokusei.redMagic), GetRandomStarPos(), Quaternion.identity, null);
            Camera.transform.DORotate(Star.transform.position - Camera.transform.position,1f);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
        
    Vector3 GetRandomStarPos()
    {
        float xzDisFromCenter = Random.Range(0, SkySphereRadius / 2);
        Vector3 temp = SkyLightCenter.transform.position + (Vector3.forward * Random.Range(0, 100) + Vector3.right * Random.Range(0, 100)).normalized * xzDisFromCenter;
        float tempheight = Mathf.Sqrt(Mathf.Pow(SkySphereRadius, 2) - Mathf.Pow(xzDisFromCenter, 2));
        Vector3 finalPos = temp + (int)tempheight * Vector3.up;
        return finalPos;
    }
}