/*Mathematics# (m#) created 14/5/16
 * Mmain 
 * 
 * 24-5-16 - jack davies
 * double declarations processing added in processCommands()
 * at the moment only basic values/ other variables can be addined
 * mathmatic expression calculators need rto be inplamented
 * 
 * 25-5-16 - Jack davies 
 * Added output line and output into processCommands()
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Msharp
{
    class Program
    {
        public static ConsoleColor terminalColour = ConsoleColor.White;//global colour of the terminal
        private static string[] program;
        static programFile mainProgramFile = null;
        public static NumberVariableList doubleVariables = new NumberVariableList();
        public static Variables mainVariables = new Variables();

        /*return informaaion about m#*/
        static string about() {
            return "Mathematics# (M#) 1.0 alpha";
        }

        static string getVersionNumber() {
            return "1.0";
        }

        /*handles command line input
         * 
         */
        static void clCommands() {
            Console.WriteLine(about());
            Console.Title = "M# 1.0";
            while (true) {
                Console.Write(">");
                string commandIn = Console.ReadLine();
                processCommand(commandIn);
            }

        }

        //static bool 

        static bool processCommand(string commandIn) {
            try {
                commandIn = commandIn.ToLower();
                commandIn = commandIn.Replace("\r", "");
                if (commandIn.Replace(" ", "").StartsWith("#")) {
                    return true;
                }

                if (commandIn.Replace(" ", "").StartsWith("$")) {
                    string nameAndValue = commandIn; //.Replace("$","");
                    nameAndValue = nameAndValue.Replace(" ", "");
                    string[] nameValueSplit = nameAndValue.Split('=');

                    if (nameValueSplit[1].Contains("+") || nameValueSplit[1].Contains("-") ||
                        nameValueSplit[1].Contains("*") || nameValueSplit[1].Contains("/")) {
                        //Call expressionCalculator and get value
                    }
                    else {
                        if (nameValueSplit[1].Contains("$")) {
                            NumberVarObject rightVariable = doubleVariables.findVariable(nameValueSplit[1].Replace("$", ""));
                            if (rightVariable == null) {
                                Strings.printErrorMessage("the variable " + nameValueSplit[1] + " does not exist!");
                            }
                            else {
                                doubleVariables.addVariable(nameValueSplit[0].Replace("$", ""), rightVariable.getValue());
                            }
                        }
                        else {
                            double rightValue = Convert.ToDouble(nameValueSplit[1]);
                            doubleVariables.addVariable(nameValueSplit[0].Replace("$", ""), rightValue);
                            
                        }
                    }
                    //doubleVariables.addVariable(nameValueSplit[0], nameValueSplit[1]);
                }

                if (commandIn.Replace(" ", "").StartsWith("output\"")) {
                    Terminal.output(commandIn);
                }

                if (commandIn.Replace(" ", "").StartsWith("outputline\"")) {
                    Terminal.outputLine(commandIn);
                }

                if (commandIn.Replace(" ", "").StartsWith("title=\"")) {
                    Terminal.setTerminalTitle(commandIn);
                }

                commandIn = commandIn.Replace(" ", "");
                commandIn = commandIn.Replace("\t", "");
                if (commandIn.Equals("@testvar")) {
                    mainVariables.addDouble("v",23);
                    return true;
                }

                switch (commandIn) {
                    case "about":
                        Console.WriteLine(about());
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        //Console.WriteLine(Strings.ERROR_INCORRECT_SYNTAX_COMMAND + " " + commandIn);
                        break;
                }

                //Console.WriteLine(Strings.ERROR_INCORRECT_SYNTAX_COMMAND + " " + commandIn);
                return true;
            }
            catch (Exception e) {
                Strings.printErrorMessage(e.Message);
                return false;

            }
        }

        public bool checkForExecutable(string path) {
            
            return false;
        }

        static void executeScript() {
            try {
                bool canRun = true;
                
                while (canRun) {
                    string currenrCommand = mainProgramFile.getNextLine();
                    canRun = processCommand(currenrCommand);
                }
                clCommands();
            } catch (Exception e) {

            }
        }

        static void tempTester(){
            try {
                // Console.WriteLine("Add var bob = 32");
                // mainVariables.addBoolean ("bob", true);
                // mainVariables.addBoolean("b1", true);
                // Object temp = mainVariables.findVariable("bob");
                // Console.WriteLine(temp.GetType());
                //BooleanVarObject t1 = (BooleanVarObject ) temp ;
                //Console.WriteLine(t1.getValue());
                //print(a.GetType() == typeof(Dog)) 
                mainVariables.addDouble("bob", 32);
                mainVariables.addDouble("jim", 42);
                mainVariables.addBoolean("bool1", true);
                mainVariables.addBoolean("bool2", false);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static void tempTester2() {
            int i = 0;
            for (i = 0; i <= 100; i++) {
                Console.WriteLine(Convert.ToChar(i));
                Thread.Sleep(5000000);
            }
        }

        static void Main(string[] args){
            /*TODO improve args to accept multable files and arguments
             * 
             */ 

            //for testing only, remove before publication
            tempTester();

            Console.ForegroundColor = terminalColour;
            try {
                if (args.Length != 0) {
                    if (File.Exists(args[0])) {
                        mainProgramFile = new programFile();
                        if (mainProgramFile.openProgramFile(args[0])) {
                            executeScript();
                            clCommands();
                        } else {
                            clCommands();
                        }
                    } else {
                        Strings.printErrorMessage("the file " + args[0] + " does not exist!");
                        clCommands();
                    }
                } else {
                    clCommands();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
