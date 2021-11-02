using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    class GetRandomNumber
    {
        private readonly Random _random = new Random();
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
