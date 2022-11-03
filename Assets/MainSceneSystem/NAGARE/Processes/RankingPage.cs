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
        
        ProgressLayer.Loading(">");
        CloudScript.GetLeaderboard(
            obj =>
            {
                ProgressLayer.Close();
                layer.DisplayOpponents(obj);
            },
            () =>
            {
                ProgressLayer.Close();
            }
        );
        
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<RankingLayer>();
    }
}
