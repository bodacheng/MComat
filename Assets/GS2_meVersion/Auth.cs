using System.Collections;
using UnityEngine;
using Gs2.Weave.Login;
using Gs2.Weave.Credential;
using Weave.Core.Runtime;

public class Auth : MonoBehaviour
{
    public static Gs2Client _myclient;
    public static Gs2GameSession _mysession;

    public static Auth target;

    /// <summary>
    /// GS2 相关
    /// </summary>
    /// 
    public me_LoginDirector loginDirector;
    public CredentialDirector credentialDirector;

    public void OnCreateGs2Client(Gs2Client client)
    {
        Debug.Log("SceneDirector::OnCreateGs2Client");
        Auth._myclient = client;
        StartCoroutine(loginDirector.Run(client.Client, new PlayerPrefsAccountRepository()));
    }

    public void OnCreateGameSession(Gs2GameSession session)// login
    {
        Debug.Log("SceneDirector::OnCreateGameSession");
        Auth._mysession = session;
    }

    public IEnumerator Gs2Login()
    {
        me_LoginDirector.loginFinished = false;
        yield return credentialDirector.Run();
        while (!me_LoginDirector.loginFinished)
        {
            yield return null;
        }
    }
}
