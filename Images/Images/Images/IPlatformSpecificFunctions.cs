using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Images {
    public interface IPlatformSpecificFunctions {
        Stream GetStreamForFilename(string filename);
    }
}
