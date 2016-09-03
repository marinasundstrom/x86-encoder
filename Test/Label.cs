using System.Collections.Generic;

namespace Test
{
    public class Label : Constant
    {
        internal Label(string name)
        {
            Name = name;

            References = new List<uint>();
        }

        public string Name { get; }

        internal List<uint> References;
    }
}