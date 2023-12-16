using System;
using System.Data;
using System.Diagnostics.Contracts;

namespace CodingChallenge.Entity
{
	public class JobListing
	{
		public int JobID { set; get; }
		public int CompanyID { set; get; }
		public string JobTitle { set; get; }
		public string Description { set; get; }
		public string JobType { set; get; }
		public string JobLocation { set; get; }
		public double Salary { set; get; }
		public DateTime PostedDate { set; get; }

        public override string ToString()
        {
			return $"{JobID}\t{CompanyID}\t{JobTitle}\t{Description}\t{JobType}\t" +
				$"{JobLocation}\t{Salary}\t{PostedDate}";
        }
    }
}

