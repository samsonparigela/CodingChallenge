using System;
namespace CodingChallenge.Exceptions
{
	public class SalaryIncorrectException:Exception
	{
		public SalaryIncorrectException(string message)
			: base(message)
		{

		}
	}
}

