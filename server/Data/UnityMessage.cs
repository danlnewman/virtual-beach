using System;
using System.Collections;
using System.Collections.Generic;

namespace server.Data
{
    public class UnityMessage 
    {
        public string type { get; set; }

        public UnityMessage(string type)
        {
            this.type = type;
        }
    }
}