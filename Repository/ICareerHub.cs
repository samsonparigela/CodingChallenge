using System;
using CodingChallenge.Entity;
using Microsoft.Data.SqlClient;

namespace CodingChallenge.Repository
{
	public interface ICareerHub
	{
        public abstract bool RegisterApplicant(SqlConnection conn);
        public abstract bool RegisterCompany(SqlConnection conn);
        public abstract bool PostJob(Company company, SqlConnection conn);
        public abstract bool ApplyForJob(Applicant applicant, SqlConnection conn);
        public abstract List<JobListing> GetAlltheJobs(SqlConnection conn);
        public abstract List<JobListing> GetAlltheJobsInRange(SqlConnection conn);
        public abstract List<Applicant> GetAllApplicants(SqlConnection conn);
        public abstract List<JobListing> GetJobsPosted(Company company, SqlConnection conn);
        public abstract List<JobApplication> GetApplicationsForJob(Company company, SqlConnection conn);
        public abstract List<JobListing> GetJobsApplied(Applicant applicant, SqlConnection conn);
        public abstract List<Company> GetCompanies(SqlConnection conn);
        public abstract bool ApplicantLogin(SqlConnection conn);
        public abstract bool CompanyLogin(SqlConnection conn);

    }

}

