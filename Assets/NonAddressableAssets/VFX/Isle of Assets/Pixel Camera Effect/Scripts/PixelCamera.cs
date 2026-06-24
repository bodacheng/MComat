using UnityEngine;
using UnityEngine.UI;

namespace PixelCameraEffect
{
    [HelpURL("https://assetstore.unity.com/packages/slug/231188")]
    public class PixelCamera : MonoBehaviour
    {
        public static PixelCamera Instance { get; private set; }

        /// <summary>
        /// Returns the main camera on the scene
        /// </summary>
        public Camera MainCamera
        {
            get
            {
                return cam;
            }
        }

        /// <summary>
        /// Sets or returns the degree of pixilization
        /// </summary>
        public int ScreenScale
        {
            get
            {
                return screenScale;
            }
            set
            {
                screenScale = value < 1 ? 1 : value;
                if (isOn)
                    DrawEffect();
            }
        }

        /// <summary>
        /// Activating and deactivating the pixelation effect
        /// </summary>
        public bool IsOn
        {
            get
            {
                return isOn;
            }
            set
            {
                if (isOn == value)
                    return;
                isOn = value;
                if (value)
                {
                    DrawEffect();
                }
                else
                {
                    if (!TryEnsureReferences())
                        return;

                    screenWidth = screenWidth > 0 ? screenWidth : (Screen.width > 0 ? Screen.width : 1920);
                    screenHeight = screenHeight > 0 ? screenHeight : (Screen.height > 0 ? Screen.height : 1080);
                    RenderTexture renderTexture = new RenderTexture(screenWidth, screenHeight, 24);
                    cam.targetTexture = renderTexture;
                    displayRawImage.texture = renderTexture;
                }
            }
        }

        [SerializeField]
        private RawImage displayRawImage;

        [SerializeField]
        [Range(1, 200)]
        private int screenScale = 1;

        [SerializeField]
        private bool initOnAwake = true, dontDestroyOnLoad;

        private Camera cam;
        private int screenWidth, screenHeight;
        private bool isOn;

        /// <summary>
        /// Rendering the effect according to the specified parameters
        /// </summary>
        private void DrawEffect()
        {
            if (!TryEnsureReferences())
                return;

            screenWidth = Screen.width > 0 ? Screen.width : 1920;
            screenHeight = Screen.height > 0 ? Screen.height : 1080;
            int safeScreenScale = Mathf.Max(1, screenScale);
            RenderTexture renderTexture = new RenderTexture(
                Mathf.Max(1, screenWidth / safeScreenScale),
                Mathf.Max(1, screenHeight / safeScreenScale),
                24)
            { filterMode = FilterMode.Point, antiAliasing = 1 };
            cam.targetTexture = renderTexture;
            displayRawImage.gameObject.SetActive(true);
            displayRawImage.texture = renderTexture;
        }

        private bool TryEnsureReferences()
        {
            if (cam == null)
                cam = GetComponent<Camera>();

            if (cam == null)
            {
                Debug.LogError($"{nameof(PixelCamera)} requires a Camera component.", this);
                return false;
            }

            if (displayRawImage == null)
            {
                Debug.LogError($"{nameof(PixelCamera)} requires a display RawImage.", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Initializing this instance
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            if (initOnAwake)
                IsOn = true;
        }

        /// <summary>
        /// Redrawing the effect if the height or width of the screen has been changed
        /// </summary>
        private void Update()
        {
            if (!isOn)
                return;
            if (Screen.width != screenWidth || Screen.height != screenHeight)
                DrawEffect();
        }

        /// <summary>
        /// Redrawing the effect when changing values in the editor during the game
        /// </summary>
        private void OnValidate()
        {
            if (!isOn)
                return;
            if (Application.isPlaying)
                DrawEffect();
        }
    }
}
