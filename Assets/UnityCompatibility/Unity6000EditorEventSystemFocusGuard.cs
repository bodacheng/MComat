#if UNITY_EDITOR && UNITY_6000_5_OR_NEWER
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Unity 6000.5's StandaloneInputModule skips all pointer processing when
/// EventSystem.isFocused is false. In the Editor this flag can remain false
/// after play-mode/domain transitions, leaving legacy uGUI buttons inert.
/// </summary>
[DefaultExecutionOrder(-32000)]
internal sealed class Unity6000EditorEventSystemFocusGuard : MonoBehaviour
{
    private static readonly FieldInfo HasFocusField = typeof(EventSystem).GetField(
        "m_HasFocus",
        BindingFlags.Instance | BindingFlags.NonPublic);

    private static Unity6000EditorEventSystemFocusGuard instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Install()
    {
        if (!Application.isEditor || HasFocusField == null || instance != null)
        {
            return;
        }

        var go = new GameObject(nameof(Unity6000EditorEventSystemFocusGuard))
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        DontDestroyOnLoad(go);
        instance = go.AddComponent<Unity6000EditorEventSystemFocusGuard>();
    }

    private void Update()
    {
        RestoreFocus(EventSystem.current);
    }

    private static void RestoreFocus(EventSystem eventSystem)
    {
        if (eventSystem == null || !eventSystem.isActiveAndEnabled || eventSystem.isFocused)
        {
            return;
        }

        HasFocusField.SetValue(eventSystem, true);
    }
}
#endif
