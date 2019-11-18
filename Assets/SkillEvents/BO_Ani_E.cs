using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BO_Ani_E : MonoBehaviour
{
    public class HiddenMethods
    {
        readonly BO_Ani_E Ani_E;
        public HiddenMethods(BO_Ani_E bae)
        {
            Ani_E = bae;
        }
        
        public void SetBodyPartsTransform()
        {
            if (Ani_E._DATA_CENTER != null)
            {
                if (Ani_E._DATA_CENTER.right_hand_t != null)
                {
                    Ani_E.right_hand = Ani_E._DATA_CENTER.right_hand_t.transform;
                    Ani_E.EffectsOnBodyParts.Add(Ani_E.right_hand,null);
                }
                if (Ani_E._DATA_CENTER.left_hand_t != null)
                {
                    Ani_E.left_hand = Ani_E._DATA_CENTER.left_hand_t.transform;
                    Ani_E.EffectsOnBodyParts.Add(Ani_E.left_hand,null);
                }
                if (Ani_E._DATA_CENTER.right_foot_t != null)
                {
                    Ani_E.right_foot = Ani_E._DATA_CENTER.right_foot_t.transform;
                    Ani_E.EffectsOnBodyParts.Add(Ani_E.right_foot,null);
                }
                if (Ani_E._DATA_CENTER.left_foot_t != null)
                {
                    Ani_E.left_foot = Ani_E._DATA_CENTER.left_foot_t.transform;
                    Ani_E.EffectsOnBodyParts.Add(Ani_E.left_foot,null);
                }
                if (Ani_E._DATA_CENTER.head_t != null)
                {
                    Ani_E.head = Ani_E._DATA_CENTER.head_t.transform;
                    Ani_E.EffectsOnBodyParts.Add(Ani_E.head,null);
                }
                if (Ani_E._DATA_CENTER.tail_t != null)
                {
                    Ani_E.tail = Ani_E._DATA_CENTER.tail_t.transform;
                    Ani_E.EffectsOnBodyParts.Add(Ani_E.tail,null);
                }
            }
        }
        
        public void CloseEffectsOnBodyParts()
        {
            foreach (KeyValuePair<Transform, Decompositioner> keyValuePair in Ani_E.EffectsOnBodyParts)
            {
                if (keyValuePair.Value != null)
                    keyValuePair.Value.StopEmissions();
            }
        }
    }
    
    public HiddenMethods hiddenMethods;
    public Data_Center _DATA_CENTER;
    
    private string personalEffectsPath;
    private string myMagicForwardPath;
    private string defaultMagicForwardPath;
    private Transform right_hand, left_hand, right_foot, left_foot, head, tail;
    private DecompositionerPool target_pool;
    private IDictionary<Transform, Decompositioner> EffectsOnBodyParts = new Dictionary<Transform, Decompositioner>();
    private Decompositioner processingHitBox;

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
    }

    void Start()
    {
        hiddenMethods.SetBodyPartsTransform();// 设置为private目的是减少出现在inpector里的函数数量
    }

    // 这个系列的函数现在也有对重要变量myMagicForwardPath赋值的作用,所以不可以放在defaultPool里去
    // 另外这个系列的函数经常因为一些初始化流程问题忽略，它必须在模型起到展示技能或实际战斗之前执行，否则找不到特效
    public IEnumerator BasicMagicAndEffectsPathDefine(Zokusei _zokusei, string personalMagic)
    {
        this.myMagicForwardPath = personalMagic;
        switch (_zokusei)
        {
            case Zokusei.darkMagic:
                personalEffectsPath = "darkMagic";
                defaultMagicForwardPath = "darkmagic";
                break;
            case Zokusei.blueMagic:
                personalEffectsPath = "blueMagic";
                defaultMagicForwardPath = "bluemagic";
                break;
            case Zokusei.greenMagic:
                personalEffectsPath = "greenMagic";
                defaultMagicForwardPath = "greenmagic";
                break;
            case Zokusei.lightMagic:
                personalEffectsPath = "lightMagic";
                defaultMagicForwardPath = "lightmagic";
                break;
            case Zokusei.redMagic:
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
	public void PlaySoundOnce(string soundClipName)
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
            processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
            processingHitBox._HitBox.SetReferenceTransformInfo(processingHitBox.transform,transform);
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
    public void Bullet_shoot_from_body_part(AnimationEvent e)
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
        processingHitBox._HitBox.SetReferenceTransformInfo(processingHitBox.transform,transform);
        processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
        }

        if (processingHitBox.danMuTest != null)
        {
            processingHitBox.transform.position = intPos;
            processingHitBox.danMuTest.StartOff(intPos,this.transform.rotation);
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
    ConstraintSource myConstraintSource;
    public void EffectOnBodyPart(AnimationEvent e)
	{
		switch(e.stringParameter)
		{
			case "right_hand":
				target = right_hand;
			break;
			case "left_hand":
				target = left_hand;
			break;
			case "right_foot":
				target = right_foot;
			break;
			case "left_foot":
				target = left_foot;
			break;
            case "head":
                target = head;
            break;
            case "tail":
                target = tail;
            break;
            default:
                target = transform;
            break;
		}
		
		switch (e.intParameter) 
		{
			case 3:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("long_effect", personalEffectsPath, target.position, target.rotation,target);
			    break;
			case 1:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("short_effect", personalEffectsPath, target.position, target.rotation,target);
                break;
			case 2:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("normal_effect", personalEffectsPath, target.position, target.rotation,target);
                break;
			default:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("short_effect", personalEffectsPath, target.position, target.rotation,target);
                break;
		}
           
        if (EffectsOnBodyParts.ContainsKey(target))
        {
            if (EffectsOnBodyParts[target] != null)
            {
                EffectsOnBodyParts[target].StopEmissions();
                EffectsOnBodyParts[target].positionConstraint.constraintActive = false;
            }
            EffectsOnBodyParts[target] = effect; 
        }
	}

    public void BlastAttack(AnimationEvent e)
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
        		
        processingHitBox = target_pool.Rent();
        switch (e.stringParameter)
		{
			case "right_hand":
                target = this.right_hand;
			break;
			case "left_hand":
                target = this.left_hand;
                break;
			case "right_foot":
                target = this.right_foot;
                break;
			case "left_foot":
                target = this.left_foot;
                break;
            case "head":
                target = this.head;
                break;
            case "tail":
                target = this.tail;
                break;
			default:
                return;
		}
        processingHitBox.transform.position = this.target.position;
        processingHitBox.transform.rotation = this.target.rotation;
        myConstraintSource.sourceTransform = target;
        myConstraintSource.weight = 1;
        processingHitBox.positionConstraint.SetSources(new List<ConstraintSource>{myConstraintSource});
        processingHitBox.positionConstraint.constraintActive = true;
        processingHitBox.positionConstraint.locked = true;
        processingHitBox.positionConstraint.translationOffset = Vector3.zero;
        processingHitBox._HitBox._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter,transform);
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

    public void ReleasePreparedMagic(string part)
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
                target = this.right_hand;
            break;
            case "left_hand":
                target = this.left_hand;
                break;
            case "right_foot":
                target = this.right_foot;
                break;
            case "left_foot":
                target = this.left_foot;
                break;
            case "head":
                target = this.head;
                break;
            case "tail":
                target = this.tail;
                break;
            case null:
                target = this._DATA_CENTER.WholeT;
                break;
            default:
                target = this._DATA_CENTER.WholeT;
                break;
        }
        processingHitBox.transform.position = target.position;
        processingHitBox.transform.rotation = target.rotation;
        myConstraintSource.sourceTransform = target;
        myConstraintSource.weight = 1;
        processingHitBox.positionConstraint.SetSources(new List<ConstraintSource>{myConstraintSource});
        processingHitBox.positionConstraint.constraintActive = true;
        processingHitBox.positionConstraint.locked = true;
        processingHitBox._HitBox._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter,transform);
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.EnableMarkers();
        }
        OnLoadMagic = null;
    }
    
    public void ReleasePreparedMagicToAir(string part)
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
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter,transform);
        processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
        processingHitBox._HitBox.EnableMarkers();
        OnLoadMagic = null;
    }
}
