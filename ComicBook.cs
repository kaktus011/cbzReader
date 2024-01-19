namespace cbzReader
{
    internal class ComicBook
    {
        internal string Title { get; set; }

        internal string Location { get; set; }

        internal Image Cover { get; set; }

        internal int Pages { get; set; }

        internal readonly List<Image> Panels = [];
    }
}
