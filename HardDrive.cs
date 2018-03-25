using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sureshdr.libddupe
{
    public class HardDrive
    {
        private string model = null;
        private string type = null;
        private string serialNo = null;
        private string guid = null;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }
    }
}
