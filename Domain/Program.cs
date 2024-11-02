using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydai5.Domain;

public class Program
{
		private string _programCode = string.Empty;

		public string ProgramCode
		{
				get => _programCode;
				set => _programCode = value;
		}

		private string _description = string.Empty;

		public string Description
		{
				get => _description;
				set => _description = value;
		}

		private List<Student> _enrolledStudents = new List<Student>();
		public List<Student> EnrolledStudents
		{
				get { return _enrolledStudents; }
		}
}
