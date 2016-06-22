/* Created 25-5-16
 * author Jack davies
 * version 0.1 input and output to the terminal
 * 25-5-16 added output and outputLine
 * 
 * 17/6/16 - Jack Davies
 * Added search for all variable types 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Msharp
{
    class Terminal
    {

        /* Handles writing a line to the terminal, accepts in a 
         * string of text which may include variable names so 
         * they must be extracted and replaced with the variabl
         * values
         *  Here we pass in a string with the
            raw text and possibly variable names
            we then loop through all of the chars
            in the text. If we come across a \ 
            (backslash operator) then we need to
            convert the next char into a special char
            such as \n to a new line. We also need 
            to convert \$ into a $ to avoid confusion 
            between a $ char and a variable name.
            When discovering a $ (start of a var name)
            we loop through and pars out the name. 
            Pass that name into the variable searcher 
            and add the returned value to the outout 
            string
         */
        public static void output(string text) {
            Console.Write(Strings.processString(text));
        }

        /* operates the same as output, only differance is at the end we
         * Console.WriteLine(outputValue); instead of Console.Write(outputValue);
         * 
         */
        public static void outputLine(string text) {
            Console.WriteLine(Strings.processString(text));
        }

        public static void setTerminalTitle(string text) {
            Console.Title = Strings.processString(text);
        }
    }
}