using project_true.Camera;
using project_true.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.MyScene
{
    public class Scene
    {
        public MyCamera Camera { get; set; }
        public List<Figure> Figures = new List<Figure>();

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
