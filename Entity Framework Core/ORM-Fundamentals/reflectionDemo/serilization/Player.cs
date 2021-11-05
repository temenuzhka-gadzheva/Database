using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilization
{
    class Player
    {
        public int  Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public Score  Score { get; set; }
        public int[] Games { get; set; }
    }

}
