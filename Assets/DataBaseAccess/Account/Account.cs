using System;

namespace dataAccess
{
    public partial class Account
    {
        public static PlayerInfoRefMode ReferenceMode;//至关重要的逻辑。其他所有玩家信息索引模块将参考本单例的该值来决定玩家存档读取模式。
        public static PlayerAccountInfo _AccInfo;//本单例模式的处理对象,一个参照数据库来定值的变量。
        
        public static void GetPlayerData(Action<int> finished)
        {
            switch (ReferenceMode)
            {
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.localTestSaveData:
                    GetPlayerDataViaLocal();
                    finished(1);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    GetUserDataRemote(finished);
                    break;
            }
        }

        public static void GetStatistics(Action<int> finished)
        {
            switch (ReferenceMode)
            {
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.localTestSaveData:
                    finished(1);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    GetStatisticsRemote(finished);
                    break;
            }
        }
    }
}