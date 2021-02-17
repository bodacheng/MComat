using System.Collections;

namespace dataAccess
{
    public partial class AccountSet
    {
        public static PlayerInfoRefMode ReferenceMode;//至关重要的逻辑。其他所有玩家信息索引模块将参考本单例的该值来决定玩家存档读取模式。
        public static PlayerAccountInfo _AccInfo = new PlayerAccountInfo();//本单例模式的处理对象,一个参照数据库来定值的变量。
        
        public static IEnumerator LoadCustomerInfo()
        {
            switch (ReferenceMode)
            {
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.localTestSaveData:
                    yield return LoadCustomerInfoViaLocalFile();
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    //yield return loadCustomerInfoFromRemoteServer();
                    yield return loadCustomerInfoFromRemoteServer(ApiLanguage.JaJp);
                    break;
            }
        }

        public static IEnumerator SaveCustomerInfo()
        {
            switch (ReferenceMode)
            {
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.localTestSaveData:
                    SetUserData();
                    yield return OverrideAccountOnLocalFile();
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    break;
            }
        }
    }
}