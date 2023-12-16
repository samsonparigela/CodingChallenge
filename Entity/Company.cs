using System;
namespace CodingChallenge.Entity
{
	public class Company
	{
		public int CompanyID { set; get; }
		public string CompanyName { set; get; }
        public string Location { set; get; }

        public override string ToString()
        {
            return $"{CompanyID}\t\t{CompanyName}\t\t{Location}";
        }
    }
}

