using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine.UI;

public partial class GotchaResultLayer : UILayer
{
        // Gotcha总过程 点击画面的话进入下一个星星
    public IEnumerator GotchaAnimProcess(List<StoneOfPlayerInfo> results)
    {
        NineForShow.transform.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        SpeedOnce.gameObject.SetActive(true);
        Skip.gameObject.SetActive(true);
        starFallAnimWholeProcess = StartCoroutine (StarFallAnim(results));
        
        while(!_starFalled)
            yield return new WaitForSeconds(0.1f);
        
        SpeedOnce.gameObject.SetActive(false);
        Reset();
        Skip.gameObject.SetActive(false);
        NineForShow.transform.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        PosDecide();
        StarSortAnim(results);
        
        string A1skillid = null, A2skillid= null, A3skillid= null, 
            B1skillid= null, B2skillid= null, B3skillid= null, 
            C1skillid= null, C2skillid= null, C3skillid= null;
        for (int i = 0; i < results.Count; i++)
        {
            switch(i)
            {
                case 0:
                    A1skillid = results[i].skillId;
                    break;
                case 1:
                    A2skillid = results[i].skillId;
                    break;
                case 2:
                    A3skillid = results[i].skillId;
                    break;
                case 3:
                    B1skillid = results[i].skillId;
                    break;
                case 4:
                    B2skillid = results[i].skillId;
                    break;
                case 5:
                    B3skillid = results[i].skillId;
                    break;
                case 6:
                    C1skillid = results[i].skillId;
                    break;
                case 7:
                    C2skillid = results[i].skillId;
                    break;
                case 8:
                    C3skillid = results[i].skillId;
                    break;
            }
        }
        
        NineForShow.ShowStones(A1skillid, A2skillid, A3skillid, B1skillid, B2skillid, B3skillid, C1skillid, C2skillid, C3skillid);
    }
}
