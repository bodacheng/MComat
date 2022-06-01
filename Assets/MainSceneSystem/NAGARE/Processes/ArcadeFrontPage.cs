using mainMenu;
using UnityEngine;

public class ArcadeFrontPage : MainSceneProcess
{
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
        Inherit(PreScene.target);
    }

    ArcadeTop arcadeTop;
    public override void ProcessEnter()
    {
        arcadeTop = ArcadeTop.Open(() =>
            {
                
            }
        );

        var stages = arcadeTop.NewStages(PlayerAccountInfo.Me.ArcadeProcess);
        arcadeTop.ShowStages(stages);
    }
    
    public override void ProcessEnd()
    {
        ArcadeTop.Close();
    }
}