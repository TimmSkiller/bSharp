using Caliburn.Micro;
using System;
using System.Net;
using System.Threading.Tasks;

namespace bSharpUILib
{
    public class Downloader : PropertyChangedBase
    {
        public string FileName { get; set; }
        public string Url { get; set; }

        public Downloader(string fileName, string url)
        {
            this.FileName = fileName;
            this.Url = url;
        }

        public Task DownloadFileAsync(IProgress<int> downloadProgress)
        {
            WebClient wc = new WebClient();

            wc.DownloadProgressChanged += (s, e) => downloadProgress.Report(e.ProgressPercentage);

            return wc.DownloadFileTaskAsync(new Uri(this.Url), $"{Environment.CurrentDirectory}\\{FileName}.cia");
        }
    }
}
