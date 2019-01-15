using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A101 {
	class Program {
	
		/// <summary>
		/// The code is read into this variable to be iterated through
		/// </summary>
		static string inputCode = "";

		/// <summary>
		/// This is where we write the output file
		/// </summary>
		static string outputCode = "";

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

			//We now know that there is a real, existing .A1 file passed as the first argument
			
			//Load in the file they passed
			LoadFile( args[0] );

			//Begin parsing the file
			ParseFile();
		}

		/// <summary>
		/// Parses the file loaded into the input code variable and writes to the output code variable
		/// </summary>
		public static void ParseFile() {
			
			
			
		}

		/// <summary>
		/// Loads in a file's code into the input code varible
		/// </summary>
		/// <param name="filePath"></param>
		public static void LoadFile( string filePath ){
			StreamReader streamReader = new StreamReader( filePath, Encoding.UTF8 );
			inputCode = streamReader.ReadToEnd();
		}
		
		/// <summary>
		/// Throws an error
		/// </summary>
		/// <param name="errorMessage"></param>
		public static void Error( string errorMessage ) {
			Print( errorMessage );
		}

		/// <summary>
		/// Prints a line to the console
		/// </summary>
		/// <param name="message"></param>
		public static void Print( string message ) {
			Console.WriteLine( message );
		}
	}
}
