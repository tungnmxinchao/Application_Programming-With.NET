using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXMLAndJson.Models
{

    public class Rootobject
    {
        public Quiz quiz { get; set; }
    }

    public class Quiz
    {
        public Sport sport { get; set; }
        public Maths maths { get; set; }
    }

    public class Sport
    {
        public Q1 q1 { get; set; }
    }

    public class Q1
    {
        public string question { get; set; }
        public string[] options { get; set; }
        public string answer { get; set; }
    }

    public class Maths
    {
        public Q11 q1 { get; set; }
        public Q2 q2 { get; set; }
    }

    public class Q11
    {
        public string question { get; set; }
        public string[] options { get; set; }
        public string answer { get; set; }
    }

    public class Q2
    {
        public string question { get; set; }
        public string[] options { get; set; }
        public string answer { get; set; }
    }

}
