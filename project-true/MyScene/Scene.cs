using project_true.Camera;
using project_true.Figures;
using project_true.Tracing;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.MyScene
{
    public class Scene
    {
        public MyCamera Camera { get; set; }
        public List<Figure> Figures { get; set; } = new List<Figure>();
        public List<MyObject> Objects { get; set; } = new List<MyObject>();

        public Scene() { }
        public Scene(MyCamera camera) 
        {
            Camera = camera;
        }
        public Scene(MyCamera camera, List<Figure> figures) : this(camera)
        {
            Figures = figures;
        }
        public void AddFigure(Figure figure)
        {
            if (figure != null)
            {
                Figures.Add(figure);
            }
        }
    }
}
