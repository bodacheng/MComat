#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

/// <summary>Unity -batchmode -executeMethod SkillAuthoringBatch.Run; same pipeline as the editor window.</summary>
public static class SkillAuthoringBatch
{
    [Serializable]
    private sealed class BatchResult
    {
        public bool success;
        public string action;
        public string error;
        public string recordId;
        public string animationAssetPath;
        public string iconAssetPath;
        public bool validationPerformed;
        public SkillAuthoringValidation validation;
        public List<string> tests = new List<string>();
    }

    public static void Run()
    {
        var result = new BatchResult { action = Arg("-skillAction", "validate") };
        var output = Arg("-skillOutput", "Logs/skill-authoring-result.json");
        try
        {
            switch (result.action)
            {
                case "analyze":
                    var clip = SkillAuthoringPipeline.ResolveClip(Required("-skillClip"), Arg("-skillClipName", null));
                    var rig = AssetDatabase.LoadAssetAtPath<GameObject>(Required("-skillRig"));
                    SkillAuthoringPipeline.Write(Required("-skillPlan"), SkillAuthoringPipeline.Analyze(clip, rig));
                    break;
                case "validate":
                    result.validationPerformed = true;
                    result.validation = SkillAuthoringPipeline.Validate(SkillAuthoringPipeline.Read(Required("-skillPlan")));
                    if (!result.validation.valid) throw new InvalidDataException(string.Join("\n", result.validation.errors));
                    break;
                case "create":
                    var createPlan = SkillAuthoringPipeline.Read(Required("-skillPlan"));
                    result.validationPerformed = true;
                    result.validation = SkillAuthoringPipeline.Validate(createPlan);
                    var created = SkillAuthoringPipeline.Create(createPlan);
                    result.recordId = created.recordId;
                    result.animationAssetPath = created.animationAssetPath;
                    result.iconAssetPath = created.iconAssetPath;
                    VerifyCreated(createPlan, created.recordId, result.tests);
                    break;
                case "verify":
                    var verifyPlan = SkillAuthoringPipeline.Read(Required("-skillPlan"));
                    RunTests(result.tests);
                    VerifyCreated(verifyPlan, verifyPlan.recordId, result.tests);
                    result.recordId = verifyPlan.recordId;
                    result.validationPerformed = true;
                    result.validation = SkillAuthoringPipeline.ValidateTiming(verifyPlan);
                    result.validation.estimatedDamage = result.validation.attackCount * verifyPlan.attackWeight;
                    break;
                case "selftest":
                    RunTests(result.tests);
                    break;
                default: throw new ArgumentException("skillAction 必须为 analyze/validate/create/verify/selftest。");
            }
            result.success = true;
        }
        catch (Exception ex) { result.error = ex.ToString(); Debug.LogException(ex); }
        SkillAuthoringPipeline.Write(output, result);
        Debug.Log("Skill authoring result: " + Path.GetFullPath(output));
        if (Application.isBatchMode) EditorApplication.Exit(result.success ? 0 : 1);
    }

    [MenuItem("Tools/Skill Motion Authoring Self Tests")]
    public static void SelfTests()
    {
        var tests = new List<string>();
        RunTests(tests);
        Debug.Log("Skill authoring tests passed: " + string.Join(", ", tests));
    }

    private static void RunTests(List<string> passed)
    {
        var backendFailures = SkillCreationBackendTests.RunSelfTests();
        if (backendFailures.Count > 0) throw new Exception(string.Join("\n", backendFailures));
        passed.Add("creation backend read-only validation regression cases");
        var motionFailures = SkillMotionAnalyzer.RunSelfTests();
        if (motionFailures.Count > 0) throw new Exception(string.Join("\n", motionFailures));
        passed.Add("motion analyzer synthetic regression cases");
        var plan = new SkillAuthoringPlan
        {
            duration = 1f, frameRate = 60, cancelTime = 0.8f,
            hits = new List<SkillAuthoringHit>
            {
                new SkillAuthoringHit { functionName = "SetLeftHandMarkerManager", startTime = .2f, endTime = .3f, confidence = .8f },
                new SkillAuthoringHit { functionName = "SetRightHandMarkerManager", startTime = .5f, endTime = .6f, confidence = .8f }
            }
        };
        Assert(SkillAuthoringPipeline.ValidateTiming(plan).valid, "valid two-hit plan");
        var events = SkillAuthoringPipeline.BuildEvents(plan);
        Assert(events.Count(x => SKillAnalyzer.IsNormalAttackStartEvent(x)) == 2, "exactly two hit starts");
        Assert(events.Count(x => x.functionName == "ClearTargets") == 0, "no synthetic extra hits");
        Assert(events.Last().functionName == "turn_on_flag", "cancel follows all closes");
        passed.Add("two-hit event contract and static attack count");
        plan.hits[1].startTime = .25f;
        Assert(!SkillAuthoringPipeline.ValidateTiming(plan).valid, "reject overlapping limbs sharing dedupe");
        plan.hits[1].startTime = .3f;
        Assert(SkillAuthoringPipeline.ValidateTiming(plan).valid, "touching attack windows");
        var boundary = SkillAuthoringPipeline.BuildEvents(plan).Where(x => Mathf.Abs(x.time - .3f) < .00001f).ToArray();
        Assert(boundary.Length == 2 && boundary[0].intParameter == 0 && boundary[1].intParameter != 0, "close before next start");
        passed.Add("overlap rejection and deterministic event order");
        plan.hits[0].endTime = .30005f;
        plan.hits[1].functionName = plan.hits[0].functionName;
        Assert(!SkillAuthoringPipeline.ValidateTiming(plan).valid, "reject subframe overlap that would close the next hit");
        plan.hits[0].endTime = .3f;
        plan.hits[1].functionName = "SetRightHandMarkerManager";
        plan.cancelTime = .4f;
        Assert(!SkillAuthoringPipeline.ValidateTiming(plan).valid, "reject early cancellation");
        plan.cancelTime = .8f;
        plan.hits[0].startTime = float.NaN;
        Assert(!SkillAuthoringPipeline.ValidateTiming(plan).valid, "reject NaN timing");
        plan.hits[0].startTime = .2f;
        plan.hits[0].functionName = "SetRightHandWeaponMarkerManager";
        Assert(!SkillAuthoringPipeline.ValidateTiming(plan).valid, "reject nonexistent runtime receiver");
        plan.hits[0].functionName = "SetLeftHandMarkerManager";
        plan.hits[0].damageType = -1;
        Assert(SkillAuthoringPipeline.ValidateTiming(plan).valid, "accept legal negative damage type");
        plan.hits[0].damageType = 0;
        Assert(!SkillAuthoringPipeline.ValidateTiming(plan).valid, "zero is close, not damage");
        passed.Add("cancel, finite numbers, receiver and damage type validation");
        var roundTrip = JsonUtility.FromJson<SkillAuthoringPlan>(JsonUtility.ToJson(plan));
        Assert(roundTrip.hits.Count == 2, "JSON plan roundtrip");
        passed.Add("JSON plan roundtrip");
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new Exception("Self test failed: " + message);
    }

    private static void VerifyCreated(SkillAuthoringPlan plan, string recordId, List<string> passed)
    {
        Assert(!string.IsNullOrEmpty(recordId), "verify requires recordId in the plan");
        var rows = new List<string[]>();
        foreach (var file in new[] { "mst_skill", "skill_ai_attrs", "skill_name", "SkillStaticAnalysis" })
        {
            var matches = CsvParser2.Parse(File.ReadAllText("Assets/ExternalAssets/Data/Config/" + file + ".csv"))
                .Skip(1).Where(x => x.Length > 0 && x[0] == recordId).ToArray();
            Assert(matches.Length == 1, file + " has exactly one skill row");
            rows.Add(matches[0]);
        }
        Assert(rows[0][1] == plan.realName && rows[0][2] == plan.type && rows[0][6] == "GR", "main table identity/state");
        Assert(rows[3][4] == plan.hits.Count(x => x.enabled).ToString(System.Globalization.CultureInfo.InvariantCulture), "static hit count");
        passed.Add("four CSV rows and consistent skill identity");
        var path = $"Assets/ExternalAssets/Animations/{plan.type}/skill/{plan.realName}.anim";
        var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
        var source = SkillAuthoringPipeline.ResolveClip(plan.sourceAssetPath, plan.clipName, plan.sourceLocalFileId);
        Assert(clip != null && clip != source && clip.isHumanMotion, "independent humanoid animation clone");
        Assert(Mathf.Abs(clip.length - source.length) < .0001f, "source motion duration preserved");
        Assert(AnimationUtility.GetCurveBindings(clip).Length == AnimationUtility.GetCurveBindings(source).Length, "source curve bindings preserved");
        Assert(!AnimationUtility.GetAnimationClipSettings(clip).loopTime, "skill animation is non-looping");
        Assert(AssetDatabase.GetAssetDependencyHash(plan.sourceAssetPath).ToString() == plan.sourceDependencyHash, "source asset/import settings unchanged");
        var expected = SkillAuthoringPipeline.BuildEvents(plan);
        var actual = AnimationUtility.GetAnimationEvents(clip);
        Assert(actual.Length == expected.Length, "generated event count");
        for (var i = 0; i < actual.Length; i++)
            Assert(actual[i].functionName == expected[i].functionName && actual[i].intParameter == expected[i].intParameter
                && Mathf.Abs(actual[i].time - expected[i].time) < .0001f, "generated event sequence " + i);
        passed.Add("FBX subclip clone, unchanged source and exact event timeline");
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        var animationEntry = settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(path));
        Assert(animationEntry != null && animationEntry.address == $"{plan.type}/skill/{plan.realName}.anim"
            && animationEntry.labels.Contains(AddressablesResourcePolicy.SkillAnimationLabel), "animation address/label");
        var icons = settings.groups.Where(x => x != null).SelectMany(x => x.entries).Where(x => x.address == recordId).ToArray();
        Assert(icons.Length == 1 && icons[0].labels.Contains(AddressablesResourcePolicy.SkillIconLabel), "icon address/label");
        Assert(AssetDatabase.LoadAssetAtPath<Sprite>(icons[0].AssetPath) != null, "loadable icon sprite");
        passed.Add("Addressables addresses, labels and sprite import");
    }
    private static string Required(string key) => Arg(key, null) ?? throw new ArgumentException("Missing argument " + key);
    private static string Arg(string key, string fallback)
    {
        var args = Environment.GetCommandLineArgs();
        var index = Array.IndexOf(args, key);
        return index >= 0 && index + 1 < args.Length ? args[index + 1] : fallback;
    }
}
#endif
