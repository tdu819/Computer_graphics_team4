namespace project_true.Primitives
{
    public class MyPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string Value { get; set; }
        public MyPoint()
        {
            
        }

        public MyPoint(double v1, double v2, double v3)
        {
            this.X = v1;
            this.Y = v2;
            this.Z = v3;
        }

        public static MyPoint operator -(MyPoint left, MyPoint right)
        {
            return new MyPoint(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
    }
}