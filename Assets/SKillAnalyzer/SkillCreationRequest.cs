#if UNITY_EDITOR
using System;
using Skill;
using UnityEngine;

/// <summary>Shared input for the creation form and automated skill authoring.</summary>
[Serializable]
public sealed class SkillCreationRequest
{
    public bool autoId = true;
    public string recordId;
    public string type = "human";
    public string realName;
    public BehaviorType behaviorType = BehaviorType.GR;
    public int spLevel;
    public float attackWeight = 1f;
    public float hpWeight = 3f;
    public string eventCode;
    public bool allowSharedRealName;
    public float aiMin = 0.2f;
    public float aiMax = 5f;
    public int aiHeight;
    public string enName;
    public string jpName;
    public string cnName;
    public string enIntro;
    public string jpIntro;
    public string cnIntro;
    public float estimatedDamage = 1f;
    public int attackCount = 1;
    public float estimatedHp = 3f;
    public bool createIconAsset = true;
    public Texture2D iconTemplate;
    public bool createAnimationAsset = true;
    public AnimationClip animationTemplate;
    public bool registerAddressables = true;
    public Color placeholderIconColor = new Color32(78, 88, 110, 255);

    // Null keeps the template events. A non-null array replaces events on a NEW clone only.
    public AnimationEvent[] animationEvents;
}

[Serializable]
public sealed class SkillCreationResult
{
    public string recordId;
    public string animationAssetPath;
    public string iconAssetPath;
    public string animationAddress;
}
#endif
