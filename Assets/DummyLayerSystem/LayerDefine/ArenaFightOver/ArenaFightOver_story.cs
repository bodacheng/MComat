using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [SerializeField] private Text storyLineText;
    
    private TweenerCore<Color, Color, ColorOptions> storyBgColorChangeTween;

    
    private int _currentSceneIndex = 0;
    private int _currentLineIndex = 0;
    
    bool picStoryEnded = false;
    public async UniTask LoadStory()
    {
        if (CommonSetting.PcMode && 
            (!String.IsNullOrEmpty(FightLoad.Fight.StoryKey) || FightScene.FightScene.target.AIStoryInfo != null))
        {
            StoryInfo story = null;
            if (!String.IsNullOrEmpty(FightLoad.Fight.StoryKey))
                story = await StoryManager.Instance.LoadStory(FightLoad.Fight.StoryKey);
            
            if (story == null)
            {
                story = FightScene.FightScene.target.AIStoryInfo;
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
                DisplayCurrentScene(story);
                storyPicBgImage.gameObject.SetActive(true);
            }
        }
        else
        {
            picStoryEnded = true;
        }
        await UniTask.WaitUntil(()=> picStoryEnded);
    }
    
    private void OnClick(StoryInfo storyInfo)
    {
        // 如果还有未显示的文字，显示下一句
        if (_currentLineIndex < storyInfo.StoryScenes[_currentSceneIndex].Lines.Count - 1)
        {
            _currentLineIndex++;
            DisplayCurrentLine(storyInfo);
            Debug.Log(storyInfo.StoryScenes[_currentSceneIndex].Lines.Count  + " : "+ _currentLineIndex);
        }
        else
        {
            // 如果当前场景还有未显示的图片，切换到下一张图片
            if (_currentSceneIndex < storyInfo.StoryScenes.Count - 1)
            {
                _currentSceneIndex++;
                _currentLineIndex = 0;
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
        storyPicBgImage.sprite = _storyInfo.StoryScenes[_currentSceneIndex].Pic;
        sizeAdjustBySpriteSize.AdjustSize();
        DisplayCurrentLine(_storyInfo);
    }

    private void DisplayCurrentLine(StoryInfo _storyInfo)
    {
        if (_storyInfo.StoryScenes[_currentSceneIndex].Lines.Count > _currentLineIndex)
            storyLineText.text =  Story.Get(_storyInfo.StoryScenes[_currentSceneIndex].Lines[_currentLineIndex]);
        else
        {
            storyLineText.text = "";
        }
    }
    
    bool clickedOnShortStory = false;
    
    public async UniTask LoadShortMessage()
    {
        var code = FightLoad.Fight.ID;

        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Quest:
                shortStory.text = ShortStory.Get(code);
                break;
            default:
                break;
        }
        
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
}
