using System;

namespace Api.Dto.Model.Common {

    /// <summary>
    /// ベースモデル(データあり)
    /// </summary>
    /// <typeparam name="T">データ型</typeparam>
    [Serializable]
    public class BaseModel<T> : AbstractModel {

        /// <summary>
        /// データ
        /// </summary>
        public T data { get; set; }
    }
}