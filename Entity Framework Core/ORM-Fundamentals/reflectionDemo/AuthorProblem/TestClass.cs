using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorProblem
{
    [Author("Marty")]
    class TestClass
    {
        [Author("Dany")]
        public void DanyMethod(){}

        [Author("Andy")]
        public void HardWorking(){ }
    }
}
