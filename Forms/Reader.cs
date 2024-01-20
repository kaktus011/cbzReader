namespace cbzReader.Forms
{
    public partial class Reader : Form
    {
        private readonly ComicBook _comic;

        private int _pageIndx;

        private readonly int _lastPage;

        internal Reader(ComicBook comic)
        {
            _comic = comic;
            _lastPage = _comic.Pages;
            InitializeComponent();
        }

        private void Reader_Load(object sender, EventArgs e)
        {
            Text = _comic.Title;
            picBox.Width = Library.PageWidth;
            picBox.Height = Library.PageHeight;
            picBox.Image = _comic.Panels[0];
            progressBar.Maximum = _lastPage;
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (_pageIndx < _lastPage)
            {
                _pageIndx++;
                picBox.Image = _comic.Panels[_pageIndx];
                progressBar.PerformStep();
            }
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            if (_pageIndx > 0)
            {
                _pageIndx--;
                picBox.Image = _comic.Panels[_pageIndx];
                progressBar.Step = -1;
                progressBar.PerformStep();
                progressBar.Step = 1;
            }
        }
    }
}
