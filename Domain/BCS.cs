using ydai5.TechnicalServices;

namespace ydai5.Domain
{
  public class BCS
  {
    public bool CreateProgram(String ProgramCode, String Description)
    {
      bool Confirmation = false;

      Programs ProgramManager = new();

      Confirmation = ProgramManager.AddProgram(ProgramCode, Description);

      return Confirmation;
    }

    public bool EnrollStudent(Student acceptedStudent, string programCode)
    {
      bool Confirmation = false;

      Students StudentManager = new();

      Confirmation = StudentManager.AddStudent(acceptedStudent, programCode);

      return Confirmation;
    }

    public Student FindStudent(String StudentID)
    {
      Students StudentManager = new();

      Student EnrolledStudent = StudentManager.GetStudent(StudentID);

      return EnrolledStudent;
    }

    public bool ModifyStudent(Student EnrolledStudent)
    {
      bool Confirmation = false;

      Students StudentManager = new();

      Confirmation = StudentManager.UpdateStudent(EnrolledStudent);

      return Confirmation;
    }
  
    public bool RemoveStudent(string StudentID)
    {
        Students StudentManager = new Students();

        // Call DeleteStudent in the repository layer and store the result in Confirmation
        bool Confirmation = StudentManager.DeleteStudent(StudentID);

        // Return Confirmation to indicate success or failure
        return Confirmation;
    }
  
    public Domain.Program FindProgram(string ProgramCode)
    {
        Programs ProgramManager = new Programs();

        // Call GetProgram from the repository layer and store the result in EnrolledProgram
        Domain.Program ActiveProgram = ProgramManager.GetProgram(ProgramCode);

        // Return the Program object, whether found or null
        return ActiveProgram;
    }
    public DatabaseUser FindDatabaseUser()
        {
            DatabaseUsers DatabaseUserManager = new();
            DatabaseUser CurrentDatabaseUser = DatabaseUserManager.GetDatabaseUser();
            return CurrentDatabaseUser;
        }
    }
}


