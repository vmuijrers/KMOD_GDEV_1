using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeConvention
{
    //Class names with captial letters
    public class CodeConvention
    {
        public float SomeProperty { get; set; }
        public const float SOME_STATIC_FLOAT = 3.14f;

        protected float _anotherFloat;

        private float _someFloat;

        public void SomeFunction()
        {
            bool someBool = true;
            //Single line if-statements can be confined to one line
            if (someBool) { return; }

            if (!someBool)
            {

            }
            else 
            {
                someBool = false;
            }

            switch(someBool)
            {
                case true: 
                    break;
                case false: 
                    break;
            }

            //Spaces between operators
            int result = 3 * 5;
            result += 10;
        }
    }

    //Interfaces start with capital I
    public interface ISomeInterface
    {

    }
}
