using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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

		//The lookup dictionary for opcodes
		static Dictionary<string, byte> opcodeLookup = new Dictionary<string, byte>() {
			{ "MOV",	1 },
			{ "ADD",	2 },
			{ "SUB",	3 },
			{ "BNE",	4 },
			{ "BEQ",	5 },
			{ "BGT",	6 },
			{ "BLT",	7 },
			{ "BR" ,	8 },
			{ "LOAD",	9 },
			{ "STORE",  10 },
			{ "APND",   11 },
			{ "PRNT",   12 },
			{ "DUMP",   13 },
			{ "CLR",	14 },
			{ "ASL",	15 },
			{ "ASR",	16 },
		};

		//The lookup dictionary for the number of arguments an opcode should have
		static Dictionary<string, byte> argumentCountLookup = new Dictionary<string, byte>() {
			{ "MOV",    3 },
			{ "ADD",    4 },
			{ "SUB",    4 },
			{ "BNE",    4 },
			{ "BEQ",    4 },
			{ "BGT",    4 },
			{ "BLT",    4 },
			{ "BR" ,    2 },
			{ "LOAD",   2 },
			{ "STORE",  2 },
			{ "APND",   2 },
			{ "PRNT",   1 },
			{ "DUMP",   1 },
			{ "CLR",    1 },
			{ "ASL",    4 },
			{ "ASR",    4 },
		};

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

			//Stores our current line number
			int lineNumber = 0;

			//This dictionary holds labels and their line number
			Dictionary<string, int> labels = new Dictionary<string, int>();

			//Add every operation line to the working code
			for( int i = 0; i < splitCode.Length; i++ ) {
				string line = splitCode[i];
				line = line.Trim();

				//If the line is skipable, skip it
				if( ShouldSkipLine( line ) ) {
					//This also only increments the line number when we have added a line, which makes life easier for labels
					continue;
				}

				//If this is a label
				if( line.EndsWith( ":" ) ) {

					//Make sure it's a valid label
					if( !IsValidLabelDeclaration( line ) ) {
						Error( Strings.MalformedLabelFound );
					}

					//Store the label
					string labelName = line.Replace( ":", "" );
					labels.Add( labelName, lineNumber );

					//Don't add this line or incrememnt the line number
					continue;
				}

				//Holds the bytes we will be writing to the file
				//1 byte for opcode, 2 bytes for argument 1, 2 bytes for argument 2, 2 bytes for argument 3 = 7 total bytes
				byte[] outputLine = new byte[7];

				//Split this line into its pieces
				string[] splitLine = line.Split( ' ' );

				/*
				 *	Opcode
				 */

				//Make sure this is a valid opcode
				if( !opcodeLookup.ContainsKey( splitLine[0] ) ) {
					Error( Strings.UnrecognizedOpcode );
				}

				//Look up the opcode's byte
				byte opcodeByte = opcodeLookup[ splitLine[0] ];

				//Save that byte into our output line
				outputLine[0] = opcodeByte;

				/*
				 *	Arguments
				 */

				//Check that we have the right number of arguments
				if( splitLine.Length != argumentCountLookup[splitLine[0]] ) {
					Error( Strings.WrongNumberOfArguments );
				}

				/*
				 *	Argument #1
				 */



				lineNumber++;
			}

		}

		/// <summary>
		/// Returns whether or not a line is a valid label
		/// Primarily whether it has no spaces as well as some non-number characters
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static bool IsValidLabelDeclaration( string line ) {
			
			//Check for white space
			if( Regex.IsMatch( line, "\\s" ) ) {
				return false;
			}

			//Check for at least some non-number characters
			if( !Regex.IsMatch( line, "\\D" ) ) {
				return false;
			}

			//Check for an ending ":"
			if( !line.EndsWith( ":" ) ) {
				return false;
			}

			//Otherwise, it's probably a label
			return true;
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
