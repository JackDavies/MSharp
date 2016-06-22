using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Msharp
{
    class BooleanVar
    {
    }

    class BooleanVarObject
    {
        private bool value;
        private string variableName;

        public BooleanVarObject(string variableName, bool value) {
            this.variableName = variableName;
            this.value = value;
        }

        public string getName() {
            return this.variableName;
        }

        public bool getValue() {
            return this.value;
        }
    }

    class BooleanVariableList
    {
        private ArrayList booleanList = new ArrayList();

        public void addVariable(string variableName, bool variableValue) {
            /* this allows us to add variables to the variable list, if the 
             * list is empty (i.e the count = 0) then we add the variable to 
             * the arraylist. else we search for the index of an existing var 
             * with the same name as the one we are adding. if an existing one 
             * is found we update it. Else we add a new variable to the end of 
             * the array list.
             * 
             */

            if (booleanList.Count == 0) {
                booleanList.Add(new BooleanVarObject(variableName, variableValue));
            }
            else {
                int existingVariableIndex = getVariableIndex(variableName);

                if (existingVariableIndex == -1) {
                    booleanList.Add(new BooleanVarObject(variableName, variableValue));//add new variable to the list 
                }
                else {
                    booleanList.Insert(existingVariableIndex, new BooleanVarObject(variableName, variableValue));//update existing var
                }
            }
        }

        public BooleanVarObject findVariable(string variableName) {
            int i = 0;
            for (i = 0; i <= booleanList.Count - 1; i++) {
                BooleanVarObject currentVariable = (BooleanVarObject)booleanList[i];
                if (currentVariable.getName().ToLower().Equals(variableName.ToLower())) {
                    return currentVariable;
                }
            }
            return null;
        }

        public int getVariableIndex(string variableName) {
            /*Loop through the Boolean variables in BooleanList and return the variables
             * index in the arraylist. if the varaible does not exist then -1 is returned
             * 
             */

            int i = 0;
            for (i = 0; i <= booleanList.Count - 1; i++) {
                BooleanVarObject currentVariable = (BooleanVarObject)booleanList[i];
                if (currentVariable.getName().ToLower().Equals(variableName.ToLower())) {
                    return i;
                }
            }

            return -1;
        }

        public void printAllVariables() {
            foreach (BooleanVarObject currentVariable in booleanList) {
                Console.WriteLine(currentVariable.getName() + "=" + currentVariable.getValue());
            }
        }
    }
}
