using project_true.Primitives;
using System.Drawing;

namespace project_true.Figures
{
    public class MyTriangle
    {
        public MyPoint A { get; set; }
        public MyPoint B { get; set; }
        public MyPoint C { get; set; }

        public MyTriangle()
        {
            
        }
        
        public MyTriangle(MyPoint a, MyPoint b, MyPoint c)
        {
            A = a;
            B = b;
            C = c;
        }


    }
}