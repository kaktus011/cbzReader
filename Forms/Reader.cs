namespace cbzReader.Forms
{

    public partial class Reader : Form
    {
        private readonly ComicBook _comic;

        private int _pageIndx = -1;

        private readonly int _lastPage;

        internal Reader(ComicBook comic)
        {
            InitializeComponent();
            _comic = comic;
            this.Text = _comic.Title;
            _lastPage = comic.Pages;
        }

        private void Reader_Load(object sender, EventArgs e)
        {
            picBox.Width = Library.PageWidth;
            picBox.Height = Library.PageHeight;
            picBox.Image = _comic.Panels[0];

            _pageIndx++;
            progressBar.Maximum = _lastPage;
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (_pageIndx < _lastPage)
            {
                picBox.Image = _comic.Panels[_pageIndx++];
                progressBar.PerformStep();
            }
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            if (_pageIndx > 0)
            {
                picBox.Image = _comic.Panels[_pageIndx--];
                progressBar.Step = -1;
                progressBar.PerformStep();
                progressBar.Step = 1;
            }
        }
    }
}
