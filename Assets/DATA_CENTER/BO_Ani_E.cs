using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.SceneManagement; // 考虑到生成对象池所用的大量时间从而加入这个以根据场景判断是不是进行对象池创建。

public class BO_Ani_E : MonoBehaviour
{
    private string personalEffectsPath;
    private string myMagicForwardPath;
    private string defaultMagicForwardPath;

    private Transform right_hand, left_hand, right_foot, left_foot, head, tail;
    private EZObjectPool bullet_pool, big_bullet_pool, super_bullet_pool;//这个程序内部烤进去就可以
    private EZObjectPool blastAttack_pool;
    private Data_Center _DATA_CENTER;
    private BO_Health my_HealthBody;

    public IEnumerator setBodyPartsTransform(Data_Center the_DATA_CENTER, BO_Health my_HealthBody)
    {
        this._DATA_CENTER = the_DATA_CENTER;
        this.my_HealthBody = my_HealthBody;
        if (_DATA_CENTER != null)
        {
            if (_DATA_CENTER.right_hand != null)
            {
                this.right_hand = _DATA_CENTER.right_hand.transform;
            }
            if (_DATA_CENTER.left_hand != null)
            {
                this.left_hand = _DATA_CENTER.left_hand.transform;
            }
            if (_DATA_CENTER.right_foot != null)
            {
                this.right_foot = _DATA_CENTER.right_foot.transform;
            }
            if (_DATA_CENTER.left_foot != null)
            {
                this.left_foot = _DATA_CENTER.left_foot.transform;
            }
            if (_DATA_CENTER.head != null)
            {
                this.head = _DATA_CENTER.head.transform;
            }
            if (_DATA_CENTER.tail != null)
            {
                this.tail = _DATA_CENTER.tail.transform;
            }
        }
        yield break;
    }
            		
	void Awake()
	{
        _AudioSource = GetComponent<AudioSource>();
    }

    //这个系列的函数现在也有对重要变量myMagicForwardPath赋值的作用,所以不可以放在defaultPool里去
    // 另外这个系列的函数经常因为一些初始化流程问题忽略，它必须在模型起到展示技能或实际战斗之前执行，否则找不到特效
    public IEnumerator basicMagicAndEffectsPathDefine(zokusei _zokusei, string personalMagic)
    {
        this.myMagicForwardPath = personalMagic;
        switch (_zokusei)
        {
            case zokusei.darkMagic:
                personalEffectsPath = "darkMagic";
                defaultMagicForwardPath = "darkmagic";
                break;
            case zokusei.blueMagic:
                personalEffectsPath = "blueMagic";
                defaultMagicForwardPath = "bluemagic";
                break;
            case zokusei.greenMagic:
                personalEffectsPath = "greenMagic";
                defaultMagicForwardPath = "greenmagic";
                break;
            case zokusei.lightMagic:
                personalEffectsPath = "lightMagic";
                defaultMagicForwardPath = "lightmagic";
                break;
            case zokusei.redMagic:
                personalEffectsPath = "redMagic";
                defaultMagicForwardPath = "redmagic";
                break;
            default:
                personalEffectsPath = "defaultEffects";
                defaultMagicForwardPath = "defaultmagic";
                break;
        }
        switch(defaultPools.Instance.MagicLoadingMode)
        {
            case ResourceLoadMode.CachAB:
            
            break;
            case ResourceLoadMode.Resource:
                if (this.myMagicForwardPath != null)
                    yield return (defaultPools.Instance.PrepareMagicFromCach(AssetBundleLoader.BundleURL,this.myMagicForwardPath));
                yield return (defaultPools.Instance.PrepareMagicFromCach(AssetBundleLoader.BundleURL + "/Magics", defaultMagicForwardPath));
                yield return (defaultPools.Instance.PrepareMagicFromCach(AssetBundleLoader.BundleURL + "/Magics","defaultmagic"));
            break;
            case ResourceLoadMode.StreamingAssetAB:
                if (this.myMagicForwardPath != null)
                    yield return (defaultPools.Instance.PrepareMagicFromStreamingAssets(this.myMagicForwardPath));
                yield return (defaultPools.Instance.PrepareMagicFromStreamingAssets(defaultMagicForwardPath));
                yield return (defaultPools.Instance.PrepareMagicFromStreamingAssets("defaultmagic"));
            break;
        }
    }

    private AudioSource _AudioSource;
    private AudioClip audioClip;
	public void playSoundOnce(string soundClipName)
	{
        defaultPools.Instance.soundClipsDic.TryGetValue("Audios/effects/" + soundClipName, out audioClip);
        if (audioClip != null)
            _AudioSource.PlayOneShot(audioClip);
	}

    private GameObject boom;
    private BO_Marker_Manager _BM;
    private Vector3 magicFoward_shoot_direction;
    private EZObjectPool _MagicForwardObjectPool;
    private Rigidbody _MagicObjectRigidbody;
    public void MagicForward(AnimationEvent e)
	{
        if (e.stringParameter == null || e.stringParameter == "")
            return;

        _MagicForwardObjectPool = defaultPools.Instance.getHurtObjectPool(e.stringParameter, myMagicForwardPath, defaultMagicForwardPath);
        if (_MagicForwardObjectPool != null)
        {
            boom = _MagicForwardObjectPool.TryGetNextObject(_DATA_CENTER.geometryCenter.position + gameObject.transform.forward * e.floatParameter,transform.rotation);
            if (boom == null)
            {
                Debug.Log("魔法伤害体"+ e.stringParameter + "对应对象池出错");
            }
            _BM = boom.GetComponent<BO_Marker_Manager>();
            if (_BM != null)
            {
                _BM.setWeaponOwnerHealth(this.my_HealthBody);
                _BM._WeaponMode = WeaponMode.FlyerWeapon;
                if (_DATA_CENTER._TeamConfig != null)
                {
                    _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                    _BM.setAT(_DATA_CENTER._playerBattleInfo.AT);
                }
                if (_BM.onGroundMagic)
                    boom.transform.position = new Vector3(boom.transform.position.x,this.transform.position.y, boom.transform.position.z);
            }
            if (e.intParameter != 0)//其实这个尽量不用，这里面的处理也没有什么逻辑合理性可言，我们就是觉得放着个参数不用少了点啥。这个的意思就是让伤害特效可以推出去。
            {
                magicFoward_shoot_direction = gameObject.transform.forward;
                magicFoward_shoot_direction.y = 0;
                _MagicObjectRigidbody = boom.GetComponent<Rigidbody>();
                if (_MagicObjectRigidbody == null)
                    _MagicObjectRigidbody = boom.AddComponent<Rigidbody>();
                _MagicObjectRigidbody.useGravity = false;
                switch (e.intParameter)
                {
                    case 1:
                        _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * 3f;
                        break;
                    case 2:
                        _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * 8f;
                        break;
                    case 3:
                        _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * 15f;
                        break;
                    default:
                        _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * 3f;
                        break;
                }
            }
        }
	}

    private Vector3 intPos;
    private Quaternion intRot;
    private GameObject bullet_clone;
    public void bullet_shoot_from_body_part(AnimationEvent e)
	{
        switch (e.stringParameter)
        {
            case "right_hand":
                intPos = right_hand.transform.position;
                //intRot = right_hand.transform.rotation;
                break;
            case "left_hand":
                intPos = left_hand.transform.position;
                //intRot = left_hand.transform.rotation;
                break;
            case "right_foot":
                intPos = right_foot.transform.position;
                //intRot = right_foot.transform.rotation;
                break;
            case "left_foot":
                intPos = left_foot.transform.position;
                //intRot = left_foot.transform.rotation;
                break;
            case "head":
                intPos = head.transform.position;
                //intRot = head.transform.rotation;
                break;
            case "tail":
                intPos = tail.transform.position;
                //intRot = tail.transform.rotation;
                break;
            default:
                intPos = _DATA_CENTER.geometryCenter.position + gameObject.transform.forward * 0.5f;
                break;
        }

        switch (e.intParameter)
        {
            case 1:
                if (bullet_pool == null)
                {
                    bullet_pool = defaultPools.Instance.getHurtObjectPool("bullet", myMagicForwardPath, defaultMagicForwardPath);
                }
                bullet_clone = bullet_pool.TryGetNextObject(intPos, intRot);
                break;
            case 2:
                if (big_bullet_pool == null)
                {
                    big_bullet_pool = defaultPools.Instance.getHurtObjectPool("big_bullet", myMagicForwardPath, defaultMagicForwardPath);
                }
                bullet_clone = big_bullet_pool.TryGetNextObject(intPos, intRot);
                break;
            case 3:
                if (super_bullet_pool == null)
                {
                    super_bullet_pool = defaultPools.Instance.getHurtObjectPool("super_bullet", myMagicForwardPath, defaultMagicForwardPath);
                }
                bullet_clone = super_bullet_pool.TryGetNextObject(intPos, intRot);
                break;
            default:
                if (bullet_pool == null)
                {
                    bullet_pool = defaultPools.Instance.getHurtObjectPool("bullet", myMagicForwardPath, defaultMagicForwardPath);
                }
                bullet_clone = bullet_pool.TryGetNextObject(intPos, intRot);
                break;
        }
        bullet_clone.gameObject.SetActive(false);
        _BM = bullet_clone.GetComponent<BO_Marker_Manager>();
        if (_BM != null)
        {
            _BM._WeaponMode = WeaponMode.FlyerWeapon;
            if (_DATA_CENTER._TeamConfig != null)
            {
                _BM.setWeaponOwnerHealth(this.my_HealthBody);
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.setAT(_DATA_CENTER._playerBattleInfo.AT);
            }
        }
        bullet_clone.gameObject.SetActive(true);//这个时候我们已经是先把武器组件的敌人层设置好了，这样如果有bullet_GPS组件，它在OnEnable()中设置自身追踪层就没问题。
        bullet_clone.transform.position = intPos;
        magicFoward_shoot_direction = gameObject.transform.forward;
        magicFoward_shoot_direction.y = 0;
        _MagicObjectRigidbody = bullet_clone.GetComponent<Rigidbody>();
        if (_MagicObjectRigidbody == null)
            _MagicObjectRigidbody = bullet_clone.AddComponent<Rigidbody>();
        _MagicObjectRigidbody.useGravity = false;
        _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * e.floatParameter;
    }

    private Transform target;
    private GameObject effect;
    public void effectOnBodyPart(AnimationEvent e)
	{
		switch(e.stringParameter)
		{
			case "right_hand":
			if (right_hand!=null)
				target = right_hand;
			break;
			case "left_hand":
			if (left_hand!=null)
				target = left_hand;
			break;
			case "right_foot":
			if (right_foot!=null)
				target = right_foot;
			break;
			case "left_foot":
			if (left_foot!=null)
				target = left_foot;
			break;
            case "head":
                if (head != null)
                    target = head;
                break;
            case "tail":
                if (tail != null)
                    target = tail;
                break;
            default:
                target = transform;
                break;
		}
		
		switch (e.intParameter) 
		{
			case 3:
                defaultPools.Instance.GenerateEffect("long_effect", personalEffectsPath, target.position, target.rotation, target);
			    return;
			case 1:
                defaultPools.Instance.GenerateEffect("short_effect", personalEffectsPath, target.position, target.rotation, target);
                return;
			case 2:
                defaultPools.Instance.GenerateEffect("normal_effect", personalEffectsPath, target.position, target.rotation, target);
                return;
			default:
                defaultPools.Instance.GenerateEffect("short_effect", personalEffectsPath, target.position, target.rotation, target);
                return;
		}
	}

    private GameObject a_blastAttackBall;
    public void blastAttack(AnimationEvent e)
	{
        switch (e.intParameter)
        {
            case 0:
                blastAttack_pool = defaultPools.Instance.getHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 1:
                blastAttack_pool = defaultPools.Instance.getHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 2:
                blastAttack_pool = defaultPools.Instance.getHurtObjectPool("big_blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            default:
                blastAttack_pool = defaultPools.Instance.getHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
        }
        		
        switch (e.stringParameter)
		{
			case "right_hand":
			    a_blastAttackBall = blastAttack_pool.TryGetNextObject (this.right_hand.position, this.right_hand.rotation);
                a_blastAttackBall.transform.SetParent(this.right_hand);
			break;
			case "left_hand":
    			a_blastAttackBall = blastAttack_pool.TryGetNextObject (this.left_hand.position, this.left_hand.rotation);
                a_blastAttackBall.transform.SetParent(this.left_hand);
                break;
			case "right_foot":
    			a_blastAttackBall = blastAttack_pool.TryGetNextObject (this.right_foot.position, this.right_foot.rotation);
                a_blastAttackBall.transform.SetParent(this.right_foot);
                break;
			case "left_foot":
    			a_blastAttackBall = blastAttack_pool.TryGetNextObject (this.left_foot.position, this.left_foot.rotation);
                a_blastAttackBall.transform.SetParent(this.left_foot);
                break;
            case "head":
                a_blastAttackBall = blastAttack_pool.TryGetNextObject(this.head.position, this.head.rotation);
                a_blastAttackBall.transform.SetParent(this.head);
                break;
            case "tail":
                a_blastAttackBall = blastAttack_pool.TryGetNextObject(this.tail.position, this.tail.rotation);
                a_blastAttackBall.transform.SetParent(this.tail);
                break;
			default:
                return;
		}
        if (_DATA_CENTER._TeamConfig != null)
		{
            _BM = a_blastAttackBall.GetComponent<BO_Marker_Manager>();
            if (_BM != null)
            {
                _BM._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
                _BM.setWeaponOwnerHealth(this.my_HealthBody);
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.setAT(_DATA_CENTER._playerBattleInfo.AT);
                _BM.setHolderCenter(_DATA_CENTER.geometryCenter);
                _BM.EnableMarkers();
            }
		}
    }

    private EZObjectPool _prepareMagicPool;
    private string OnLoadMagic = null;
    private void PrepareOneMagic(string magicname)
    {
        OnLoadMagic = magicname;
    }
    public void releasePreparedMagic(string part)
    {
        if (OnLoadMagic == null)
            return;
        _prepareMagicPool = defaultPools.Instance.getHurtObjectPool(OnLoadMagic, myMagicForwardPath, defaultMagicForwardPath);
        if (_prepareMagicPool == null)
            return;

        switch (part)
        {
            case "right_hand":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.right_hand.position, this.right_hand.rotation);
                a_blastAttackBall.transform.SetParent(this.right_hand);
                break;
            case "left_hand":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.left_hand.position, this.left_hand.rotation);
                a_blastAttackBall.transform.SetParent(this.left_hand);
                break;
            case "right_foot":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.right_foot.position, this.right_foot.rotation);
                a_blastAttackBall.transform.SetParent(this.right_foot);
                break;
            case "left_foot":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.left_foot.position, this.left_foot.rotation);
                a_blastAttackBall.transform.SetParent(this.left_foot);
                break;
            case "head":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.head.position, this.head.rotation);
                a_blastAttackBall.transform.SetParent(this.head);
                break;
            case "tail":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.tail.position, this.tail.rotation);
                a_blastAttackBall.transform.SetParent(this.tail);
                break;
            case "center":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this._DATA_CENTER.geometryCenter.position, Quaternion.identity);
                a_blastAttackBall.transform.SetParent(this._DATA_CENTER.geometryCenter);
                break;
            case null:
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this._DATA_CENTER.transform.position, Quaternion.identity);
                break;
            default:
                return;
        }
        if (_DATA_CENTER._TeamConfig != null)
        {
            _BM = a_blastAttackBall.GetComponent<BO_Marker_Manager>();
            if (_BM != null)
            {
                _BM._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
                _BM.setWeaponOwnerHealth(this.my_HealthBody);
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.setAT(_DATA_CENTER._playerBattleInfo.AT);
                _BM.setHolderCenter(_DATA_CENTER.geometryCenter);
                _BM.EnableMarkers();
            }
        }
        OnLoadMagic = null;
    }
    
    public void releasePreparedMagicToAir(string part)
    {
        if (OnLoadMagic == null)
            return;
        _prepareMagicPool = defaultPools.Instance.getHurtObjectPool(OnLoadMagic, myMagicForwardPath, defaultMagicForwardPath);
        if (_prepareMagicPool == null)
            return;

        switch (part)
        {
            case "right_hand":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.right_hand.position, this.right_hand.rotation);
                break;
            case "left_hand":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.left_hand.position, this.left_hand.rotation);
                break;
            case "right_foot":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.right_foot.position, this.right_foot.rotation);
                break;
            case "left_foot":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.left_foot.position, this.left_foot.rotation);
                break;
            case "head":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.head.position, this.head.rotation);
                break;
            case "tail":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this.tail.position, this.tail.rotation);
                break;
            case "center":
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this._DATA_CENTER.geometryCenter.position, Quaternion.identity);
                break;
            case null:
                a_blastAttackBall = _prepareMagicPool.TryGetNextObject(this._DATA_CENTER.transform.position, Quaternion.identity);
                break;
            default:
                return;
        }
        if (_DATA_CENTER._TeamConfig != null)
        {
            _BM = a_blastAttackBall.GetComponent<BO_Marker_Manager>();
            if (_BM != null)
            {
                _BM._WeaponMode = WeaponMode.FlyerWeapon;
                _BM.setWeaponOwnerHealth(this.my_HealthBody);
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.setAT(_DATA_CENTER._playerBattleInfo.AT);
                _BM.setHolderCenter(_DATA_CENTER.geometryCenter);
                _BM.EnableMarkers();
            }
        }
        OnLoadMagic = null;
    }
}
