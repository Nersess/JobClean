using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class ResponseModel<T>
    {

        public ResponseModel()
        {
            Data = new List<T>();
        }
        /// <summary>
        /// Gets or sets taken data
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        ///Count of records
        /// </summary>
        public int Count { get; set; }

    }
}
