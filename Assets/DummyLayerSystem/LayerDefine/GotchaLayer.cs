using mainMenu;
using dataAccess;
using System.Collections.Generic;

public class GotchaLayer : UILayer
{
    public void OneTime()
    {
    }
    
    public void NineTimes()
    {
    }
    
    public void GachaTest()
    {
        //Server.RandomRemove25Items();
        CloudScript.GachaTest(temp);
        void temp(List<StoneOfPlayerInfo> stones)
        {
            GachaResult.Result = stones;
            PreScene.target.trySwitchToStep(MainSceneStep.GotchaAnim, true);
        }
    }

    public void GetAllSK()
    {
        CloudScript.GrantStonesTest();
    }

    public void GetAllM()
    {
        CloudScript.GrantMonsterTest();
    }

    public void Remove25Stones()
    {
        CloudScript.Remove25Stones();
    }
    
    public static GotchaLayer Open()
    {
        return UILayerLoader.Load(PreScene.target.T,"GotchaLayer") as GotchaLayer;
    }

    public static void Close()
    {
        UILayerLoader.Remove("GotchaLayer");
    }
}