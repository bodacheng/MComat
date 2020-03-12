
using UnityEngine;
using HittingDetection;

public partial class BO_Ani_E : MonoBehaviour
{
    public void Bullet_shoot_from_Transform(Transform startPoint,int grade,float speed)
    {
        switch (grade)
        {
            case 1:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("bullet", myMagicForwardPath, magic_path);
                break;
            case 2:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("big_bullet", myMagicForwardPath, magic_path);
                break;
            case 3:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("super_bullet", myMagicForwardPath, magic_path);
                break;
            default:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("bullet", myMagicForwardPath, magic_path);
                break;
        }
        processingHitBox = target_pool.Rent();
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
        processingHitBox.transform.position = startPoint.position;
        processingHitBox.transform.rotation = startPoint.rotation;
        EffectAndHurtObjectLoading.Instance.GenerateEffect(processingHitBox._HitBox.muzzle, magic_path, processingHitBox.transform.position, transform.rotation, null);
        processingHitBox._HitBox.SetReferenceTransformInfo(processingHitBox.transform);
        processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.MarkersEnablingStarts();
        }
        if (processingHitBox.TrackControl != null)
        {
            processingHitBox.TrackControl.StartOff(processingHitBox.transform.position, processingHitBox.transform.rotation, speed);
        }
    }
    
    public void ReleasePreparedMagicToAir_Special(string part)
    {
        if (OnLoadMagic == null)
            return;
        target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool(OnLoadMagic, myMagicForwardPath, magic_path);
        if (target_pool == null)
            return;

        processingHitBox = target_pool.Rent();
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
        switch (part)
        {
            case "right_hand":
                processingHitBox.transform.position = right_hand.position;
                processingHitBox.transform.rotation = right_hand.rotation;
                break;
            case "left_hand":
                processingHitBox.transform.position = left_hand.position;
                processingHitBox.transform.rotation = left_hand.rotation;
                break;
            case "right_foot":
                processingHitBox.transform.position = right_foot.position;
                processingHitBox.transform.rotation = right_foot.rotation;
                break;
            case "left_foot":
                processingHitBox.transform.position = left_foot.position;
                processingHitBox.transform.rotation = left_foot.rotation;  
                break;
            case "head":
                processingHitBox.transform.position = head.position;
                processingHitBox.transform.rotation = head.rotation;            
                break;
            case "tail":
                processingHitBox.transform.position = tail.position;
                processingHitBox.transform.rotation = tail.rotation;
                break;
            case "center":
                processingHitBox.transform.position = _DATA_CENTER.geometryCenter.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                break;
            case null:
                processingHitBox.transform.position = _DATA_CENTER.WholeT.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                break;
            default:
                processingHitBox.transform.position = _DATA_CENTER.WholeT.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                break;
        }
        
        processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter);
        processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
        processingHitBox._HitBox.MarkersEnablingStarts();
        if (processingHitBox.TrackControl != null)
        {
            processingHitBox.TrackControl.StartOff(intPos, transform.rotation, 1);
        }
        OnLoadMagic = null;
    }
}
