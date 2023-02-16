using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using DG.Tweening;
using mainMenu;

public partial class GotchaResultLayer : UILayer
{
    #region 屏幕星星飞入位置
    [SerializeField] RectTransform starWaitPos1, starWaitPos2, starWaitPos3, starWaitPos4, starWaitPos5, starWaitPos6, starWaitPos7, starWaitPos8, starWaitPos9;
    readonly List<RectTransform> waitPos = new();
    #endregion
    
    readonly List<Vector3> slotScreenPos = new();
    
    void SetWaitPos()
    {
        waitPos.Clear();
        waitPos.Add(starWaitPos1);
        waitPos.Add(starWaitPos2);
        waitPos.Add(starWaitPos3);
        waitPos.Add(starWaitPos4);
        waitPos.Add(starWaitPos5);
        waitPos.Add(starWaitPos6);
        waitPos.Add(starWaitPos7);
        waitPos.Add(starWaitPos8);
        waitPos.Add(starWaitPos9);
    }
    
    void StarSortAnim(List<StoneOfPlayerInfo> results)
    {
        for (int i = 0; i < results.Count; i++)
        {
            StarScreenMoveAnim(results[i], PosCal.GetWorldPos(PreScene.target.postProcessCamera, waitPos[i], 5f), slotScreenPos[i]);
        }
    }
    
    /// <summary>
    /// 一颗星星从屏幕外移动向格子内的动画
    /// </summary>
    /// <param name="info"></param>
    /// <param name="waitPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    void StarScreenMoveAnim(StoneOfPlayerInfo info, Vector3 waitPos, Vector3 endPos)
    {
        var effectSet = effectDic[info];
        var screenStar = effectSet.stoneFigure;
        screenStar.Play();
        screenStar.transform.position = waitPos;
        effectSet.RunSequence(
            DOTween.Sequence().Append(screenStar.transform.DOMove(endPos, 2f).OnComplete(
            () =>
            {
                screenStar.Stop();
                var effect = effectSet.screenExplosionFigure;
                effect.transform.position = endPos;
                effect.Play(true);
            })
        ));
    }
    
    // 必须使用时候即时运行因为里面几个决定位置的运算要考虑当前相机位置等
    void PosDecide()
    {
        // 星星落入格子
        var a1ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.A1T.GetComponent<RectTransform>(), 5f);
        var a2ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.A2T.GetComponent<RectTransform>(), 5f);
        var a3screenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.A3T.GetComponent<RectTransform>(), 5f);
        var b1ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.B1T.GetComponent<RectTransform>(), 5f);
        var b2ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.B2T.GetComponent<RectTransform>(), 5f);
        var b3ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.B3T.GetComponent<RectTransform>(), 5f);
        var c1ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.C1T.GetComponent<RectTransform>(), 5f);
        var c2ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.C2T.GetComponent<RectTransform>(), 5f);
        var c3ScreenPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, NineForShow.C3T.GetComponent<RectTransform>(), 5f);
        slotScreenPos.Clear();
        slotScreenPos.Add(a1ScreenPos);
        slotScreenPos.Add(a2ScreenPos);
        slotScreenPos.Add(a3screenPos);
        slotScreenPos.Add(b1ScreenPos);
        slotScreenPos.Add(b2ScreenPos);
        slotScreenPos.Add(b3ScreenPos);
        slotScreenPos.Add(c1ScreenPos);
        slotScreenPos.Add(c2ScreenPos);
        slotScreenPos.Add(c3ScreenPos);
    }
}
