/* Created 13-6-16 - Jack Davies
 * This will store all the constant string messages and the methods 
 * for processing and handeling strings in m#
 * 18-6-16 Added processString()
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Msharp {
    class Strings
    {
        public const string ERROR_INCORRECT_SYNTAX_COMMAND = "Error unknown command or incorrect syntax";
        public const string ERROR_INVALID_PROGRAM = "Invalid m# program!";
        public const string ERROR_UNDEFINED_VARIABLE = "Error undefined variable";
        public const string ERROR_ILLEGAL_VARIABLE_NAME = "Variable was declared with an illegal name!";

        public static void printErrorMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
            Console.ForegroundColor = Program.terminalColour;
        }

        /* paramiter m# "code" string that is to be processed
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
        public static string processString(string stringIn) {
            string[] textSplit = stringIn.Split('\"');
            string rawValue = textSplit[1];
            string outputValue = "";
            bool isBackslashOperator = false;

            int i = 0;
            for (i = 0; i <= rawValue.Length - 1; i++) {

                if (rawValue[i] == '\\') {
                    isBackslashOperator = true;//make surewe only run the \ oporator converter
                }

                if (!isBackslashOperator) {
                    if (rawValue[i] == '$') {
                        //loop through getting the variable name
                        string variableName = "";
                        i++;
                        while (i <= rawValue.Length - 1 && rawValue[i] != '$' && rawValue[i] != ' ' && rawValue[i] != '\\'
                                && rawValue[i] != ',' && rawValue[i] != '.' && rawValue[i] != '?') {
                            //stop if we come across another var ($), space or \ oporator etc
                            variableName = variableName + rawValue[i];//add current char to variable name
                            i++;
                        }

                        Object variableObject = Program.mainVariables.findVariable(variableName);

                        if (variableObject != null) {
                            if (variableObject.GetType() == typeof(NumberVarObject)) {
                                NumberVarObject doubleObject = (NumberVarObject)variableObject;
                                double variableValue = doubleObject.getValue();
                                outputValue = outputValue + Convert.ToString(variableValue);
                            }

                            if (variableObject.GetType() == typeof(BooleanVarObject)) {
                                BooleanVarObject booleanObject = (BooleanVarObject)variableObject;
                                bool variableValue = booleanObject.getValue();
                                outputValue = outputValue + Convert.ToString(variableValue);
                            }
                            //double variableValue = variableObject.getValue();
                            //outputValue = outputValue + Convert.ToString(variableValue);
                        }
                        else {
                            throw new Exception("The variable " + variableName + " does not exist!");
                        }
                    }
                    else {
                        outputValue = outputValue + rawValue[i];//add the variable value to the output string
                    }
                }
                else {

                    isBackslashOperator = false;
                    i++;
                    if (i < rawValue.Length - 1) {
                        char currentChar = rawValue[i];
                        switch (currentChar) {
                            case 'n':
                                outputValue = outputValue + '\n';
                                break;
                            case '\\':
                                outputValue = outputValue + '\\';
                                break;
                            case '$':
                                outputValue = outputValue + '$';
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return outputValue;
        }
        
                public static bool convertToBool(string lineIn) {
            lineIn = lineIn.ToLower();
            switch (lineIn) {
                case "false":
                    return false;
                break;
                case "f":
                return false;
                break;
                case "0":
                return false;
                break;

                case "true":
                return true;
                break;
                case "t":
                return true;
                break;
                case "1":
                return true;
                break;

                default:
                throw new Exception("Cannot interoperate string as boolean!");
                break;
            }
            return false;
        }
    }
}
