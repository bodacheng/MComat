using System;
using System.Collections.Generic;
using System.Reflection;
using PlayFab;
using UnityEditor;
using UnityEngine;

// Run in batch mode with -executeMethod SteamLoginRegressionCheck.Run.
public static class SteamLoginRegressionCheck
{
    [MenuItem("Tools/Steam/Verify login error handling")]
    public static void Run()
    {
        const string fakeTicket = "TEST_AUTH_TICKET_DO_NOT_EXPOSE";
        var raw = new PlayFabError
        {
            Error = PlayFabErrorCode.InvalidSteamTicket,
            HttpCode = 400,
            ErrorMessage = "Steam API AuthenticateUserTicket: Ticket for other app for authentication ticket " + fakeTicket,
            ErrorDetails = new Dictionary<string, List<string>> { { "ticket", new List<string> { fakeTicket } } },
            CustomData = fakeTicket
        };
        var safe = Sanitize(raw);
        Require(!safe.GenerateErrorReport().Contains(fakeTicket), "Ticket leaked in the error report.");
        Require(safe.ErrorDetails == null && safe.CustomData == null, "Ticket-bearing metadata was retained.");
        Require(safe.ErrorMessage.Contains("102"), "Wrong-app diagnostic was lost.");
        Require(!Retry(safe), "Wrong-app errors must not retry.");
        Require(raw.ErrorMessage.Contains(fakeTicket), "The SDK error was mutated.");

        raw.ErrorMessage = "An unexpected response containing " + fakeTicket;
        raw.Error = PlayFabErrorCode.ServiceUnavailable;
        raw.HttpCode = 503;
        safe = Sanitize(raw);
        Require(!safe.GenerateErrorReport().Contains(fakeTicket), "An unexpected Steam error exposed its ticket.");
        Require(Retry(safe), "Transient server errors must still retry.");
        Require(Retry(new PlayFabError { Error = PlayFabErrorCode.ConnectionError, HttpCode = 408 }), "Timeouts must retry.");
        Require(!Retry(new PlayFabError { Error = PlayFabErrorCode.InvalidTicket }), "Local invalid tickets must not retry forever.");
        Require(!Retry(new PlayFabError { Error = PlayFabErrorCode.SteamNotEnabledForTitle }), "Missing Steam configuration must not retry.");
        Require(!string.IsNullOrEmpty(Sanitize(null).ErrorMessage), "Null errors must have a safe fallback.");
        Debug.Log("STEAM_LOGIN_REGRESSION_CHECK_PASSED (11 assertions)");
    }

    static PlayFabError Sanitize(PlayFabError error) => (PlayFabError)Method("SanitizeSteamLoginError").Invoke(null, new object[] { error });
    static bool Retry(PlayFabError error) => (bool)Method("IsTransientLoginError").Invoke(null, new object[] { error });
    static MethodInfo Method(string name) => typeof(PlayFabReadClient).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)
        ?? throw new Exception("Missing login method: " + name);
    static void Require(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
    }
}
