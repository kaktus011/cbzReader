using cbzReader.Forms;

namespace cbzReader
{
    public partial class Library : Form
    {


        public Library()
        {
            InitializeComponent();
        }

        private void importBtn_Click(object? sender, EventArgs e)
        {
            var opnFile = new OpenFileDialog();
            opnFile.Filter = "CBZ Files|*.cbz";

            if (opnFile.ShowDialog() == DialogResult.OK)
            {
                var path = opnFile.FileName;
                ComicBook comic = new ComicBook()
                {
                    Location = path,
                    Name = Path.GetFileName(Path.GetDirectoryName(opnFile.FileName))
                };

                Read(comic);
            }
        }

        private void Read(ComicBook book)
        {
            Reader reader = new Reader(book);
            reader.Show();
        }
    }
}
