using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

sealed class FullResolutionParticleIcon
{
    Material _material;
    Texture _texture;
    Color _color = Color.white;
    RawImage _activeTarget;

    public void Initialize(ParticleSystem source)
    {
        var renderer = source != null ? source.GetComponent<ParticleSystemRenderer>() : null;
        var material = renderer != null ? renderer.sharedMaterial : null;
        var texture = material != null ? material.mainTexture : null;

        if (renderer == null || texture == null)
        {
            return;
        }

        var main = source.main;
        _material = material;
        _texture = texture;
        _color = main.startColor.color;

        // Only the root renderer is the fixed icon. Child particle rings and glows keep rendering normally.
        renderer.enabled = false;
    }

    public void Show(RawImage target)
    {
        if (target == null || _material == null || _texture == null)
        {
            return;
        }

        _activeTarget = target;
        _activeTarget.material = _material;
        _activeTarget.texture = _texture;
        _activeTarget.color = _color;
        _activeTarget.enabled = true;
    }

    public void Hide()
    {
        if (_activeTarget == null)
        {
            return;
        }

        _activeTarget.enabled = false;
        _activeTarget = null;
    }
}

public class ElementEffectsGroup
{
    IDictionary<Button, ParticleSystem> _btnRefreshEffects = new Dictionary<Button, ParticleSystem>();
    readonly IDictionary<Button, RectTransform> _buttonRects = new Dictionary<Button, RectTransform>();
    ParticleSystem _triggerExplosion0;
    ParticleSystem _triggerExplosion1;
    ParticleSystem _triggerExplosion2;
    ParticleSystem _triggerExplosion3;
    IDictionary<Button, ParticleSystem> _buttonSlotEffects;
    ParticleSystem _defendBtn;
    ParticleSystem _rushBtn;
    ParticleSystem _dreamComboBtn;
    ParticleSystem _pressingExplosion; // 这个不需要对象池。
    readonly FullResolutionParticleIcon _defendIcon = new FullResolutionParticleIcon();
    readonly FullResolutionParticleIcon _rushIcon = new FullResolutionParticleIcon();
    readonly FullResolutionParticleIcon _dreamComboIcon = new FullResolutionParticleIcon();
    RawImage _dreamComboIconTarget;

    public void DreamComboEffectOn(bool on)
    {
        if (on)
        {
            _dreamComboBtn.Play(true);
            _dreamComboIcon.Show(_dreamComboIconTarget);
        }
        else
        {
            _dreamComboBtn.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _dreamComboIcon.Hide();
        }
    }

    public void StartPressing(Button targetBtn)
    {
        if (!_buttonRects.TryGetValue(targetBtn, out var targetRect) || targetRect == null)
        {
            return;
        }

        var targetPos = PosCal.GetWorldPos(FightScene.FightScene.target.fxCamera, targetRect, 7);
        _pressingExplosion.transform.position = targetPos;
        _pressingExplosion.Play();
    }

    public void StopPressing()
    {
        _pressingExplosion.Stop();
    }

    public void BtnRefreshEffect()
    {
        foreach (var pair in _btnRefreshEffects)
        {
            if (!_buttonRects.TryGetValue(pair.Key, out var targetRect) || targetRect == null)
            {
                continue;
            }

            pair.Value.transform.position = PosCal.GetWorldPos(FightScene.FightScene.target.fxCamera, targetRect, 4);
            pair.Value.Play(true);
        }
    }

    public ParticleSystem GetExplosionEffect(int spLevel)
    {
        ParticleSystem targetExplode;
        switch(spLevel)
        {
            case 0:
                targetExplode = _triggerExplosion0;
                break;
            case 1:
                targetExplode = _triggerExplosion1;
                break;
            case 2:
                targetExplode = _triggerExplosion2;
                break;
            case 3:
                targetExplode = _triggerExplosion3;
                break;
            default:
                return null;
        }
        return targetExplode;
    }

    public void Close(ParticleSystemStopBehavior systemStopBehavior)
    {
        foreach (var kv in _buttonSlotEffects)
        {
            kv.Value.Stop(true, systemStopBehavior);
        }

        _triggerExplosion0.Stop(true, systemStopBehavior);
        _triggerExplosion1.Stop(true, systemStopBehavior);
        _triggerExplosion2.Stop(true, systemStopBehavior);
        _triggerExplosion3.Stop(true, systemStopBehavior);

        foreach (var keyValue in _btnRefreshEffects)
        {
            keyValue.Value.Stop(true, systemStopBehavior);
        }
        _pressingExplosion.Stop(true, systemStopBehavior);
        _rushBtn.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _dreamComboBtn.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _defendIcon.Hide();
        _rushIcon.Hide();
        _dreamComboIcon.Hide();
        _dreamComboIconTarget = null;

        if (FightGlobalSetting.HasDefend)
            _defendBtn.Stop(true, systemStopBehavior);
    }

    public void Open(
        Vector3 defendBtnPos,
        Vector3 rushBtnPos,
        Vector3 dreamComboPos,
        RawImage defendIcon,
        RawImage rushIcon,
        RawImage dreamComboIcon)
    {
        _triggerExplosion0.Stop(true);
        _triggerExplosion1.Stop(true);
        _triggerExplosion2.Stop(true);
        _triggerExplosion3.Stop(true);

        foreach (var keyValue in _btnRefreshEffects)
        {
            keyValue.Value.Stop(true);
        }
        _pressingExplosion.Stop(true);
        _rushBtn.gameObject.transform.position = rushBtnPos;
        _rushBtn.Play(true);
        _rushIcon.Show(rushIcon);

        _dreamComboBtn.transform.position = dreamComboPos;
        _dreamComboIconTarget = dreamComboIcon;

        if (FightGlobalSetting.HasDefend)
        {
            _defendBtn.gameObject.transform.position = defendBtnPos;
            _defendBtn.Play(true);
            _defendIcon.Show(defendIcon);
        }
    }

    public async UniTask InitializeCommon(Transform targetRectT, Element element, Button a1Btn, Button a2Btn, Button a3Btn, Button dreamComboBtn)
    {
        var path = FightGlobalSetting.EffectPathDefine(element);
        _buttonRects.Clear();
        CacheButtonRect(a1Btn);
        CacheButtonRect(a2Btn);
        CacheButtonRect(a3Btn);
        CacheButtonRect(dreamComboBtn);

        var tasks = new List<UniTask<ParticleSystem>>
        {
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/slot.prefab")
        };
        var results = await UniTask.WhenAll(tasks);
        var attackSlot = results[0];
        var fire1Slot = results[1];
        var fire2Slot = results[2];
        var dreamSlot = results[3];

        attackSlot.transform.SetParent(targetRectT);
        fire1Slot.transform.SetParent(targetRectT);
        fire2Slot.transform.SetParent(targetRectT);
        dreamSlot.transform.SetParent(targetRectT);

        _buttonSlotEffects = new Dictionary<Button, ParticleSystem>
        {
            { a1Btn, attackSlot },
            { a2Btn, fire1Slot },
            { a3Btn, fire2Slot },
            { dreamComboBtn, dreamSlot }
        };

        if (FightGlobalSetting.HasDefend)
        {
            _defendBtn = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/defend.prefab");
            _defendIcon.Initialize(_defendBtn);
        }

        var tasks2 = new List<UniTask<ParticleSystem>>
        {
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/rush.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/refresh.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion0.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion1.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion2.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion3.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/pressing.prefab"),
            AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/dreamCombo.prefab")
        };

        var results2 = await UniTask.WhenAll(tasks2);

        _rushBtn = results2[0];
        var a1Refresh = results2[1];
        var a2Refresh = results2[2];
        var a3Refresh = results2[3];
        _triggerExplosion0 = results2[4];
        _triggerExplosion1 = results2[5];
        _triggerExplosion2 = results2[6];
        _triggerExplosion3 = results2[7];
        _pressingExplosion = results2[8];
        _dreamComboBtn = results2[9];
        _rushIcon.Initialize(_rushBtn);
        _dreamComboIcon.Initialize(_dreamComboBtn);

        _btnRefreshEffects = new Dictionary<Button, ParticleSystem>
        {
            { a1Btn, a1Refresh },
            { a2Btn, a2Refresh },
            { a3Btn, a3Refresh }
        };

        foreach (var p in results2)
        {
            p.transform.SetParent(targetRectT);
        }
    }

    void CacheButtonRect(Button button)
    {
        if (button == null)
        {
            return;
        }

        var rectTransform = button.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            _buttonRects[button] = rectTransform;
        }
    }

    public void RefreshSlotEffect(Button button, string skillId, Vector3 pos)
    {
        if (String.IsNullOrEmpty(skillId))
        {
            _buttonSlotEffects[button].transform.position = pos;
            _buttonSlotEffects[button].Play(true);
        }else{
            _buttonSlotEffects[button].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
