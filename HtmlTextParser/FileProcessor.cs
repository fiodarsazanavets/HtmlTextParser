using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlTextParser
{
	internal class FileProcessor
	{
		public void ProcessFile(string fullFilePath)
		{
			var inputText = System.Web.HttpUtility.HtmlEncode(File.ReadAllText(fullFilePath));
			var paragraphs = Regex.Split(inputText, @"(\r\n?|\n)")
							  .Where(p => p.Any(char.IsLetterOrDigit));

			var sb = new StringBuilder();

			foreach (var paragraph in paragraphs)
			{
				if (paragraph.Length == 0)
					continue;

				ApplyHeadingIfRelevant(paragraph, sb, 1);
				ApplyHeadingIfRelevant(paragraph, sb, 2);
				ApplyHeadingIfRelevant(paragraph, sb, 3);
				ApplyParagraphIfRelevant(paragraph, sb);
			}

			sb.AppendLine("<br/>");
			WriteToFile(fullFilePath, sb.ToString());
		}

		private void ApplyHeadingIfRelevant(string paragraph, StringBuilder sb, int headerType)
		{
			if (paragraph.StartsWith($"={headerType}=") && paragraph.EndsWith($"={headerType}="))
			{
				sb.AppendLine("<br/>");
				sb.AppendLine($"<h{headerType}>{paragraph.Replace($"={headerType}=", string.Empty)}</h{headerType}>");
			}
		}

		private bool IsParagraph(string paragraph)
		{
			return !paragraph.StartsWith($"=1=") && !paragraph.EndsWith($"=1=") &&
				!paragraph.StartsWith($"=2=") && !paragraph.EndsWith($"=2=") &&
				!paragraph.StartsWith($"=3=") && !paragraph.EndsWith($"=3=");
		}

		private void ApplyParagraphIfRelevant(string paragraph, StringBuilder sb)
		{
			if (IsParagraph(paragraph))
			{
				paragraph = ApplyAnchorsIfRelevant(paragraph);
				sb.AppendLine($"<p>{paragraph}</p>");
			}
		}

		private string ApplyAnchorsIfRelevant(string paragraph)
		{
			if (IsParagraph(paragraph) &&
				paragraph.Contains("=="))
			{
				// Exit if there is an uneven number of anchor placeholders
				if (CountStringOccurrences(paragraph, "==") % 2 == 0)
					paragraph = ApplyAnchorPlaceholders(paragraph, "==");
			}

			return paragraph;
		}

		private int CountStringOccurrences(string text, string pattern)
		{
			int count = 0;
			int currentIndex = 0;
			while ((currentIndex = text.IndexOf(pattern, currentIndex)) != -1)
			{
				currentIndex += pattern.Length;
				count++;
			}
			return count;
		}

		private string ApplyAnchorPlaceholders(string text, string pattern)
		{
			int count = 0;
			int currentIndex = 0;

			while ((currentIndex = text.IndexOf(pattern, currentIndex)) != -1)
			{
				count++;

				if (count % 2 != 0)
				{
					var prepend = "<a href=\"...\">";
					text = text.Insert(currentIndex, prepend);
					currentIndex += prepend.Length + pattern.Length;
				}
				else
				{
					var append = "</a>";
					text = text.Insert(currentIndex, append);
					currentIndex += append.Length + pattern.Length;
				}
			}

			return text.Replace(pattern, string.Empty);
		}

		private void WriteToFile(string fullFilePath, string text)
		{
			var outputFilePath = Path.GetDirectoryName(fullFilePath) + Path.DirectorySeparatorChar +
				Path.GetFileNameWithoutExtension(fullFilePath) + "-html" + Path.GetExtension(fullFilePath);

			using (StreamWriter file =
			new StreamWriter(outputFilePath))
			{
				file.Write(text);
			}
		}
	}
}
