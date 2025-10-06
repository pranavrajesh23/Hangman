using System;
namespace Hangman.Model
{
	public class WordEntry
	{
		public string Word {  get; set; }
		public string Hint { get; set; }

		public WordEntry(string word, string hint)
		{
			Word = word;
			Hint = hint;
		}
	}
}