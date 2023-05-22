using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Images.iOS {
    class PlatformSpecificFunctions : Images.IPlatformSpecificFunctions {
        public Stream GetStreamForFilename(string filename) {
            return File.OpenRead(filename);
        }
    }
}