using System;
namespace CodingChallenge.Exceptions
{
	public class EmailFormatException:Exception
	{
		public EmailFormatException(string message)
			:base(message)
		{
		}
	}
}

