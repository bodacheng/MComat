using System.Collections;

namespace dataAccess
{
    public partial class AccountSet
    {
        public static playerInfoRefMode _playerinfoReferenceMode;//至关重要的逻辑。其他所有玩家信息索引模块将参考本单例的该值来决定玩家存档读取模式。
        public static PlayerAccountInfo _AccInfo = new PlayerAccountInfo();//本单例模式的处理对象,一个参照数据库来定值的变量。
        public static string sessionId;
                
        public static IEnumerator LoadCustomerInfo()
        {
            switch (_playerinfoReferenceMode)
            {
                case playerInfoRefMode.formalVersion:
                    break;
                case playerInfoRefMode.localTestSaveData:
                    yield return LoadCustomerInfoViaLocalFile();
                    break;
                case playerInfoRefMode.remoteTestPlayer:
                    //yield return loadCustomerInfoFromRemoteServer();
                    yield return loadCustomerInfoFromRemoteServer(ApiLanguage.JaJp);
                    break;
            }
        }

        public static IEnumerator SaveCustomerInfo()
        {
            switch (_playerinfoReferenceMode)
            {
                case playerInfoRefMode.formalVersion:
                    break;
                case playerInfoRefMode.localTestSaveData:
                    yield return OverrideAccountOnLocalFile();
                    break;
                case playerInfoRefMode.remoteTestPlayer:
                    break;
            }
        }
    }
}