using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    [Serializable]
    public class ProductChangeMessage
    {
        public string Ids { get; set; }
    }
}
