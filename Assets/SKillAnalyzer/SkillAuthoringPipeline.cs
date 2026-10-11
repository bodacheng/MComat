#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skill;
using UnityEditor;
using UnityEngine;

/// <summary>A portable, reviewable plan. Times are seconds in the source clip, never normalized time.</summary>
[Serializable]
public sealed class SkillAuthoringPlan
{
    public int schemaVersion = 1;
    public string sourceAssetPath;
    public string sourceGuid;
    public long sourceLocalFileId;
    public string sourceDependencyHash;
    public string rigAssetPath;
    public string rigDependencyHash;
    public string clipName;
    public float duration;
    public float frameRate;
    public string type = "human";
    public string realName;
    public string recordId;
    public string attackType = "GR";
    public int spLevel;
    public float attackWeight = 1f;
    public float hpWeight = 3f;
    public float aiMin = 0.2f;
    public float aiMax = 3f;
    public int aiHeight;
    public string enName;
    public string jpName;
    public string cnName;
    public string enIntro;
    public string jpIntro;
    public string cnIntro;
    public string iconAssetPath;
    public float cancelTime;
    public string reviewNotes;
    public List<SkillAuthoringHit> hits = new List<SkillAuthoringHit>();
    public SkillMotionAnalysisReport analysis;
}

[Serializable]
public sealed class SkillAuthoringHit
{
    public bool enabled = true;
    public string functionName;
    public float startTime;
    public float endTime;
    public int damageType = 1;
    public float confidence;
    public string reason;
}

[Serializable]
public sealed class SkillAuthoringValidation
{
    public bool valid;
    public int attackCount;
    public float estimatedDamage;
    public List<string> errors = new List<string>();
    public List<string> warnings = new List<string>();
}

public static class SkillAuthoringPipeline
{
    private static readonly HashSet<string> LimbEvents = new HashSet<string>(StringComparer.Ordinal)
    {
        "SetRightHandMarkerManager", "SetLeftHandMarkerManager",
        "SetRightFootMarkerManager", "SetLeftFootMarkerManager",
        "SetHeadMarkerManager", "SetTailMarkerManager"
    };

    public static SkillAuthoringPlan Analyze(AnimationClip clip, GameObject rig)
    {
        if (clip == null || rig == null) throw new ArgumentException("请选择动作 Clip 和项目角色 Prefab。");
        var sourcePath = AssetDatabase.GetAssetPath(clip);
        var rigPath = AssetDatabase.GetAssetPath(rig);
        if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(rigPath))
            throw new ArgumentException("动作和角色必须是已保存的项目资源。");
        if (clip.legacy || !clip.isHumanMotion)
            throw new ArgumentException("此流程需要 Humanoid 动作；请先在导入设置中配置 Avatar。");
        var report = SkillMotionAnalyzer.Analyze(clip, rig);
        if (!report.succeeded) throw new InvalidOperationException(report.error);
        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(clip, out string guid, out long localId);
        var plan = new SkillAuthoringPlan
        {
            sourceAssetPath = sourcePath, sourceGuid = guid, sourceLocalFileId = localId,
            sourceDependencyHash = AssetDatabase.GetAssetDependencyHash(sourcePath).ToString(),
            rigAssetPath = rigPath, rigDependencyHash = AssetDatabase.GetAssetDependencyHash(rigPath).ToString(),
            clipName = clip.name, duration = clip.length, frameRate = clip.frameRate,
            realName = SanitizeName(clip.name) + "_skill", enName = clip.name, jpName = clip.name, cnName = clip.name,
            analysis = report
        };
        foreach (var candidate in report.candidates)
            plan.hits.Add(new SkillAuthoringHit
            {
                functionName = candidate.functionName, startTime = candidate.startTime, endTime = candidate.endTime,
                confidence = candidate.confidence, reason = candidate.reason,
                enabled = candidate.confidence >= SkillMotionAnalyzer.RecommendedConfidence
                    && candidate.endTime <= clip.length - 2f / Mathf.Max(1f, clip.frameRate)
            });
        // Select strongest non-overlapping suggestions; all rejected candidates remain inspectable.
        var selected = new List<SkillAuthoringHit>();
        foreach (var hit in plan.hits.OrderByDescending(x => x.confidence))
        {
            if (!hit.enabled) continue;
            if (selected.Any(x => hit.startTime < x.endTime && hit.endTime > x.startTime)) hit.enabled = false;
            else selected.Add(hit);
        }
        var lastEnd = selected.Count == 0 ? clip.length * 0.6f : selected.Max(x => x.endTime);
        plan.cancelTime = Mathf.Min(clip.length - 1f / Mathf.Max(1f, clip.frameRate), lastEnd + 0.12f);
        return plan;
    }

    public static SkillAuthoringPlan Read(string path)
    {
        var plan = JsonUtility.FromJson<SkillAuthoringPlan>(File.ReadAllText(path));
        if (plan == null) throw new InvalidDataException("无法读取技能方案 JSON。");
        return plan;
    }

    public static void Write(string path, object value)
    {
        var directory = Path.GetDirectoryName(Path.GetFullPath(path));
        Directory.CreateDirectory(directory);
        File.WriteAllText(path, JsonUtility.ToJson(value, true) + "\n", new System.Text.UTF8Encoding(false));
    }

    public static AnimationClip ResolveClip(string assetPath, string clipName = null, long localId = 0)
    {
        var clips = AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<AnimationClip>()
            .Where(x => !x.name.StartsWith("__preview__", StringComparison.Ordinal)).ToList();
        if (localId != 0)
            clips = clips.Where(x => AssetDatabase.TryGetGUIDAndLocalFileIdentifier(x, out string _, out long id) && id == localId).ToList();
        if (!string.IsNullOrEmpty(clipName)) clips = clips.Where(x => x.name == clipName).ToList();
        if (clips.Count != 1)
            throw new InvalidDataException($"必须精确选定一个 Clip：{assetPath}。匹配数 {clips.Count}，请指定 clipName/localFileId。");
        return clips[0];
    }

    public static SkillAuthoringValidation Validate(SkillAuthoringPlan plan)
    {
        var result = ValidateTiming(plan);
        if (plan == null) return result;
        AnimationClip clip = null;
        try
        {
            if (string.IsNullOrEmpty(plan.sourceAssetPath) || !plan.sourceAssetPath.StartsWith("Assets/", StringComparison.Ordinal))
                throw new InvalidDataException("sourceAssetPath 必须是项目 Assets 内的资源。");
            clip = ResolveClip(plan.sourceAssetPath, plan.clipName, plan.sourceLocalFileId);
            if (clip.legacy || !clip.isHumanMotion) result.errors.Add("此流程只支持 Humanoid 动作。");
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(clip, out string guid, out long id);
            if (guid != plan.sourceGuid || id != plan.sourceLocalFileId)
                result.errors.Add("源动作的 GUID/localFileId 已变化，请重新分析。");
            if (AssetDatabase.GetAssetDependencyHash(plan.sourceAssetPath).ToString() != plan.sourceDependencyHash)
                result.errors.Add("源动作或导入设置已变化，请重新分析后再应用。");
            if (Mathf.Abs(clip.length - plan.duration) > 0.0001f || Mathf.Abs(clip.frameRate - plan.frameRate) > 0.0001f)
                result.errors.Add("方案中的动作长度/帧率与源资源不符。");
            if (clip.events.Length > 0)
                result.warnings.Add("新技能仅使用方案生成的近战事件；源动作的其他事件不会复制。原素材保持不变。");
        }
        catch (Exception ex) { result.errors.Add(ex.Message); }

        var rig = string.IsNullOrEmpty(plan.rigAssetPath) ? null : AssetDatabase.LoadAssetAtPath<GameObject>(plan.rigAssetPath);
        if (rig == null) result.errors.Add("找不到角色 Prefab，请填写 rigAssetPath。");
        else
        {
            if (AssetDatabase.GetAssetDependencyHash(plan.rigAssetPath).ToString() != plan.rigDependencyHash)
                result.errors.Add("角色绑定已变化，请重新分析后再应用。");
            var center = rig.GetComponentInChildren<Data_Center>(true);
            if (center == null || center.bO_Weapon_Animation_Events == null || center._SkillCancelFlag == null)
                result.errors.Add("角色缺少 Data_Center、攻击事件接收器或取消事件接收器。");
            else if (plan.hits != null)
                foreach (var hit in plan.hits.Where(x => x != null && x.enabled))
                    if (GetBinding(center, hit.functionName) == null)
                        result.errors.Add("角色缺少攻击部位绑定：" + hit.functionName);
            var animator = center != null && center.WholeT != null ? center.WholeT.GetComponent<Animator>() : null;
            if (animator == null || animator.avatar == null || !animator.avatar.isHuman || !animator.avatar.isValid)
                result.errors.Add("角色 WholeT 必须具有有效的 Humanoid Animator/Avatar。");
            else if (center.bO_Weapon_Animation_Events == null || center._SkillCancelFlag == null
                || center.bO_Weapon_Animation_Events.gameObject != animator.gameObject || center._SkillCancelFlag.gameObject != animator.gameObject)
                result.errors.Add("攻击和取消事件接收器必须与 WholeT Animator 位于同一 GameObject。");
        }
        if (!SafeSegment(plan.realName) || !SafeSegment(plan.type))
            result.errors.Add("realName/type 必须是安全的单个名称，不能包含路径、逗号或控制字符。");
        if (plan.type != "human" || string.IsNullOrEmpty(plan.rigAssetPath) || !plan.rigAssetPath.StartsWith("Assets/ExternalAssets/Prefabs/Units/human/", StringComparison.Ordinal))
            result.errors.Add("当前近战方案需使用 human 类型及 Units/human 下的项目角色，避免把人形动作注册给不兼容骨架。");
        if (plan.attackType != "GR")
            result.errors.Add("此动作生成流程目前支持 GR 近战技能；位移、反击和法术请使用技能工作台另行编排。");
        if (plan.spLevel < 0 || plan.spLevel > 3) result.errors.Add("spLevel 必须为 0–3。");
        if (!Finite(plan.attackWeight) || plan.attackWeight <= 0 || !Finite(plan.hpWeight) || plan.hpWeight <= 0)
            result.errors.Add("伤害/HP 权重必须为有限正数。");
        if (!Finite(plan.aiMin) || !Finite(plan.aiMax) || plan.aiMin < 0 || plan.aiMax <= plan.aiMin)
            result.errors.Add("AI 距离必须有限且满足 0 ≤ aiMin < aiMax。");
        if (plan.aiHeight < -1 || plan.aiHeight > 2) result.errors.Add("aiHeight 必须为 -1–2。");
        if (string.IsNullOrWhiteSpace(plan.reviewNotes))
            result.errors.Add("请在 reviewNotes 记录对攻击部位、时机、取消帧和 AI 距离的审阅结论。");
        if (!string.IsNullOrEmpty(plan.iconAssetPath) && AssetDatabase.LoadAssetAtPath<Texture2D>(plan.iconAssetPath) == null)
            result.errors.Add("iconAssetPath 不是可用的 Texture2D。");
        if (string.IsNullOrEmpty(plan.iconAssetPath)) result.warnings.Add("将生成占位图标，正式发布前需要替换美术。");
        if (SafeSegment(plan.realName) && File.Exists($"Assets/ExternalAssets/Animations/{plan.type}/skill/{plan.realName}.anim"))
            result.errors.Add("目标技能动画已存在；重复应用不会覆盖，请使用新的 realName。");
        result.estimatedDamage = result.attackCount * plan.attackWeight;
        if (result.errors.Count == 0)
        {
            var backendError = SkillCreationPanel.ValidateRequest(ToRequest(plan, result));
            if (!string.IsNullOrEmpty(backendError)) result.errors.Add(backendError);
        }
        result.valid = result.errors.Count == 0;
        return result;
    }

    // Kept independent of AssetDatabase so timing invariants can be regression tested with synthetic plans.
    public static SkillAuthoringValidation ValidateTiming(SkillAuthoringPlan plan)
    {
        var result = new SkillAuthoringValidation();
        if (plan == null) { result.errors.Add("方案为空。"); return result; }
        if (plan.schemaVersion != 1) result.errors.Add("不支持的 schemaVersion。");
        if (!Finite(plan.duration) || plan.duration <= 0 || !Finite(plan.frameRate) || plan.frameRate < 1)
            result.errors.Add("动作长度和帧率无效。");
        if (plan.hits == null || plan.hits.Any(x => x == null))
        { result.errors.Add("hits 不能为空或包含 null。"); return result; }
        var hits = plan.hits.Where(x => x.enabled).OrderBy(x => x.startTime).ToList();
        if (hits.Count == 0) result.errors.Add("至少需要一个启用的攻击窗口。低置信度候选必须经过审阅后启用。");
        foreach (var hit in hits)
        {
            if (!LimbEvents.Contains(hit.functionName ?? "")) result.errors.Add("不支持的攻击事件：" + hit.functionName);
            if (!Finite(hit.startTime) || !Finite(hit.endTime) || hit.startTime < 0 || hit.endTime <= hit.startTime || hit.endTime >= plan.duration)
                result.errors.Add("攻击窗口必须满足 0 ≤ startTime < endTime < duration。");
            if (hit.endTime - hit.startTime < 1f / Mathf.Max(1f, plan.frameRate) - 0.0001f)
                result.errors.Add("攻击窗口不能短于一帧。");
            if (hit.damageType != -1 && (hit.damageType < 1 || hit.damageType > 15)) result.errors.Add("damageType 必须为项目支持的 -1 或 1–15；0 用于关闭。");
            if (!Finite(hit.confidence) || hit.confidence < 0 || hit.confidence > 1) result.errors.Add("confidence 必须在 0–1 内。");
            if (hit.confidence < 0.65f) result.warnings.Add("已启用低置信度窗口：" + hit.functionName);
        }
        var previousEnd = -1f;
        foreach (var hit in hits)
        {
            if (hit.startTime < previousEnd)
                result.errors.Add("攻击窗口不能重叠：多个部位共享命中去重状态，交错开启会产生额外命中。请分开窗口或选择主要攻击部位。");
            previousEnd = Mathf.Max(previousEnd, hit.endTime);
        }
        var lastEnd = hits.Count == 0 ? 0f : hits.Max(x => x.endTime);
        if (!Finite(plan.cancelTime) || plan.cancelTime <= lastEnd || plan.cancelTime >= plan.duration)
            result.errors.Add("取消时间必须晚于所有攻击关闭时间，并早于动作结束。");
        result.attackCount = hits.Count;
        result.valid = result.errors.Count == 0;
        return result;
    }

    public static AnimationEvent[] BuildEvents(SkillAuthoringPlan plan)
    {
        var validation = ValidateTiming(plan);
        if (!validation.valid) throw new InvalidDataException(string.Join("\n", validation.errors));
        var events = new List<AnimationEvent>();
        var activeEnd = -1f;
        foreach (var hit in plan.hits.Where(x => x.enabled).OrderBy(x => x.startTime).ThenBy(x => x.functionName, StringComparer.Ordinal))
        {
            // RegisterBodyPartWeapon already resets the runtime's shared target set.
            events.Add(new AnimationEvent { time = hit.startTime, functionName = hit.functionName, intParameter = hit.damageType });
            events.Add(new AnimationEvent { time = hit.endTime, functionName = hit.functionName, intParameter = 0 });
            activeEnd = Mathf.Max(activeEnd, hit.endTime);
        }
        events.Add(new AnimationEvent { time = activeEnd, functionName = "ClearMarkerManagers" });
        events.Add(new AnimationEvent { time = plan.cancelTime, functionName = "turn_on_flag" });
        return events.OrderBy(x => x.time).ThenBy(EventOrder).ThenBy(x => x.functionName, StringComparer.Ordinal).ToArray();
    }

    public static SkillCreationResult Create(SkillAuthoringPlan plan)
    {
        var validation = Validate(plan);
        if (!validation.valid) throw new InvalidDataException(string.Join("\n", validation.errors));
        return SkillCreationPanel.Create(ToRequest(plan, validation));
    }

    private static SkillCreationRequest ToRequest(SkillAuthoringPlan plan, SkillAuthoringValidation validation)
    {
        return new SkillCreationRequest
        {
            autoId = string.IsNullOrEmpty(plan.recordId), recordId = plan.recordId,
            type = plan.type, realName = plan.realName, behaviorType = BehaviorType.GR,
            spLevel = plan.spLevel, attackWeight = plan.attackWeight, hpWeight = plan.hpWeight,
            aiMin = plan.aiMin, aiMax = plan.aiMax, aiHeight = plan.aiHeight,
            enName = plan.enName, jpName = plan.jpName, cnName = plan.cnName,
            enIntro = plan.enIntro, jpIntro = plan.jpIntro, cnIntro = plan.cnIntro,
            estimatedDamage = validation.estimatedDamage, estimatedHp = plan.hpWeight, attackCount = validation.attackCount,
            animationTemplate = ResolveClip(plan.sourceAssetPath, plan.clipName, plan.sourceLocalFileId),
            animationEvents = BuildEvents(plan),
            iconTemplate = string.IsNullOrEmpty(plan.iconAssetPath) ? null : AssetDatabase.LoadAssetAtPath<Texture2D>(plan.iconAssetPath)
        };
    }

    private static int EventOrder(AnimationEvent value)
    {
        if (value.functionName == "turn_on_flag") return 3;
        if (value.functionName == "ClearMarkerManagers" || value.intParameter == 0) return 0;
        return 2;
    }

    private static Transform GetBinding(Data_Center center, string functionName)
    {
        switch (functionName)
        {
            case "SetRightHandMarkerManager": return center.right_hand_t;
            case "SetLeftHandMarkerManager": return center.left_hand_t;
            case "SetRightFootMarkerManager": return center.right_foot_t;
            case "SetLeftFootMarkerManager": return center.left_foot_t;
            case "SetHeadMarkerManager": return center.head_t;
            case "SetTailMarkerManager": return center.tail_t;
            default: return null;
        }
    }

    private static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
    private static bool SafeSegment(string value) => !string.IsNullOrWhiteSpace(value) && value == value.Trim()
        && value != "." && value != ".." && value.IndexOfAny("/\\:*?\"<>|,.\r\n\t".ToCharArray()) < 0 && !value.Any(char.IsControl);
    private static string SanitizeName(string value) => new string(value.Select(x => char.IsLetterOrDigit(x) || x == '_' || x == '-' ? x : '_').ToArray());
}
#endif
