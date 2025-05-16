using DummyLayerSystem;
using mainMenu;

public class RenameProcess : MSceneProcess
{
    private NickNameLayer nickNameLayer;
    private bool fromArena  = false;
    public RenameProcess()
    {
        Step = MainSceneStep.Rename;
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<NickNameLayer>();
    }

    public override void ProcessEnter<T>(T t)
    {
        fromArena = true;
        ProcessEnter();
    }

    public override void ProcessEnter()
    {
        nickNameLayer = UILayerLoader.Load<NickNameLayer>();
        nickNameLayer.Setup(
            (x) =>
            {
                PopupLayer.ArrangeConfirmWindow(
                    () =>
                    {
                        ProgressLayer.Loading("");
                        nickNameLayer.LoadingRender(true);
                        PlayFabReadClient.UpdateUserTitleDisplayName(
                            x,
                            (result) =>
                            {
                                PlayerAccountInfo.Me.TitleDisplayName = result.DisplayName;
                                UILayerLoader.Remove<NickNameLayer>();
                                ProgressLayer.Close();
                                PopupLayer.ArrangeWarnWindow(
                                    ()=>
                                    {
                                        if (!fromArena)
                                            ReturnLayer.POP();
                                        else
                                        {
                                            ReturnLayer.ForceClear();
                                            ReturnLayer.Stack(MainSceneStep.FrontPage, (x)=> PreScene.target.trySwitchToStep(x, false));
                                            PreScene.target.trySwitchToStep(MainSceneStep.Arena, false);
                                        }
                                    },
                                    Translate.Get("NicknameSet"));
                            },
                            (playFabError) =>
                            {
                                nickNameLayer.LoadingRender(false);
                                ProgressLayer.Close();
                            }
                        );
                    }, 
                    Translate.Get("IfSetNickName"));
            }
        );
        SetLoaded(true);
    }
}
