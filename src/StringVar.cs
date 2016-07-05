/* Created 19/6/16 Jack Davies
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Msharp
{
    class StringVar
    {
    }

    class StringVarObject
    {
        private string value;
        private string variableName;

        public StringVarObject(string variableName, string value) {
            this.variableName = variableName;
            this.value = value;
        }

        public string getName() {
            return this.variableName;
        }

        public string getValue() {
            return this.value;
        }
    }

    class StringVariableList
    {
        private ArrayList StringList = new ArrayList();

        public void addVariable(string variableName, string variableValue) {
            /* this allows us to add variables to the variable list, if the 
             * list is empty (i.e the count = 0) then we add the variable to 
             * the arraylist. else we search for the index of an existing var 
             * with the same name as the one we are adding. if an existing one 
             * is found we update it. Else we add a new variable to the end of 
             * the array list.
             * 
             */
            if (variableName == "" || variableName.Contains("$") || variableName.Contains("+") || variableName.Contains("-")
                || variableName.Contains("*") || variableName.Contains("/") || variableName.Contains("(") || variableName.Contains(")")
                || variableName.Contains("[") || variableName.Contains("]") || variableName.Contains("{") || variableName.Contains("}")
                || variableName.Contains("\"")) {
                throw new Exception(Strings.ERROR_ILLEGAL_VARIABLE_NAME);
            }

            if (StringList.Count == 0) {
                StringList.Add(new StringVarObject(variableName, variableValue));
            }
            else {
                int existingVariableIndex = getVariableIndex(variableName);

                if (existingVariableIndex == -1) {
                    StringList.Add(new StringVarObject(variableName, variableValue));//add new variable to the list 
                }
                else {
                    StringList.Insert(existingVariableIndex, new StringVarObject(variableName, variableValue));//update existing var
                }
            }
        }

        public StringVarObject findVariable(string variableName) {
            int i = 0;
            for (i = 0; i <= StringList.Count - 1; i++) {
                StringVarObject currentVariable = (StringVarObject)StringList[i];
                if (currentVariable.getName().ToLower().Equals(variableName.ToLower())) {
                    return currentVariable;
                }
            }
            return null;
        }

        public int getVariableIndex(string variableName) {
            /*Loop through the String variables in StringList and return the variables
             * index in the arraylist. if the varaible does not exist then -1 is returned
             * 
             */

            int i = 0;
            for (i = 0; i <= StringList.Count - 1; i++) {
                StringVarObject currentVariable = (StringVarObject)StringList[i];
                if (currentVariable.getName().ToLower().Equals(variableName.ToLower())) {
                    return i;
                }
            }

            return -1;
        }

        public void printAllVariables() {
            foreach (StringVarObject currentVariable in StringList) {
                Console.WriteLine(currentVariable.getName() + "=" + currentVariable.getValue());
            }
        }
    }
}
