
using cwiczenia3napodstawie2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw2.DAL
{
    public interface IStudentsDal
    {
        public IEnumerable<Student> GetStudents();
    }
}
