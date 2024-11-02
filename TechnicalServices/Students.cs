using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ydai5.Domain;

namespace ydai5.TechnicalServices;

public class Students
{

    private string ydai5ConnectionString = "Server=dev1.baist.ca;Database=ydai5;User Id=ydai5;Password=Xiaodai0712@;Encrypt=true;TrustServerCertificate=true;";
  
    public bool AddStudent(Student accessptedStudent, string programCode)
  {
    bool Success = false;

    using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
    {
      connection.Open();

      SqlCommand AddStudentCommand = new()
      {
          Connection = connection,
          CommandType = CommandType.StoredProcedure, // Set the command type to StoredProcedure
          CommandText = "AddStudent" // The name of the stored procedure
      };

      // Define and add parameters
      AddStudentCommand.Parameters.Add(new SqlParameter
      {
          ParameterName = "@StudentID",
          SqlDbType = SqlDbType.VarChar,
          Direction = ParameterDirection.Input,
          SqlValue = accessptedStudent.StudentID
      });

      AddStudentCommand.Parameters.Add(new SqlParameter
      {
          ParameterName = "@FirstName",
          SqlDbType = SqlDbType.VarChar,
          Direction = ParameterDirection.Input,
          SqlValue = accessptedStudent.FirstName
      });

      AddStudentCommand.Parameters.Add(new SqlParameter
      {
          ParameterName = "@LastName",
          SqlDbType = SqlDbType.VarChar,
          Direction = ParameterDirection.Input,
          SqlValue = accessptedStudent.LastName
      });

      AddStudentCommand.Parameters.Add(new SqlParameter
      {
          ParameterName = "@Email",
          SqlDbType = SqlDbType.VarChar,
          Direction = ParameterDirection.Input,
          SqlValue = accessptedStudent.Email
      });

      AddStudentCommand.Parameters.Add(new SqlParameter
      {
          ParameterName = "@ProgramCode",
          SqlDbType = SqlDbType.VarChar,
          Direction = ParameterDirection.Input,
          SqlValue = programCode
      });

      // Execute the stored procedure
      AddStudentCommand.ExecuteNonQuery();

      Console.WriteLine("Student successfully added.");
      Console.WriteLine("----------------------------");

      connection.Close();
      Success = true;
    }

    return Success;
  }

    public Student GetStudent(string StudentID)
  {
      Console.WriteLine("GetStudent");

      Student? student = null;

      using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
      {
          connection.Open();
          
          SqlCommand GetStudentByStudentIDCommand = new SqlCommand("GetStudentByStudentID", connection)
          {
              CommandType = CommandType.StoredProcedure
          };

          // Define and add mandatory parameter (StudentID)
          SqlParameter studentIDParameter = new SqlParameter
          {
              ParameterName = "@StudentID",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              SqlValue = StudentID
          };

          GetStudentByStudentIDCommand.Parameters.Add(studentIDParameter);

          using (SqlDataReader reader = GetStudentByStudentIDCommand.ExecuteReader())
          {
              if (reader.Read()) // Read the single row (if exists)
              {
                  // Initialize and populate the Student object with data from the database
                  student = new Student
                  {
                      StudentID = reader["StudentID"].ToString(),
                      FirstName = reader["FirstName"].ToString(),
                      LastName = reader["LastName"].ToString(),
                      Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null
                  };
              }
              else
              {
                  Console.WriteLine("No student found with the provided StudentID.");
              }

              reader.Close();
          }

          connection.Close();
      }

      return student;
  }

    public bool UpdateStudent(Student EnrolledStudent)
  {
      bool Success = false;  // Initialize return value as null

      Console.WriteLine("UpdateStudent");

      using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
      {
          connection.Open();

          SqlCommand updateStudentCommand = new SqlCommand("UpdateStudent", connection)
          {
              CommandType = CommandType.StoredProcedure
          };

          // Define and add mandatory parameter (StudentID)
          updateStudentCommand.Parameters.Add(new SqlParameter
          {
              ParameterName = "@StudentID",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              SqlValue = EnrolledStudent.StudentID
          });

          // Define and add optional parameters (FirstName, LastName, Email, ProgramCode)
          updateStudentCommand.Parameters.Add(new SqlParameter
          {
              ParameterName = "@FirstName",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              IsNullable = true,
              SqlValue = EnrolledStudent.FirstName
          });

          updateStudentCommand.Parameters.Add(new SqlParameter
          {
              ParameterName = "@LastName",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              IsNullable = true,
              SqlValue = EnrolledStudent.LastName
          });

          updateStudentCommand.Parameters.Add(new SqlParameter
          {
              ParameterName = "@Email",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              IsNullable = true,
              SqlValue = (object)EnrolledStudent.Email ?? DBNull.Value
          });

          // Execute the stored procedure
          updateStudentCommand.ExecuteNonQuery();

          Console.WriteLine("Student successfully updated.");

          // After updating, return the updated Student

          Success = true;
          connection.Close();
      }

      return Success;  // Return the updated student object
  }

    public bool DeleteStudent(string StudentID)
  {
      bool Success = false;  // Initialize success flag

      Console.WriteLine("DeleteStudent");

      using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
      {
          connection.Open();

          SqlCommand deleteStudentCommand = new SqlCommand("DeleteStudent", connection)
          {
              CommandType = CommandType.StoredProcedure
          };

          // Define and add the StudentID parameter (required)
          SqlParameter studentIDParam = new SqlParameter
          {
              ParameterName = "@StudentID",
              SqlDbType = SqlDbType.VarChar,
              Direction = ParameterDirection.Input,
              SqlValue = StudentID
          };

          deleteStudentCommand.Parameters.Add(studentIDParam);

          // Execute the stored procedure and check if any rows were affected
          int rowsAffected = deleteStudentCommand.ExecuteNonQuery();

          if (rowsAffected > 0)
          {
              Console.WriteLine("Student successfully deleted.");
              Success = true;  // Set success to true if student was deleted
          }
          else
          {
              Console.WriteLine("No student found with the provided StudentID.");
          }

          connection.Close();
      }

      return Success;  // Return whether the student was deleted or not
  }

    public List<Student> GetStudents(string ProgramCode)
    {
        List<Student> EnrolledStudents = new List<Student>();

        Console.WriteLine("GetStudentsByProgram");

        using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
        {
            connection.Open();

            SqlCommand getStudentsByProgramCodeCommand = new SqlCommand("GetStudentsByProgramCode", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Define and add the ProgramCode parameter
            SqlParameter programCodeParameter = new SqlParameter
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = ProgramCode
            };

            getStudentsByProgramCodeCommand.Parameters.Add(programCodeParameter);

            // Execute the command and get a SqlDataReader to retrieve multiple rows
            using (SqlDataReader reader = getStudentsByProgramCodeCommand.ExecuteReader())
            {
                // always has a Student, so we don't need to hasRow() check
                while (reader.Read())
                {
                    // Create a Student object and populate its properties from the database
                    Student student = new Student
                    {
                        StudentID = reader["StudentID"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                    };

                    // Add the student to the list
                    EnrolledStudents.Add(student);
                }
                reader.Close();
            }

            connection.Close();
        }

        return EnrolledStudents;  // Return the list of students
    }
}
