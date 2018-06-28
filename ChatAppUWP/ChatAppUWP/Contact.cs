using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppUWP
{
    public class Contact
    {
        string _id;
        string _name;
        string _content;
        string _image;
        bool _online;

        public Contact(string id, string name, string content, string image, bool online)
        {
            _name = name;
            _content = content;
            _image = image;
            _id = id;
            Online = online;
        }

        public string Name { get => _name; set => _name = value; }
        public string Content { get => _content; set => _content = value; }
        public string Image { get => _image; set => _image = value; }
        public string Id { get => _id; set => _id = value; }
        public bool Online { get => _online; set => _online = value; }
    }
}
