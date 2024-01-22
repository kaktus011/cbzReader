using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace cbzReader.Forms
{
    public partial class Library : Form
    {
        private readonly List<ComicBook> _books = [];
        private readonly List<PictureBox> _picBoxes = [];

        internal static readonly string ComicExtractLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cbzViewerLib";

        internal const int PageWidth = 794;
        internal const int PageHeight = 1123;
        private const int CoverWidth = 105;
        private const int CoverHeight = 147;
        private const int InitX = 12;
        private const int InitY = 72;

        private int _coverPosX = InitX;
        private int _coverPosY = InitY;

        public Library()
        {
            InitializeComponent();
            RefreshLibBtn.Visible = false;
        }

        private void ImportBtn_Click(object? sender, EventArgs e)
        {
            var opnFile = new OpenFileDialog();
            opnFile.Filter = "CBZ Files|*.cbz";

            if (opnFile.ShowDialog() == DialogResult.OK)
            {
                var zipPath = opnFile.FileName;
                if (CheckForFileInLib(zipPath))
                {
                    MessageBox.Show("Error: File already in library. Click 'Restore' if it isn't shown");
                    return;
                }

                var comicTitle = Path.GetFileName(zipPath).Substring(0, Path.GetFileName(zipPath).IndexOf('.'));
                var comic = new ComicBook
                {
                    Title = comicTitle,
                    Location = ComicExtractLocation + @$"\{comicTitle}",
                };

                if (_books.FirstOrDefault(book => book.Title == comic.Title) == null)
                    Import(comic, zipPath);
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
            RefreshLibBtn.Visible = true;
        }

        private void RefreshLibBtn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ComicExtractLocation))
                return;

            _books.Clear();

            foreach (var picBox in _picBoxes)
                picBox.Dispose();
            _picBoxes.Clear();

            _coverPosX = InitX;
            _coverPosY = InitY;

            ImportOnOpening();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteComic(_books);
            deleteForm.ShowDialog();


            var path = deleteForm.SelectedPath;
            var book = deleteForm.Book;

            if (path == null || book == null)
                return;

            var picBox = _picBoxes.First(item => (string)item.Tag! == book.Title);
            var dir = new DirectoryInfo(path);

            _picBoxes.Remove(picBox);
            _books.Remove(book);
            picBox.Dispose();
            DeleteDirectory(dir);
            ReorderLibAfterDeleting();
            Refresh();
        }

        private void DeleteDirectory(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                DeleteDirectory(subDirectory);//imported a file and it the path was user/piwki/downloads???? so it gave an error
            }

            directory.Delete();
        }

        private bool CheckForFileInLib(string path)
        {
            var tmpTitle = Path.GetFileNameWithoutExtension(path);

            var folderPath = Path.Combine(ComicExtractLocation, tmpTitle);

            return Directory.Exists(folderPath);
        }

        private void Import(ComicBook comic, string zipPath)
        {
            importingLbl.Visible = true;
            importProgBar.Value = 0;
            importProgBar.Visible = true;

            var imgPaths = ExtractComic(comic, zipPath);
            importProgBar.Maximum = imgPaths.Length;
            Refresh();
            comic.Pages = imgPaths.Length;
            var tmp = Image.FromFile(imgPaths[0]);

            var newPicBox = new PictureBox
            {
                Size = new Size(105, 147),
                Image = ResizeImage(tmp, 105, 147),
                Location = new Point(_coverPosX, _coverPosY),
                BorderStyle = BorderStyle.FixedSingle,
                Text = comic.Title,
                Tag = (string)comic.Title
            };
            newPicBox.MouseDoubleClick += (sender, e) => { Read(newPicBox.Text); };
            _picBoxes.Add(newPicBox);

            Controls.Add(newPicBox);
            CalculateNextCoverPos();

            foreach (var path in imgPaths)
            {
                var img = Image.FromFile(path);
                comic.Panels.Add(ResizeImage(img, PageWidth, PageHeight));
                img.Dispose();
                importProgBar.PerformStep();
            }

            _books.Add(comic);

            importingLbl.Visible = false;
            importProgBar.Visible = false;
            tmp.Dispose();
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
                    Pages = imgPaths.Length,
                };

                foreach (var path in imgPaths)
                {
                    var img = Image.FromFile(path);
                    newComic.Panels.Add(ResizeImage(img, PageWidth, PageHeight));
                    img.Dispose();
                }

                var tmp = Image.FromFile(imgPaths[0]);

                var newPicBox = new PictureBox
                {
                    Size = new Size(105, 147),
                    Image = ResizeImage(tmp, 105, 147),
                    Location = new Point(_coverPosX, _coverPosY),
                    BorderStyle = BorderStyle.FixedSingle,
                    Text = newComic.Title,
                    Tag = (string)newComic.Title
                };
                newPicBox.MouseDoubleClick += (sender, e) => { Read(newPicBox.Text); };
                _picBoxes.Add(newPicBox);

                Controls.Add(newPicBox);
                importProgBar.PerformStep();
                CalculateNextCoverPos();

                _books.Add(newComic);
                tmp.Dispose();
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

        private void ReorderLibAfterDeleting()
        {
            _coverPosX = InitX;
            _coverPosY = InitY;

            foreach (var picBox in _picBoxes)
                Controls.Remove(picBox);

            foreach (var picBox in _picBoxes)
            {
                picBox.Location = new Point(_coverPosX, _coverPosY);
                CalculateNextCoverPos();
                Controls.Add(picBox);
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
            image.Dispose();

            return destImage;
        }

        private string[] ExtractComic(ComicBook comic, string zipPath)
        {
            //var dir = ComicExtractLocation + "\\" + comic.Title;

            //extract to dir
            using ZipArchive archive = ZipFile.OpenRead(zipPath);
            archive.ExtractToDirectory(comic.Location);

            //get all the locations of every file in dir
            var result = Directory.EnumerateFiles(comic.Location, "*.*", SearchOption.AllDirectories).ToArray();
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
