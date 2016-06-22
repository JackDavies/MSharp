using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Msharp {
    class programFile {
        private string[] program;
        private int currentLine = 0;
        private string[,] subAddresses = new string[1000,1000];

        public bool openProgramFile(string fileLocation) {
            try {
                StreamReader fileReader = new StreamReader(fileLocation);
                string programText = fileReader.ReadToEnd();
                program = programText.Split('\n');

                if (!checkValidHeaderAndVersion(program[0].Replace(" ","").Replace("\r",""))) {
                    return false;
                }

                getSubRoutineAddresses();

                //throw new Exception("error");
            } catch (IOException ioError) {
                Strings.printErrorMessage("Could not open " + fileLocation + "\n" + ioError.Message);
                return false;
            }
            return true;
        }

        private bool checkValidHeaderAndVersion(string lineIn) {
            /*checks that the file header (#msharp:versionNumber) exists and that 
             * the minimum verision is valid
             */
            if (!lineIn.ToLower().Contains("#msharp:")){
                Strings.printErrorMessage(Strings.ERROR_INVALID_PROGRAM);
                return false;//return false if progam head does not exist
            }

            lineIn  = lineIn.Replace("#msharp:","").ToLower();
            double minimumRequiredVersion = Convert.ToDouble(lineIn);

            if (minimumRequiredVersion <= About.getVersionNumber()) {
                return true;
            } else {
                Strings.printErrorMessage("Error, this program can not be run!\n" + "This requires version "
                    + minimumRequiredVersion + "\n" + "Current m# version " + About.getVersionNumber());
                return false;
            }
        }

        private void getSubRoutineAddresses() {
            int i = 0;
            for (i = 0; i <= program.Length - 1; i++) {
                string currentLine = program[i];
                if (currentLine.ToLower().StartsWith("sub:")) {
                    subAddresses[i, 0] = currentLine.Replace("sub:", "");
                    subAddresses[i, 1] = Convert.ToString(i);
                }
            }
        }

        public string getNextLine() {
            if (currentLine > program.Length - 1) {
                return "exit";
            } else {
                currentLine++;
                return program[currentLine];
            }
        }

        public static bool isValidProgramFile(string fileAddress) {
            
            return false;
        }

        public void addLineToProgram(string line) {
           // program += line;
        }
    }
}
