﻿namespace AtlusScriptLib.FlowScriptLanguage.Syntax
{
    public class FlowScriptLogicalNotOperator : FlowScriptPrefixOperator
    {
        public override string ToString()
        {
            return $"!({Operand})";
        }
    }
}