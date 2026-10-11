#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public sealed class SkillMotionAnalysisReport
{
    public int schemaVersion = 1;
    public bool succeeded;
    public string error = string.Empty;
    public string sourceAssetPath;
    public string sourceGuid;
    public long sourceLocalFileId;
    public string clipName;
    public string rigName;
    public float duration;
    public float frameRate;
    public float sampleRate;
    public int sampleCount;
    public bool candidateTimesAreSubframes;
    public string candidateTimeBasis = "Seconds on the uniform sampling timeline; may fall between source frames. Do not round boundaries independently.";
    public string poseCoordinateSpace = "Hips-relative world meters in the prefab's initial root orientation (+Y up, +Z forward).";
    public string[] poseBoneNames = Array.Empty<string>();
    public int[] poseParents = Array.Empty<int>();
    public List<SkillMotionPoseFrame> poseFrames = new List<SkillMotionPoseFrame>();
    public List<SkillMotionTrackReport> tracks = new List<SkillMotionTrackReport>();
    public List<SkillMotionCandidate> candidates = new List<SkillMotionCandidate>();
    public List<string> warnings = new List<string>();
}

[Serializable]
public sealed class SkillMotionPoseFrame
{
    public float time;
    public Vector3[] positions;
}

[Serializable]
public sealed class SkillMotionTrackReport
{
    public string limb;
    public string functionName;
    public string bonePath;
    public string referencePath;
    public float limbLength;
    public float maxNormalizedSpeed;
    public float maxReach;
    public float reachRange;
    public float[] sampleTimes;
    public float[] normalizedSpeed;
    public float[] normalizedReach;
    public float[] forwardReach;
    // Hip-relative evidence; do not use as ground clearance when the body crouches.
    public float[] height;
    // Fixed root-plane vertical coordinate in limb lengths; used for foot clearance.
    public float[] groundHeight;
}

[Serializable]
public sealed class SkillMotionCandidate
{
    public string functionName;
    public string limb;
    public float startTime;
    public float peakTime;
    public float endTime;
    // Heuristic evidence score, not a calibrated probability of a successful hit.
    public float confidence;
    public string reason;
}

/// <summary>
/// Editor-only motion evidence for authoring, independent of the workbench UI.
/// Coordinates remove limb-origin translation but retain upper-arm/thigh rotation.
/// Distances and speeds are normalized by the entire limb's length.
/// </summary>
public static class SkillMotionAnalyzer
{
    public const float RecommendedConfidence = 0.65f;
    private const float MinimumActiveTime = 0.06f;
    private const float MaximumActiveTime = 0.4f;
    private const int MaximumWindowsPerLimb = 6;
    private const int MaximumSamples = 18001;

    private sealed class Track
    {
        public SkillMotionTrackReport report;
        public Transform bone;
        public Transform origin;
        public Vector3[] positions;
        public float[] radialSpeed;
        public bool isFoot;
    }

    public static SkillMotionAnalysisReport Analyze(AnimationClip clip, GameObject prefabOrInstance)
    {
        var report = new SkillMotionAnalysisReport();
        if (clip == null || prefabOrInstance == null)
        {
            report.error = "需要动画片段和用于采样的角色 prefab。";
            return report;
        }

        report.sourceAssetPath = AssetDatabase.GetAssetPath(clip);
        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(clip, out string guid, out long localId);
        report.sourceGuid = guid;
        report.sourceLocalFileId = localId;
        report.clipName = clip.name;
        report.rigName = prefabOrInstance.name;
        report.duration = clip.length;
        report.frameRate = clip.frameRate;
        report.sampleRate = Mathf.Clamp(clip.frameRate, 30f, 120f);
        report.sampleCount = GetSampleCount(clip.length, report.sampleRate);
        if (clip.length <= 0f || report.sampleCount < 3 || report.sampleCount > MaximumSamples)
        {
            report.error = "动画过短，或采样量超过 18000 帧；请先将长素材切为单个技能动作。";
            return report;
        }

        report.sampleRate = (report.sampleCount - 1) / clip.length;
        GameObject instance = null;
        Scene previewScene = default;
        var ownsAnimationMode = !AnimationMode.InAnimationMode();
        try
        {
            previewScene = EditorSceneManager.NewPreviewScene();
            instance = UnityEngine.Object.Instantiate(prefabOrInstance);
            instance.name = prefabOrInstance.name;
            instance.hideFlags = HideFlags.HideAndDontSave;
            SceneManager.MoveGameObjectToScene(instance, previewScene);
            foreach (var behaviour in instance.GetComponentsInChildren<Behaviour>(true))
                behaviour.enabled = false;
            foreach (var particle in instance.GetComponentsInChildren<ParticleSystem>(true))
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            var animator = instance.GetComponentInChildren<Animator>(true);
            if (clip.isHumanMotion && (animator == null || animator.avatar == null || !animator.avatar.isHuman || !animator.avatar.isValid))
            {
                report.error = "人形动画需要带有效 Humanoid Avatar 的角色；请更换采样角色。";
                return report;
            }
            if (animator != null)
            {
                // Sampling a humanoid clip with a controller can evaluate unrelated state.
                animator.runtimeAnimatorController = null;
                animator.fireEvents = false;
            }

            var hips = ResolveBone(animator, instance, HumanBodyBones.Hips, "Hips", "Pelvis");
            if (hips == null)
            {
                report.error = "采样角色缺少髋骨，无法区分位移与肢体动作。";
                return report;
            }
            var tracks = new List<Track>();
            AddTrack(tracks, report, animator, instance, "右手", "SetRightHandMarkerManager", false,
                HumanBodyBones.RightHand, HumanBodyBones.RightLowerArm, HumanBodyBones.RightUpperArm,
                new[] { "RightHand", "Hand_R", "RHand" }, new[] { "RightForeArm", "RightLowerArm", "Forearm_R" }, new[] { "RightUpperArm", "UpperArm_R", "RightArm", "RArm" });
            AddTrack(tracks, report, animator, instance, "左手", "SetLeftHandMarkerManager", false,
                HumanBodyBones.LeftHand, HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftUpperArm,
                new[] { "LeftHand", "Hand_L", "LHand" }, new[] { "LeftForeArm", "LeftLowerArm", "Forearm_L" }, new[] { "LeftUpperArm", "UpperArm_L", "LeftArm", "LArm" });
            AddTrack(tracks, report, animator, instance, "右脚", "SetRightFootMarkerManager", true,
                HumanBodyBones.RightFoot, HumanBodyBones.RightLowerLeg, HumanBodyBones.RightUpperLeg,
                new[] { "RightFoot", "Foot_R", "RFoot" }, new[] { "RightLeg", "RightLowerLeg", "Calf_R" }, new[] { "RightUpperLeg", "UpperLeg_R", "RightUpLeg", "RThigh" });
            AddTrack(tracks, report, animator, instance, "左脚", "SetLeftFootMarkerManager", true,
                HumanBodyBones.LeftFoot, HumanBodyBones.LeftLowerLeg, HumanBodyBones.LeftUpperLeg,
                new[] { "LeftFoot", "Foot_L", "LFoot" }, new[] { "LeftLeg", "LeftLowerLeg", "Calf_L" }, new[] { "LeftUpperLeg", "UpperLeg_L", "LeftUpLeg", "LThigh" });
            if (tracks.Count == 0)
            {
                report.error = "未找到完整的手臂或腿骨链；检查 Avatar 或骨骼命名。";
                return report;
            }

            var referenceRotation = Quaternion.Inverse(instance.transform.rotation);
            var referencePosition = instance.transform.position;
            var poseBones = BuildPoseBones(report, animator, instance);
            var poseFrameIndices = new HashSet<int>(Enumerable.Range(0, 12)
                .Select(i => Mathf.RoundToInt(i * (report.sampleCount - 1) / 11f)));
            var samplingRoot = clip.isHumanMotion && animator != null ? animator.gameObject : instance;
            if (ownsAnimationMode) AnimationMode.StartAnimationMode();
            for (var frame = 0; frame < report.sampleCount; frame++)
            {
                // Uniform samples avoid a nearly zero final delta on non-integral clip lengths.
                var time = clip.length * frame / (report.sampleCount - 1);
                AnimationMode.BeginSampling();
                try { AnimationMode.SampleAnimationClip(samplingRoot, clip, time); }
                finally { AnimationMode.EndSampling(); }
                foreach (var track in tracks)
                {
                    var position = referenceRotation * (track.bone.position - track.origin.position) / track.report.limbLength;
                    track.positions[frame] = position;
                    track.report.sampleTimes[frame] = time;
                    track.report.height[frame] = (referenceRotation * (track.bone.position - hips.position)).y / track.report.limbLength;
                    track.report.groundHeight[frame] = (referenceRotation * (track.bone.position - referencePosition)).y / track.report.limbLength;
                }
                if (poseFrameIndices.Contains(frame))
                {
                    report.poseFrames.Add(new SkillMotionPoseFrame
                    {
                        time = time,
                        positions = poseBones.Select(bone => referenceRotation * (bone.position - hips.position)).ToArray()
                    });
                }
            }
            foreach (var track in tracks)
            {
                CalculateMetrics(track);
                report.candidates.AddRange(DetectCandidates(track));
            }
            if (report.tracks.All(t => t.maxNormalizedSpeed < 0.1f))
                report.warnings.Add("几乎没有采样到肢体运动。素材可能是静止动作，或动画与角色骨架不匹配；请在预览中确认骨骼实际移动。");

            // A weaker concurrent limb is often a counterbalance. Keep its evidence, but require review.
            foreach (var candidate in report.candidates)
            {
                var ownTrack = report.tracks.First(t => t.functionName == candidate.functionName);
                var stronger = report.candidates.Any(other => other != candidate && other.startTime < candidate.endTime && other.endTime > candidate.startTime
                    && report.tracks.First(t => t.functionName == other.functionName).maxNormalizedSpeed > ownTrack.maxNormalizedSpeed * 2f);
                if (stronger)
                {
                    candidate.confidence *= 0.72f;
                    candidate.reason += "；同时存在明显更强的另一肢体动作，可能是平衡摆动，需复核";
                }
            }
            report.candidates = report.candidates.OrderBy(c => c.startTime).ThenBy(c => c.functionName, StringComparer.Ordinal).ToList();
            report.candidateTimesAreSubframes = report.candidates.Any(c => IsSubframe(c.startTime, report.frameRate)
                || IsSubframe(c.peakTime, report.frameRate) || IsSubframe(c.endTime, report.frameRate));
            if (report.candidates.Count == 0)
                report.warnings.Add("没有足够明确的近战窗口。不要强行补攻击；请手动指定接触部位/时刻，或按法术、武器动作处理。");
            if (report.candidates.Any(c => c.confidence < RecommendedConfidence))
                report.warnings.Add("低分候选只作动作证据，不自动写入攻击事件。可结合接触姿势逐帧复核。");
            report.warnings.Add("评分是动作启发式，不能证明命中意图。武器、法术、肘膝和目标距离仍需作者/Codex 判断；建议帧需在游戏中验证。");
            report.succeeded = true;
        }
        catch (Exception ex)
        {
            report.error = "动作采样失败：" + ex.Message;
        }
        finally
        {
            if (ownsAnimationMode && AnimationMode.InAnimationMode()) AnimationMode.StopAnimationMode();
            if (instance != null) UnityEngine.Object.DestroyImmediate(instance);
            if (previewScene.IsValid()) EditorSceneManager.ClosePreviewScene(previewScene);
        }
        return report;
    }

    private static int GetSampleCount(float duration, float sampleRate)
    {
        var intervals = duration * sampleRate;
        var nearest = Mathf.RoundToInt(intervals);
        return (Mathf.Abs(intervals - nearest) <= 0.001f ? nearest : Mathf.CeilToInt(intervals)) + 1;
    }

    private static bool IsSubframe(float time, float frameRate)
    {
        var frame = time * frameRate;
        return frameRate <= 0f || Mathf.Abs(frame - Mathf.Round(frame)) > 0.001f;
    }

    private static List<Transform> BuildPoseBones(SkillMotionAnalysisReport report, Animator animator, GameObject root)
    {
        var bones = new List<Transform>();
        var names = new List<string>();
        var parents = new List<int>();
        // Missing optional bones are omitted; children connect to the nearest present ancestor.
        int Add(HumanBodyBones bone, int parent, params string[] hints)
        {
            var transform = ResolveBone(animator, root, bone, hints);
            if (transform == null) return parent;
            var index = bones.Count;
            bones.Add(transform);
            names.Add(bone.ToString());
            parents.Add(parent);
            return index;
        }
        var hips = Add(HumanBodyBones.Hips, -1, "Hips", "Pelvis");
        var spine = Add(HumanBodyBones.Spine, hips, "Spine", "Spine0");
        var chest = Add(HumanBodyBones.Chest, spine, "Chest", "Spine1", "Spine01", "Spine2");
        Add(HumanBodyBones.Head, chest, "Head");
        var leftShoulder = Add(HumanBodyBones.LeftShoulder, chest, "LeftShoulder", "Clavicle_L", "Shoulder_L");
        var leftArm = Add(HumanBodyBones.LeftUpperArm, leftShoulder, "LeftUpperArm", "UpperArm_L", "LeftArm", "LArm");
        var leftForearm = Add(HumanBodyBones.LeftLowerArm, leftArm, "LeftForeArm", "LeftLowerArm", "Forearm_L");
        Add(HumanBodyBones.LeftHand, leftForearm, "LeftHand", "Hand_L", "LHand");
        var rightShoulder = Add(HumanBodyBones.RightShoulder, chest, "RightShoulder", "Clavicle_R", "Shoulder_R");
        var rightArm = Add(HumanBodyBones.RightUpperArm, rightShoulder, "RightUpperArm", "UpperArm_R", "RightArm", "RArm");
        var rightForearm = Add(HumanBodyBones.RightLowerArm, rightArm, "RightForeArm", "RightLowerArm", "Forearm_R");
        Add(HumanBodyBones.RightHand, rightForearm, "RightHand", "Hand_R", "RHand");
        var leftThigh = Add(HumanBodyBones.LeftUpperLeg, hips, "LeftUpperLeg", "UpperLeg_L", "LeftUpLeg", "LThigh");
        var leftCalf = Add(HumanBodyBones.LeftLowerLeg, leftThigh, "LeftLeg", "LeftLowerLeg", "Calf_L");
        Add(HumanBodyBones.LeftFoot, leftCalf, "LeftFoot", "Foot_L", "LFoot");
        var rightThigh = Add(HumanBodyBones.RightUpperLeg, hips, "RightUpperLeg", "UpperLeg_R", "RightUpLeg", "RThigh");
        var rightCalf = Add(HumanBodyBones.RightLowerLeg, rightThigh, "RightLeg", "RightLowerLeg", "Calf_R");
        Add(HumanBodyBones.RightFoot, rightCalf, "RightFoot", "Foot_R", "RFoot");
        report.poseBoneNames = names.ToArray();
        report.poseParents = parents.ToArray();
        return bones;
    }

    private static void AddTrack(List<Track> tracks, SkillMotionAnalysisReport report, Animator animator, GameObject root,
        string limb, string function, bool isFoot, HumanBodyBones endBone, HumanBodyBones middleBone, HumanBodyBones originBone,
        string[] endHints, string[] middleHints, string[] originHints)
    {
        var bone = ResolveBone(animator, root, endBone, endHints);
        var middle = ResolveBone(animator, root, middleBone, middleHints);
        var origin = ResolveBone(animator, root, originBone, originHints);
        if (bone == null || middle == null || origin == null)
        {
            report.warnings.Add(limb + "骨链不完整，未分析此部位。");
            return;
        }
        var length = Vector3.Distance(origin.position, middle.position) + Vector3.Distance(middle.position, bone.position);
        if (length <= 0.0001f)
        {
            report.warnings.Add(limb + "骨链长度异常，未分析此部位。");
            return;
        }
        var metrics = new SkillMotionTrackReport
        {
            limb = limb, functionName = function,
            bonePath = AnimationUtility.CalculateTransformPath(bone, root.transform),
            referencePath = AnimationUtility.CalculateTransformPath(origin, root.transform),
            limbLength = length,
            sampleTimes = new float[report.sampleCount], normalizedSpeed = new float[report.sampleCount],
            normalizedReach = new float[report.sampleCount], forwardReach = new float[report.sampleCount],
            height = new float[report.sampleCount], groundHeight = new float[report.sampleCount]
        };
        report.tracks.Add(metrics);
        tracks.Add(new Track { report = metrics, bone = bone, origin = origin, isFoot = isFoot,
            positions = new Vector3[report.sampleCount], radialSpeed = new float[report.sampleCount] });
    }

    private static void CalculateMetrics(Track track)
    {
        var metrics = track.report;
        var count = track.positions.Length;
        var smooth = new Vector3[count];
        for (var i = 0; i < count; i++)
        {
            smooth[i] = (track.positions[Mathf.Max(0, i - 1)] + 2f * track.positions[i] + track.positions[Mathf.Min(count - 1, i + 1)]) * 0.25f;
            metrics.normalizedReach[i] = smooth[i].magnitude;
            metrics.forwardReach[i] = smooth[i].z;
        }
        for (var i = 0; i < count; i++)
        {
            var before = Mathf.Max(0, i - 1);
            var after = Mathf.Min(count - 1, i + 1);
            var dt = Mathf.Max(0.0001f, metrics.sampleTimes[after] - metrics.sampleTimes[before]);
            metrics.normalizedSpeed[i] = (smooth[after] - smooth[before]).magnitude / dt;
            track.radialSpeed[i] = (metrics.normalizedReach[after] - metrics.normalizedReach[before]) / dt;
        }
        metrics.maxNormalizedSpeed = metrics.normalizedSpeed.Max();
        metrics.maxReach = metrics.normalizedReach.Max();
        metrics.reachRange = metrics.maxReach - metrics.normalizedReach.Min();
    }

    private static List<SkillMotionCandidate> DetectCandidates(Track track)
    {
        var metrics = track.report;
        var results = new List<SkillMotionCandidate>();
        if (metrics.maxNormalizedSpeed < 1.1f || metrics.maxReach < 0.45f) return results;
        var count = metrics.sampleTimes.Length;
        var dt = metrics.sampleTimes[1] - metrics.sampleTimes[0];
        var sortedSpeeds = metrics.normalizedSpeed.OrderBy(v => v).ToArray();
        var speedScale = Mathf.Max(1.1f, sortedSpeeds[Mathf.Clamp(Mathf.FloorToInt(count * 0.95f), 0, count - 1)]);
        var speedThreshold = Mathf.Max(1.1f, speedScale * 0.42f);
        var baselineHeight = metrics.groundHeight.Min();
        var scores = new float[count];
        for (var i = 1; i < count - 1; i++)
        {
            var speed = metrics.normalizedSpeed[i];
            var reach = metrics.normalizedReach[i];
            var radial = track.radialSpeed[i];
            var clearance = metrics.groundHeight[i] - baselineHeight;
            // Reject retraction and near-ground support-foot movement before scoring peaks.
            if (speed < speedThreshold || reach < 0.5f || radial < -Mathf.Max(0.3f, speed * 0.22f)) continue;
            if (track.isFoot && clearance < 0.12f) continue;
            var strength = Mathf.Clamp01(speed / Mathf.Max(2.5f, speedScale));
            var extension = Mathf.InverseLerp(0.45f, 0.95f, reach);
            var outward = Mathf.Clamp01(radial / Mathf.Max(0.5f, speed * 0.4f));
            var contactPosition = track.isFoot ? Mathf.InverseLerp(0.12f, 0.45f, clearance) : Mathf.InverseLerp(-0.3f, 0.35f, metrics.forwardReach[i]);
            scores[i] = strength * 0.45f + extension * 0.3f + outward * 0.1f + contactPosition * 0.15f;
        }
        var peaks = Enumerable.Range(1, count - 2).Where(i => scores[i] >= 0.5f && scores[i] >= scores[i - 1] && scores[i] > scores[i + 1])
            .OrderByDescending(i => scores[i]).ThenBy(i => i);
        var maxSpan = Mathf.Max(1, Mathf.FloorToInt(MaximumActiveTime / dt));
        var minSpan = Mathf.Max(1, Mathf.CeilToInt(MinimumActiveTime / dt));
        foreach (var peak in peaks)
        {
            var start = peak;
            var end = peak;
            var edgeSpeed = Mathf.Max(0.75f, metrics.normalizedSpeed[peak] * 0.32f);
            while (start > 0 && peak - start < maxSpan / 2 && IsActiveEdge(track, start - 1, edgeSpeed, baselineHeight)) start--;
            while (end < count - 1 && end - start < maxSpan && IsActiveEdge(track, end + 1, edgeSpeed, baselineHeight)) end++;
            // Do not pad a brief spike through a recovery merely to satisfy a minimum duration.
            if (end - start < minSpan) continue;
            if (results.Any(c => metrics.sampleTimes[start] < c.endTime + 0.08f && metrics.sampleTimes[end] > c.startTime - 0.08f)) continue;
            var confidence = Mathf.Clamp01(scores[peak]);
            var reason = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0}：峰值速度 {1:F2} 肢长/秒，伸展 {2:P0}，向外速度 {3:F2} 肢长/秒；{4}", metrics.limb,
                metrics.normalizedSpeed[peak], metrics.normalizedReach[peak], track.radialSpeed[peak],
                track.isFoot ? "已排除贴地支撑运动" : "保留肩关节带动的摆动");
            results.Add(new SkillMotionCandidate { functionName = metrics.functionName, limb = metrics.limb,
                startTime = metrics.sampleTimes[start], peakTime = metrics.sampleTimes[peak], endTime = metrics.sampleTimes[end],
                confidence = confidence, reason = reason });
            if (results.Count >= MaximumWindowsPerLimb) break;
        }
        return results;
    }

    private static bool IsActiveEdge(Track track, int frame, float speedThreshold, float baselineHeight)
    {
        var metrics = track.report;
        return metrics.normalizedSpeed[frame] >= speedThreshold && metrics.normalizedReach[frame] >= 0.45f
            && track.radialSpeed[frame] >= -Mathf.Max(0.35f, metrics.normalizedSpeed[frame] * 0.25f)
            && (!track.isFoot || metrics.groundHeight[frame] - baselineHeight >= 0.1f);
    }

    private static Transform ResolveBone(Animator animator, GameObject root, HumanBodyBones bone, params string[] hints)
    {
        if (animator != null && animator.avatar != null && animator.avatar.isValid && animator.avatar.isHuman)
        {
            var mapped = animator.GetBoneTransform(bone);
            if (mapped != null) return mapped;
        }
        var transforms = root.GetComponentsInChildren<Transform>(true);
        foreach (var hint in hints)
        {
            var normalized = NormalizeName(hint);
            var exact = transforms.FirstOrDefault(t => NormalizeName(t.name) == normalized);
            if (exact != null) return exact;
        }
        // Namespace prefixes such as mixamorig: are allowed, but fingers and twist bones
        // must not win merely because their names contain "Hand" or "Arm".
        foreach (var hint in hints)
        {
            var normalized = NormalizeName(hint);
            var suffix = transforms.FirstOrDefault(t => NormalizeName(t.name).EndsWith(normalized, StringComparison.Ordinal));
            if (suffix != null) return suffix;
        }
        return null;
    }

    private static string NormalizeName(string name)
    {
        return new string(name.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
    }

    /// <summary>Deterministic synthetic regressions; no assets, scenes, or test-framework assembly required.</summary>
    public static List<string> RunSelfTests()
    {
        var failures = new List<string>();
        if (GetSampleCount(1.400000095f, 30f) != 43) failures.Add("Floating clip length must not add a phantom sample interval.");
        if (GetSampleCount(1.41f, 30f) != 44) failures.Add("A truly partial frame still requires a final sample.");
        if (IsSubframe(0.5f, 30f) || !IsSubframe(0.51f, 30f)) failures.Add("Candidate timing metadata must distinguish source frames from subframes.");
        var punch = SyntheticTrack(false, t => new Vector3(0f, 0f, t < 0.3f ? 0.4f : t < 0.5f ? 0.4f + (t - 0.3f) * 3f : t < 0.7f ? 1f - (t - 0.5f) * 3f : 0.4f));
        var punches = DetectCandidates(punch);
        if (punches.Count == 0) failures.Add("Straight extension should produce an attack window.");
        if (punches.Any(c => c.peakTime > 0.52f)) failures.Add("Retraction must not produce an attack peak.");
        if (punches.Any(c => c.endTime - c.startTime > MaximumActiveTime + 0.0001f)) failures.Add("Active duration exceeded the total bound.");
        var arc = SyntheticTrack(false, t => { var angle = Mathf.SmoothStep(-1f, 1f, Mathf.Clamp01((t - 0.2f) / 0.5f)); return new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)); });
        if (DetectCandidates(arc).Count == 0) failures.Add("Rigid shoulder-driven arc should remain visible at constant reach.");
        var support = SyntheticTrack(true, t => new Vector3(Mathf.Sin(t * 12f) * 0.25f, -0.95f, 0f));
        if (DetectCandidates(support).Count != 0) failures.Add("Grounded support-foot motion should be rejected.");
        var crouchingSupport = SyntheticTrack(true, t => new Vector3(Mathf.Sin(t * 12f) * 0.45f, -0.6f - Mathf.Sin(t * 8f) * 0.3f, 0.4f));
        // The leg-origin moves above a stationary planted foot: hips-relative height varies,
        // but the actual ground-plane height must remain zero throughout the clip.
        Array.Clear(crouchingSupport.report.groundHeight, 0, crouchingSupport.report.groundHeight.Length);
        if (DetectCandidates(crouchingSupport).Count != 0) failures.Add("Moving hips above a planted foot must not imply foot clearance.");
        var idle = SyntheticTrack(false, t => new Vector3(0f, 0f, 0.8f + Mathf.Sin(t) * 0.002f));
        if (DetectCandidates(idle).Count != 0) failures.Add("Idle noise should not create attack windows.");
        var retract = SyntheticTrack(false, t => new Vector3(0f, 0f, Mathf.Lerp(1f, 0.3f, Mathf.Clamp01((t - 0.2f) / 0.2f))));
        if (DetectCandidates(retract).Count != 0) failures.Add("Retraction-only motion should have no attack candidates.");
        var spike = SyntheticTrack(false, t => new Vector3(0f, 0f, Mathf.Abs(t - 0.5f) < 0.001f ? 1f : 0.5f));
        if (DetectCandidates(spike).Count != 0) failures.Add("Single-sample noise should not create an attack window.");
        var longArc = SyntheticTrack(false, t => new Vector3(Mathf.Sin(t * 5f), 0f, Mathf.Cos(t * 5f)));
        if (DetectCandidates(longArc).Any(c => c.endTime - c.startTime > MaximumActiveTime + 0.0001f)) failures.Add("Long arc window exceeded 0.4 seconds.");
        return failures;
    }

    private static Track SyntheticTrack(bool isFoot, Func<float, Vector3> position)
    {
        const int count = 61;
        var track = new Track { isFoot = isFoot, positions = new Vector3[count], radialSpeed = new float[count], report = new SkillMotionTrackReport
        {
            limb = "synthetic", functionName = isFoot ? "SetRightFootMarkerManager" : "SetRightHandMarkerManager", limbLength = 1f,
            sampleTimes = new float[count], normalizedSpeed = new float[count], normalizedReach = new float[count], forwardReach = new float[count], height = new float[count], groundHeight = new float[count]
        } };
        for (var i = 0; i < count; i++)
        {
            var time = i / 60f;
            track.positions[i] = position(time);
            track.report.sampleTimes[i] = time;
            track.report.height[i] = track.positions[i].y;
            track.report.groundHeight[i] = track.positions[i].y;
        }
        CalculateMetrics(track);
        return track;
    }
}
#endif
