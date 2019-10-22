using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BO_Ani_E : MonoBehaviour
{
    public Data_Center _DATA_CENTER;
    private string personalEffectsPath;
    private string myMagicForwardPath;
    private string defaultMagicForwardPath;
    private Transform right_hand, left_hand, right_foot, left_foot, head, tail;
    private DecompositionerPool target_pool;
    private IDictionary<Transform, Decompositioner> EffectsOnBodyParts = new Dictionary<Transform, Decompositioner>();
    private Decompositioner processingHitBox;
    
    private void SetBodyPartsTransform()
    {
        if (_DATA_CENTER != null)
        {
            if (_DATA_CENTER.right_hand_t != null)
            {
                this.right_hand = _DATA_CENTER.right_hand_t.transform;
                EffectsOnBodyParts.Add(this.right_hand,null);
            }
            if (_DATA_CENTER.left_hand_t != null)
            {
                this.left_hand = _DATA_CENTER.left_hand_t.transform;
                EffectsOnBodyParts.Add(this.left_hand,null);
            }
            if (_DATA_CENTER.right_foot_t != null)
            {
                this.right_foot = _DATA_CENTER.right_foot_t.transform;
                EffectsOnBodyParts.Add(this.right_foot,null);
            }
            if (_DATA_CENTER.left_foot_t != null)
            {
                this.left_foot = _DATA_CENTER.left_foot_t.transform;
                EffectsOnBodyParts.Add(this.left_foot,null);
            }
            if (_DATA_CENTER.head_t != null)
            {
                this.head = _DATA_CENTER.head_t.transform;
                EffectsOnBodyParts.Add(this.head,null);
            }
            if (_DATA_CENTER.tail_t != null)
            {
                this.tail = _DATA_CENTER.tail_t.transform;
                EffectsOnBodyParts.Add(this.tail,null);
            }
        }
    }
    
    public void CloseEffectsOnBodyParts()
    {
        foreach (KeyValuePair<Transform, Decompositioner> keyValuePair in EffectsOnBodyParts)
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value.StopEmissions();
        }
    }

    void Start()
    {
        SetBodyPartsTransform();// 设置为private目的是减少出现在inpector里的函数数量
    }

    // 这个系列的函数现在也有对重要变量myMagicForwardPath赋值的作用,所以不可以放在defaultPool里去
    // 另外这个系列的函数经常因为一些初始化流程问题忽略，它必须在模型起到展示技能或实际战斗之前执行，否则找不到特效
    public IEnumerator BasicMagicAndEffectsPathDefine(zokusei _zokusei, string personalMagic)
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

    private Vector3 magicFoward_shoot_direction;
    public void MagicForward(AnimationEvent e)
	{
        if (string.IsNullOrEmpty(e.stringParameter))
            return;

        target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool(e.stringParameter, myMagicForwardPath, defaultMagicForwardPath);
        if (target_pool != null)
        {
            processingHitBox = target_pool.Rent();
            processingHitBox.transform.position = _DATA_CENTER.geometryCenter.position + gameObject.transform.forward * e.floatParameter;
            processingHitBox.transform.rotation = transform.rotation;
            processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER.BO_Health);
            processingHitBox._HitBox.SetHolderCenter(processingHitBox.transform);
            processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            if (processingHitBox._HitBox.onGroundMagic)
                processingHitBox.transform.position = new Vector3(processingHitBox.transform.position.x,this.transform.position.y, processingHitBox.transform.position.z);
            magicFoward_shoot_direction = gameObject.transform.forward;
            magicFoward_shoot_direction.y = 0;
            if (processingHitBox.Rigidbody != null)
            {
                switch (e.intParameter)
                {
                    case 1:
                        processingHitBox.Rigidbody.velocity = magicFoward_shoot_direction.normalized * 3f;
                        break;
                    case 2:
                        processingHitBox.Rigidbody.velocity = magicFoward_shoot_direction.normalized * 8f;
                        break;
                    case 3:
                        processingHitBox.Rigidbody.velocity = magicFoward_shoot_direction.normalized * 15f;
                        break;
                    case 0:
                        processingHitBox.Rigidbody.velocity = Vector3.zero;
                        break;
                    default:
                        processingHitBox.Rigidbody.velocity = magicFoward_shoot_direction.normalized * 3f;
                        break;
                }
            }
        }
	}

    private Vector3 intPos;
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
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("bullet", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 2:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("big_bullet", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 3:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("super_bullet", myMagicForwardPath, defaultMagicForwardPath);
                break;
            default:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("bullet", myMagicForwardPath, defaultMagicForwardPath);
                break;
        }
        processingHitBox = target_pool.Rent();
        processingHitBox.transform.position = intPos;
        processingHitBox._HitBox.SetHolderCenter(processingHitBox.transform);
        processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER.BO_Health);
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
        }

        if (processingHitBox.danMuTest != null)
        {
            processingHitBox.transform.position = intPos;
            processingHitBox.danMuTest.startOff(intPos,this.transform.rotation);
        } else {
            processingHitBox.gameObject.transform.position = intPos;
            magicFoward_shoot_direction = gameObject.transform.forward;
            magicFoward_shoot_direction.y = 0;
            if (processingHitBox.Rigidbody != null)
            {
                processingHitBox.Rigidbody.velocity = magicFoward_shoot_direction.normalized * e.floatParameter;
            }
        }
    }

    private Transform target;
    private Decompositioner effect;
    public void effectOnBodyPart(AnimationEvent e)
	{
		switch(e.stringParameter)
		{
			case "right_hand":
			if (right_hand != null)
				target = right_hand;
			break;
			case "left_hand":
			if (left_hand != null)
				target = left_hand;
			break;
			case "right_foot":
			if (right_foot != null)
				target = right_foot;
			break;
			case "left_foot":
			if (left_foot != null)
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
        {
            if (EffectsOnBodyParts[target] != null)
                EffectsOnBodyParts[target].StopEmissions();
            EffectsOnBodyParts[target] = effect; 
        }
	}

    public void blastAttack(AnimationEvent e)
	{
        switch (e.intParameter)
        {
            case 0:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 1:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            case 2:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("big_blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
            default:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("blast", myMagicForwardPath, defaultMagicForwardPath);
                break;
        }
        		
        switch (e.stringParameter)
		{
			case "right_hand":
			    processingHitBox = target_pool.Rent();
                processingHitBox.transform.position = this.right_hand.position;
                processingHitBox.transform.rotation = this.right_hand.rotation;
                processingHitBox.transform.SetParent(this.right_hand);
			break;
			case "left_hand":
                processingHitBox = target_pool.Rent();
                processingHitBox.transform.position = this.left_hand.position;
                processingHitBox.transform.rotation = this.left_hand.rotation;
                processingHitBox.transform.SetParent(this.left_hand);
                break;
			case "right_foot":
                processingHitBox = target_pool.Rent();
                processingHitBox.transform.position = this.right_foot.position;
                processingHitBox.transform.rotation = this.right_foot.rotation;
                processingHitBox.transform.SetParent(this.right_foot);
                break;
			case "left_foot":
                processingHitBox = target_pool.Rent();
                processingHitBox.transform.position = this.left_foot.position;
                processingHitBox.transform.rotation = this.left_foot.rotation;
                processingHitBox.transform.SetParent(this.left_foot);
                break;
            case "head":
                processingHitBox = target_pool.Rent();
                processingHitBox.transform.position = this.head.position;
                processingHitBox.transform.rotation = this.head.rotation;
                processingHitBox.transform.SetParent(this.head);
                break;
            case "tail":
                processingHitBox = target_pool.Rent();
                processingHitBox.transform.position = this.tail.position;
                processingHitBox.transform.rotation = this.tail.rotation;
                processingHitBox.transform.SetParent(this.tail);
                break;
			default:
                return;
		}
        
        processingHitBox._HitBox._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER.BO_Health);
        processingHitBox._HitBox.SetHolderCenter(_DATA_CENTER.geometryCenter);
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.EnableMarkers();
        }
    }

    private string OnLoadMagic;
    private void PrepareOneMagic(string magicname)
    {
        OnLoadMagic = magicname;
    }
    public void releasePreparedMagic(string part)
    {
        if (OnLoadMagic == null)
            return;
        target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool(OnLoadMagic, myMagicForwardPath, defaultMagicForwardPath);
        if (target_pool == null)
            return;
        processingHitBox = target_pool.Rent();
        switch (part)
        {
            case "right_hand":
                processingHitBox.transform.position = this.right_hand.position;
                processingHitBox.transform.rotation = this.right_hand.rotation;
                processingHitBox.transform.SetParent(this.right_hand);           
                break;
            case "left_hand":
                processingHitBox.transform.position = this.left_hand.position;
                processingHitBox.transform.rotation = this.left_hand.rotation;
                processingHitBox.transform.SetParent(this.left_hand);
                break;
            case "right_foot":
                processingHitBox.transform.position = this.right_foot.position;
                processingHitBox.transform.rotation = this.right_foot.rotation;
                processingHitBox.transform.SetParent(this.right_foot);
                break;
            case "left_foot":
                processingHitBox.transform.position = this.left_foot.position;
                processingHitBox.transform.rotation = this.left_foot.rotation;
                processingHitBox.transform.SetParent(this.left_foot);     
                break;
            case "head":
                processingHitBox.transform.position = this.head.position;
                processingHitBox.transform.rotation = this.head.rotation;
                processingHitBox.transform.SetParent(this.head);                
                break;
            case "tail":
                processingHitBox.transform.position = this.tail.position;
                processingHitBox.transform.rotation = this.tail.rotation;
                processingHitBox.transform.SetParent(this.tail);   
                break;
            case "center":
                processingHitBox.transform.position = this._DATA_CENTER.geometryCenter.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                processingHitBox.transform.SetParent(this._DATA_CENTER.geometryCenter);
                break;
            case null:
                processingHitBox.transform.position = this._DATA_CENTER.WholeT.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                processingHitBox.transform.SetParent(this._DATA_CENTER.WholeT);
                break;
            default:
                processingHitBox.transform.position = this._DATA_CENTER.WholeT.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                processingHitBox.transform.SetParent(this._DATA_CENTER.WholeT);
                break;
        }
        
        processingHitBox._HitBox._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER.BO_Health);
        processingHitBox._HitBox.SetHolderCenter(_DATA_CENTER.geometryCenter);
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.EnableMarkers();
        }
        OnLoadMagic = null;
    }
    
    public void releasePreparedMagicToAir(string part)
    {
        if (OnLoadMagic == null)
            return;
        target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool(OnLoadMagic, myMagicForwardPath, defaultMagicForwardPath);
        if (target_pool == null)
            return;

        processingHitBox = target_pool.Rent();
        switch (part)
        {
            case "right_hand":
                processingHitBox.transform.position = this.right_hand.position;
                processingHitBox.transform.rotation = this.right_hand.rotation;         
                break;
            case "left_hand":
                processingHitBox.transform.position = this.left_hand.position;
                processingHitBox.transform.rotation = this.left_hand.rotation;
                break;
            case "right_foot":
                processingHitBox.transform.position = this.right_foot.position;
                processingHitBox.transform.rotation = this.right_foot.rotation;
                break;
            case "left_foot":
                processingHitBox.transform.position = this.left_foot.position;
                processingHitBox.transform.rotation = this.left_foot.rotation;  
                break;
            case "head":
                processingHitBox.transform.position = this.head.position;
                processingHitBox.transform.rotation = this.head.rotation;            
                break;
            case "tail":
                processingHitBox.transform.position = this.tail.position;
                processingHitBox.transform.rotation = this.tail.rotation;
                break;
            case "center":
                processingHitBox.transform.position = this._DATA_CENTER.geometryCenter.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                break;
            case null:
                processingHitBox.transform.position = this._DATA_CENTER.WholeT.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                break;
            default:
                processingHitBox.transform.position = this._DATA_CENTER.WholeT.position;
                processingHitBox.transform.rotation = Quaternion.identity;
                break;
        }
        
        processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER.BO_Health);
        processingHitBox._HitBox.SetHolderCenter(_DATA_CENTER.geometryCenter);
        processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
        processingHitBox._HitBox.EnableMarkers();
        OnLoadMagic = null;
    }
}
