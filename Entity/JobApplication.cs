using System;
namespace CodingChallenge.Entity
{
	public class JobApplication
	{
        public int ApplicationID { set; get; }
        public int JobID { set; get; }
        public int ApplicantID { set; get; }
        public DateTime ApplicationDate { set; get; }
        public string CoverLetter { set; get; }

        public override string ToString()
        {
            return $"{ApplicationID}\t{JobID}\t{ApplicantID}\t{ApplicationDate}\t{CoverLetter}";
        }
    }
}

