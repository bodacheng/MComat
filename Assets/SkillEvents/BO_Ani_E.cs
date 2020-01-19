using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using HittingDetection;

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
        
        public void CloseEffectsOnBodyParts(bool clearParticles)
        {
            foreach (KeyValuePair<Transform, Decompositioner> keyValuePair in Ani_E.EffectsOnBodyParts)
            {
                if (keyValuePair.Value != null)
                {
                    keyValuePair.Value.StopEmissions(clearParticles);
                }
            }
        }
    }
    
    public HiddenMethods hiddenMethods;
    public Data_Center _DATA_CENTER;

    string myMagicForwardPath;
    string magic_path;
    Transform right_hand, left_hand, right_foot, left_foot, head, tail;
    DecompositionerPool target_pool;
    IDictionary<Transform, Decompositioner> EffectsOnBodyParts = new Dictionary<Transform, Decompositioner>();
    Decompositioner processingHitBox;

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
        myMagicForwardPath = personalMagic;
        magic_path = FightGlobalSetting.EffectPathDefine(_zokusei);
        switch(ResourceLoadingSetting.Instance.MagicLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                if (myMagicForwardPath != null)
                    yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL,this.myMagicForwardPath));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL + "/Magics", magic_path));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL + "/Magics","defaultmagic"));
            break;
            case ResourceLoadMode.Resource:

            break;
            case ResourceLoadMode.StreamingAssetAB:
                if (myMagicForwardPath != null)
                    yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets(this.myMagicForwardPath));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets(magic_path));
                yield return (EffectAndHurtObjectLoading.Instance.PrepareMagicFromStreamingAssets("defaultmagic"));
            break;
        }
    }

    AudioClip audioClip;
    public void PlaySoundOnce(string soundClipName)
	{
        AudioResourceLoading.Instance.soundClipsDic.TryGetValue("Audios/effects/" + soundClipName, out audioClip);
        if (audioClip != null)
            _DATA_CENTER._AudioSource.PlayOneShot(audioClip);
	}

    public void MagicForward(AnimationEvent e)
	{
        if (string.IsNullOrEmpty(e.stringParameter))
        {
            return;
        }
        
        target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool(e.stringParameter, myMagicForwardPath, magic_path);
        if (target_pool != null)
        {
            processingHitBox = target_pool.Rent();
            processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
            processingHitBox.transform.position = _DATA_CENTER.geometryCenter.position + transform.forward * e.floatParameter;
            processingHitBox.transform.rotation = transform.rotation;            
            processingHitBox._HitBox.SetReferenceTransformInfo(processingHitBox.transform,transform);
            processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.MarkersEnablingStarts();
            if (processingHitBox._HitBox.onGroundMagic)
            {
                processingHitBox.transform.position = new Vector3(processingHitBox.transform.position.x, transform.position.y, processingHitBox.transform.position.z);
            }
            if (processingHitBox.TrackControl != null)
            {
                switch (e.intParameter)
                {
                    case 1:
                        processingHitBox.TrackControl.StartOff(processingHitBox.transform.position,processingHitBox.transform.rotation,1f);
                        break;
                    case 2:
                        processingHitBox.TrackControl.StartOff(processingHitBox.transform.position,processingHitBox.transform.rotation,1.2f);
                        break;
                    case 3:
                        processingHitBox.TrackControl.StartOff(processingHitBox.transform.position,processingHitBox.transform.rotation,1.5f);
                        break;
                    case 0:
                        processingHitBox.TrackControl.StartOff(processingHitBox.transform.position,processingHitBox.transform.rotation,1f);
                        break;
                    default:
                        processingHitBox.TrackControl.StartOff(processingHitBox.transform.position,processingHitBox.transform.rotation,1f);
                        break;
                }
            }
        }
	}

    Vector3 intPos;
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
        processingHitBox.transform.position = intPos;
        EffectAndHurtObjectLoading.Instance.GenerateEffect(processingHitBox._HitBox.muzzle, magic_path, processingHitBox.transform.position, transform.rotation, null);
        processingHitBox._HitBox.SetReferenceTransformInfo(processingHitBox.transform,transform);
        processingHitBox._HitBox._WeaponMode = WeaponMode.FlyerWeapon;
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.MarkersEnablingStarts();
        }
        if (processingHitBox.TrackControl != null)
        {
            processingHitBox.TrackControl.StartOff(intPos, transform.rotation, e.floatParameter);
        }
        if (!processingHitBox._HitBox.enabled)
            Debug.Log("奇怪");
    }

    Transform target;
    Decompositioner effect;
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
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("long_effect", magic_path, target.position, target.rotation,target);
			    break;
			case 1:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("short_effect", magic_path, target.position, target.rotation,target);
                break;
			case 2:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("normal_effect", magic_path, target.position, target.rotation,target);
                break;
			default:
                effect = EffectAndHurtObjectLoading.Instance.GenerateEffect("short_effect", magic_path, target.position, target.rotation,target);
                break;
		}
           
        if (EffectsOnBodyParts.ContainsKey(target))
        {
            if (EffectsOnBodyParts[target] != null)
            {
                EffectsOnBodyParts[target].StopEmissions(true);
                EffectsOnBodyParts[target].GetPositionConstraint().constraintActive = false;
            }
            EffectsOnBodyParts[target] = effect; 
        }
	}

    public void BlastAttack(AnimationEvent e)
	{
        switch (e.intParameter)
        {
            case 0:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("blast", myMagicForwardPath, magic_path);
                break;
            case 1:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("blast", myMagicForwardPath, magic_path);
                break;
            case 2:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("big_blast", myMagicForwardPath, magic_path);
                break;
            default:
                target_pool = EffectAndHurtObjectLoading.Instance.GetHurtObjectPool("blast", myMagicForwardPath, magic_path);
                break;
        }
        		
        processingHitBox = target_pool.Rent();
        processingHitBox._HitBox.SetOwnerFightAttriCalReference(_DATA_CENTER._FightAttriCalReference);
        switch (e.stringParameter)
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
                return;
		}
        processingHitBox.transform.position = target.position;
        processingHitBox.transform.rotation = target.rotation;
        myConstraintSource.sourceTransform = target;
        myConstraintSource.weight = 1;
        processingHitBox.GetPositionConstraint().SetSources(new List<ConstraintSource>{myConstraintSource});
        processingHitBox.GetPositionConstraint().constraintActive = true;
        processingHitBox.GetPositionConstraint().locked = true;
        processingHitBox.GetPositionConstraint().translationOffset = Vector3.zero;
        processingHitBox._HitBox._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter,transform);
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.MarkersEnablingStarts();
        }
    }

    string OnLoadMagic;
    void PrepareOneMagic(string magicname)
    {
        OnLoadMagic = magicname;
    }

    public void ReleasePreparedMagic(string part)
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
            case "center":
                target = _DATA_CENTER.geometryCenter;
                break;
            case null:
                target = _DATA_CENTER.WholeT;
                break;
            default:
                target = _DATA_CENTER.WholeT;
                break;
        }
        processingHitBox.transform.position = target.position;
        processingHitBox.transform.rotation = target.rotation;
        myConstraintSource.sourceTransform = target;
        myConstraintSource.weight = 1;
        processingHitBox.GetPositionConstraint().SetSources(new List<ConstraintSource>{myConstraintSource});
        processingHitBox.GetPositionConstraint().constraintActive = true;
        processingHitBox.GetPositionConstraint().locked = true;
        processingHitBox._HitBox._WeaponMode = WeaponMode.EnergyFromBodyWeapon;
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter,transform);
        if (_DATA_CENTER._TeamConfig != null)
        {
            processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
            processingHitBox._HitBox.MarkersEnablingStarts();
        }
        OnLoadMagic = null;
    }
    
    public void ReleasePreparedMagicToAir(string part)
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
        processingHitBox._HitBox.SetReferenceTransformInfo(_DATA_CENTER.geometryCenter,transform);
        processingHitBox._HitBox.SetTeamConfig(_DATA_CENTER._TeamConfig);
        processingHitBox._HitBox.MarkersEnablingStarts();
        OnLoadMagic = null;
    }
}
