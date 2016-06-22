/* 16/6/16 - Jack Davies
 * Added variable name validation in addVariable to throw exception if 
 * a new variable with an illegal name is added.
 * 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Msharp {

    class NumberVarObject
    {
        private double value;
        private string variableName;

        public NumberVarObject(string variableName, double value) {
            this.variableName = variableName;
            this.value = value;
        }

        public string getName() {
            return this.variableName;
        }

        public double getValue() {
            return this.value;
        }
    }

    class NumberVariableList
    {
        private ArrayList numberList = new ArrayList();

        public void addVariable(string variableName, double variableValue) {
            /* this allows us to add variables to the variable list, if the 
             * list is empty (i.e the count = 0) then we add the variable to 
             * the arraylist. else we search for the index of an existing var 
             * with the same name as the one we are adding. if an existing one 
             * is found we update it. Else we add a new variable to the end of 
             * the array list.
             * 
             */

            /* Validate that the new variable being added does not contain illigal charictors
             */ 
            if (variableName == "" || variableName.Contains("$") || variableName.Contains("+") || variableName.Contains("-")
                || variableName.Contains("*") || variableName.Contains("/") || variableName.Contains("(") || variableName.Contains(")")
                || variableName.Contains("[") || variableName.Contains("]") || variableName.Contains("{") || variableName.Contains("}")
                || variableName.Contains("\"")) {
                throw new Exception(Strings.ERROR_ILLEGAL_VARIABLE_NAME);
            }

            if (numberList.Count == 0) {
                numberList.Add(new NumberVarObject(variableName, variableValue));
            }
            else {
                int existingVariableIndex = getVariableIndex(variableName);

                if (existingVariableIndex == -1) {
                    numberList.Add(new NumberVarObject(variableName, variableValue));//add new variable to the list 
                }
                else {
                    numberList.Insert(existingVariableIndex, new NumberVarObject(variableName, variableValue));//update existing var
                }
            }
        }

        public NumberVarObject findVariable(string variableName) {
            int i = 0;
            for (i = 0; i <= numberList.Count -1; i++) {
                NumberVarObject currentVariable = (NumberVarObject)numberList[i];
                if (currentVariable.getName().ToLower().Equals(variableName.ToLower())) {
                    return currentVariable;
                }
            }
            return null;
        }

        public int getVariableIndex(string variableName) {
            /*Loop through the number variables in numberList and return the variables
             * index in the arraylist. if the varaible does not exist then -1 is returned
             * 
             */

            int i = 0;
            for (i = 0; i <= numberList.Count - 1; i++) {
                NumberVarObject currentVariable = (NumberVarObject)numberList[i];
                if (currentVariable.getName().ToLower().Equals(variableName.ToLower())) {
                    return i;
                }
            }

            return -1;
        }

        public string toString() {
            return "double";
        }

        public void printAllVariables() {
            foreach (NumberVarObject currentVariable in numberList) {
                Console.WriteLine(currentVariable.getName() + "=" + currentVariable.getValue());
            }
        }
    }

    class NumberVariableOperations {
        
    }
}
