using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public partial class BO_Ani_E : MonoBehaviour
{
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
    
    Quaternion SlightRotateToEnemy(Transform startT)
    {
        if (_DATA_CENTER.Sensor.GetEnemiesByDistance(true).Count > 0)
        {
            Vector3 relativePos = _DATA_CENTER.Sensor.GetEnemiesByDistance(false)[0].transform.position - startT.position;
            relativePos.y = 0;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            return Quaternion.RotateTowards(transform.rotation, rotation, 1f);
        }
        return transform.rotation;
    }

    // 这个系列的函数现在也有对重要变量myMagicForwardPath赋值的作用,所以不可以放在defaultPool里去
    // 另外这个系列的函数经常因为一些初始化流程问题忽略，它必须在模型起到展示技能或实际战斗之前执行，否则找不到特效
    public IEnumerator BasicMagicAndEffectsPathDefine(Zokusei _zokusei, string personalMagic)
    {
        myMagicForwardPath = personalMagic;
        magic_path = FightGlobalSetting.EffectPathDefine(_zokusei);
        switch(ResourceLoadingSetting.MagicLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                if (myMagicForwardPath != null)
                    yield return (EffectsManager.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL,this.myMagicForwardPath));
                yield return (EffectsManager.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL + "/Magics", magic_path));
                yield return (EffectsManager.PrepareMagicFromCach(ResourceLordSceneStarter.BundleURL + "/Magics","defaultmagic"));
            break;
            case ResourceLoadMode.Resource:

            break;
            case ResourceLoadMode.StreamingAssetAB:
                if (myMagicForwardPath != null)
                    yield return (EffectsManager.PrepareMagicFromStreamingAssets(this.myMagicForwardPath));
                yield return (EffectsManager.PrepareMagicFromStreamingAssets(magic_path));
                yield return (EffectsManager.PrepareMagicFromStreamingAssets("defaultmagic"));
            break;
        }
    }
    
    void DecideTarget(string bodypartName)
    {
        switch (bodypartName)
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
            default:
                target = _DATA_CENTER.WholeT;
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
        this.hiddenMethods.MagicForward_core(
            e.stringParameter,_DATA_CENTER.geometryCenter.position + transform.forward * e.floatParameter,
            transform.rotation,
            e.intParameter,
            null);
    }

    public void ReleasePreparedMagic(AnimationEvent e)
    {
        DecideTarget(e.stringParameter);
        hiddenMethods.ReleasePreparedMagic_core(target.position,transform.rotation,target,e.floatParameter,null);
    }
    
    public void ReleasePreparedMagicToAir(AnimationEvent e)
    {
        DecideTarget(e.stringParameter);
        hiddenMethods.ReleasePreparedMagic_core(target.position,transform.rotation,null,e.floatParameter,null);
    }

    Vector3 intPos;
    public void Bullet_shoot_from_body_part(AnimationEvent e)
	{
        DecideTarget(e.stringParameter);
        hiddenMethods.Bullet_shoot_from_Core(target.position, transform.rotation,e.intParameter, e.floatParameter,null);
    }
    
    Transform target;
    Decompositioner effect;
    ConstraintSource myConstraintSource;
    public void EffectOnBodyPart(AnimationEvent e)
	{
        DecideTarget(e.stringParameter);
		switch (e.intParameter) 
		{
			case 3:
                effect = EffectsManager.GenerateEffect("long_effect", magic_path, target.position, target.rotation,target);
			    break;
			case 1:
                effect = EffectsManager.GenerateEffect("short_effect", magic_path, target.position, target.rotation,target);
                break;
			case 2:
                effect = EffectsManager.GenerateEffect("normal_effect", magic_path, target.position, target.rotation,target);
                break;
			default:
                effect = EffectsManager.GenerateEffect("short_effect", magic_path, target.position, target.rotation,target);
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
        DecideTarget(e.stringParameter);
        hiddenMethods.BlastAttack_core(target.position,target.rotation,target,e.intParameter,null);//这部分函数直接让processingHitBox等于刚rent出来的物件，所以接下来可以直接用processingHitBox
    }
    
    string OnLoadMagic;
    void PrepareOneMagic(string magicname)
    {
        OnLoadMagic = magicname;
    }
}
