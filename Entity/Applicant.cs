using System;
namespace CodingChallenge.Entity
{
	public class Applicant
	{
		public int ApplicantID { set; get; }
		public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Resume { set; get; }

        public override string ToString()
        {
            return $"{ApplicantID}\t{FirstName}\t{LastName}\t{Email}\t{Phone}\t{Resume}";
        }
    }
}

