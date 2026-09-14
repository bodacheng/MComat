using System;
using PlayFab;
using PlayFab.ClientModels;
using Cysharp.Threading.Tasks;
using UnityEngine;
#if UNITY_STANDALONE_WIN
using Steamworks;
#endif

public partial class PlayFabReadClient
{
#if UNITY_STANDALONE_WIN
    static UniTaskVoid StartSteamLoginAsync(Action<LoginResult> onSuccess, Action<PlayFabError> onError)
    {
        LoginResult loginResult = null;
        return RunSteamAuthenticationAsync((ticket, complete) =>
        {
            PlayFabClientAPI.LoginWithSteam(new LoginWithSteamRequest
            {
                CreateAccount = true,
                SteamTicket = ticket,
                TicketIsServiceSpecific = true
            }, result =>
            {
                loginResult = result;
                complete(null);
            }, complete);
        }, () => onSuccess?.Invoke(loginResult), onError);
    }

    static void StartSteamLinkAsync(Action onSuccess, Action<PlayFabError> onError)
    {
        RunSteamAuthenticationAsync((ticket, complete) =>
        {
            PlayFabClientAPI.LinkSteamAccount(new LinkSteamAccountRequest
            {
                SteamTicket = ticket,
                TicketIsServiceSpecific = true,
                ForceLink = true
            }, result => complete(null), complete);
        }, onSuccess, onError).Forget();
    }

    // Login and linking both need a service-specific ticket kept alive until PlayFab replies.
    static async UniTaskVoid RunSteamAuthenticationAsync(
        Action<string, Action<PlayFabError>> sendRequest,
        Action onSuccess, Action<PlayFabError> onError)
    {
        var ticketHandle = HAuthTicket.Invalid;
        Callback<GetTicketForWebApiResponse_t> callback = null;
        PlayFabError loginError = null;
        try
        {
            var runtimeAppId = SteamUtils.GetAppID().m_AppId;
            if (runtimeAppId != SteamManager.AppId)
            {
                loginError = new PlayFabError
                {
                    Error = PlayFabErrorCode.InvalidSteamTicket,
                    HttpCode = 400,
                    ErrorMessage = "This game was started under the wrong Steam application. Please launch it from its Steam library entry."
                };
            }
            else
            {
                // Register before requesting; SteamManager.Update pumps this callback.
                var ticketReady = new UniTaskCompletionSource<GetTicketForWebApiResponse_t>();
                callback = Callback<GetTicketForWebApiResponse_t>.Create(response =>
                {
                    if (response.m_hAuthTicket == ticketHandle)
                        ticketReady.TrySetResult(response);
                });
                ticketHandle = SteamUser.GetAuthTicketForWebApi("AzurePlayFab");
                if (ticketHandle == HAuthTicket.Invalid)
                    throw new InvalidOperationException("Steam could not issue a login ticket.");

                var ticket = await ticketReady.Task.Timeout(TimeSpan.FromSeconds(15), DelayType.Realtime);
                if (ticket.m_eResult != EResult.k_EResultOK || ticket.m_rgubTicket == null ||
                    ticket.m_cubTicket <= 0 || ticket.m_cubTicket > ticket.m_rgubTicket.Length)
                    throw new InvalidOperationException("Steam login ticket was not ready.");

                var ticketHex = BitConverter.ToString(ticket.m_rgubTicket, 0, ticket.m_cubTicket).Replace("-", "");
                Debug.Log($"Steam login: AppID={runtimeAppId}, PlayFab TitleID={PlayFabSettings.TitleId}.");
                var requestReady = new UniTaskCompletionSource<PlayFabError>();
                sendRequest(ticketHex, error => requestReady.TrySetResult(
                    error == null ? null : SanitizeSteamLoginError(error)));
                loginError = await requestReady.Task
                    .Timeout(TimeSpan.FromSeconds(45), DelayType.Realtime);
            }
        }
        catch (TimeoutException)
        {
            loginError = new PlayFabError
            {
                Error = PlayFabErrorCode.ConnectionError,
                HttpCode = 408,
                ErrorMessage = "Steam login timed out. Check your connection and try again."
            };
        }
        catch (Exception)
        {
            // Never propagate SDK exception text: it may include an authentication ticket.
            loginError = new PlayFabError
            {
                Error = PlayFabErrorCode.InvalidSteamTicket,
                HttpCode = 400,
                ErrorMessage = "Steam authentication failed. Restart Steam and try again."
            };
        }
        finally
        {
            callback?.Dispose();
            if (ticketHandle != HAuthTicket.Invalid && SteamManager.Initialized)
                SteamUser.CancelAuthTicket(ticketHandle);
        }

        // Release the ticket before entering the next scene or scheduling another attempt.
        if (loginError == null)
            onSuccess?.Invoke();
        else
            onError?.Invoke(loginError);
    }

    static PlayFabError SanitizeSteamLoginError(PlayFabError error)
    {
        var wrongApp = error?.ErrorMessage?.IndexOf("Ticket for other app", StringComparison.OrdinalIgnoreCase) >= 0;
        if (wrongApp)
            Debug.LogWarning($"Steam authentication configuration mismatch (102). Check the Steam add-on for PlayFab Title {PlayFabSettings.TitleId}: expected AppID {SteamManager.AppId}.");

        // PlayFab's Steam error response can echo the whole ticket in ErrorMessage/ErrorDetails.
        // Copy only safe status fields before retry logging and the shared error popup see it.
        return new PlayFabError
        {
            Error = wrongApp ? PlayFabErrorCode.InvalidSteamTicket : error?.Error ?? PlayFabErrorCode.Unknown,
            HttpCode = wrongApp ? 400 : error?.HttpCode ?? 0,
            ErrorMessage = wrongApp
                ? "Steam login configuration mismatch (102). Please contact support."
                : "Steam login failed. Please try again. (" + (error?.Error.ToString() ?? "Unknown") + ")"
        };
    }
#endif
}
