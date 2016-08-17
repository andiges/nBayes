namespace nBayes
{
    using System.Linq;

    internal class MemoryIndex : Index
    {
        internal IndexTable<string, double> table = new IndexTable<string, double>();

        public MemoryIndex()
        {
        }

        public override void Add(Entry document)
        {
            TextCount++;
            foreach (string token in document)
            {
                if (table.ContainsKey(token))
                {
                    table[token]++;
                }
                else
                {
                    table.Add(token, 1);
                }
            }
        }

        public override void Add(string word, double probability)
        {
            table.Add(word, probability);
        }

        public override double GetTokenProbability(string token)
        {
            if (table.ContainsKey(token))
            {
                return table[token];
            }
            return 0;
        }
    }
}
