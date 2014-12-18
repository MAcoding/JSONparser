using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class Project
    {
        //Attributes
        private string _id;
        private string _date;
        private string _name;
        private string _blog;
        private string _type;
        private List<string> _tags;
        private string _desc;
        private string _image;

        //Constructor
        public Project() { }

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

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string blog
        {
            get { return _blog; }
            set { _blog = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public List<string> tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string image
        {
            get { return _image; }
            set { _image = value; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
