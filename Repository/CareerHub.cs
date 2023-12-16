using CodingChallenge.Entity;
using CodingChallenge.Exceptions;
using Microsoft.Data.SqlClient;

namespace CodingChallenge.Repository
{
	public class CareerHub:ICareerHub
	{

        #region RegisterApplicant
        public bool RegisterApplicant(SqlConnection conn)
		{

            Console.WriteLine("Applicant ID");
            int applicantID=int.Parse(Console.ReadLine());

            string query = $"SELECT * FROM APPLICANT WHERE APPLICANTID={applicantID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            int n = cmd.ExecuteNonQuery();
            if(n>0)
            {
                Console.WriteLine("Applicant already found");
                return false;
            }
            
            Console.WriteLine("First Name");
            string FisrtName = Console.ReadLine();

            Console.WriteLine("Last Name");
            string LastName = Console.ReadLine();

            Console.WriteLine("Email ");

            string Email = Console.ReadLine();
            if (!Email.Contains('@'))
                throw new EmailFormatException("Wrong Format");


            Console.WriteLine("Phone");
            string Phone = Console.ReadLine();

            Console.WriteLine("Resume");
            string Resume = Console.ReadLine();



            Applicant applicant = new Applicant();
            applicant.ApplicantID = applicantID;
            applicant.FirstName = FisrtName;
            applicant.LastName = LastName;
            applicant.Email = Email;
            applicant.Phone = Phone;
            applicant.Resume = Resume;

            query = "INSERT INTO APPLICANT VALUES(@applicantID,@FirstName,@LastName,@Email,@Phone,@Resume)";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@applicantID", applicant.ApplicantID);
            cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
            cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
            cmd.Parameters.AddWithValue("@Email", applicant.Email);
            cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
            cmd.Parameters.AddWithValue("@Resume", applicant.Resume);

            n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region RegisterCompany
        public bool RegisterCompany(SqlConnection conn)
        {

            Console.WriteLine("Company ID");
            int companyID = int.Parse(Console.ReadLine());

            string query = $"SELECT * FROM Company WHERE CompanyID={companyID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            int n = cmd.ExecuteNonQuery();
            if (n >0)
            {
                Console.WriteLine("Company already found");
                return false;
            }

            Console.WriteLine("Company Name");
            string companyName = Console.ReadLine();

            Console.WriteLine("Location");
            string location = Console.ReadLine();

            Company company = new Company();
            company.CompanyID = companyID;
            company.CompanyName = companyName;
            company.Location = location;


            query = "INSERT INTO COMPANY VALUES(@CompanyID,@CompanyName,@Location)";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CompanyID", company.CompanyID);
            cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            cmd.Parameters.AddWithValue("@Location", company.Location);


            n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region PostJob
        public bool PostJob(Company company,SqlConnection conn)
        {

            Console.WriteLine("Enter Job ID you want to post");
            int JobID = int.Parse(Console.ReadLine());

            string query = $"SELECT * FROM JobListing WHERE JobID={JobID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                Console.WriteLine("Job already Exists");
                return false;
            }

            Console.WriteLine("Job Title");
            string JobTitle = Console.ReadLine();

            Console.WriteLine("Job Description");
            string JobDescription = Console.ReadLine();

            Console.WriteLine("Job Location");
            string JobLocation = Console.ReadLine();


            Console.WriteLine("Salary");
            double Salary = double.Parse(Console.ReadLine());

            if (Salary < 0)
                throw new SalaryIncorrectException("Salary must be greater than 0");

            Console.WriteLine("Job Type");
            string jobType = Console.ReadLine();

            DateTime postedDate = new DateTime(2023, 12, 31);

            JobListing jl= new JobListing();
            jl.CompanyID = company.CompanyID;
            jl.JobID = JobID;
            jl.JobTitle = JobTitle;
            jl.Description = JobDescription;
            jl.JobLocation = JobLocation;
            jl.Salary = Salary;
            jl.JobType = jobType;
            jl.PostedDate = postedDate;

            query = "INSERT INTO JobListing VALUES(@JobID,@CompanyID,@JobTitle,@JobDescription,@JobLocation,@Salary,@JobType,@PostedDate)";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@JobID", jl.JobID);
            cmd.Parameters.AddWithValue("@CompanyID", jl.CompanyID);
            cmd.Parameters.AddWithValue("@JobTitle", jl.JobTitle);
            cmd.Parameters.AddWithValue("@JobDescription", jl.Description);
            cmd.Parameters.AddWithValue("@JobLocation", jl.JobLocation);
            cmd.Parameters.AddWithValue("@Salary", jl.Salary);
            cmd.Parameters.AddWithValue("@JobType", jl.JobType);
            cmd.Parameters.AddWithValue("@PostedDate", postedDate);

            n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                Console.WriteLine("Successfully Posted Job");
                return true;
            }
            return false;
        }
        #endregion

        #region ApplyForJob
        public bool ApplyForJob(Applicant applicant, SqlConnection conn)
        {
            string query = "SELECT * FROM JobListing";
            SqlCommand cmd = new SqlCommand(query,conn);
            SqlDataReader rd= cmd.ExecuteReader();
            while(rd.Read())
            {
                Console.WriteLine($"{rd["JobID"]}  {rd["CompanyID"]}  {rd["JobTitle"]}  {rd["JobLocation"]}" +
                    $"  {rd["JobDescription"]}" +
                    $"  {rd["Salary"]}{rd["JobType"]}  {rd["PostedDate"]}");
            }
            rd.Close();

            Console.WriteLine("Enter the Job ID you want to apply for?");
            int jobID=int.Parse(Console.ReadLine());

            cmd.CommandText = $"SELECT * FROM JobApplication where ApplicantID={applicant.ApplicantID} AND JobID={jobID}";
            rd = cmd.ExecuteReader();

            if (rd.HasRows)
            {
                Console.WriteLine("Already Applied For the job");
            }
            else
            {
                rd.Close();
                query = "INSERT INTO JobApplication VALUES(@applicationID,@JobID,@ApplicantID,@ApplicationDate,@CoverLetter)";
                cmd = new SqlCommand(query,conn);
               // cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@applicationID", applicant.ApplicantID);
                cmd.Parameters.AddWithValue("@JobID", jobID);
                cmd.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                DateTime dt = new DateTime(2023, 10, 12);
                cmd.Parameters.AddWithValue("@ApplicationDate", dt);
                cmd.Parameters.AddWithValue("@CoverLetter", applicant.Resume);
                //cmd.Connection = conn;
                int n = cmd.ExecuteNonQuery();
                if (n == 1)
                {
                    Console.WriteLine("Successfully Applied");
                }
                else
                    Console.WriteLine("Cannot Apply");


            }
            return true;
        }
        #endregion

        #region GetAlltheJobs
        public List<JobListing> GetAlltheJobs(SqlConnection conn)
        {
            List<JobListing> jbList = new List<JobListing>();

            string query = $"SELECT * FROM JobListing";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                JobListing jb = new JobListing();

                jb.JobID = (int)rd["JobID"];
                jb.CompanyID = (int)rd["CompanyID"];
                jb.JobTitle = (string)rd["JobTitle"];
                jb.Description = (string)rd["JobDescription"];
                jb.JobLocation = (string)rd["JobLocation"];
                jb.Salary = (double)rd["Salary"];
                jb.JobType = (string)rd["JobType"];
                jb.PostedDate = (DateTime)rd["PostedDate"];

                jbList.Add(jb);

            }
            rd.Close();
            return jbList;


        }
        #endregion

        #region GetAlltheJobsByRange
        public List<JobListing> GetAlltheJobsInRange(SqlConnection conn)
        {
            List<JobListing> jbList = new List<JobListing>();
            Console.WriteLine("Enter Min and Max Salary to search");
            int min = int.Parse(Console.ReadLine());
            int max = int.Parse(Console.ReadLine());
            string query = $"SELECT * FROM JobListing WHERE SALARY BETWEEN {min} AND {max}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                JobListing jb = new JobListing();

                jb.JobID = (int)rd["JobID"];
                jb.CompanyID = (int)rd["CompanyID"];
                jb.JobTitle = (string)rd["JobTitle"];
                jb.Description = (string)rd["JobDescription"];
                jb.JobLocation = (string)rd["JobLocation"];
                jb.Salary = (double)rd["Salary"];
                jb.JobType = (string)rd["JobType"];
                jb.PostedDate = (DateTime)rd["PostedDate"];

                jbList.Add(jb);

            }
            rd.Close();
            return jbList;


        }
        #endregion

        #region GetAllApplicants
        public List<Applicant> GetAllApplicants(SqlConnection conn)
        {
            List<Applicant> ApplicantList = new List<Applicant>();

            string query = $"SELECT * FROM Applicant";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Applicant applicant = new Applicant();

                applicant.ApplicantID = (int)rd["ApplicantID"];
                applicant.FirstName = (string)rd["FirstName"];
                applicant.LastName = (string)rd["LastName"];
                applicant.Email = (string)rd["Email"];
                applicant.Phone = (string)rd["Phone"];
                applicant.Resume = (string)rd["Resume"];


                ApplicantList.Add(applicant);

            }
            rd.Close();
            return ApplicantList;

        }
        #endregion

        #region GetJobsPosted

        public List<JobListing> GetJobsPosted(Company company,SqlConnection conn)
        {
            List<JobListing> jbList = new List<JobListing>();
            //Console.WriteLine("KKK");
            string query = $"SELECT * FROM JobListing where CompanyID={company.CompanyID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                JobListing jb = new JobListing();

                jb.JobID = (int)rd["JobID"];
                jb.CompanyID = (int)rd["CompanyID"];
                jb.JobTitle = (string)rd["JobTitle"];
                jb.Description = (string)rd["JobDescription"];
                jb.JobLocation = (string)rd["JobLocation"];
                jb.Salary = (double)rd["Salary"];
                jb.JobType = (string)rd["JobType"];
               
                jb.PostedDate = (DateTime)rd["PostedDate"];

                jbList.Add(jb);

            }
            rd.Close();
            return jbList;


        }



        #endregion

        #region GetApplicantionsForJob
        public List<JobApplication> GetApplicationsForJob(Company company,SqlConnection conn)
        {
            List<JobApplication> appList = new List<JobApplication>();
            Console.WriteLine("The Jobs Posted by you are\n");

            string query = $"SELECT * FROM JobListing where CompanyID={company.CompanyID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine($"JobID :: {rd["JobID"]}  JobTitle :: {rd["JobTitle"]}  " +
                    $"Salary :: {rd["Salary"]}  JobDescription  :: {rd["JobDescription"]}");
            }
            rd.Close();

            Console.WriteLine("\nEnter Job ID of Job Applications you wish to see?\n");
            int jobID = int.Parse(Console.ReadLine());

            query = $"SELECT * FROM JobAPplication where JobID={jobID}\n";
            cmd = new SqlCommand(query, conn);
            rd = cmd.ExecuteReader();

            while(rd.Read())
            {
                JobApplication application = new JobApplication();
                application.ApplicationID= (int)rd["ApplicantID"];
                application.JobID = (int)rd["JobID"];
                application.ApplicantID = (int)rd["ApplicantID"];
                application.ApplicationDate = (DateTime)rd["ApplicationDate"];
                application.CoverLetter = (string)rd["CoverLetter"];
                appList.Add(application);

            }
            rd.Close();
            return appList;


        }
        #endregion

        #region GetJobsApplied
        public List<JobListing> GetJobsApplied(Applicant applicant,SqlConnection conn)
        {
            List<JobListing> jbList = new List<JobListing>();

            string query = $"SELECT * FROM JOBLISTING WHERE JOBID IN (SELECT JOBID FROM JOBAPPLICATION WHERE ApplicantID={applicant.ApplicantID})";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.HasRows)
                throw new Exception();
            while(rd.Read())
            {
                JobListing jb = new JobListing();

                jb.JobID = (int)rd["JobID"];
                jb.CompanyID = (int)rd["CompanyID"];
                jb.JobTitle = (string)rd["JobTitle"];
                jb.Description = (string)rd["JobDescription"];
                jb.JobLocation = (string)rd["JobLocation"];
                jb.Salary = (double)rd["Salary"];
                jb.JobType = (string)rd["JobType"];
                jb.PostedDate = (DateTime)rd["PostedDate"];

                jbList.Add(jb);
            }

            rd.Close();
            return jbList;
        }
        #endregion

        #region GetCompanies
        public List<Company> GetCompanies(SqlConnection conn)
        {
            List<Company> CompanyList = new List<Company>();

            string query = $"SELECT * FROM Company";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Company company = new Company();

                company.CompanyID = (int)rd["CompanyID"];
                company.CompanyName = (string)rd["CompanyName"];
                company.Location = (string)rd["Location"];

                CompanyList.Add(company);

            }
            rd.Close();
            return CompanyList;


        }
        #endregion

        #region ApplicantLogin
        public bool ApplicantLogin(SqlConnection conn)
        {

            List<Company> companyList = new List<Company>();
            List<JobListing> jobListings = new List<JobListing>();

            Console.WriteLine("Enter Applicant ID");
            int applicantID = int.Parse(Console.ReadLine());

            string query = $"SELECT * FROM APPLICANT WHERE APPLICANTID={applicantID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.HasRows)
            {
                Console.WriteLine("Applicant Doesnot exist");
                return false;
            }

            Console.WriteLine("Enter FirstName");
            string firstName = (Console.ReadLine());
            Applicant applicant = new Applicant();
            while(rd.Read())
            {
                applicant.ApplicantID = (int) rd["applicantID"];
                applicant.FirstName = (string) rd["FirstName"];
                if(firstName!= (string)rd["FirstName"])
                {
                    Console.WriteLine("Wrong Credentials");
                    return false;
                }
                applicant.LastName = (string)rd["LastName"];
                applicant.Email = (string) rd["Email"];
                applicant.Phone = (string) rd["Phone"];
                applicant.Resume = (string) rd["Resume"];
            }

            rd.Close();
            Console.WriteLine($"\nLogged in Successfully as Student\n");
            int n = 1;
            do
            {
                Console.WriteLine("\n1. Apply for Job\n2. Get the jobs you Applied for\n3. Get all the Jobs" +
                "\n4. Get Companies\n5. User Profile\n6. Get The Job in salary range\n7. Logout\n");
                Console.WriteLine("Choose from 1-6");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        ApplyForJob(applicant,conn);
                        break;
                    case 2:
                        jobListings = GetJobsApplied(applicant,conn);
                        Console.WriteLine("Job ID\t Company ID\t Title\t Description\t Location\t Salary\t Type\t Date\n");
                        foreach (JobListing jb in jobListings)
                            Console.WriteLine(jb);
                        break;
                    case 3:
                        jobListings = GetAlltheJobs(conn);
                        Console.WriteLine("Job ID\t Company ID\t Title\t Description\t Location\t Salary\t Type\t Date\n");
                        foreach (JobListing jb in jobListings)
                            Console.WriteLine(jb);
                        break;
                    case 4:
                        Console.WriteLine("Company Id \tCompany Name \tLocation\n");
                        companyList = GetCompanies(conn);
                        foreach (Company cp in companyList)
                            Console.WriteLine(cp);
                        break;
                    case 5:
                        Console.WriteLine("Applicant ID \tFirst Name \tLast Name \tEmail \tPhone \tResume\n");
                        Console.WriteLine(applicant);
                        break;
                    case 6:
                        jobListings = GetAlltheJobsInRange(conn);
                        Console.WriteLine("Job ID\t Company ID\t Title\t Description\t Location\t Salary\t Type\t Date\n");
                        foreach (JobListing jb in jobListings)
                            Console.WriteLine(jb);
                        break;
                    case 7:
                        Console.WriteLine("Logging out");
                        n = 0;
                        break;
                    default:
                        Console.WriteLine("Wrong Option!");
                        break;
                }
            } while (n == 1);
            
            return true;
        }
        #endregion

        #region CompanyLogin

        public bool CompanyLogin(SqlConnection conn)
        {
            List<JobListing> jblist = new List<JobListing>();
            List<JobApplication> JAList = new List<JobApplication>();
            List<Applicant> AppList = new List<Applicant>();

            Console.WriteLine("Enter Company ID\n");
            int CompanyID = int.Parse(Console.ReadLine());

            string query = $"SELECT * FROM COMPANY WHERE CompanyID={CompanyID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if(!rd.HasRows)
            {
                Console.WriteLine("Company doesnot exist");
                return false;
            }
               

            Console.WriteLine("Enter Company Name");
            string companyName = (Console.ReadLine());

            Company company = new Company(); 
            while (rd.Read())
            {
               company.CompanyID = (int)rd["CompanyID"];
               company.CompanyName = (string)rd["CompanyName"];
                if(companyName!= (string)rd["CompanyName"])
                {
                    Console.WriteLine("Wrong Credentials!");
                    return false;
                }
               company.Location = (string)rd["Location"];
            }
            rd.Close();
            Console.WriteLine($"\nLogged in Successfully as Company\n");

           
            int n = 1;
            do
            {

                Console.WriteLine("\n1. Get Applications For Job\n2. Get the jobs you Posted\n3. Get all the Applicants" +
               "\n4. Post on Job Listing\n5. Logout");

                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        JAList=GetApplicationsForJob(company,conn);
                        Console.WriteLine(" ");
                        Console.WriteLine("Application ID\t Job ID\t Applicant ID\t Application Date\t Cover Letter\n");
                        Console.WriteLine(" ");
                        foreach (JobApplication jb in JAList)
                            Console.WriteLine(jb);
                        break;
                    case 2:
                        jblist= GetJobsPosted(company,conn);
                        Console.WriteLine(" ");
                        Console.WriteLine("Job ID\t Company ID\t Title\t Description\t Location\t Salary\t Type\t Date\n");
                        foreach(JobListing jb in jblist)
                            Console.WriteLine(jb);
                        break;
                    case 3:
                        AppList=GetAllApplicants(conn);
                        Console.WriteLine("Applicant ID \tFirst Name \tLast Name \tEmail \tPhone \tResume\n");
                        foreach (Applicant ap in AppList)
                            Console.WriteLine(ap);
                        break;
                    case 4:
                        PostJob(company,conn);
                        break;
                    case 5:
                        n = 0;
                        Console.WriteLine("Logging out!");
                        break;
                    default:
                        Console.WriteLine("Wrong Choice");
                        break;
                }


            } while (n==1);
            Console.ReadLine();
            return true;
        }
        #endregion


    }
}

