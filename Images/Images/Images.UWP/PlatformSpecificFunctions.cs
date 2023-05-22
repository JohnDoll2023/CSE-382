using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Images.UWP {
    public class PlatformSpecificFunctions : Images.IPlatformSpecificFunctions {
        public Stream GetStreamForFilename(string filename) {
            return Package.Current.InstalledLocation.GetFileAsync(filename)
                .AsTask().Result
                .OpenStreamForReadAsync().Result;
        }
    }
}
