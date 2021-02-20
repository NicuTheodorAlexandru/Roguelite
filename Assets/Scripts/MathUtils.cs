using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class MathUtils
    {
        public static bool Equal(float a, float b, float epsilon = 0.0001f)
        {
            return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b)) < epsilon;
        }

        public static bool BiggerThan(float a, float b, float epsilon = 0.0001f)
        {
            return a - b > epsilon;
        }
    }
}
