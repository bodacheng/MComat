using DummyLayerSystem;
using mainMenu;

public class RankingPage : MSceneProcess
{
    private RankingLayer layer;
    
    public RankingPage()
    {
        Step = MainSceneStep.Ranking;
    }
    
    public override void ProcessEnter()
    {
        layer = UILayerLoader.Load<RankingLayer>();
        CloudScript.GetLeaderboard(
            obj =>
            {
                layer.DisplayOpponents(obj);
            }
        );
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<RankingLayer>();
    }
}
