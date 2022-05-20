using project_true.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.Figures
{
    public abstract class Figure
    {
        
        // Part 3 and all of the inheritance classes
        public abstract bool RayIntersect(MyPoint rayOrigin,
                                          MyPoint rayPointer,
                                          ref MyPoint IntersectionPoint);
        
       
        public abstract MyVector GetNormal(MyPoint intersectionPoint);


    }
}
