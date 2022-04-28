﻿using project_true.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.Figures
{
    public abstract class Figure
    {
        public abstract bool RayIntersect(MyPoint rayOrigin,
                                          MyPoint rayPointer,
                                          ref MyPoint IntersectionPoint);
        public abstract MyVector GetNormal(MyPoint intersectionPoint);

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
