using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using FightScene;

public partial class ArenaFightOver : UILayer
{
    [SerializeField] private BOButton storyMaskBtn;
    [SerializeField] private Image storyBgImage;
    [SerializeField] private Text shortStory;
    [SerializeField] private Color storyBgFromColor;
    [SerializeField] private Color storyBgToColor;
    [SerializeField] private Color gbStoryBgToColor;
    [SerializeField] private float storyBgColorChangeDuration;
    [SerializeField] private AudioSource storyLayerAudio;

    [SerializeField] private GameObject storyLayerBg;
    [SerializeField] private BOButton storyPicOnClickBtn;
    [SerializeField] private Image storyPicBgImage;
    [SerializeField] private SizeAdjustBySpriteSize sizeAdjustBySpriteSize;
    [SerializeField] private GameObject storyLineBg;
    [SerializeField] private Text storyLineText;
    
    private TweenerCore<Color, Color, ColorOptions> storyBgColorChangeTween;

    
    private int _currentSceneIndex = 0;
    private int _currentLineIndex = 0;
    private bool _storyContentLogged;
    
    bool picStoryEnded = false;
    public async UniTask LoadStory()
    {
        _storyContentLogged = false;
        bool storyFromAI = false;
        if (CommonSetting.PcMode && 
            (!String.IsNullOrEmpty(FightLoad.Fight.StoryKey) || FightScene.FightScene.target.AIStoryInfo != null))
        {
            StoryInfo story = null;
            if (!String.IsNullOrEmpty(FightLoad.Fight.StoryKey))
                story = await StoryManager.Instance.LoadStory(FightLoad.Fight.StoryKey);
            
            if (story == null)
            {
                story = FightScene.FightScene.target.AIStoryInfo;
                storyFromAI = story != null;
            }
            
            if (story == null)
            {
                picStoryEnded = true;
            }
            else
            {
                RectTransform rectTransform = storyPicBgImage.GetComponent<RectTransform>();
                // 获取当前宽度
                float currentWidth = rectTransform.sizeDelta.x;
                // 设置新的大小，宽度保持不变，高度设置为目标值
                rectTransform.sizeDelta = new Vector2(currentWidth, PosCal.CanvasHeight);
                storyLayerBg.SetActive(true);
                storyBgColorChangeTween = storyPicBgImage.DOColor(Color.white, storyBgColorChangeDuration);
                storyPicBgImage.gameObject.SetActive(true);
                storyPicOnClickBtn.SetListener(() => { OnClick(story); });
                if (!_storyContentLogged)
                {
                    AIServiceManager.LogStoryContentForDebug(story);
                    _storyContentLogged = true;
                }
                _currentSceneIndex = 0;
                DisplayCurrentScene(story);
                storyPicBgImage.gameObject.SetActive(true);
            }
        }
        else
        {
            picStoryEnded = true;
        }
        await UniTask.WaitUntil(()=> picStoryEnded);
        
        if (storyFromAI)
        {
            var manager = FightScene.FightScene.target?.AIServiceManager;
            manager?.MarkEventStoryAsShown();
        }
    }
    
    private void OnClick(StoryInfo storyInfo)
    {
        // 如果还有未显示的文字，显示下一句
        if (_currentLineIndex < storyInfo.StoryScenes[_currentSceneIndex].Lines.Count - 1)
        {
            _currentLineIndex++;
            DisplayCurrentLine(storyInfo);
        }
        else
        {
            // 如果当前场景还有未显示的图片，切换到下一张图片
            if (_currentSceneIndex < storyInfo.StoryScenes.Count - 1)
            {
                _currentSceneIndex++;
                DisplayCurrentScene(storyInfo);
            }
            else
            {
                // 剧情播放完毕，可以在这里实现一些游戏逻辑，比如回到主菜单或进入下一关
                storyLayerBg.gameObject.SetActive(false);
                storyPicBgImage.gameObject.SetActive(false);
                picStoryEnded = true;
            }
        }
    }
    
    private void DisplayCurrentScene(StoryInfo _storyInfo)
    {
        _currentLineIndex = -1;
        storyPicBgImage.sprite = _storyInfo.StoryScenes[_currentSceneIndex].Pic;
        sizeAdjustBySpriteSize.AdjustSize();
        storyLineText.text = string.Empty;
        if (storyLineBg != null)
        {
            storyLineBg.SetActive(false);
        }
    }

    private void DisplayCurrentLine(StoryInfo _storyInfo)
    {
        if (_currentLineIndex < 0)
        {
            if (storyLineBg != null)
            {
                storyLineBg.SetActive(false);
            }
            return;
        }

        string displayLine = string.Empty;

        if (_storyInfo.StoryScenes[_currentSceneIndex].Lines.Count > _currentLineIndex)
        {
            var line = Story.Get(_storyInfo.StoryScenes[_currentSceneIndex].Lines[_currentLineIndex]);
            if (!String.IsNullOrEmpty(line))
            {
                displayLine = line;
            }
            else
            {
                displayLine = _storyInfo.StoryScenes[_currentSceneIndex].Lines[_currentLineIndex];
            }
        }
        else
        {
            displayLine = string.Empty;
        }

        storyLineText.text = displayLine;
        if (storyLineBg != null)
        {
            storyLineBg.SetActive(!string.IsNullOrEmpty(displayLine));
        }
    }
    
    bool clickedOnShortStory = false;
    private static readonly Dictionary<string, string> QuestShortStoryCache = new Dictionary<string, string>();
    private static readonly HashSet<string> QuestShortStoryPrefetching = new HashSet<string>();
    private static int QuestShortStoryCacheVersion;

    public static void ClearQuestShortStoryCache()
    {
        QuestShortStoryCache.Clear();
        QuestShortStoryPrefetching.Clear();
        QuestShortStoryCacheVersion++;
    }
    
    public async UniTask LoadShortMessage()
    {
        var code = FightLoad.Fight.ID;
        string shortMessage = string.Empty;

        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Quest:
                shortMessage = GetPrefetchedQuestShortStory(code);
                break;
            default:
                break;
        }
        
        shortStory.text = shortMessage ?? string.Empty;
        storyLayerAudio.volume = AppSetting.Value.BgmVolume;
        
        bool notNull = !string.IsNullOrEmpty(shortStory.text);
        storyBgImage.gameObject.SetActive(notNull);
        shortStory.gameObject.SetActive(notNull);
        if (notNull)
        {
            storyBgImage.color = storyBgFromColor;
            storyBgColorChangeTween = storyBgImage.DOColor(FightLoad.Fight.EventType == FightEventType.Quest ? storyBgToColor : gbStoryBgToColor, storyBgColorChangeDuration);
            storyMaskBtn.SetListener(()=>
            {
                storyBgImage.gameObject.SetActive(false);
                clickedOnShortStory = true;
            });
        }
        else
        {
            clickedOnShortStory = true;
        }
        await UniTask.WaitUntil(()=> clickedOnShortStory);
    }
    
    public static void PreloadQuestShortStoryIfNeeded()
    {
        var fight = FightLoad.Fight;
        if (fight == null || fight.EventType != FightEventType.Quest)
        {
            return;
        }

        var code = fight.ID;
        if (string.IsNullOrWhiteSpace(code) || QuestShortStoryCache.ContainsKey(code) || QuestShortStoryPrefetching.Contains(code))
        {
            return;
        }

        var tableStory = ShortStory.Get(code);
        if (!string.IsNullOrWhiteSpace(tableStory))
        {
            QuestShortStoryCache[code] = tableStory.Trim();
            return;
        }

        QuestShortStoryPrefetching.Add(code);
        PrefetchQuestShortStoryAsync(code, QuestShortStoryCacheVersion).Forget();
    }

    private static async UniTaskVoid PrefetchQuestShortStoryAsync(string code, int cacheVersion)
    {
        var manager = FightScene.FightScene.target?.AIServiceManager;
        if (manager == null)
        {
            Debug.LogWarning("[ArenaFightOver] AIServiceManager missing, cannot generate short story fallback.");
            QuestShortStoryPrefetching.Remove(code);
            return;
        }

        try
        {
            var generated = await manager.GenerateLocalizedProverbAsync();
            generated = string.IsNullOrWhiteSpace(generated) ? null : generated.Trim();
            if (!string.IsNullOrEmpty(generated))
            {
                if (cacheVersion == QuestShortStoryCacheVersion)
                {
                    QuestShortStoryCache[code] = generated;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[ArenaFightOver] Failed to generate short story via AI: {e.Message}");
        }
        finally
        {
            QuestShortStoryPrefetching.Remove(code);
        }
    }

    private string GetPrefetchedQuestShortStory(string code)
    {
        if (QuestShortStoryCache.TryGetValue(code, out var cached) && !string.IsNullOrWhiteSpace(cached))
        {
            return cached;
        }

        return null;
    }
}
