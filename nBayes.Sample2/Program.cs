using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nBayes.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            Index spam = Index.CreateMemoryIndex();
            Index notspam = Index.CreateMemoryIndex();

            // train the indexes
            HashSet<String> textFirstClass = new HashSet<string>();
            textFirstClass.Add("want some viagra?");
            textFirstClass.Add("cialis can make you larger");
            HashSet<String> textSecondClass = new HashSet<string>();
            textSecondClass.Add("Hello, how are you?");
            textSecondClass.Add("Did you go to the park today?");

            Dictionary<string, Word> allWords = new Dictionary<string, Word>();
            int numberDocsFirst = 0;
            foreach (string text in textFirstClass)
            {
                numberDocsFirst++;
                Entry words = Entry.FromString(text);
                foreach (string word in words)
                {
                    if (allWords.ContainsKey(word))
                    {
                        allWords[word].tfFirst++;
                        allWords[word].docsFirst.Add(numberDocsFirst);
                    }
                    else
                    {
                        Word w = new Word();
                        w.tfFirst = 1;
                        w.docsFirst.Add(numberDocsFirst);
                        allWords.Add(word, w);
                    }
                }
            }
            int numberDocsSecond = 0;
            foreach (string text in textSecondClass)
            {
                numberDocsSecond++;
                Entry words = Entry.FromString(text);
                foreach (string word in words)
                {
                    if (allWords.ContainsKey(word))
                    {
                        allWords[word].tfSecond++;
                        allWords[word].docsSecond.Add(numberDocsSecond);
                    }
                    else
                    {
                        Word w = new Word();
                        w.tfSecond = 1;
                        w.docsSecond.Add(numberDocsSecond);
                        allWords.Add(word, w);
                    }
                }
            }

            foreach (var w in allWords)
            {
                w.Value.CalcProbability(numberDocsFirst, numberDocsSecond);
                spam.Add(w.Key, w.Value.pwFirst);
                notspam.Add(w.Key, w.Value.pwSecond);
            }
            spam.TextCount = numberDocsFirst;
            notspam.TextCount = numberDocsSecond;

            Analyzer analyzer = new Analyzer();
            CategorizationResult result = analyzer.Categorize(
                 Entry.FromString("cialis viagra"),
                 spam,
                 notspam);

            switch (result)
            {
                case CategorizationResult.First:
                    Console.WriteLine("Spam");
                    break;
                case CategorizationResult.Undetermined:
                    Console.WriteLine("Undecided");
                    break;
                case CategorizationResult.Second:
                    Console.WriteLine("Not Spam");
                    break;
            }
            Console.ReadKey();
        }
    }
}
