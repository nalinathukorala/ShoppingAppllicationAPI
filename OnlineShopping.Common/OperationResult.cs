using System;
using System.Collections.Generic;
using System.Text;
using static OnlineShopping.Common.Enum;

namespace OnlineShopping.Common
{
    public class OperationResult
    {
        /// <summary>
        /// StatusId
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Responce message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Data object
        /// </summary>
        public dynamic Data { get; set; }
    }
}
