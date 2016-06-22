/* Created 16/6/16 Jack Davies
 * This is to 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Msharp
{
    class Variables
    {
        public static NumberVariableList doubleVariables = new NumberVariableList();
        public static BooleanVariableList booleanVariables = new BooleanVariableList();

        public  object findVariable(string variableName) {
            Object varObject = null;
            varObject = doubleVariables.findVariable(variableName);
            if (varObject != null) {
                return varObject;
            }

            varObject = booleanVariables.findVariable(variableName);
            if (varObject != null) {
                return varObject;
            }

            if (varObject == null) {
                throw new Exception("Variable " + variableName + " does not exist!");
            }
            return null;
        }

        public bool addDouble(string doubleName, double doubleValue) {
                doubleVariables.addVariable(doubleName, doubleValue);
                return true;
        }

        public NumberVarObject findDouble(string variableName) {
            return doubleVariables.findVariable(variableName);
        }

        public bool addBoolean(string booleanName, bool booleanValue) {
                booleanVariables.addVariable(booleanName, booleanValue);
                return true;
        }

        public BooleanVarObject findBoolean(string variableName) {
            return booleanVariables.findVariable(variableName);
        }
    }
}   
