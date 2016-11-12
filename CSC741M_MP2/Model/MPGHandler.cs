using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC741M_MP2.Model
{
    public class MPGHandler
    {
        public delegate void ProgressUpdateEvent(int progress);
        public event ProgressUpdateEvent ProgressUpdate;

        private string path;
        public MPGHandler(string path)
        {
            this.path = path;
        }
    }
}
