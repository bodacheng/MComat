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
        /// Sets or returns the pixel size at the 1080p reference resolution.
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
                DrawEffect();
            }
        }

        [SerializeField]
        private RawImage displayRawImage;

        [SerializeField]
        [Range(1, 200)]
        [Tooltip("Pixel size at 1080p. The amount of detail stays the same at other output resolutions.")]
        private int screenScale = 1;

        [SerializeField]
        private bool initOnAwake = true, dontDestroyOnLoad;

        private Camera cam;
        private int screenWidth, screenHeight;
        private bool isOn;
        private RenderTexture ownedTexture;
        private const int ReferenceHeight = 1080;

        /// <summary>
        /// Rendering the effect according to the specified parameters
        /// </summary>
        private void DrawEffect()
        {
            if (!TryEnsureReferences())
                return;

            ResizeRenderTexture(Screen.width, Screen.height);
        }

        private void ResizeRenderTexture(int outputWidth, int outputHeight)
        {
            screenWidth = outputWidth > 0 ? outputWidth : 1920;
            screenHeight = outputHeight > 0 ? outputHeight : ReferenceHeight;

            // Keep the vertical sample count fixed so changing output resolution
            // cannot change character detail. Only the aspect ratio affects width.
            int height = isOn ? Mathf.Max(1, ReferenceHeight / Mathf.Max(1, screenScale)) : screenHeight;
            int width = isOn ? Mathf.Max(1, Mathf.RoundToInt(height * (float)screenWidth / screenHeight)) : screenWidth;
            if (ownedTexture == null || ownedTexture.width != width || ownedTexture.height != height)
            {
                ReleaseTexture();
                ownedTexture = new RenderTexture(width, height, 24)
                {
                    filterMode = isOn ? FilterMode.Point : FilterMode.Bilinear,
                    antiAliasing = 1
                };
            }

            // The main-menu and gacha cameras share a RawImage. Rebind even when
            // reusing the texture, so switching cameras restores the right output.
            ownedTexture.filterMode = isOn ? FilterMode.Point : FilterMode.Bilinear;
            cam.targetTexture = ownedTexture;
            displayRawImage.texture = ownedTexture;
            if (isOn)
                displayRawImage.gameObject.SetActive(true);
        }

        private void ReleaseTexture()
        {
            if (ownedTexture == null)
                return;

            if (cam != null && cam.targetTexture == ownedTexture)
                cam.targetTexture = null;
            if (displayRawImage != null && displayRawImage.texture == ownedTexture)
                displayRawImage.texture = null;

            ownedTexture.Release();
            if (Application.isPlaying)
                Destroy(ownedTexture);
            else
                DestroyImmediate(ownedTexture);
            ownedTexture = null;
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
            // Only the camera currently displayed may rebind the shared RawImage
            // on a window resize. Hidden cameras catch up when switched on again.
            if (ownedTexture == null || displayRawImage == null || displayRawImage.texture != ownedTexture)
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

        private void OnDestroy()
        {
            ReleaseTexture();
            if (Instance == this)
                Instance = null;
        }
    }
}
