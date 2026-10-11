#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Skill;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

/// <summary>Read-only regression checks; never creates assets or changes project configuration.</summary>
public static class SkillCreationBackendTests
{
    public static List<string> RunSelfTests()
    {
        var failures = new List<string>();
        var uniqueName = "__skill_validation_" + Guid.NewGuid().ToString("N");
        Func<SkillCreationRequest> fresh = () => new SkillCreationRequest
        {
            realName = uniqueName,
            registerAddressables = false
        };
        Action<string, Action<SkillCreationRequest>, string> reject = (name, change, expected) =>
        {
            try
            {
                var request = fresh();
                change(request);
                var error = SkillCreationPanel.ValidateRequest(request);
                if (string.IsNullOrEmpty(error) || !error.Contains(expected))
                {
                    failures.Add(name + ": expected validation containing '" + expected + "', got '" + error + "'.");
                }
            }
            catch (Exception ex)
            {
                failures.Add(name + ": validation unexpectedly threw " + ex.Message);
            }
        };

        var baseline = SkillCreationPanel.ValidateRequest(fresh());
        if (!string.IsNullOrEmpty(baseline)) failures.Add("Valid request: " + baseline);
        if (string.IsNullOrEmpty(SkillCreationPanel.ValidateRequest(null))) failures.Add("Null request was accepted.");
        reject("NaN attack weight", x => x.attackWeight = float.NaN, "有限数");
        reject("Infinite HP weight", x => x.hpWeight = float.PositiveInfinity, "有限数");
        reject("NaN minimum distance", x => x.aiMin = float.NaN, "有限数");
        reject("Infinite maximum distance", x => x.aiMax = float.PositiveInfinity, "有限数");
        reject("NaN damage estimate", x => x.estimatedDamage = float.NaN, "有限数");
        reject("Infinite HP estimate", x => x.estimatedHp = float.NegativeInfinity, "有限数");
        reject("Invalid icon color", x => x.placeholderIconColor = new Color(float.NaN, 0, 0), "有限数");
        reject("Negative distance", x => x.aiMin = -1f, "不能为负数");
        reject("Reversed distance interval", x => x.aiMin = x.aiMax + 1f, "最小触发距离");
        reject("Nonpositive attack weight", x => x.attackWeight = 0f, "ATTACK_WEIGHT");
        reject("Nonpositive HP weight", x => x.hpWeight = 0f, "HP_WEIGHT");
        reject("Negative damage estimate", x => x.estimatedDamage = -1f, "不能为负数");
        reject("Negative HP estimate", x => x.estimatedHp = -1f, "不能为负数");
        reject("Empty attack count", x => x.attackCount = 0, "Attack Count");
        reject("Invalid SP level", x => x.spLevel = 4, "SP_LEVEL");
        reject("Invalid height", x => x.aiHeight = -2, "TRIGGER_HEIGHT");
        reject("Unsupported state", x => x.behaviorType = BehaviorType.MV, "行为类型");
        reject("Name traversal", x => x.realName = "../outside", "REAL_NAME");
        reject("Type traversal", x => x.type = "../human", "USEABLE_MONSTER_TYPE");
        reject("Backslash traversal", x => x.realName = "..\\outside", "REAL_NAME");
        reject("Name control character", x => x.realName = "unsafe\nname", "REAL_NAME");
        reject("Empty animation name", x => x.realName = "", "REAL_NAME");
        reject("Noncanonical ID", x => { x.autoId = false; x.recordId = "000123"; }, "前导零");
        reject("ID traversal", x => { x.autoId = false; x.recordId = "../123"; }, "正整数");
        reject("Events without source", x => x.animationEvents = Array.Empty<AnimationEvent>(), "动画模板");

        var rows = CsvParser2.Parse(File.ReadAllText("Assets/ExternalAssets/Data/Config/mst_skill.csv"));
        var existing = rows.Skip(1).FirstOrDefault(row => row.Length >= 3 && int.TryParse(row[0], out _));
        if (existing == null)
        {
            failures.Add("Existing skill fixture is missing.");
        }
        else
        {
            reject("Existing record ID", x => { x.autoId = false; x.recordId = existing[0].Trim(); }, "已存在");
            reject("Existing type/name", x => { x.type = existing[2].Trim(); x.realName = existing[1].Trim(); }, "同类型下已存在");
        }

        var clip = AssetDatabase.FindAssets("t:AnimationClip", new[] { "Assets/ExternalAssets/Animations/human/skill" })
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => AssetDatabase.LoadAssetAtPath<AnimationClip>(path))
            .FirstOrDefault(value => value != null && value.length > 0f);
        if (clip == null)
        {
            failures.Add("Animation event validation fixture is missing.");
        }
        else
        {
            reject("NaN event time", x => { x.animationTemplate = clip; x.animationEvents = new[] { new AnimationEvent { functionName = "ResetState", time = float.NaN } }; }, "动画事件");
            reject("Event past clip end", x => { x.animationTemplate = clip; x.animationEvents = new[] { new AnimationEvent { functionName = "ResetState", time = clip.length + 1f } }; }, "动画事件");
            reject("NaN event parameter", x => { x.animationTemplate = clip; x.animationEvents = new[] { new AnimationEvent { functionName = "Speed", floatParameter = float.NaN } }; }, "动画事件");
            reject("Missing event function", x => { x.animationTemplate = clip; x.animationEvents = new[] { new AnimationEvent() }; }, "动画事件");
        }

        // A real request normally resolves an existing address back to its same asset.
        // Exercise the collision guard directly to cover the other-GUID case without
        // adding fake entries to production Addressables settings.
        try
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var entry = settings?.groups.Where(group => group != null).SelectMany(group => group.entries)
                .FirstOrDefault(value => !string.IsNullOrEmpty(value.address));
            if (entry == null)
            {
                failures.Add("Addressables collision fixture is missing.");
            }
            else
            {
                var issues = new List<string>();
                var collisionGuard = typeof(SkillCreationPanel).GetMethod("ValidateAddressCollision", BindingFlags.Static | BindingFlags.NonPublic);
                if (collisionGuard == null) throw new MissingMethodException("ValidateAddressCollision");
                collisionGuard.Invoke(null, new object[] { settings, "Assets/" + uniqueName + ".anim", entry.address, issues });
                if (!issues.Any(issue => issue.Contains("已被资源"))) failures.Add("Address assigned to another GUID was accepted.");
            }
        }
        catch (Exception ex)
        {
            failures.Add("Addressables collision check: " + ex.Message);
        }
        return failures;
    }
}
#endif
