using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nBayes
{
    public class Word
    {
        public int tfFirst { get; set; }
        public int tfSecond { get; set; }
        public HashSet<int> docsFirst { get; set; }
        public HashSet<int> docsSecond { get; set; }
        public double pwFirst { get; set; }
        public double pwSecond { get; set; }

        public Word()
        {
            docsFirst = new HashSet<int>();
            docsSecond = new HashSet<int>();
        }

        public void CalcProbability(int numberDocsFirst, int numberDocsSecond)
        {
            double idf = Math.Log((numberDocsFirst + numberDocsSecond + 1) / (double)(docsFirst.Count + docsSecond.Count));
            double cfFirst = (double)numberDocsFirst / docsFirst.Count;
            if (double.IsInfinity(cfFirst))
            {
                cfFirst = 0;
            }
            double cfSecond = (double)numberDocsSecond / docsSecond.Count;
            if (double.IsInfinity(cfSecond))
            {
                cfSecond = 0;
            }
            pwFirst = Math.Log(tfFirst + 1) * idf * cfFirst;
            pwSecond = Math.Log(tfSecond + 1) * idf * cfSecond;
        }
    }
}
