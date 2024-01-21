using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace cbzReader.Forms
{
    //BUG(s)
    //adding big files takes a while


    public partial class Library : Form
    {
        private readonly List<ComicBook> _books = [];
        private readonly List<PictureBox> _picBoxes = [];

        private readonly string _comicExtractLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cbzViewerLib";

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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteComic(_books);
            deleteForm.ShowDialog();


            var path = deleteForm.SelectedPath;
            var book = deleteForm.Book;

            if (path == null || book == null)
                return;

            var picBox = _picBoxes.First(item => item.Tag == path);
            var dir = new DirectoryInfo(Path.GetDirectoryName(path));

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
                DeleteDirectory(subDirectory);
            }

            directory.Delete();
        }

        private bool CheckForFileInLib(string path)
        {
            var tmpTitle = Path.GetFileNameWithoutExtension(path);

            var folderPath = Path.Combine(_comicExtractLocation, tmpTitle);

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

            var newPicBox = new PictureBox
            {
                Size = new Size(105, 147),
                Image = ResizeImage(tmp, 105, 147),
                Location = new Point(_coverPosX, _coverPosY),
                BorderStyle = BorderStyle.FixedSingle,
                Text = comic.Title,
                Tag = (string)comic.Location
            };
            newPicBox.MouseDoubleClick += (sender, e) => { Read(newPicBox.Text); };
            _picBoxes.Add(newPicBox);

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
            tmp.Dispose();
        }

        private void ImportOnOpening()
        {
            if (!Directory.Exists(_comicExtractLocation))
                return;

            importingLbl.Visible = true;
            importProgBar.Value = 0;
            importProgBar.Visible = true;
            Refresh();

            var folderPaths = Directory.EnumerateDirectories(_comicExtractLocation).ToArray();
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
                    Tag = (string)newComic.Location
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
