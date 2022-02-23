using System.Drawing;

namespace project_true.Figures
{
    public class MyTriangle
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }

        public MyTriangle()
        {
            
        }
        
        public MyTriangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}