namespace SuitsupplyTask.Application.Utils
{
    public class ContentModel
    {
        public ContentModel(string filename, int contentLength, byte[] content)
        {
            Filename = filename;
            ContentLength = contentLength;
            Content = content;
        }

        public string Filename { get; set; }

        public int ContentLength { get; set; }

        public byte[] Content { get; set; }

    }
}