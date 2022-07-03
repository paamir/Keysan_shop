using _0_Framework;
using _0_Framework.Domain;

namespace Domain.SlideAgg
{
    public class Slide : EntityBase
    {
        public string Picture { get;private set; }
        public string PictureAlt { get;private set; }
        public string PictureTitle { get;private set; }
        public bool IsDeleted { get;private set; }
        public string Title { get;private set; }
        public string Header { get;private set; }
        public string Text { get;private set; }
        public string ButtonText { get;private set; }
        public string Link { get;private set; }


        public Slide(string picture, string pictureAlt, string pictureTitle, bool IsDeleted, string title,
            string header, string text, string buttonText, string link)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            this.IsDeleted = IsDeleted;
            Title = title;
            Header = header;
            Text = text;
            ButtonText = buttonText;
            Link = link;
            IsDeleted = false;
        }

        public void Edit(string picture, string pictureAlt, string pictureTitle, bool IsDeleted, string title,
            string header, string text, string buttonText, string link)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            this.IsDeleted = IsDeleted;
            Title = title;
            Header = header;
            Text = text;
            ButtonText = buttonText;
            Link = link;
            IsDeleted = false;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
        public void Restore()
        {
            IsDeleted = false;
        }
    }
}