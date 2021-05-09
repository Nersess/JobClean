using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class RequestModel
    {
        /// <summary>
        /// Gets or sets records to take
        /// </summary>
        public int Take { get; set; }        

        /// <summary>
        /// Gets or sets count to skip
        /// </summary>
        public int Skip { get; set; }


        /// <summary>
        /// Gets or sets filters in data set
        /// </summary>
        public SortModel Filter { get; set; }
    }

    public class SortModel
    {
        public string Field { get; set; }        
        public string Direction { get; set; }
    }
}
