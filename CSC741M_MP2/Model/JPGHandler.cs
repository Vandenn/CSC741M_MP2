using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC741M_MP2.Model
{
    public class JPGHandler
    {
        public delegate void ProgressUpdateEvent(int progress);
        public event ProgressUpdateEvent ProgressUpdate;

        private string path;
        private List<String> shotBoundaryPaths;
        private List<String> keyframePaths;

        public JPGHandler(string path)
        {
            this.path = path;
            shotBoundaryPaths = new List<String>();
            keyframePaths = new List<String>();
        }

        public static Color convertImage(string path)
        {
            Bitmap image = new Bitmap(path);
            LockBitmap lbmp = new LockBitmap(image);
            Color convertedImage = new Color();

            lbmp.LockBits();
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    convertedImage = lbmp.GetPixel(j, i);
                }
            }
            lbmp.UnlockBits();

            return convertedImage;
        }

        public List<String> getShotBoundaries()
        {
            // Implementation here.
            shotBoundaryPaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).Take(10).ToList();
            Dictionary<int, double> histogram1, histogram2;
            Color convertedImage;
            double similarity;
            List<string> results = new List<string>();
            string p;

            for (int i = 0; i < shotBoundaryPaths.Count-1; i++)
            {
                p = shotBoundaryPaths[i];
                convertedImage = convertImage(p);
                histogram1 = generateHistogram(convertedImage);
                
                p = shotBoundaryPaths[i + 1];
                convertedImage = convertImage(p);
                histogram2 = generateHistogram(convertedImage);

                similarity = getSimilarity(histogram1, histogram2);

                if (similarity < settings.SimilarityThreshold)
                {
                    results.Add(shotBoundaryPaths[i + 1]);
                }

                int progress = i / (shotBoundaryPaths.Count-1);
                ProgressUpdate(progress);
            }

            //ProgressUpdate(100);
            return shotBoundaryPaths;
        }

        public List<String> getKeyframes()
        {
            // Implementation here.
            keyframePaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).Take(10).ToList();
            ProgressUpdate(100);
            return keyframePaths;
        }

        public static Dictionary<int, double> generateHistogram(Color image)
        {
            Dictionary<int, double> histogram = new Dictionary<int, double>();

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int key = CIEConvert.LuvIndexOf(image[i, j]);
                    if (histogram.ContainsKey(key))
                    {
                        histogram[key] += 1.0;
                    }
                    else
                    {
                        histogram.Add(key, 1.0);
                    }
                }
            }

            double totalPixels = histogram.Sum(x => x.Value);

            foreach (int key in histogram.Keys.ToList())
            {
                histogram[key] /= totalPixels;
            }

            return histogram;
        }

        //private double getSimilarity(Dictionary<int, double> query, Dictionary<int, double> data, double threshold)
        private double getSimilarity(Dictionary<int, double> query, Dictionary<int, double> data)
        {
            Dictionary<int, double> compilation = new Dictionary<int, double>();
            double threshold = 0.5;
            for (int i = 0; i < query.Count; i++)
            {
                int queryKey = query.Keys.ElementAt(i);
                if (query[queryKey] >= threshold)
                {
                    compilation.Add(queryKey, AlgorithmHelper.getColorExactSimilarity(queryKey, query, data));
                }
            }

            double total = 0.0;
            int keyCount = compilation.Keys.Count;
            foreach (int key in compilation.Keys)
            {
                total += compilation[key];
            }
            total /= keyCount;

            return total;
            }
        }
    }
}
