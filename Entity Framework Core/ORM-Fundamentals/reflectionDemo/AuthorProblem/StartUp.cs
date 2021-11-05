using System;

namespace AuthorProblem
{
    [Author("Me")]
    class StartUp
    {
        [Author("Me")]
        static void Main(string[] args)
        {
            Tracker tracker = new Tracker();
             tracker.PrintMethodsByAuthor();
        }
        [Author("Alex")]
        private static void NextGen(){}
    }
}
