using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A101 {
	class Config {

		/*
		 * Application Settings
		 */

		//The file extension that will be loaded
		public static string loadedFileExtension = "A1";

		//The file extension that will be exported
		public static string compiledFileExtension = "A1C";

		/*
		 * Compiler Settings
		 * 
		 */

		//The range of numbers the system can use
		public static int minNumberRange = -32768;
		public static int maxNumberRange = 32767;

		//The number of non-special registers
		public static int registerCount = 7;

		//The size of main memory
		public static int mainMemoryLength = 10000;

		//The size of the string buffer, in characters
		public static int stringBufferSize = 256;

	}
}
