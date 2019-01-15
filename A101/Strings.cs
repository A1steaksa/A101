using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A101 {
	class Strings {

		public static string NumberOutOfBounds = "A number was outside the bounds [" + Config.minNumberRange + ", " + Config.maxNumberRange + "]!";

		public static string MemoryHeadOutOfBounds = "The main memory read/write head is outside the bounds [0, " + ( Config.mainMemoryLength - 1 ) + "]!";

		public static string ArgumentIsNotRegister = "An argument was expected to be a register but was not or was out of bounds!";

		public static string UnrecognizedOpcode = "An invalid opcode was found!";

		public static string InvalidRegisterReference = "A reference to a non-existant register was made!";

		public static string InvalidLabelReference = "A reference to a non-existant label was made!";

		public static string UnrecognizedDataType = "An argument was neither a register reference or a literal number!";

		public static string WrongNumberOfArguments = "The wrong number of arguments passed!";

		public static string LabelContainedSpaces = "A label contained spaces!";

		public static string EmptyFile = "You cannot run a file without executable commands!";

		public static string NoFileArgumentFound = "Argument #1 must be a valid file path!";

		public static string FileIsNotA1File = "The file path did not point to a .A1 file";

		public static string FileDoesNotExist = "The passed file does not exist!";

		public static string MalformedLabelFound = "A label was found to be malformed!";

		/*
		 * String buffer errors
		 */

		public static string BufferValueOutOfASCIIRange = "A value outside of the range [32, 126] was written to the string buffer!";


		/*
		 * Exit condition messages
		 */

		public static string ExitWithError = "Execution halted with error(s)";

		public static string ExitNormal = "Execution finished";

	}
}
