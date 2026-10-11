#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public sealed class SkillAuthoringWindow : EditorWindow
{
    private AnimationClip _clip;
    private GameObject _rig;
    private string _planPath = "SkillAuthoring/draft.json";
    private string _status = "选择素材和角色，导出分析方案；用 Codex 修改 JSON 后，校验并创建技能。";
    private Vector2 _scroll;

    [MenuItem("Tools/Skill Motion Authoring")]
    public static void Open() => GetWindow<SkillAuthoringWindow>("动作生成技能");

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("人形近战流程：采样动作 → 导出候选与证据 → 审阅 JSON → 校验 → 创建独立技能。新技能只包含方案中的攻击与取消事件。", MessageType.Info);
        _clip = (AnimationClip)EditorGUILayout.ObjectField("动作 Clip（支持 FBX 子片段）", _clip, typeof(AnimationClip), false);
        _rig = (GameObject)EditorGUILayout.ObjectField("项目角色 Prefab", _rig, typeof(GameObject), false);
        _planPath = EditorGUILayout.TextField("方案 JSON", _planPath);
        if (GUILayout.Button("分析动作并导出方案")) Execute(() =>
        {
            var path = EditorUtility.SaveFilePanel("保存技能分析方案", "SkillAuthoring", "draft", "json");
            if (string.IsNullOrEmpty(path)) return;
            var plan = SkillAuthoringPipeline.Analyze(_clip, _rig);
            SkillAuthoringPipeline.Write(path, plan);
            _planPath = path;
            _status = $"已导出 {plan.hits.Count} 个候选。请检查 hits、cancelTime、技能名称、AI 距离，并填写 reviewNotes。\n{path}";
        });
        if (GUILayout.Button("选择现有方案 JSON"))
        {
            var path = EditorUtility.OpenFilePanel("选择技能方案", "SkillAuthoring", "json");
            if (!string.IsNullOrEmpty(path)) _planPath = path;
        }
        if (GUILayout.Button("校验方案（不写入项目）")) Execute(() =>
        {
            var validation = SkillAuthoringPipeline.Validate(SkillAuthoringPipeline.Read(_planPath));
            _status = JsonUtility.ToJson(validation, true);
        });
        if (GUILayout.Button("按方案创建技能", GUILayout.Height(32))) Execute(() =>
        {
            var result = SkillAuthoringPipeline.Create(SkillAuthoringPipeline.Read(_planPath));
            _status = $"已新增技能 {result.recordId}\n动画：{result.animationAssetPath}\n图标：{result.iconAssetPath}";
            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<AnimationClip>(result.animationAssetPath));
        });
        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        EditorGUILayout.SelectableLabel(_status, EditorStyles.wordWrappedLabel, GUILayout.MinHeight(260));
        EditorGUILayout.EndScrollView();
    }

    private void Execute(Action action)
    {
        try { action(); }
        catch (Exception ex) { _status = ex.Message; Debug.LogException(ex); }
    }
}
#endif
