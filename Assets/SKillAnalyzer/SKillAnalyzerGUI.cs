#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using UnityEngine.Networking;
using System.Threading.Tasks;

public partial class SKillAnalyzerGUI : EditorWindow
{
    readonly SKillAnalyzer target = new SKillAnalyzer();
    string _focusingType = "human";
    string _targetSkillName;
    string _targetEventName;
    float _attackFrameStartAtMAX, _attackFrameStartAtMIN, _attackFrameEndToCancelFrameTimeMAX, _attackFrameEndToCancelFrameTimeMIN;
    string skillTypeFolderName = "G_Attack_State";
    readonly string[] _skillTypeFolderNames = { "G_Attack_State", "G_Attack_State_Stay", "GMStates"};
    string old_name, new_name;
    string azureTextPrompt = "青春故事";
    string azureImagePrompt = "赛博朋克风格的城市夜景";
    string azureImageAspectRatio = "16:9";
    int azureImageSampleCount = 1;
    Texture2D azureImagePreview;
    string azureImageStatus;
    void OnGUI()
    {
        HandleClipDragAndDrop(Event.current);
        EnsureSkillCreationPanel();
        DrawWorkbenchToolbar();
        EditorGUILayout.Space(8f);

        if (_selectedWorkbenchTab == 0)
        {
            DrawClipEventEditorSection();
            return;
        }

        if (_selectedWorkbenchTab == 1)
        {
            DrawSkillCreationWorkbench();
            return;
        }

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("旧版分析与云调试工具", EditorStyles.boldLabel);
        EditorGUILayout.LabelField(" 技能参数统计类  ");
        _focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息", _focusingType);
        _targetEventName = EditorGUILayout.TextField("选择拥有该事件的技能动画片段", _targetEventName);
        _attackFrameStartAtMAX = EditorGUILayout.FloatField("攻击帧启动时间小于等于：", _attackFrameStartAtMAX);
        _attackFrameStartAtMIN = EditorGUILayout.FloatField("攻击帧启动时间大于：", _attackFrameStartAtMIN);
        _attackFrameEndToCancelFrameTimeMAX = EditorGUILayout.FloatField("收手时间小于等于", _attackFrameEndToCancelFrameTimeMAX);
        _attackFrameEndToCancelFrameTimeMIN = EditorGUILayout.FloatField("收手时间大于：", _attackFrameEndToCancelFrameTimeMIN);
        if (GUILayout.Button("满足以上条件的技能资源名如下：(console显示)"))
        {
            target.SkillsAnalyzeByFrames(_focusingType, _targetEventName, _attackFrameStartAtMIN, _attackFrameStartAtMAX, _attackFrameEndToCancelFrameTimeMIN, _attackFrameEndToCancelFrameTimeMAX).Forget();
        }
        EditorGUILayout.LabelField(" 整体替换动画事件名(千万慎用。一般用不上此功能）");
        old_name = EditorGUILayout.TextField("寻找该动画事件名", old_name);
        new_name = EditorGUILayout.TextField("替换成以下动画事件名", new_name);
        if (GUILayout.Button("该动画事件名替换(请慎用此功能）"))
        {
            target.ReplaceAnimEventName(_focusingType, old_name, new_name);
        }
        // 
        _focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息", _focusingType);
        skillTypeFolderName = _skillTypeFolderNames[EditorGUILayout.Popup("技能文件夹", Array.IndexOf(_skillTypeFolderNames, skillTypeFolderName), _skillTypeFolderNames)];
        _targetSkillName = EditorGUILayout.TextField("技能名", _targetSkillName);
        if (GUILayout.Button("分析以下技能"))
        {
            UnityEngine.Object animObject = Resources.Load("Animations/" + _focusingType + "/" + skillTypeFolderName + "/" + _targetSkillName, typeof(AnimationClip));
            if (animObject)
                target.EvaluateSKill(animObject as AnimationClip);
            else
                Debug.Log("没找到对应技能文件");
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Login"))
        {
            PlayFabReadClient.LoginByDevice(
                (x, y) => {
                    Debug.Log(" 登陆成功，获得下面这样一个东西： " + x.EntityToken.EntityToken);
                }
            );
        }
        GUILayout.Space(10);

        if (GUILayout.Button("任意CloudScript测试"))
        {
            CloudScript.ExecuteCloudScriptMainSceneCommon(
                new ExecuteCloudScriptRequest
                {
                    FunctionName = "azureTest",
                    //FunctionParameter = new { stage = 10 },
                    GeneratePlayStreamEvent = true
                },
                (x) =>
                {
                    Debug.Log(x);
                }
            );
        }
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Azure Function 调试", EditorStyles.boldLabel);
        azureTextPrompt = EditorGUILayout.TextField("文本 Prompt", azureTextPrompt);
        if (GUILayout.Button("任意Azure Function文本测试"))
        {
            RequestAzureText();
        }

        GUILayout.Space(5);
        azureImagePrompt = EditorGUILayout.TextField("图片 Prompt", azureImagePrompt);
        azureImageAspectRatio = EditorGUILayout.TextField("图片宽高比", azureImageAspectRatio);
        azureImageSampleCount = EditorGUILayout.IntSlider("Sample Count", azureImageSampleCount, 1, 4);
        if (GUILayout.Button("任意Azure Function图片测试"))
        {
            RequestAzureImage();
        }
        if (!string.IsNullOrEmpty(azureImageStatus))
        {
            EditorGUILayout.HelpBox(azureImageStatus, MessageType.Info);
        }
        if (azureImagePreview != null)
        {
            var maxWidth = position.width - 20f;
            maxWidth = Mathf.Clamp(maxWidth, 64f, 512f);
            var ratio = azureImagePreview.height > 0 ? (float)azureImagePreview.height / azureImagePreview.width : 1f;
            var rect = GUILayoutUtility.GetRect(maxWidth, maxWidth * ratio, GUILayout.ExpandWidth(false));
            EditorGUI.DrawPreviewTexture(rect, azureImagePreview, null, ScaleMode.ScaleToFit);
        }
    }

    private void RequestAzureText()
    {
        CloudScript.ExecuteFunctionCommon(
            new ExecuteFunctionRequest()
            {
                FunctionName = "generateGeminiText",
                FunctionParameter = new
                {
                    prompt = azureTextPrompt,
                    model = "gemini-2.5-flash-lite",
                    timeoutMs = 10000
                },
                GeneratePlayStreamEvent = true
            },
            x =>
            {
                if (x.Error != null)
                {
                    Debug.LogError($"[AzureFn/Text] Error: {x.Error.Error} - {x.Error.Message}\n{x.Error.StackTrace}");
                    return;
                }

                if (x.FunctionResult == null)
                {
                    Debug.LogWarning("[AzureFn/Text] FunctionResult is null");
                    return;
                }

                var raw = PlayFab.Json.PlayFabSimpleJson.SerializeObject(x.FunctionResult);
                Debug.Log($"[AzureFn/Text] Raw result: {raw}");

                try
                {
                    var dict = PlayFab.Json.PlayFabSimpleJson.DeserializeObject<Dictionary<string, object>>(raw);
                    if (dict != null && dict.TryGetValue("text", out var story))
                    {
                        Debug.Log($"[AzureFn/Text] Generated story:\n{story}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[AzureFn/Text] Failed to parse story text: {ex.Message}");
                }
            },
            error =>
            {
                Debug.LogError($"[AzureFn/Text] Request failed: {error.GenerateErrorReport()}");
            }
        );
    }

    private void RequestAzureImage()
    {
        CloudScript.ExecuteFunctionCommon(
            new ExecuteFunctionRequest()
            {
                FunctionName = "generateGeminiImages",
                FunctionParameter = new
                {
                    prompt = azureImagePrompt,
                    imageModel = "gemini-3.1-flash-image",
                    sampleCount = Mathf.Clamp(azureImageSampleCount, 1, 4),
                    aspectRatio = string.IsNullOrWhiteSpace(azureImageAspectRatio) ? "1:1" : azureImageAspectRatio
                },
                GeneratePlayStreamEvent = true
            },
            x =>
            {
                if (x.Error != null)
                {
                    azureImageStatus = $"AzureFn/Image Error: {x.Error.Error} - {x.Error.Message}";
                    Debug.LogError($"[AzureFn/Image] {azureImageStatus}\n{x.Error.StackTrace}");
                    ClearPreviewTexture();
                    Repaint();
                    return;
                }

                if (x.FunctionResult == null)
                {
                    if (x.FunctionResultTooLarge == true)
                    {
                        azureImageStatus = "返回数据过大（PlayFab 限制 350KB），请降低分辨率或让函数返回URL。";
                        Debug.LogWarning("[AzureFn/Image] FunctionResult too large, consider returning smaller payload.");
                    }
                    else
                    {
                        azureImageStatus = "AzureFn/Image FunctionResult is null";
                        Debug.LogWarning("[AzureFn/Image] FunctionResult is null");
                    }
                    ClearPreviewTexture();
                    Repaint();
                    return;
                }

                try
                {
                    var raw = PlayFab.Json.PlayFabSimpleJson.SerializeObject(x.FunctionResult);
                    Debug.Log($"[AzureFn/Image] Raw result: {raw}");

                    if (TryHandleImageUrls(raw))
                    {
                        return;
                    }

                    var response = PlayFab.Json.PlayFabSimpleJson.DeserializeObject<Imagen4Service.CloudScriptImageResponse>(raw);
                    if (response?.predictions != null && response.predictions.Length > 0)
                    {
                        var first = response.predictions[0];
                        if (!string.IsNullOrEmpty(first.bytesBase64Encoded))
                        {
                            var bytes = Convert.FromBase64String(first.bytesBase64Encoded);
                            var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false)
                            {
                                hideFlags = HideFlags.DontSave
                            };
                            if (tex.LoadImage(bytes))
                            {
                                ClearPreviewTexture();
                                azureImagePreview = tex;
                                azureImageStatus = $"生成成功 ({tex.width}x{tex.height})";
                            }
                            else
                            {
                                UnityEngine.Object.DestroyImmediate(tex);
                                azureImageStatus = "图片解码失败";
                                ClearPreviewTexture();
                            }
                        }
                        else
                        {
                            azureImageStatus = "返回的数据为空";
                            ClearPreviewTexture();
                        }
                    }
                    else
                    {
                        azureImageStatus = "没有返回图片";
                        ClearPreviewTexture();
                    }
                }
                catch (Exception ex)
                {
                    azureImageStatus = $"解析失败: {ex.Message}";
                    Debug.LogWarning($"[AzureFn/Image] {azureImageStatus}");
                    ClearPreviewTexture();
                }

                Repaint();
            },
            error =>
            {
                azureImageStatus = $"AzureFn/Image Request failed: {error.GenerateErrorReport()}";
                Debug.LogError($"[AzureFn/Image] {azureImageStatus}");
                ClearPreviewTexture();
                Repaint();
            }
        );
    }

    private void ClearPreviewTexture()
    {
        if (azureImagePreview != null)
        {
            UnityEngine.Object.DestroyImmediate(azureImagePreview);
            azureImagePreview = null;
        }
    }

    private void OnDisable()
    {
        DisposeClipEditorState();
        DisposeSkillCreationPanel();
        ClearPreviewTexture();
    }

    [Serializable]
    private class AzureImageEntry
    {
        public string url;
        public string mimeType;
    }

    [Serializable]
    private class AzureImageResponse
    {
        public AzureImageEntry[] images;
    }

    private bool TryHandleImageUrls(string raw)
    {
        try
        {
            var response = PlayFab.Json.PlayFabSimpleJson.DeserializeObject<AzureImageResponse>(raw);
            if (response?.images == null || response.images.Length == 0)
            {
                return false;
            }

            foreach (var entry in response.images)
            {
                if (entry == null || string.IsNullOrEmpty(entry.url))
                {
                    continue;
                }

                DownloadImageFromUrl(entry.url);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[AzureFn/Image] Failed to parse image urls: {ex.Message}");
        }

        return false;
    }

    private async void DownloadImageFromUrl(string url)
    {
        try
        {
            azureImageStatus = "下载图片中...";
            ClearPreviewTexture();
            Repaint();

            var tex = await DownloadTextureAsync(url);
            if (tex != null)
            {
                ClearPreviewTexture();
                tex.hideFlags = HideFlags.DontSave;
                azureImagePreview = tex;
                azureImageStatus = $"生成成功 ({tex.width}x{tex.height})";
            }
            else
            {
                azureImageStatus = "下载失败";
            }
        }
        catch (Exception ex)
        {
            azureImageStatus = $"下载失败: {ex.Message}";
            Debug.LogWarning($"[AzureFn/Image] Download failed: {ex.Message}");
            ClearPreviewTexture();
        }
        finally
        {
            Repaint();
        }
    }

    private async Task<Texture2D> DownloadTextureAsync(string url)
    {
        using (var req = UnityWebRequestTexture.GetTexture(url))
        {
            var op = req.SendWebRequest();
            while (!op.isDone)
            {
                await Task.Yield();
            }

#if UNITY_2020_2_OR_NEWER
            if (req.result != UnityWebRequest.Result.Success)
#else
            if (req.isHttpError || req.isNetworkError)
#endif
            {
                throw new Exception(req.error);
            }

            return DownloadHandlerTexture.GetContent(req);
        }
    }
}
#endif
