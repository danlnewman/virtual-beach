using System;
using System.Collections;
using System.Collections.Generic;

namespace server.Data
{
    public class UnityMessage 
    {
        public string mtype { get; set; }

        public UnityMessage(string mtype)
        {
            this.mtype = mtype;
        }
    }
}