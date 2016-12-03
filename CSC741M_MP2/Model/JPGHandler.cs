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
        private List<String> imagePaths;
        private List<String> shotBoundaryPaths;
        private List<String> keyframePaths;
        private Settings settings;

        public JPGHandler(string path)
        {
            this.path = path;
            shotBoundaryPaths = new List<String>();
            keyframePaths = new List<String>();
            imagePaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).ToList();
            settings = Settings.getInstance();
        }

        public Dictionary<int, int> convertImage(string path)
        {
            Bitmap image = new Bitmap(path);
            LockBitmap lbmp = new LockBitmap(image);
            Dictionary<int, int> histogram = new Dictionary<int, int>();

            lbmp.LockBits();
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color c = lbmp.GetPixel(j, i);
                    int key = ((c.R & 192) >> 2) + ((c.G & 192) >> 4) + ((c.B & 192) >> 6) & 63;
                    if (histogram.ContainsKey(key))
                    {
                        histogram[key] += 1;
                    }
                    else
                    {
                        histogram.Add(key, 1);
                    }
                }
            }
            lbmp.UnlockBits();

            return histogram;
        }

        public List<string> getShotBoundaries()
        {
            shotBoundaryPaths = new List<string>();
            Dictionary<int, int> histogram1, histogram2;
            double[] differences = new double[imagePaths.Count - 1];

            histogram1 = convertImage(imagePaths[0]);
            for (int i = 1; i < imagePaths.Count; i++)
            {
                histogram2 = convertImage(imagePaths[i]);
                differences[i - 1] = getDifference(histogram1, histogram2);
                histogram1 = histogram2;
                ProgressUpdate((i / (imagePaths.Count - 1)) * 100);
            }

            double averageDifference = differences.Average();
            double sumOfSquaresOfDifferences = differences.Select(diff => (diff - averageDifference) * (diff - averageDifference)).Sum();
            double differenceStandardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / differences.Length);
            double breakThreshold = averageDifference + settings.constantAValue * differenceStandardDeviation;
            double transitionThreshold = averageDifference + differenceStandardDeviation;
            int postTransitionShotsCtr = 0;

            bool inTransition = false;
            int possibleTransitionEndIndex = -1;
            shotBoundaryPaths.Add(imagePaths[0]);
            for (int i = 0; i < differences.Length; i++)
            {
                if (differences[i] > breakThreshold)
                {
                    shotBoundaryPaths.Add(imagePaths[i]);
                    shotBoundaryPaths.Add(imagePaths[i + 1]);
                }
                else if (differences[i] > transitionThreshold)
                {
                    if (!inTransition)
                    {
                        shotBoundaryPaths.Add(imagePaths[i]);
                        inTransition = true;
                    }
                    else if (possibleTransitionEndIndex >= 0)
                    {
                        possibleTransitionEndIndex = -1;
                        postTransitionShotsCtr = 0;
                    }
                }
                else
                {
                    if (inTransition)
                    {
                        if (postTransitionShotsCtr < settings.postTransitionFrameTolerance)
                        {
                            if (postTransitionShotsCtr == 0)
                            {
                                possibleTransitionEndIndex = i - 1;
                            }
                            postTransitionShotsCtr++;
                        }
                        else
                        {
                            if (possibleTransitionEndIndex >= 0)
                            {
                                if (!shotBoundaryPaths.Contains(imagePaths[possibleTransitionEndIndex]))
                                    shotBoundaryPaths.Add(imagePaths[possibleTransitionEndIndex]);
                                else //Remove single frame "transitions"
                                    shotBoundaryPaths.Remove(imagePaths[possibleTransitionEndIndex]);
                                possibleTransitionEndIndex = -1;
                            }
                            inTransition = false;
                            postTransitionShotsCtr = 0;
                        }
                    }
                }

                ProgressUpdate(50 + (i / (differences.Length - 1)) * 100);
            }

            if (!shotBoundaryPaths.Contains(imagePaths[imagePaths.Count - 1]))
                shotBoundaryPaths.Add(imagePaths[imagePaths.Count - 1]);

            ProgressUpdate(100);
            shotBoundaryPaths.Sort();
            return shotBoundaryPaths;
        }

        public List<String> getKeyframes(List<String> shotBoundaries)
        {
            keyframePaths = new List<string>();
            int count = 0;
            int j = 0;
            Dictionary<int, int> histogram = new Dictionary<int, int>();

            for (int i = 0; i <= shotBoundaries.Count - 2; i += 2)
            {
                if (i + 1 >= shotBoundaries.Count) break;

                Dictionary<string, Dictionary<int, int>> sumHistogram = new Dictionary<string, Dictionary<int, int>>();

                while (!shotBoundaries[i].Equals(imagePaths[j]))
                    j++;

                count = 0;
                string aveHistogram;
                while (!shotBoundaries[i+1].Equals(imagePaths[j]))
                {
                    histogram = convertImage(imagePaths[j]);
                    sumHistogram.Add(imagePaths[j], histogram);
                    j++;
                    count++;
                }
                aveHistogram = getAverageHistogram(sumHistogram, count);

                keyframePaths.Add(aveHistogram);

                ProgressUpdate(i / (shotBoundaries.Count - 2) * 100);
            }
            
            
            ProgressUpdate(100);
            return keyframePaths.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().ToList();
        }

        public string getAverageHistogram(Dictionary<string, Dictionary<int, int>> histogram, int count)
        {
            string imagePath = "";
            Dictionary<int, int> sum = new Dictionary<int, int>();

            // iterate through each frame between boundary shots
            foreach (string key in histogram.Keys.ToList())
            {
                // add 
                foreach (int k in histogram[key].Keys.ToList())
                {
                    if (sum.ContainsKey(k))
                    {
                        sum[k] += histogram[key][k];
                    }
                    else
                    {
                        sum.Add(k, histogram[key][k]);
                    }
                }
            }

            // get average histogram
            foreach (int k in sum.Keys.ToList())
            {
                sum[k] /= count;
            }

            //iterate through frames which is closest to average
            double diff = 0;
            double min = 0;
            foreach (string key in histogram.Keys.ToList())
            {
                // compararison of histogram 
                diff = getDifference(histogram[key], sum);
                if (min == 0 || diff < min)
                {
                    min = diff;
                    imagePath = key;
                }
            }
            Console.WriteLine(imagePath);

            return imagePath;
        }

        public double getDifference(Dictionary<int, int> a, Dictionary<int, int> b)
        {
            Dictionary<int, int> aCopy = a;
            Dictionary<int, int> bCopy = b.ToDictionary(e => e.Key, e => e.Value);
            double difference = 0;

            foreach (int key in aCopy.Keys.ToList())
            {
                if (bCopy.ContainsKey(key))
                {
                    difference += Math.Abs(bCopy[key]-aCopy[key]);
                    bCopy.Remove(key);
                }
                else
                {
                    difference += aCopy[key];
                }
            }

            foreach (int key in bCopy.Keys.ToList())
            {
                difference += bCopy[key];
            }

            return difference;
        }
    }

}

