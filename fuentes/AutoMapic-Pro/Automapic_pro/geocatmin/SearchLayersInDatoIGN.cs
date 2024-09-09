using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapic_pro.modulos
{
    public class SearchLayersInDatoIGN
    {
        public static string FindMostSimilarWord(string targetWord, List<string> wordList)
        {
            string mostSimilarWord = string.Empty;
            double highestSimilarityPercentage = 0.0;

            foreach (string word in wordList)
            {
                double similarityPercentage = CalculateSimilarityPercentage(targetWord, word);
                if (similarityPercentage > highestSimilarityPercentage)
                {
                    highestSimilarityPercentage = similarityPercentage;
                    mostSimilarWord = word;
                }
            }
            return mostSimilarWord;
        }

        public static double CalculateSimilarityPercentage(string text1, string text2)
        {
            int bigramMatchCount = 0;

            // Convertir los textos en conjuntos de bigramas (pares de caracteres adyacentes)
            var bigrams1 = GetBigrams(text1);
            var bigrams2 = GetBigrams(text2);

            // Contar el número de bigramas coincidentes
            foreach (var bigram in bigrams1)
            {
                if (bigrams2.Contains(bigram))
                {
                    bigramMatchCount++;
                }
            }

            // Calcular el coeficiente de Dice
            double similarity = (2.0 * bigramMatchCount) / (bigrams1.Count + bigrams2.Count);

            // Calcular el porcentaje de similitud
            double similarityPercentage = similarity * 100;

            return similarityPercentage;
        }

        private static List<string> GetBigrams(string text)
        {
            List<string> bigrams = new List<string>();

            for (int i = 0; i < text.Length - 1; i++)
            {
                string bigram = text.Substring(i, 2);
                bigrams.Add(bigram);
            }

            return bigrams;
        }

        public static List<string> GetFileNamesFromFolder(string folderPath)
        {
            string[] fileNames = Directory.GetFiles(folderPath);
            List<string> fileNameList = new List<string>(fileNames);
            return fileNameList;
        }

    }
}
