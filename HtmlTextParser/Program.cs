using System;
using System.Threading.Tasks;

namespace HtmlTextParser
{
	class Program
	{
		static void Main()
		{
			try
			{
				Console.WriteLine("Welcome to HTML text parser.");
				Console.WriteLine("The following actions will be performed:");
				Console.WriteLine("- The string would be HTML-encoded.");
				Console.WriteLine("- Every paragraph will be enclsed in <p> tag.");
				Console.WriteLine("- Every text enclosed in =1= will be enclosed in <h1> tag.");
				Console.WriteLine("- Every text enclosed in =2= will be enclosed in <h2> tag.");
				Console.WriteLine("- Every text enclosed in =3= will be enclosed in <h3> tag.");
				Console.WriteLine("- A line with <br/> tag will be added before any header tag.");
				Console.WriteLine("- Any text enclosed in == tags will be enclosed in <a> tag with a placeholder for the URL.");
				Console.WriteLine("Please specify the text file to convert to HTML.");

				var fileProcessor = new FileProcessor();
				var fullFilePath = Console.ReadLine();

				fileProcessor.ProcessFile(fullFilePath);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error processing file: {ex.Message}");
			}

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
