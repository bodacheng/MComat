using System.Collections;
using System.Collections.Generic;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public static bool CheckIfContainsAccountCharsSetKey(string monsterofPlayerId)
        {
            return monsterofPlayerId != null && AccountCharListObjectsDic.Keys.Contains(monsterofPlayerId);
        }
        
        public IEnumerator LoadMyOwnedAccountCharInfoList()
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    LoadAccountCharacterInfoListObjectsViaJsonFile();
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return LoadAccountCharacterInfoListObjectsRemote(ApiLanguage.JaJp);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            foreach (KeyValuePair<string, MonsterOfPlayerListModel> keyValuePair in AccountCharListObjectsDic)
            {
                IEnumerator getchar = Instance.GetAccountCharInfo(keyValuePair.Value.monsterOfPlayerId);
                yield return getchar;
            }
            yield break;
        }
    }
}