using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Recorder;
using UnityEditor.Recorder.Encoder;
using UnityEditor.Recorder.Input;
#endif

public class ScreenCaptureExample : MonoBehaviour
{
    // 截图快捷键
    public KeyCode screenshotKey = KeyCode.Space;
    // 录屏开关快捷键
    public KeyCode videoToggleKey = KeyCode.V;
    // 保存图片、视频的基础路径
    public string screenshotFolderPath = "Screenshots";
    // 录屏时使用的帧率
    [Range(1, 120)]
    public int recordingFrameRate = 30;
    // 是否同时录制音频
    public bool captureAudioWhileRecording = true;

    static ScreenCaptureExample _instance;
    bool _isVideoRecording;

#if UNITY_EDITOR
    RecorderController _recorderController;
    RecorderControllerSettings _recorderControllerSettings;
    MovieRecorderSettings _movieRecorderSettings;
    string _currentRecordingOutputPath;
#endif

    void Awake()
    {
        // Ensure a single persistent instance survives scene changes.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_EDITOR
    void Update()
    {
        if (IsKeyAssigned(screenshotKey) && Input.GetKeyDown(screenshotKey))
        {
            CaptureScreenshot();
        }

        if (IsKeyAssigned(videoToggleKey) && Input.GetKeyDown(videoToggleKey))
        {
            ToggleVideoRecording();
        }
    }
    
#endif

    /// <summary>
    /// 供 UI Button 调用的截图方法。
    /// </summary>
    public void CaptureScreenshotFromButton()
    {
        CaptureScreenshot();
    }

    /// <summary>
    /// 供 UI Button 调用的录屏开关方法。
    /// </summary>
    public void ToggleVideoRecording()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Debug.LogWarning("录屏功能只能在播放模式下使用。");
            return;
        }

        if (_isVideoRecording)
        {
            StopVideoRecordingInternal();
        }
        else
        {
            StartVideoRecordingInternal();
        }
#else
        Debug.LogWarning("当前构建环境不支持 Unity Recorder 录屏。");
#endif
    }

#if UNITY_EDITOR
    bool IsKeyAssigned(KeyCode keyCode)
    {
        return keyCode != KeyCode.None;
    }
#endif

    void CaptureScreenshot()
    {
        string targetDirectory = ResolveOutputDirectory();
        Directory.CreateDirectory(targetDirectory);

        // 使用时间戳创建文件名，以免重复
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string filename = "Screenshot_" + timestamp + ".png";
        string fullPath = Path.Combine(targetDirectory, filename);

        // 捕捉并保存截图
        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("Screenshot saved at: " + fullPath);
    }

    string ResolveOutputDirectory()
    {
        if (string.IsNullOrEmpty(screenshotFolderPath))
        {
            return Path.Combine(Path.GetDirectoryName(Application.dataPath), "Screenshots");
        }

        if (Path.IsPathRooted(screenshotFolderPath))
        {
            return screenshotFolderPath;
        }

        var projectRoot = Path.GetDirectoryName(Application.dataPath);
        if (string.IsNullOrEmpty(projectRoot))
        {
            return Path.GetFullPath(screenshotFolderPath);
        }

        return Path.GetFullPath(Path.Combine(projectRoot, screenshotFolderPath));
    }

#if UNITY_EDITOR
    void StartVideoRecordingInternal()
    {
        EnsureRecorderInitialized();

        string targetDirectory = ResolveOutputDirectory();
        Directory.CreateDirectory(targetDirectory);

        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string baseFileName = "Recording_" + timestamp;
        string absolutePathWithoutExtension = Path.Combine(targetDirectory, baseFileName);

        try
        {
            _movieRecorderSettings.OutputFile = absolutePathWithoutExtension;
            _movieRecorderSettings.FileNameGenerator.ForceAssetsFolder = false;
            _movieRecorderSettings.FrameRate = recordingFrameRate;
            _movieRecorderSettings.CaptureAudio = captureAudioWhileRecording;
            _movieRecorderSettings.Take = 1;

            _recorderControllerSettings.FrameRate = recordingFrameRate;

            _currentRecordingOutputPath = absolutePathWithoutExtension + ".mp4";

            _recorderController.PrepareRecording();
            if (_recorderController.StartRecording())
            {
                _isVideoRecording = true;
                Debug.Log("开始录屏，输出文件：" + _currentRecordingOutputPath);
            }
            else
            {
                Debug.LogWarning("录屏启动失败，请检查 Recorder 配置。");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("启动录屏失败: " + ex.Message);
            _isVideoRecording = false;
        }
    }

    void StopVideoRecordingInternal()
    {
        if (!_isVideoRecording)
        {
            return;
        }

        _recorderController.StopRecording();
        _isVideoRecording = false;

        if (!string.IsNullOrEmpty(_currentRecordingOutputPath))
        {
            Debug.Log("录屏结束，文件保存在：" + _currentRecordingOutputPath);
            _currentRecordingOutputPath = null;
        }
    }

    void OnDestroy()
    {
        CleanupRecorderResources();
    }

    void OnDisable()
    {
        CleanupRecorderResources();
    }

    void EnsureRecorderInitialized()
    {
        if (_recorderController != null)
        {
            return;
        }

        _recorderControllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        _recorderControllerSettings.hideFlags = HideFlags.HideAndDontSave;
        _recorderControllerSettings.SetRecordModeToManual();
        _recorderControllerSettings.FrameRate = recordingFrameRate;
        _recorderControllerSettings.FrameRatePlayback = FrameRatePlayback.Variable;
        _recorderControllerSettings.CapFrameRate = false;
        _recorderControllerSettings.ExitPlayMode = false;

        _movieRecorderSettings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        _movieRecorderSettings.hideFlags = HideFlags.HideAndDontSave;
        _movieRecorderSettings.name = "ScreenCaptureExampleMovieRecorder";
        _movieRecorderSettings.Enabled = true;
        _movieRecorderSettings.CaptureAudio = captureAudioWhileRecording;
        _movieRecorderSettings.ImageInputSettings = new GameViewInputSettings();
        _movieRecorderSettings.FrameRatePlayback = FrameRatePlayback.Variable;
        _movieRecorderSettings.EncoderSettings = new CoreEncoderSettings
        {
            Codec = CoreEncoderSettings.OutputCodec.MP4,
            EncodingQuality = CoreEncoderSettings.VideoEncodingQuality.High
        };

        _recorderControllerSettings.AddRecorderSettings(_movieRecorderSettings);
        _recorderController = new RecorderController(_recorderControllerSettings);
    }

    void CleanupRecorderResources()
    {
        if (_isVideoRecording && _recorderController != null)
        {
            _recorderController.StopRecording();
        }

        if (_movieRecorderSettings != null)
        {
            Destroy(_movieRecorderSettings);
            _movieRecorderSettings = null;
        }

        if (_recorderControllerSettings != null)
        {
            Destroy(_recorderControllerSettings);
            _recorderControllerSettings = null;
        }

        _recorderController = null;
        _currentRecordingOutputPath = null;
        _isVideoRecording = false;
    }

#endif
}
