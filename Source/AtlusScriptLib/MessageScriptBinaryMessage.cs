﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlusScriptLib.MessageScript
{
    public abstract class MessageScriptBinaryMessage
    {
        public abstract MessageScriptBinaryMessageType Type { get; }
    }
}