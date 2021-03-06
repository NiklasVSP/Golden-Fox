﻿using System;

namespace GoldenFox.Internal.Operators
{
    internal class Constant : IOperator
    {
        private readonly DateTime _constant;

        public Constant(DateTime constant)
        {
            _constant = constant;
        }

        public DateTime Evaluate(DateTime from, bool inclusive)
        {
            return _constant;
        }
    }
}