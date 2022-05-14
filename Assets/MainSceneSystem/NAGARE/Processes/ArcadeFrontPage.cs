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
                if (arcadeTop.ArcadeStages.ContainsKey(PlayerAccountInfo.Me.ArcadeProcess))
                {
                    var StageInfo = arcadeTop.ArcadeStages[PlayerAccountInfo.Me.ArcadeProcess];
                    arcadeTop.IconButtonFeature(StageInfo.stageButton.MemberIcons[0]);
                }else{
                    Debug.Log("巨大错误。玩家关卡进度值不对应任何关卡" + PlayerAccountInfo.Me.ArcadeProcess);
                }
            }
        );
    }
    
    public override void ProcessEnd()
    {
        ArcadeTop.Close();
    }
}