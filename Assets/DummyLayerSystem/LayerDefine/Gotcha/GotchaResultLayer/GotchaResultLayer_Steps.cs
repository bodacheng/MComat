using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using dataAccess;

public partial class GotchaResultLayer : UILayer
{
    // Gotcha总动画过程 点击画面的话进入下一个星星
    public async UniTask WholeAnimProcess(List<StoneOfPlayerInfo> results)
    {
        Reset();
        NineForShow.transform.gameObject.SetActive(false);
        await UniTask.DelayFrame(1);
        SpeedOnce.gameObject.SetActive(true);
        Skip.gameObject.SetActive(true);
        starFallAnimWholeProcess = StartCoroutine (StarFallAnim(results));
        
        while(!_starFallen)
            await UniTask.DelayFrame(1);
        
        ClearFallingStars();
        SpeedOnce.gameObject.SetActive(false);
        Skip.gameObject.SetActive(false);
        NineForShow.transform.gameObject.SetActive(true);
        
        await UniTask.DelayFrame(1);
        
        PosDecide();
        StarSortAnim(results);
        
        await UniTask.Delay( TimeSpan.FromSeconds( 2 ) );
        
        string a1 = null, a2 = null, a3 = null, b1 = null, b2 = null, b3 = null, c1 = null, c2 = null, c3 = null;
        for (var i = 0; i < results.Count; i++)
        {
            switch(i)
            {
                case 0:
                    a1 = results[i].skillId;
                    break;
                case 1:
                    a2 = results[i].skillId;
                    break;
                case 2:
                    a3 = results[i].skillId;
                    break;
                case 3:
                    b1 = results[i].skillId;
                    break;
                case 4:
                    b2 = results[i].skillId;
                    break;
                case 5:
                    b3 = results[i].skillId;
                    break;
                case 6:
                    c1 = results[i].skillId;
                    break;
                case 7:
                    c2 = results[i].skillId;
                    break;
                case 8:
                    c3 = results[i].skillId;
                    break;
            }
        }
        await NineForShow.ShowStones(a1, a2, a3, b1, b2, b3, c1, c2, c3);
    }
}
