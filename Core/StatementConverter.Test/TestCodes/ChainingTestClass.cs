using System;
namespace StatementConverter.Test
{
    public class ChainingTestClass
    {
        private int chainCount = 0;
        public ChainingTestClass Chain()
        {
            chainCount++;
            Tracker.LastValue = chainCount;
            return this;
        }
    }
}
