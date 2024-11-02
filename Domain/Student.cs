using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydai5.Domain;

public class Student
{
	private string _studentID = string.Empty;

	public string StudentID
	{
		get
		{
		return _studentID;
		}

		set
		{
			_studentID = value;
		}
	}
		private string _firstName = string.Empty;

		public string FirstName
		{
			get => _firstName;

			set => _firstName = value;
		}

		public string LastName { get; set; } = string.Empty;
	
		public string? Email { get; set; }

	}
