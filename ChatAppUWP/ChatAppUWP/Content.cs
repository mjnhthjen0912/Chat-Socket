using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppUWP
{
    [DataContract]
    public class Content : INotifyPropertyChanged
    {
        //[DataMember]
        string _id;
        //[DataMember]
        string _name;
        //[DataMember]
        string _contentChat;
        //[DataMember]
        string _image;
        //[DataMember]
        string _hide;
        //[DataMember]
        string _height;
        //[DataMember]
        string _timeRow;
        //[DataMember]
        string _radius;
        //[DataMember]
        string _imageVisibility;
        //[DataMember]
        string _horizonAlign;


        public Content(string id, string name, string content, string image, string hide, string height, string timerow, string radius, string imagevisibility, string horizonalign)
        {
            _name = name;
            _contentChat = content;
            _image = image;
            _id = id;
            _height = height;
            _hide = hide;
            _timeRow = timerow;
            _radius = radius;
            _imageVisibility = imagevisibility;
            _horizonAlign = horizonalign;
        }

        [DataMember]
        public string Name { get => _name; set => _name = value; }
        [DataMember]
        public string ContentChat { get => _contentChat; set => _contentChat = value; }
        [DataMember]
        public string Image { get => _image; set => _image = value; }
        [DataMember]
        public string Id { get => _id; set => _id = value; }
        [DataMember]
        public string Hide { get => _hide; set => _hide = value; }
        [DataMember]
        public string Height { get => _height; set => _height = value; }
        [DataMember]
        public string TimeRow { get => _timeRow; set => _timeRow = value; }
        [DataMember]
        public string Radius { get => _radius; set { _radius = value; OnPropertyChanged("Radius"); } }
        [DataMember]
        public string ImageVisibility { get => _imageVisibility; set => _imageVisibility = value; }
        [DataMember]
        public string HorizonAlign { get => _horizonAlign; set => _horizonAlign = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
