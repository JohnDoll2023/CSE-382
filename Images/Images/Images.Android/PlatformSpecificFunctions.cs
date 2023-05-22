using System.IO;
using Android.Content;

namespace Images.Droid {
    public class PlatformSpecificFunctions : Images.IPlatformSpecificFunctions {
        private readonly Context context;

        public PlatformSpecificFunctions(Context context) {
            this.context = context;
        }
        public Stream GetStreamForFilename(string filename) {
            return context.Assets.Open(filename);
        }
    }
}