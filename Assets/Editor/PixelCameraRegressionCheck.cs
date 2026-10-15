using System;
using System.Reflection;
using PixelCameraEffect;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class PixelCameraRegressionCheck
{
    const BindingFlags PrivateInstance = BindingFlags.Instance | BindingFlags.NonPublic;

    [MenuItem("Tools/Rendering/Verify resolution-independent pixelation")]
    public static void Run()
    {
        Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Run this check outside Play mode.");
        var scene = EditorSceneManager.NewPreviewScene();
        PixelCamera first = null, second = null;
        try
        {
            var root = new GameObject("PixelCamera regression fixtures") { hideFlags = HideFlags.HideAndDontSave };
            root.SetActive(false);
            SceneManager.MoveGameObjectToScene(root, scene);
            var imageObject = new GameObject("Shared output", typeof(RectTransform), typeof(RawImage));
            imageObject.transform.SetParent(root.transform, false);
            var output = imageObject.GetComponent<RawImage>();
            first = CreateCamera(root, output);
            second = CreateCamera(root, output);

            RenderTexture reference = null;
            foreach (var size in new[] { new Vector2Int(1280, 720), new Vector2Int(1920, 1080),
                         new Vector2Int(2560, 1440), new Vector2Int(3840, 2160) })
            {
                Resize(first, size.x, size.y);
                var texture = Check(first, output, 320, 180, FilterMode.Point);
                if (reference != null) Require(texture == reference, "Same-aspect resolution change recreated the texture.");
                reference = texture;
            }

            foreach (var size in new[] { new Vector3Int(1920, 1200, 288), new Vector3Int(1024, 768, 240),
                         new Vector3Int(3440, 1440, 430), new Vector3Int(1080, 1920, 101) })
            {
                var previous = Texture(first);
                Resize(first, size.x, size.y);
                Check(first, output, size.z, 180, FilterMode.Point);
                Require(previous == null, "Replaced texture was not destroyed.");
            }

            Set(first, "screenScale", 3);
            Resize(first, 1920, 1080);
            Check(first, output, 640, 360, FilterMode.Point);
            Set(first, "screenScale", 8);
            Resize(first, 3840, 2160);
            Check(first, output, 240, 135, FilterMode.Point);

            Set(first, "isOn", false);
            Resize(first, 1280, 720);
            Check(first, output, 1280, 720, FilterMode.Bilinear);
            var native = Texture(first);
            Resize(first, 1600, 900);
            Check(first, output, 1600, 900, FilterMode.Bilinear);
            Require(native == null, "Resizing with the effect off leaked the native-resolution texture.");

            // Main-menu and gacha both update while sharing a RawImage. Only its
            // current owner may react to a window resize automatically.
            native = Texture(first);
            Resize(second, 1920, 1080);
            var activeTexture = Check(second, output, 320, 180, FilterMode.Point);
            Set(first, "screenWidth", -1);
            Set(first, "screenHeight", -1);
            Method("Update").Invoke(first, null);
            Require(output.texture == activeTexture && first.MainCamera.targetTexture == native,
                "Automatic resize on the inactive camera stole the active camera's output.");
            Set(second, "screenWidth", -1);
            Set(second, "screenHeight", -1);
            Method("Update").Invoke(second, null);
            Require(output.texture == Texture(second) && second.MainCamera.targetTexture == Texture(second),
                "The active camera lost its output during automatic resize.");

            Set(first, "isOn", true);
            Set(first, "screenScale", 6);
            Resize(first, 1920, 1080);
            var firstTexture = Check(first, output, 320, 180, FilterMode.Point);
            Resize(second, 1920, 1080);
            var secondTexture = Check(second, output, 320, 180, FilterMode.Point);
            Require(firstTexture != secondTexture, "Cameras must own separate textures.");
            Resize(first, 3840, 2160);
            Require(Check(first, output, 320, 180, FilterMode.Point) == firstTexture,
                "Switching back to a camera failed to rebind its existing texture.");
            Release(second);
            Require(secondTexture == null && second.MainCamera.targetTexture == null,
                "Releasing an inactive camera failed to free its texture and detach its camera.");
            Require(output.texture == firstTexture, "Releasing an inactive camera cleared another camera's output.");
            Resize(second, 1920, 1080);
            secondTexture = Texture(second);
            Release(first);
            Require(firstTexture == null && output.texture == secondTexture,
                "Releasing the first camera damaged the second camera's output.");
            Release(second);
            Require(secondTexture == null && output.texture == null && second.MainCamera.targetTexture == null,
                "Final release left a texture or output binding behind.");
            Release(second); // Cleanup is safe to repeat.
            Debug.Log("PIXEL_CAMERA_REGRESSION_CHECK_PASSED (resolution, aspect, scale, native output, ownership and cleanup)");
        }
        finally
        {
            try
            {
                if (first != null) Release(first);
            }
            finally
            {
                try
                {
                    if (second != null) Release(second);
                }
                finally
                {
                    EditorSceneManager.ClosePreviewScene(scene);
                }
            }
        }
    }

    static PixelCamera CreateCamera(GameObject root, RawImage output)
    {
        var go = new GameObject("PixelCamera fixture", typeof(Camera));
        go.transform.SetParent(root.transform, false);
        var pixel = go.AddComponent<PixelCamera>();
        Set(pixel, "cam", go.GetComponent<Camera>());
        Set(pixel, "displayRawImage", output);
        Set(pixel, "screenScale", 6);
        Set(pixel, "isOn", true);
        return pixel;
    }

    static RenderTexture Check(PixelCamera pixel, RawImage output, int width, int height, FilterMode filter)
    {
        var texture = Texture(pixel);
        Require(texture != null && texture.width == width && texture.height == height,
            $"Expected {width}x{height} texture.");
        Require(texture.filterMode == filter && texture.antiAliasing == 1, "Incorrect sampling settings.");
        Require(pixel.MainCamera.targetTexture == texture && output.texture == texture, "Incorrect output binding.");
        Require(texture.IsCreated() || texture.Create(), "RenderTexture allocation failed.");
        return texture;
    }

    static RenderTexture Texture(PixelCamera pixel) => (RenderTexture)Field("ownedTexture").GetValue(pixel);
    static void Set(PixelCamera pixel, string name, object value) => Field(name).SetValue(pixel, value);
    static FieldInfo Field(string name) => typeof(PixelCamera).GetField(name, PrivateInstance)
        ?? throw new Exception("Missing PixelCamera field: " + name);
    static void Resize(PixelCamera pixel, int width, int height) => Method("ResizeRenderTexture").Invoke(pixel, new object[] { width, height });
    static void Release(PixelCamera pixel) => Method("ReleaseTexture").Invoke(pixel, null);
    static MethodInfo Method(string name) => typeof(PixelCamera).GetMethod(name, PrivateInstance)
        ?? throw new Exception("Missing PixelCamera method: " + name);
    static void Require(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
    }
}
