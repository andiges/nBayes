namespace nBayes
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Index
    {
        public Index()
        {
        }

        public int TextCount { get; set; }

        public abstract void Add(Entry document);
        public abstract void Add(string word, double probability);
        public abstract double GetTokenProbability(string token);

        public static Index CreateMemoryIndex()
        {
            return new MemoryIndex();
        }
    }
}
