using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class UnitInstructionLayer : UILayer
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Text unitName;
    [SerializeField] private Text unitIntro;
    [SerializeField] private float unitImagePos = 100f;
    [SerializeField] private float emergeDuration = 2f;
    
    public async void RandomChangeUnitImage()
    {
        if (unitImageKeys.Count == 0) return;
        int randomIndex = Random.Range(0, unitImageKeys.Count);
        var value = await AddressablesLogic.LoadT<Sprite>(unitImageKeys[randomIndex]);
        var bigCurtainRect = unitImage.transform.GetComponent<RectTransform>();
        bigCurtainRect.sizeDelta = new Vector2(value.rect.width * bigCurtainRect.rect.height / value.rect.height, bigCurtainRect.rect.height);
        unitImage.sprite = value;

        float posX = bigCurtainRect.anchoredPosition.x;
        DOTween.To(()=> posX, (value)=>posX = value, -unitImagePos, emergeDuration).
            OnUpdate(
                () =>
                {
                    bigCurtainRect.anchoredPosition = new Vector2(posX, bigCurtainRect.anchoredPosition.y);
                }
        );
    }
    
    private static readonly List<string> unitImageKeys = new ();
    public static async UniTask LoadUnitImage()
    {
        var loadPath = Addressables.LoadResourceLocationsAsync( new List<string> {"unit_image"} , Addressables.MergeMode.Intersection);
        await loadPath;
        if (loadPath.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var path in loadPath.Result)
            {
                if (!unitImageKeys.Contains(path.PrimaryKey))
                    unitImageKeys.Add(path.PrimaryKey);
            }
        }
        Addressables.Release(loadPath);
    }
}
