using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    [Serializable]
    class StudentLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string FavouriteColour { get; set; }
        public string FavouriteFood { get; set; }
        public string MotherMaidenName { get; set; }
    }
}
