using Newtonsoft.Json;

namespace nBayes
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class FileIndex : Index
    {
        private MemoryIndex index = new MemoryIndex();
        private string filePath;

        public FileIndex(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            this.filePath = filePath;
        }

        /// <exception cref="InvalidOperationException">Occurs when the serializer has trouble
        /// deserializing the file on disk. Can occur if the file is corrupted.</exception>
        public void Open()
        {
            if (File.Exists(this.filePath))
            {
                string json = File.ReadAllText(filePath);
                MemoryIndex memoryIndex = JsonConvert.DeserializeObject<MemoryIndex>(json);
                index.table = memoryIndex.table;
                index.TextCount = memoryIndex.TextCount;
            }
        }

        public override void Add(Entry document)
        {
            this.index.Add(document);
        }

        public override void Add(string word, double probability)
        {
            index.Add(word, probability);
        }

        public override double GetTokenProbability(string token)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            string indexAsString = JsonConvert.SerializeObject(index, Formatting.Indented);
            File.WriteAllText(filePath, indexAsString);
        }
    }
}
