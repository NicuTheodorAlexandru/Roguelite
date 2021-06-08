using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class NPCText
    {
        public Text text;
        string[] textLines;
        int textIndex;
        int charIndex;
        string currentText;
        int textSpeed;
        int frames;

        public NPCText(Text text, string[] textLines, int textSpeed)
        {
            textIndex = -1;
            charIndex = 0;
            currentText = "";
            frames = 0;
            this.text = text;
            this.textLines = textLines;
            this.textSpeed += textSpeed;
        }

        public void Reset()
        {
            textIndex = -1;
            charIndex = 0;
            currentText = "";
            frames = 0;
        }

        public void LoadNext()
        {
            if (textIndex >= textLines.Length - 1)
                return;
            textIndex++;
            charIndex = 0;
            currentText = "";
        }

        public void Update()
        {
            LoadText();
            frames++;
        }

        private void LoadText()
        {
            if(frames < textSpeed)
            {
                return;
            }
            if (textIndex < 0)
            {
                return;
            }
            if (charIndex >= textLines[textIndex].Length)
            {
                return;
            }
            currentText += textLines[textIndex][charIndex];
            text.text = currentText;
            charIndex++;
            frames = 0;
        }

        public class TextTree
        {
            
        }
    }
}
