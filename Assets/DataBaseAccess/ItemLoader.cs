using System;

namespace dataAccess
{
    public static class ItemLoader
    {
        public static void LoadAll(Action<int> finished)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    MyMonsters.LoadLocal();
                    MySkillStones.LoadAllLocal();
                    finished.Invoke(1);
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabRead.LoadItems(finished);
                    break;
            }
        }
    }
}