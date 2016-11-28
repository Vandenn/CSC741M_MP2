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

        public static Dictionary<int, int> convertImage(string path)
        {
            Bitmap image = new Bitmap(path);
            LockBitmap lbmp = new LockBitmap(image);
            Color[,] convertedImage = new Color[image.Height, image.Width];
            Dictionary<int, int> histogram = new Dictionary<int, int>();

            lbmp.LockBits();
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    convertedImage[i, j] = lbmp.GetPixel(j, i);
                    int key = (convertedImage[i, j].R ^ 48) + ((convertedImage[i, j].G ^ 48) >> 2) + ((convertedImage[i, j].B ^ 48) >> 4);
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

        public List<String> getShotBoundaries()
        {
            // Implementation here.
            shotBoundaryPaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).Take(10).ToList();
            Dictionary<int, int> histogram1, histogram2;
            double similarity;
            List<string> results = new List<string>();
            string p;

            for (int i = 0; i < shotBoundaryPaths.Count - 1; i++)
            {
                p = shotBoundaryPaths[i];
                histogram1 = convertImage(p);

                p = shotBoundaryPaths[i + 1];
                histogram2 = convertImage(p);

                similarity = getDifference(histogram1, histogram2);

                double similarityThreshold = 0.3;
                if (similarity < similarityThreshold)
                {
                    results.Add(shotBoundaryPaths[i + 1]);
                }

                int progress = i / (shotBoundaryPaths.Count - 1);
                ProgressUpdate(progress);
            }
            return shotBoundaryPaths;
        }

        public List<String> getKeyframes()
        {
            // Implementation here.
            keyframePaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).Take(10).ToList();
            ProgressUpdate(100);
            return keyframePaths;
        }

        public double getDifference(Dictionary<int, int> a, Dictionary<int, int> b)
        {
            double difference = 0;
            int count = 0;

            foreach (int key in a.Keys.ToList())
            {
                difference += a[key];

                if (b.ContainsKey(key))
                {
                    difference += b[key];
                    b.Remove(key);
                }
            }

            foreach (int key in b.Keys.ToList())
            {
                difference += b[key];
            }

            count = a.Count + b.Count;

            difference /= count;

            return difference;
        }
    }

}

