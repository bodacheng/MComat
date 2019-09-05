namespace Api.Dto.Form.Common {

    /// <summary>
    /// 認証フォーム
    /// </summary>
    public class CertificationForm : AbstractForm {

        /// <summary>
        /// セッションID
        /// </summary>
        public string sessionId { get; set; }
    }
}