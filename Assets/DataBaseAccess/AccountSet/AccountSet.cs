using System.Collections;

namespace dataAccess
{
    public partial class AccountSet
    {
        public static AccountSet instance;
        public playerinfoReferenceMode _playerinfoReferenceMode;//至关重要的逻辑。其他所有玩家信息索引模块将参考本单例的该值来决定玩家存档读取模式。
        public PlayerAccountInfo _PlayerAccountInfo = new PlayerAccountInfo();//本单例模式的处理对象,一个参照数据库来定值的变量。
        public string sessionId;

        private AccountSet()
        {
        }

        public static AccountSet Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountSet();
                }
                return instance;
            }
        }

        public IEnumerator LoadCustomerInfo()
        {
            switch (_playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.formalVersion:
                    break;
                case playerinfoReferenceMode.localTestSaveData:
                    yield return LoadCustomerInfoViaLocalFile();
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    //yield return loadCustomerInfoFromRemoteServer();
                    yield return loadCustomerInfoFromRemoteServer(ApiLanguage.JaJp);
                    break;
            }
        }

        public IEnumerator SaveCustomerInfo()
        {
            switch (_playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.formalVersion:
                    break;
                case playerinfoReferenceMode.localTestSaveData:
                    yield return OverrideAccountOnLocalFile();
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    break;
            }
        }
    }
}