namespace nBayes
{
    using System;

    public class Analyzer
    {
        private double I = 0;
        private double invI = 0;

        public Analyzer()
        {
            this.Tolerance = .00f;
        }

        public float Tolerance { get; set; }

        public CategorizationResult Categorize(Entry item, Index first, Index second)
        {
            double prediction = GetPrediction(item, first, second);

            if (prediction < .5f - this.Tolerance)
                return CategorizationResult.Second;

            if (prediction > .5 + this.Tolerance)
                return CategorizationResult.First;


            return CategorizationResult.Undetermined;
        }

        public double GetPrediction(Entry item, Index first, Index second)
        {
            I = 0;
            invI = 0;
            foreach (string token in item)
            {
                double firstProbability = first.GetTokenProbability(token);
                double secondProbability = second.GetTokenProbability(token);
                if (firstProbability == 0 || secondProbability == 0)
                {
                    continue;
                }
                I += Math.Log(firstProbability);
                invI += Math.Log(secondProbability);
            }

            double prediction = CombineProbability(first.TextCount, second.TextCount);
            return prediction;
        }

        #region Private Methods
        private double CombineProbability(double cat1total, double cat2total)
        {
            double logposD = Math.Log(cat1total / (cat1total + cat2total));
            I += logposD;
            double lognegD = Math.Log(cat2total / (cat1total + cat2total));
            invI += lognegD;
            while (I < -600 || invI < -600)
            {
                I /= 10;
                invI /= 10;
            }
            return Math.Exp(I) / (Math.Exp(I) + Math.Exp(invI));
        }

        #endregion
    }
}
