namespace project_true.Camera
{
    public class ScreenState
    {
        private double[,] screen;
        public double this[int i, int j] 
        { 
            get
            {
                return screen[i, j];
            }
            set
            {
                screen[i, j] = value;
            }
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public ScreenState(int height, int width)
        {
            Height = height;
            Width = width;
            screen = new double[height, width];
        }
    }
}
