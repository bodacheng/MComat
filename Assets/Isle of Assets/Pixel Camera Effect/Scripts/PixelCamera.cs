using UnityEngine;
using UnityEngine.UI;

namespace PixelCameraEffect
{
    [HelpURL("https://assetstore.unity.com/packages/slug/231188")]
    public class PixelCamera : MonoBehaviour
    {
        
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
                isOn = value;
                
                if (value)
                {
                    DrawEffect();
                }
                else
                {
                    RenderTexture renderTexture = new RenderTexture(screenWidth, screenHeight, 24);
                    cam.targetTexture = renderTexture;
                    displayRawImage.texture = renderTexture;
                }
            }
        }
        
        [SerializeField]private Camera cam;

        [SerializeField]
        private RawImage displayRawImage;

        [SerializeField]
        [Range(1, 200)]
        private int screenScale = 1;

        [SerializeField]
        private bool initOnAwake = true, dontDestroyOnLoad;
        
        private int screenWidth, screenHeight;
        private bool isOn;

        /// <summary>
        /// Rendering the effect according to the specified parameters
        /// </summary>
        private void DrawEffect()
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            if (screenWidth <= 0 || screenHeight <= 0)
            {
                screenWidth = 1920;
                screenHeight= 1080;
            }
            RenderTexture renderTexture = new RenderTexture(screenWidth / screenScale, screenHeight / screenScale, 24) { filterMode = FilterMode.Point, antiAliasing = 1 };
            cam.targetTexture = renderTexture;
            displayRawImage.texture = renderTexture;
        }

        /// <summary>
        /// Initializing this instance
        /// </summary>
        private void Awake()
        {
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