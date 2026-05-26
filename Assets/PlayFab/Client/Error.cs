using DummyLayerSystem;
using PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PlayFabReadClient
{
    public static void ErrorReport(PlayFabError error)
    {
        ErrorReportInternal(error, true);
    }

    public static void ErrorReportStayInScene(PlayFabError error)
    {
        ErrorReportInternal(error, false);
    }

    static void ErrorReportInternal(PlayFabError error, bool returnToMainMenu)
    {
        if (error == null)
        {
            Debug.LogWarning("PlayFab error is null.");
            return;
        }

        Debug.LogWarning("PlayFab error: " + error.GenerateErrorReport());
        var shouldReturnToMainMenu = returnToMainMenu && ShouldReturnToMainMenu(error);
        if (Application.isPlaying)
        {
            switch (error.Error)
            {
                case PlayFabErrorCode.NotAuthorizedByTitle:
                    PopupLayer.ArrangeWarnWindow(
                        ()=>
                        {
                            HandleErrorReturn(shouldReturnToMainMenu);
                        },
                        Translate.Get("NotAuthorizedByTitle"));
                    break;
                case PlayFabErrorCode.ConnectionError:
                    PopupLayer.ArrangeWarnWindow(
                        ()=>
                        {
                            HandleErrorReturn(shouldReturnToMainMenu);
                        },
                        Translate.Get("ConnectionError"));
                    break;
                case PlayFabErrorCode.InvalidUsername:
                    PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidUsername"));
                    break;
                case PlayFabErrorCode.DuplicateUsername:
                    PopupLayer.ArrangeWarnWindow(Translate.Get("DuplicateUsername"));
                    break;
                case PlayFabErrorCode.InvalidParams:
                    PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidParams"));
                    break;
                case PlayFabErrorCode.AccountNotFound:
                    PopupLayer.ArrangeWarnWindow(Translate.Get("AccountNotFound"));
                    break;
                case PlayFabErrorCode.InvalidEmailOrPassword:
                    PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidEmailOrPassword"));
                    break;
                default:
                    PopupLayer.ArrangeWarnWindow(
                        ()=>
                        {
                            HandleErrorReturn(shouldReturnToMainMenu);
                        },
                        Translate.Get("ConnectionError"));
                    break;
            }
        }
    }

    static bool ShouldReturnToMainMenu(PlayFabError error)
    {
        switch (error.Error)
        {
            case PlayFabErrorCode.AccountBanned:
            case PlayFabErrorCode.InvalidSessionTicket:
            case PlayFabErrorCode.NotAuthenticated:
            case PlayFabErrorCode.ExpiredAuthToken:
            case PlayFabErrorCode.NotAuthorizedByTitle:
                return true;
            default:
                return false;
        }
    }

    static void HandleErrorReturn(bool returnToMainMenu)
    {
        if (!returnToMainMenu)
        {
            UILayerLoader.Remove<PopupLayer>();
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            UILayerLoader.Remove<PopupLayer>();
        }
    }
}
