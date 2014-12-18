using System;
namespace WpfApplication1
{
    public class BlogPost
    {
        //Attributes
        public string title;
        public string text;
        public string shortText;

        //Constructor
        public BlogPost() { }

        public void getTitle()
        {
            return title;
        }

        public void setTitle(string _title)
        {
            title = _title;
        }

        public void getText()
        {
            return text;
        }

        public void setText(string _text)
        {
            text = _text;
        }

        public void getShortText()
        {
            return shortText;
        }

        public void setShortText(string _shortText)
        {
            shortText = _shortText;
        }
    }
}