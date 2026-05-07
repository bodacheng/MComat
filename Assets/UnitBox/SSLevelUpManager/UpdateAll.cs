using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using dataAccess;
using DummyLayerSystem;
using mainMenu;

public partial class SSLevelUpManager : MonoBehaviour
{
    partial void OnLevelUpAllStonesRequested()
    {
        ConfirmUpdateAll(s => { });
    }

    void ConfirmUpdateAll(Action<string> refreshStoneData)
    {
        _stoneListLayer.box._tabEffects.TurnShowingTagEffects(false);
        var stoneUpdatesConfirm = UILayerLoader.Load<StoneUpdatesConfirm>();
        stoneUpdatesConfirm.ShowInfo(
            async ()=>
            {
                UILayerLoader.Remove<StoneUpdatesConfirm>();
                await ExecuteUpdateAll(refreshStoneData);
                _stoneListLayer.box._tabEffects.TurnShowingTagEffects(true);
            },
            ()=>
            {
                _stoneListLayer.box._tabEffects.TurnShowingTagEffects(true);
                UILayerLoader.Remove<StoneUpdatesConfirm>();
            },
            StoneLevelUpProccessor.needGoldWhole,
            StoneLevelUpProccessor.UpdateAllStoneForms
        );
    }

    async UniTask ExecuteUpdateAll(Action<string> refreshStoneData)
    {
        if (Currencies.CoinCount.Value < StoneLevelUpProccessor.needGoldWhole)
        {
            PopupLayer.ArrangeWarnWindow(Translate.Get("NoEnoughGD"));
            return;
        }

        var _returnLayer = UILayerLoader.Get<ReturnLayer>();
        if (_returnLayer != null)
            _returnLayer.gameObject.SetActive(false);
        bool canNext = true;
        void Next(string x)
        {
            refreshStoneData(x);
            canNext = true;
        }
        foreach (var updateAllStoneForm in StoneLevelUpProccessor.UpdateAllStoneForms)
        {
            await UniTask.WaitUntil(()=> canNext);
            canNext = false;
            await ExecuteLevelUpStone(updateAllStoneForm.targetStoneID, updateAllStoneForm.stoneInstances, Next);
        }

        PopupLayer.ArrangeWarnWindow(Translate.Get("AutoMergeFinished"));
        if (_returnLayer != null)
            _returnLayer.gameObject.SetActive(true);

        StoneLevelUpProccessor.CalUpdateAllForms();
        LevelUpAllStonesBtn.interactable = StoneLevelUpProccessor.HasStoneToBeUpdate();
        LevelUpAllStonesBtnAnimator.SetBool("on", false);
    }
}
