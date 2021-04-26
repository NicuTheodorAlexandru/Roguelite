using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class NPCText
    {
        TextOptions textOptions;

        public class TextTree
        {
            
        }

        public class TextOptions
        {
            private Dictionary<string, string[]> textOptions;

            public TextOptions(Dictionary<string, string[]> text)
            {
                textOptions = text;
            }
        }
    }
}
