using System;
using System.Collections.Generic;
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

        public List<String> getShotBoundaries()
        {
            // Implementation here.
            shotBoundaryPaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).Take(10).ToList();
            ProgressUpdate(100);
            return shotBoundaryPaths;
        }

        public List<String> getKeyframes()
        {
            // Implementation here.
            keyframePaths = Directory.GetFiles(path).Where(p => p.EndsWith(".jpg") || p.EndsWith(".jpeg")).Take(10).ToList();
            ProgressUpdate(100);
            return keyframePaths;
        }
    }
}
