namespace cbzReader.Forms
{
    internal partial class DeleteComic : Form
    {
        private readonly List<ComicBook> _books;
        internal ComicBook? Book;
        internal string? SelectedPath;

        internal DeleteComic(List<ComicBook> books)
        {
            _books = books;
            InitializeComponent();
        }

        private void DeleteComic_Load(object sender, EventArgs e)
        {
            foreach (var book in _books)
            {
                listBox.Items.Add(book.Title);
            }
        }

        private void DeleteSelectedBtn_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedItem == null)
                return;

            var bookTitle = (string)listBox.SelectedItem;
            Book = _books.First(item => item.Title == bookTitle);
            SelectedPath = Book.Location;
            Close();
        }
    }
}
