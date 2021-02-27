using System.Collections;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static IEnumerator StoneGotcha()
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return SkillStoneGotcha("POLI0000000000000002", ApiLanguage.JaJp);
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
        }
    }
}