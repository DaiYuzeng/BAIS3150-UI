using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ydai5.Domain;

namespace ydai5.TechnicalServices;

public class Programs
{

  private string ydai5ConnectionString = "Server=dev1.baist.ca;Database=ydai5;User Id=ydai5;Password=Xiaodai0712@;Encrypt=true;TrustServerCertificate=true;";
  
  public bool AddProgram(string ProgramCode, string Description)
  {
      bool Success = false;

      Console.WriteLine("AddProgram");

      using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
      {
          connection.Open();

          SqlCommand AddProgramCommand = new SqlCommand("AddProgram", connection)
          {
              CommandType = CommandType.StoredProcedure
          };

          // Define and add parameters explicitly
          SqlParameter programCodeParameter = new SqlParameter
          {
              ParameterName = "@ProgramCode",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              SqlValue = ProgramCode
          };

          SqlParameter descriptionParameter = new SqlParameter
          {
              ParameterName = "@Description",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              SqlValue = Description
          };

          AddProgramCommand.Parameters.Add(programCodeParameter);
          AddProgramCommand.Parameters.Add(descriptionParameter);

          // Execute the stored procedure
          AddProgramCommand.ExecuteNonQuery();

          Console.WriteLine("Program successfully added.");
          Success = true;  // Set success to true after successful execution
      }

      return Success;  // Return success flag
  }
  
  public Domain.Program GetProgram(string ProgramCode)
  {
      Domain.Program activeProgram = null; // Initialize Program object as null

      Students StudentManager = new();
      List<Student> EnrolledStudents = StudentManager.GetStudents(ProgramCode);
      
      Console.WriteLine("GetProgram");

      using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
      {
          connection.Open();

          SqlCommand getProgramCommand = new SqlCommand("GetProgramByProgramCode", connection)
          {
              CommandType = CommandType.StoredProcedure
          };

          // Define the ProgramCode parameter and assign it the input value
          SqlParameter programCodeParameter = new SqlParameter
          {
              ParameterName = "@ProgramCode",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              SqlValue = ProgramCode
          };

          // Add the parameter to the command
          getProgramCommand.Parameters.Add(programCodeParameter);

          // Execute the command and read the returned data
          using (SqlDataReader reader = getProgramCommand.ExecuteReader())
          {
              if (reader.Read()) // Read the single row (if exists)
              {
                  // Populate the Program object with data from the database
                  activeProgram = new Domain.Program
                  {
                      ProgramCode = reader["ProgramCode"].ToString(),
                      Description = reader["Description"].ToString()
                  };

                  activeProgram.EnrolledStudents.AddRange(EnrolledStudents);
              }
              else
              {
                  Console.WriteLine("No data found for the provided ProgramCode.");
              }

              reader.Close();
          }

          connection.Close();
      }

      return activeProgram; // Return the Program object
  }
}
