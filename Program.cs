using CodingChallenge.DBUtil;
using CodingChallenge.Repository;
using Microsoft.Data.SqlClient;

SqlConnection conn = DBConnection.Connect();
CareerHub careerHub = new CareerHub();
trail:
try
{
    do
    {
        Console.WriteLine("-----Career Hub-----\n");
        Console.WriteLine("1. Regsiter Applicant\n2. Login Applicant\n3. Register Company\n4. Login Company\n");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                careerHub.RegisterApplicant(conn);
                break;
            case 2:
                careerHub.ApplicantLogin(conn);
                break;
            case 3:
                careerHub.RegisterCompany(conn);
                break;
            case 4:
                careerHub.CompanyLogin(conn);
                break;
            default:
                Console.WriteLine("Wrong Choice! Try Again!");
                break;

        }
    } while (true);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
goto trail;
