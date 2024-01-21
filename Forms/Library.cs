using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

namespace cbzReader.Forms
{
    //TODO
    //option for next page going left or next page going right
    //make everything dark mode (easier on the eyes) multiple
    //be able to delete a file
    //TODO in ImportBtn_Click() check if file is already in _comicExtractLocation at the start

    //BUG(s)
    //adding big files takes a while


    public partial class Library : Form
    {
        private readonly List<ComicBook> _books = [];

        private const string ComicExtractLocation = "C:\\Users\\PiwKi\\Desktop" + @"\cbzViewerLib";

        //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cbzViewerLib";

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
                if (CheckForFileInLib(path))
                {
                    MessageBox.Show("Error: File already in library. Click 'Restore' if it isn't shown");
                    return;
                }

                var comic = new ComicBook
                {
                    Location = path,
                    Title = Path.GetFileName(path).Substring(0, Path.GetFileName(path).IndexOf('.'))
                };

                if (_books.FirstOrDefault(book => book.Title == comic.Title) == null)
                    Import(comic);
                else
                {
                    MessageBox.Show("Error: File already in library.");
                }
            }
        }

        private void RestoreBtn_Click(object sender, EventArgs e)
        {
            ImportOnOpening();
            RestoreBtn.Visible = false;
        }

        private bool CheckForFileInLib(string path)
        {
            var tmpTitle = Path.GetFileNameWithoutExtension(path);

            var folderPath = Path.Combine(ComicExtractLocation, tmpTitle);

            return Directory.Exists(folderPath);
        }

        private void Import(ComicBook comic)
        {
            importingLbl.Visible = true;
            importProgBar.Value = 0;
            importProgBar.Visible = true;

            var imgPaths = ExtractComic(comic);
            importProgBar.Maximum = imgPaths.Length;
            Refresh();
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
                importProgBar.PerformStep();
            }

            _books.Add(comic);

            importingLbl.Visible = false;
            importProgBar.Visible = false;
        }

        private void ImportOnOpening()
        {
            if (!Directory.Exists(ComicExtractLocation))
                return;

            importingLbl.Visible = true;
            importProgBar.Value = 0;
            importProgBar.Visible = true;
            Refresh();

            var folderPaths = Directory.EnumerateDirectories(ComicExtractLocation).ToArray();
            importProgBar.Maximum = folderPaths.Length;
            importProgBar.Step = 1;

            foreach (var fPath in folderPaths)
            {
                var imgPaths = Directory.EnumerateFiles(fPath, "*.*", SearchOption.AllDirectories).ToArray();

                //bad name -> returns comic title with '\\' at the beginning
                var tmpTitle = fPath.Substring(fPath.LastIndexOf("\\"));
                var newComic = new ComicBook
                {
                    Title = tmpTitle.Substring(1),
                    Location = fPath + tmpTitle,
                    Cover = ResizeImage(Image.FromFile(imgPaths[0]), 105, 147),
                    Pages = imgPaths.Length,
                };

                foreach (var path in imgPaths)
                {
                    var img = Image.FromFile(path);
                    newComic.Panels.Add(ResizeImage(img, PageWidth, PageHeight));
                }

                var newPicBox = new PictureBox
                {
                    Size = new Size(105, 147),
                    Image = newComic.Cover,
                    Location = new Point(_coverPosX, _coverPosY),
                    BorderStyle = BorderStyle.FixedSingle,
                    Text = newComic.Title,
                };
                newPicBox.DoubleClick += (sender, e) => { Read(newPicBox.Text); };
                Controls.Add(newPicBox);

                importProgBar.PerformStep();
                CalculateNextCoverPos();

                _books.Add(newComic);
                Refresh();
            }

            importingLbl.Visible = false;
            importProgBar.Visible = false;
            importProgBar.Step = 5;
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
            var dir = ComicExtractLocation + "\\" + comic.Title;

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
