using DummyLayerSystem;
using PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PlayFabReadClient
{
    public static void ErrorReport(PlayFabError error)
    {
        Debug.Log("error.ErrorMessage:"+ error.ErrorMessage);
        switch (error.Error)
        {
            case PlayFabErrorCode.NotAuthorizedByTitle:
                PopupLayer.ArrangeWarnWindow(
                    ()=>
                    {
                        if (SceneManager.GetActiveScene().buildIndex != 0)
                        {
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            UILayerLoader.Remove<PopupLayer>();
                        }
                    },
                    Translate.Get("NotAuthorizedByTitle"));
                break;
            case PlayFabErrorCode.ConnectionError:
                PopupLayer.ArrangeWarnWindow(
                    ()=>
                    {
                        if (SceneManager.GetActiveScene().buildIndex != 0)
                        {
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            UILayerLoader.Remove<PopupLayer>();
                        }
                    },
                    Translate.Get("ReturnToLobbyForConnectionError"));
                break;
            case PlayFabErrorCode.InvalidUsername:
                PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidUsername"));
                break;
            case PlayFabErrorCode.DuplicateUsername:
                PopupLayer.ArrangeWarnWindow(Translate.Get("DuplicateUsername"));
                break;
            case PlayFabErrorCode.InvalidParams:
                PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidUsername"));
                break;
            default:
                PopupLayer.ArrangeWarnWindow(Translate.Get("ConnectionError"));
                break;
        }
    }
}
