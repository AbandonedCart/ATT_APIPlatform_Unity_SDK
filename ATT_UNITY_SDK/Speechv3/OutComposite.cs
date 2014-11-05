using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK.Speechv3
{
    /// <summary>
    /// Encapsulates OutComposite data retuned in speech to text response
    /// </summary>
     public class OutComposite
    {
        /// <summary>
        /// Gets or sets Grammar
        /// </summary>
         public string Grammar { get; set; }

         /// <summary>
         /// Gets or sets Out
         /// </summary>
         public string Out { get; set; }
         
    }
}
