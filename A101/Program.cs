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
		/// This holds modifiable code while it is being worked on
		/// </summary>
		static LinkedList<string> workingCode = new LinkedList<string>();

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

			Console.Read();
		}

		/// <summary>
		/// Parses the file loaded into the input code variable and writes to the output code variable
		/// </summary>
		public static void ParseFile() {

			//Remove \r and leave only \n
			inputCode = inputCode.Replace( "\r", "" );

			//Split the code by line
			string[] splitCode = inputCode.Split( '\n' );

			//Add every line to the working code
			for( int i = 0; i < splitCode.Length; i++ ) {
				string line = splitCode[i];
				line = line.Trim();

				//If the line is skipable, skip it
				if( ShouldSkipLine( line ) ) {
					continue;
				}

				workingCode.AddLast( line );

				Print( line );
			}

			//Begin processing the working code
			
		}

		/// <summary>
		/// Determines if a line should be skipped and ignored
		/// Primarily this will be empty lines and comments
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static bool ShouldSkipLine( string line ) {

			//Check for empty lines
			if( line.Length == 0 ) {
				return true;
			}

			//Check for comments
			if( line.StartsWith( "#" ) ) {
				return true;
			}

			//Otherwise, don't skip it
			return false;

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
		/// Puts an error into the console and halts compilation
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
