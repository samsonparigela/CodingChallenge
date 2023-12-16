using System;
using Microsoft.Data.SqlClient;

namespace CodingChallenge.DBUtil
{

	public class DBConnection
	{
		public static SqlConnection Connect()
		{

			string connectionString = @"Server=localhost;Database=CodingChallenge;User ID=sa;Password=reallyStrongPwd123;TrustServerCertificate=True;";
			SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();
			return conn;

		}
	}
}

