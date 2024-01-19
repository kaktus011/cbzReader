using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace cbzReader.Forms
{

    public partial class Reader : Form
    {
        private ComicBook _comic;

        private List<Image> _panels = new();

        private int pageIndx = -1;

        private readonly string _comicExtractLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                        + @"\CBZ Viewer\comics";


        internal Reader(ComicBook comic)
        {
            InitializeComponent();
            _comic = comic;
            Text = _comic.Name;
        }

        private void Reader_Load(object sender, EventArgs e)
        {
            string[] imgPaths = ExtractComic(_comic);

            foreach (var path in imgPaths)
            {
                var img = Image.FromFile(path);

                _panels.Add(ResizeImage(img, 720, 1024));
            }

            picBox.Image = _panels[0];
            pageIndx++;
            progressBar.Maximum = _panels.Count;
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private string[] ExtractComic(ComicBook comic)
        {
            var dir = _comicExtractLocation + _comic.Name;

            //extract to dir
            using ZipArchive archive = ZipFile.OpenRead(comic.Location);
            archive.ExtractToDirectory(dir);

            //get all the locations of every file in dir
            var result = Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).ToArray();
            Array.Sort(result);

            return result;
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (pageIndx < _panels.Count - 1)
            {
                picBox.Image = _panels[pageIndx++];
                progressBar.PerformStep();
            }
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (pageIndx > 0)
            {
                picBox.Image = _panels[pageIndx--];
                progressBar.Step = -1;
                progressBar.PerformStep();
                progressBar.Step = 1;
            }
        }
    }
}
