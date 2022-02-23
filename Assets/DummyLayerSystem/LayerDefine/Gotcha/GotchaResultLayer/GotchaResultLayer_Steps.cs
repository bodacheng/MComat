using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using dataAccess;

public partial class GotchaResultLayer : UILayer
{
    // Gotcha总动画过程 点击画面的话进入下一个星星
    public IEnumerator WholeAnimProcess(List<StoneOfPlayerInfo> results)
    {
        Reset();
        NineForShow.transform.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        SpeedOnce.gameObject.SetActive(true);
        Skip.gameObject.SetActive(true);
        starFallAnimWholeProcess = StartCoroutine (StarFallAnim(results));
        
        while(!_starFallen)
            yield return new WaitForSeconds(0.1f);

        ClearFallingStars();
        
        SpeedOnce.gameObject.SetActive(false);
        Skip.gameObject.SetActive(false);
        NineForShow.transform.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);// 间隔这段时间才能确保PosDecide正常运算
        
        PosDecide();
        StarSortAnim(results);
        
        yield return new WaitForSeconds(2f);
        
        string a1 = null, a2 = null, a3 = null, b1 = null, b2 = null, b3 = null, c1 = null, c2 = null, c3 = null;
        for (int i = 0; i < results.Count; i++)
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
        
        NineForShow.ShowStones(a1, a2, a3, b1, b2, b3, c1, c2, c3);
    }
}
