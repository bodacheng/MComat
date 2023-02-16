using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using dataAccess;
using DG.Tweening;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;

public partial class GotchaResultLayer : UILayer
{
    public NineForShow NineForShow;
    
    #region 动画的跳过以及加速
    [SerializeField] Button Skip;
    [SerializeField] Button SpeedOnce;
    bool _starFallen;
    bool _oneStarFallen;
    Coroutine starFallAnimWholeProcess;
    Coroutine starFallAnimOneProcess;
    #endregion

    private readonly Dictionary<StoneOfPlayerInfo, StoneFallEffectSet> effectDic = new();
    private class StoneFallEffectSet
    {
        public ParticleSystem stoneFigure;
        public ParticleSystem stoneFlashFigure;
        public ParticleSystem screenExplosionFigure;
        Sequence currentSequence;
        
        public async UniTask Load(int spLevel)
        {
            string stoneFigureName = string.Empty;
            string flashName = string.Empty;
            string explosionName = string.Empty;
            
            switch(spLevel) // 这里应该是rarelevel
            {
                case 0:
                    stoneFigureName = "gachastar0";
                    flashName = "screenStarExplostionTest0";
                    explosionName = "ButtonEffects/redmagic/explosion0.prefab";
                    break;
                case 1:
                    stoneFigureName = "gachastar1";
                    flashName = "screenStarExplostionTest1";
                    explosionName = "ButtonEffects/redmagic/explosion1.prefab";
                    break;
                case 2:
                    stoneFigureName = "gachastar2";
                    flashName = "screenStarExplostionTest2";
                    explosionName = "ButtonEffects/redmagic/explosion2.prefab";
                    break;
                case 3:
                    stoneFigureName = "gachastar3";
                    flashName = "screenStarExplostionTest3";
                    explosionName = "ButtonEffects/redmagic/explosion3.prefab";
                    break;
            }
            stoneFigure = await AddressablesLogic.LoadTOnObject<ParticleSystem>(stoneFigureName);
            stoneFlashFigure = await AddressablesLogic.LoadTOnObject<ParticleSystem>(flashName);
            screenExplosionFigure = await AddressablesLogic.LoadTOnObject<ParticleSystem>(explosionName);
            
            stoneFigure.Stop(true);
            stoneFlashFigure.Stop(true);
            screenExplosionFigure.Stop(true);
        }

        public void Clear()
        {
            if (stoneFigure != null)
                Destroy(stoneFigure.gameObject);
            if (stoneFlashFigure != null)
                Destroy(stoneFlashFigure.gameObject);
            if (screenExplosionFigure != null)
                Destroy(screenExplosionFigure.gameObject);

            KillSequence();
        }

        void KillSequence()
        {
            if (currentSequence != null)
            {
                currentSequence.Kill();
                currentSequence = null;
            }
        }

        public void RunSequence(Sequence task)
        {
            KillSequence();
            currentSequence = task;
        }
    }

    async UniTask PrepareEffects(List<StoneOfPlayerInfo> results)
    {
        async UniTask Prepare(StoneOfPlayerInfo result)
        {
            var set = new StoneFallEffectSet();
            var skillConfig = SkillConfigTable.GetSkillConfig(result.SkillId);
            await set.Load(skillConfig.SP_LEVEL);
            DicAdd<StoneOfPlayerInfo, StoneFallEffectSet>.Add(effectDic, result, set);
        }
        var tasks = new List<UniTask>();
        foreach (var result in results)
        {
            tasks.Add(Prepare(result));
        }
        await UniTask.WhenAll(tasks);
    }
    
    public void Setup()
    {
        Skip.onClick.AddListener(SkipStarFallAnim);
        SpeedOnce.onClick.AddListener(SpeedOneGotchaAnim);
        SetWaitPos();
    }

    public static void Close()
    {
        var layer = UILayerLoader.Get<GotchaResultLayer>();
        if (layer != null)
        {
            layer.Reset();
        }
        UILayerLoader.Remove<GotchaResultLayer>();
    }
    
    // 清理相关特效等等
    public void Reset()
    {
        _starFallen = false;
        _oneStarFallen = false;
        ClearDetail();
        ClearAllParticle();
    }

    void ClearAllParticle()
    {
        foreach (var kv in effectDic)
        {
            kv.Value.Clear();
        }
        effectDic.Clear();
    }

    void FallingStarsFade()
    {
        foreach (var kv in effectDic)
        {
            kv.Value.stoneFlashFigure.Stop();
            kv.Value.stoneFigure.Stop();
        }
    }
}
