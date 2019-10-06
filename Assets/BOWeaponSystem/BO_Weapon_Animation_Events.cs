using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HittingDetection;
using UniRx;

public class BO_Weapon_Animation_Events : MonoBehaviour {

    private TeamConfig _TeamConfig;
    private List<Transform> _Used_Targets = new List<Transform>();
    private IDictionary<Transform,Decompositioner> bodyPartsWeaponRegisterDic = new Dictionary<Transform,Decompositioner>();
    private Transform right_hand, left_hand, right_foot, left_foot, head, tail;    
    private Transform geometryCenter;
    private BO_Health myownheath;
    private ParticleSystem left_sword, right_sword;
    private damageType damageType;
    private Decompositioner target_hitbox;
    static DecompositionerPool default_hitboxPool;

    void Awake()
    {
        if (default_hitboxPool == null)
        {
            GameObject hurtObject = Resources.Load("HurtObjects/defaultmagic/d_hitbox") as GameObject;
            default_hitboxPool = new DecompositionerPool(hurtObject);
            default_hitboxPool.PreloadAsync(20, 1).Subscribe(_ => Debug.Log("已经为对象池:d_hitbox预留物件"));
        }
    }

    // 当前这个版本因为把“身体固化武器”的检测对象给联合化了。。其实关于_Used_Targets清理，
    // 在以下的ClearTargets()DisableMarkers ()EnableMarkers()
    // 三个函数中都存在重复执行，都把本模块内的_Used_Targets给多次执行clear操作了。
    // 因为武器在作为飞行道具，伤害性特效的情况下，是自己保有一个单独的_Used_Targets列表，也是靠这三个函数来连带着对其进行清空
    // 所以找不太到一种能更好统合固化武器，特效武器这方面的写法，所以干脆就保持着这个让_Used_Targets重复被clear的状态。
    public void ClearTargets()
	{
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value._HitBox.ClearTargets();
        }
    }
	public void DisableMarkers()
	{
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                default_hitboxPool.Return(keyValuePair.Value);
        }
	}
	public void EnableMarkers()
	{
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value._HitBox.EnableMarkers();
        }
	}

    public void SetDamageType(AnimationEvent e)
    {
        switch (e.intParameter)
        {
			case 0:
                damageType = damageType.slight_damage;
				break;
            case 1 :
                damageType = damageType.light_damage;
                break;
            case 2:
                damageType = damageType.heavy_damage;
                break;
			case 3:
                damageType = damageType.supper_damage;
				break;
            default:
                damageType = damageType.light_damage;
                break;
        }
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value._HitBox.SetDamageType(damageType);
        }
    }

    public void assignWeaponsFromDataCenter(BO_Health Ownheath,Transform geometryCenter,ParticleSystem left_sword, ParticleSystem right_sword, Transform right_hand,Transform left_hand,Transform right_foot,Transform left_foot,Transform head,Transform tail)
    {
        this.myownheath = Ownheath;
        this.geometryCenter = geometryCenter;
        this.right_sword = right_sword;
        this.left_sword = left_sword;
        this.right_hand = right_hand;
        this.left_hand = left_hand;
        this.right_foot = right_foot;
        this.left_foot = left_foot;
        this.head = head;
        this.tail = tail;
    }
    
    public void assignTeamFlag(TeamConfig teamConfig)
    {
        this._TeamConfig = teamConfig;
    }
    
    public void clearMarkerManagers()
    {
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                default_hitboxPool.Return(keyValuePair.Value);
        }
        turn_off_Left_energy_blade();
        turn_off_Right_energy_blade();
    }

    public void SetAllBodyMarkerManagersIn()
    {
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic)
        {
            RegisterBodyPartWeapon(keyValuePair.Key,1);
        }
    }

    //注意看0被空出来是和添加 删除有效武器列表中的0参数有关
    private void SetThisWeaponDamageTypeByNum(int heavynum, BO_Marker_Manager theweapon)
    {
        switch (heavynum)
        {
            case -1:
                damageType = damageType.slight_damage;
                break;
            case 1:
                damageType = damageType.light_damage;
                break;
            case 2:
                damageType = damageType.heavy_damage;
                break;
            case 3:
                damageType = damageType.supper_damage;
                break;
            default:
                damageType = damageType.light_damage;
                break;
        }
        theweapon.SetDamageType(damageType);
    }

    private void RegisterBodyPartWeapon(Transform t)
    {
        target_hitbox = default_hitboxPool.Rent();
        target_hitbox.transform.SetParent(t);
        target_hitbox.transform.localPosition = Vector3.zero;        
        target_hitbox._HitBox.SetTeamConfig(_TeamConfig);
        target_hitbox._HitBox.SetHolderCenter(this.geometryCenter);
        target_hitbox._HitBox.SetWeaponOwnerHealth(myownheath);
        target_hitbox._HitBox.SetDectionTargetsUnion(this._Used_Targets);
        target_hitbox._HitBox.EnableMarkers();
        bodyPartsWeaponRegisterDic[t] = target_hitbox;
    }
    
    private void RemoveBodyPartWeapon(Transform t)
    {
        if (bodyPartsWeaponRegisterDic[t] != null)
        {
            bodyPartsWeaponRegisterDic[t]._HitBox.DisableMarkers();
            bodyPartsWeaponRegisterDic[t]._HitBox.SetWeaponOwnerHealth(null);
            bodyPartsWeaponRegisterDic[t]._HitBox.SetDectionTargetsUnion(null);
            default_hitboxPool.Return(bodyPartsWeaponRegisterDic[t]);
            bodyPartsWeaponRegisterDic[t] = null;
        }
    }
    
    private void RegisterBodyPartWeapon(Transform t,int hit_type)
    {
        if (hit_type != 0)
        {
            RegisterBodyPartWeapon(t);
            SetThisWeaponDamageTypeByNum(hit_type,bodyPartsWeaponRegisterDic[t]._HitBox);
        }else{
            RemoveBodyPartWeapon(t);
        }
    }

	public void  SetRightHandMarkerManager (int in_or_out = 1)
	{
        RegisterBodyPartWeapon(right_hand,in_or_out);
    }

    public void SetLeftHandMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(left_hand,in_or_out);
    }

    public void SetLeftFootMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(left_foot,in_or_out);
    }

    public void SetRightFootMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(right_foot,in_or_out);
    }

    public void SetHeadMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(head,in_or_out);
    }

    public void SetTailMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(tail,in_or_out);
    }

    public void turn_on_Right_energy_blade()
    {
        turnRightEnergyBlade(true);
    }
    public void turn_off_Right_energy_blade()
    {
        turnRightEnergyBlade(false);
    }
    private void turnRightEnergyBlade(bool _on)
    {
        if (right_sword != null)
        {
            if (_on)
                right_sword.Play(true);
            else
                right_sword.Stop(true);
        }
    }

    public void turn_on_Left_energy_blade()
    {
    }
    public void turn_off_Left_energy_blade()
    {
    }
    private void turnLeftEnergyBlade(bool _on)
    {
        if (left_sword != null)
        {
            if (_on)
                left_sword.Play(true);
            else
                left_sword.Stop(true);
        }
    }
}
