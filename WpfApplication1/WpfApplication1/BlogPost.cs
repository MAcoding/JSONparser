using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class BlogPost
    {
        //Attributes
        private string _id;
        private string _date;
        private string _title;
        private string _text;
        private string _shortText;
        private List<string> _images;

        //Constructor
        public BlogPost() {
            _id = "";
            _date = "";
            _title = "";
            _text = "";
            _shortText = "";
            _images = new List<string>();
            _images.Add("");
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string shortText
        {
            get { return _shortText; }
            set { _shortText = value; }
        }

        public List<string> images
        {
            get { return _images; }
            set { _images = value; }
        }

        public override string ToString()
        {
            return _title;
        }
      
    }
}
