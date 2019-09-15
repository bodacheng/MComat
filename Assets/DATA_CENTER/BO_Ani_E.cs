using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using HittingDetection;

public class BO_Ani_E : MonoBehaviour
{
    public Data_Center _DATA_CENTER;
    
    private string personalEffectsPath;
    private string myMagicForwardPath;
    private string defaultMagicForwardPath;
    private Transform right_hand, left_hand, right_foot, left_foot, head, tail;
    private EZObjectPool target_pool;
    private IDictionary<Transform, GameObject> EffectsOnBodyParts = new Dictionary<Transform, GameObject>();
    
    private GameObject processingObject;

    private void setBodyPartsTransform()
    {
        if (_DATA_CENTER != null)
        {
            if (_DATA_CENTER.right_hand != null)
            {
                this.right_hand = _DATA_CENTER.right_hand.transform;
                EffectsOnBodyParts.Add(this.right_hand,null);
            }
            if (_DATA_CENTER.left_hand != null)
            {
                this.left_hand = _DATA_CENTER.left_hand.transform;
                EffectsOnBodyParts.Add(this.left_hand,null);
            }
            if (_DATA_CENTER.right_foot != null)
            {
                this.right_foot = _DATA_CENTER.right_foot.transform;
                EffectsOnBodyParts.Add(this.right_foot,null);
            }
            if (_DATA_CENTER.left_foot != null)
            {
                this.left_foot = _DATA_CENTER.left_foot.transform;
                EffectsOnBodyParts.Add(this.left_foot,null);
            }
            if (_DATA_CENTER.head != null)
            {
                this.head = _DATA_CENTER.head.transform;
                EffectsOnBodyParts.Add(this.head,null);
            }
            if (_DATA_CENTER.tail != null)
            {
                this.tail = _DATA_CENTER.tail.transform;
                EffectsOnBodyParts.Add(this.tail,null);
            }
        }
    }
    
    public void closeEffectsOnBodyParts()
    {
        foreach (KeyValuePair<Transform, GameObject> keyValuePair in EffectsOnBodyParts)
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value.SetActive(false); 
        }
    }

    void Start()
    {
        setBodyPartsTransform();
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
        switch(ResourceLoadingSetting.Instance.MagicLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                if (this.myMagicForwardPath != null)
                    yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL,this.myMagicForwardPath));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL + "/Magics", defaultMagicForwardPath));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL + "/Magics","defaultmagic"));
            break;
            case ResourceLoadMode.Resource:

            break;
            case ResourceLoadMode.StreamingAssetAB:
                if (this.myMagicForwardPath != null)
                    yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets(this.myMagicForwardPath));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets(defaultMagicForwardPath));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets("defaultmagic"));
            break;
        }
    }

    private AudioClip audioClip;
	public void playSoundOnce(string soundClipName)
	{
        AudioResourceLoading.Instance.soundClipsDic.TryGetValue("Audios/effects/" + soundClipName, out audioClip);
        if (audioClip != null)
            _DATA_CENTER._AudioSource.PlayOneShot(audioClip);
	}

    private BO_Marker_Manager _BM;
    private Vector3 magicFoward_shoot_direction;
    private Rigidbody _MagicObjectRigidbody;
    public void MagicForward(AnimationEvent e)
	{
        if (e.stringParameter == null || e.stringParameter == "")
            return;

        target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool(e.stringParameter, myMagicForwardPath, defaultMagicForwardPath);
        if (target_pool != null)
        {
            processingObject = target_pool.TryGetNextObject(_DATA_CENTER.geometryCenter.position + gameObject.transform.forward * e.floatParameter,transform.rotation);
            _BM = processingObject.GetComponent<BO_Marker_Manager>();
            if (_BM != null)
            {
                _BM.setWeaponOwnerHealth(_DATA_CENTER.BO_Health);
                _BM._WeaponMode = WeaponMode.FlyerWeapon;
                if (_DATA_CENTER._TeamConfig != null)
                {
                    _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                }
                if (_BM.onGroundMagic)
                    processingObject.transform.position = new Vector3(processingObject.transform.position.x,this.transform.position.y, processingObject.transform.position.z);
            }

            magicFoward_shoot_direction = gameObject.transform.forward;
            magicFoward_shoot_direction.y = 0;
            _MagicObjectRigidbody = processingObject.GetComponent<Rigidbody>();
            if (_MagicObjectRigidbody == null)
                _MagicObjectRigidbody = processingObject.AddComponent<Rigidbody>();
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
                case 0:
                    _MagicObjectRigidbody.velocity = Vector3.zero;
                    break;
                default:
                    _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * 3f;
                    break;
            }
        }
	}

    private Vector3 intPos;
    private Quaternion intRot;
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
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("bullet", myMagicForwardPath, defaultMagicForwardPath);
                processingObject = target_pool.TryGetNextObject(intPos, intRot);
                break;
            case 2:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("big_bullet", myMagicForwardPath, defaultMagicForwardPath);
                processingObject = target_pool.TryGetNextObject(intPos, intRot);
                break;
            case 3:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("super_bullet", myMagicForwardPath, defaultMagicForwardPath);
                processingObject = target_pool.TryGetNextObject(intPos, intRot);
                break;
            default:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("bullet", myMagicForwardPath, defaultMagicForwardPath);
                processingObject = target_pool.TryGetNextObject(intPos, intRot);
                break;
        }
        processingObject.gameObject.SetActive(false);
        _BM = processingObject.GetComponent<BO_Marker_Manager>();
        if (_BM != null)
        {
            _BM._WeaponMode = WeaponMode.FlyerWeapon;
            if (_DATA_CENTER._TeamConfig != null)
            {
                _BM.setWeaponOwnerHealth(_DATA_CENTER.BO_Health);
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
            }
        }

        DanMuTest danMuTest = processingObject.GetComponent<DanMuTest>();
        if (danMuTest)
        {
            processingObject.transform.position = intPos;
            danMuTest.startOff(intPos,this.transform.rotation);
            processingObject.SetActive(true);//这个时候我们已经是先把武器组件的敌人层设置好了，这样如果有bullet_GPS组件，它在OnEnable()中设置自身追踪层就没问题。
        }
        else{
            processingObject.SetActive(true);//这个时候我们已经是先把武器组件的敌人层设置好了，这样如果有bullet_GPS组件，它在OnEnable()中设置自身追踪层就没问题。
            processingObject.transform.position = intPos;
            magicFoward_shoot_direction = gameObject.transform.forward;
            magicFoward_shoot_direction.y = 0;
            _MagicObjectRigidbody = processingObject.GetComponent<Rigidbody>();
            if (_MagicObjectRigidbody == null)
                _MagicObjectRigidbody = processingObject.AddComponent<Rigidbody>();
            _MagicObjectRigidbody.useGravity = false;
            _MagicObjectRigidbody.velocity = magicFoward_shoot_direction.normalized * e.floatParameter;
        }
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
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("long_effect", personalEffectsPath, target.position, target.rotation, target);
			    break;
			case 1:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("short_effect", personalEffectsPath, target.position, target.rotation, target);
                break;
			case 2:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("normal_effect", personalEffectsPath, target.position, target.rotation, target);
                break;
			default:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("short_effect", personalEffectsPath, target.position, target.rotation, target);
                break;
		}
        if (EffectsOnBodyParts.ContainsKey(target))
            EffectsOnBodyParts[target] = effect;
	}

    public void blastAttack(AnimationEvent e)
	{
        switch (e.intParameter)
        {
            case 0:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 1:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 2:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("big_blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            default:
                target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
        }
        		
        switch (e.stringParameter)
		{
			case "right_hand":
			    processingObject = target_pool.TryGetNextObject (this.right_hand.position, this.right_hand.rotation);
                processingObject.transform.SetParent(this.right_hand);
			break;
			case "left_hand":
    			processingObject = target_pool.TryGetNextObject (this.left_hand.position, this.left_hand.rotation);
                processingObject.transform.SetParent(this.left_hand);
                break;
			case "right_foot":
    			processingObject = target_pool.TryGetNextObject (this.right_foot.position, this.right_foot.rotation);
                processingObject.transform.SetParent(this.right_foot);
                break;
			case "left_foot":
    			processingObject = target_pool.TryGetNextObject (this.left_foot.position, this.left_foot.rotation);
                processingObject.transform.SetParent(this.left_foot);
                break;
            case "head":
                processingObject = target_pool.TryGetNextObject(this.head.position, this.head.rotation);
                processingObject.transform.SetParent(this.head);
                break;
            case "tail":
                processingObject = target_pool.TryGetNextObject(this.tail.position, this.tail.rotation);
                processingObject.transform.SetParent(this.tail);
                break;
			default:
                return;
		}
        
        _BM = processingObject.GetComponent<BO_Marker_Manager>();
        if (_BM != null)
        {
            _BM._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
            _DATA_CENTER.bO_Weapon_Animation_Events.addMarkerManagerToUsingList(_BM);
            _BM.setWeaponOwnerHealth(_DATA_CENTER.BO_Health);
            _BM.setHolderCenter(_DATA_CENTER.geometryCenter);
            if (_DATA_CENTER._TeamConfig != null)
            {
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.EnableMarkers();
            }
        }
    }

    private string OnLoadMagic = null;
    private void PrepareOneMagic(string magicname)
    {
        OnLoadMagic = magicname;
    }
    public void releasePreparedMagic(string part)
    {
        if (OnLoadMagic == null)
            return;
        target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool(OnLoadMagic, myMagicForwardPath, defaultMagicForwardPath);
        if (target_pool == null)
            return;

        switch (part)
        {
            case "right_hand":
                processingObject = target_pool.TryGetNextObject(this.right_hand.position, this.right_hand.rotation);
                processingObject.transform.SetParent(this.right_hand);
                break;
            case "left_hand":
                processingObject = target_pool.TryGetNextObject(this.left_hand.position, this.left_hand.rotation);
                processingObject.transform.SetParent(this.left_hand);
                break;
            case "right_foot":
                processingObject = target_pool.TryGetNextObject(this.right_foot.position, this.right_foot.rotation);
                processingObject.transform.SetParent(this.right_foot);
                break;
            case "left_foot":
                processingObject = target_pool.TryGetNextObject(this.left_foot.position, this.left_foot.rotation);
                processingObject.transform.SetParent(this.left_foot);
                break;
            case "head":
                processingObject = target_pool.TryGetNextObject(this.head.position, this.head.rotation);
                processingObject.transform.SetParent(this.head);
                break;
            case "tail":
                processingObject = target_pool.TryGetNextObject(this.tail.position, this.tail.rotation);
                processingObject.transform.SetParent(this.tail);
                break;
            case "center":
                processingObject = target_pool.TryGetNextObject(this._DATA_CENTER.geometryCenter.position, Quaternion.identity);
                processingObject.transform.SetParent(this._DATA_CENTER.geometryCenter);
                break;
            case null:
                processingObject = target_pool.TryGetNextObject(this._DATA_CENTER.WholeT.position, Quaternion.identity);
                processingObject.transform.SetParent(this._DATA_CENTER.WholeT);
                break;
            default:
                processingObject = target_pool.TryGetNextObject(this._DATA_CENTER.WholeT.position, Quaternion.identity);
                processingObject.transform.SetParent(this._DATA_CENTER.WholeT);
                break;
        }
        
        _BM = processingObject.GetComponent<BO_Marker_Manager>();
        if (_BM != null)
        {
            _BM._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
            _DATA_CENTER.bO_Weapon_Animation_Events.addMarkerManagerToUsingList(_BM);
            _BM.setWeaponOwnerHealth(_DATA_CENTER.BO_Health);
            _BM.setHolderCenter(_DATA_CENTER.geometryCenter);
            if (_DATA_CENTER._TeamConfig != null)
            {
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.EnableMarkers();
            }
        }
        OnLoadMagic = null;
    }
    
    public void releasePreparedMagicToAir(string part)
    {
        if (OnLoadMagic == null)
            return;
        target_pool = EffectAndHurtObjectLoading.Instance.getHurtObjectPool(OnLoadMagic, myMagicForwardPath, defaultMagicForwardPath);
        if (target_pool == null)
            return;

        switch (part)
        {
            case "right_hand":
                processingObject = target_pool.TryGetNextObject(this.right_hand.position, this.right_hand.rotation);
                break;
            case "left_hand":
                processingObject = target_pool.TryGetNextObject(this.left_hand.position, this.left_hand.rotation);
                break;
            case "right_foot":
                processingObject = target_pool.TryGetNextObject(this.right_foot.position, this.right_foot.rotation);
                break;
            case "left_foot":
                processingObject = target_pool.TryGetNextObject(this.left_foot.position, this.left_foot.rotation);
                break;
            case "head":
                processingObject = target_pool.TryGetNextObject(this.head.position, this.head.rotation);
                break;
            case "tail":
                processingObject = target_pool.TryGetNextObject(this.tail.position, this.tail.rotation);
                break;
            case "center":
                processingObject = target_pool.TryGetNextObject(this._DATA_CENTER.geometryCenter.position, Quaternion.identity);
                break;
            case null:
                processingObject = target_pool.TryGetNextObject(this._DATA_CENTER.transform.position, Quaternion.identity);
                break;
            default:
                processingObject = target_pool.TryGetNextObject(this._DATA_CENTER.WholeT.position, Quaternion.identity);
                break;
        }
        
        _BM = processingObject.GetComponent<BO_Marker_Manager>();
        if (_BM != null)
        {
            _BM._WeaponMode = WeaponMode.FlyerWeapon;
            _DATA_CENTER.bO_Weapon_Animation_Events.addMarkerManagerToUsingList(_BM);
            _BM.setWeaponOwnerHealth(_DATA_CENTER.BO_Health);
            _BM.setHolderCenter(_DATA_CENTER.geometryCenter);
            if (_DATA_CENTER._TeamConfig != null)
            {
                _BM.setTeamConfig(_DATA_CENTER._TeamConfig);
                _BM.EnableMarkers();
            }
        }
        OnLoadMagic = null;
    }
}
