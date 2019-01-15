using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A101 {
	class Program {

		
		string code = "";

		static void Main( string[] args ) {

			//Make sure an appropriate file has been passed in
			//Check that there even is a file argument
			if( args.Length < 1 ) {
				Error( Strings.NoFileArgumentFound );
			} else {
				//If there's an argument there, make sure it at least ends in the right extension
				if( !args[0].ToLower().EndsWith( "." + Config.loadedFileExtension.ToLower() ) ) {
					Error( Strings.FileIsNotA1File );
				} else {
					//Make sure the file actually exists
					if( !File.Exists( args[0] ) ) {
						Error( Strings.FileDoesNotExist );
					}
				}
			}

			//We now know that there is a real, existing .A1 file passed into this program


			Console.Read();
			
		}

		//Throws an error
		public static void Error( string errorMessage ) {
			Print( errorMessage );
		}

		//Prints a line to the console
		public static void Print( string message ) {
			Console.WriteLine( message );
		}
	}
}
