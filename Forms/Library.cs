using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace cbzReader.Forms
{
    //TODO
    //adding big files takes a while
    //restore from library directory
    //make everything dark mode (easier on the eyes)

    //BUG(s)
    //adding the same file breaks everything


    public partial class Library : Form
    {
        private readonly List<ComicBook> _books = [];

        private readonly string _comicExtractLocation =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cbzViewerLib";

        internal const int PageWidth = 794;
        internal const int PageHeight = 1123;
        private const int CoverWidth = 105;
        private const int CoverHeight = 147;

        private int _coverPosX = 12;
        private int _coverPosY = 72;

        public Library()
        {
            InitializeComponent();
        }

        private void ImportBtn_Click(object? sender, EventArgs e)
        {
            var opnFile = new OpenFileDialog();
            opnFile.Filter = "CBZ Files|*.cbz";

            if (opnFile.ShowDialog() == DialogResult.OK)
            {
                var path = opnFile.FileName;
                var comic = new ComicBook
                {
                    Location = path,
                    Title = Path.GetFileName(path).Substring(0, Path.GetFileName(path).IndexOf('.'))
                };

                Import(comic);
            }
        }

        private void Import(ComicBook comic)
        {
            var imgPaths = ExtractComic(comic);
            comic.Pages = imgPaths.Length;
            var tmp = Image.FromFile(imgPaths[0]);
            comic.Cover = ResizeImage(tmp, 105, 147);

            var newPicBox = new PictureBox
            {
                Size = new Size(105, 147),
                Image = comic.Cover,
                Location = new Point(_coverPosX, _coverPosY),
                BorderStyle = BorderStyle.FixedSingle,
                Text = comic.Title,
            };
            newPicBox.DoubleClick += (sender, e) => { Read(newPicBox.Text); };

            Controls.Add(newPicBox);
            CalculateNextCoverPos();

            foreach (var path in imgPaths)
            {
                var img = Image.FromFile(path);
                comic.Panels.Add(ResizeImage(img, PageWidth, PageHeight));
            }

            _books.Add(comic);
        }

        private void CalculateNextCoverPos()
        {
            //this form's width
            var formWidth = Width;
            const int CoverMargin = 10;

            if (_coverPosX + CoverWidth + 30 >= formWidth)
            {
                _coverPosY += CoverHeight + CoverMargin;
                _coverPosX = 12;
            }
            else
            {
                _coverPosX += CoverWidth + CoverMargin;
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.Default;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.PixelOffsetMode = PixelOffsetMode.Default;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return destImage;
        }

        private string[] ExtractComic(ComicBook comic)
        {
            var dir = _comicExtractLocation + "\\" + comic.Title;

            //extract to dir
            using ZipArchive archive = ZipFile.OpenRead(comic.Location);
            archive.ExtractToDirectory(dir);

            //get all the locations of every file in dir
            var result = Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).ToArray();
            Array.Sort(result);

            return result;
        }

        private void Read(string comicTitle)
        {
            var comic = _books.FirstOrDefault(book => book.Title == comicTitle);
            var reader = new Reader(comic);
            reader.Show();
        }
    }
}
