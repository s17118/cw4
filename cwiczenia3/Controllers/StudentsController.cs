using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using cwiczenia3napodstawie2.Models;
using Microsoft.AspNet.SignalR.Messaging;
using System.Data.SqlClient;
using cw2.DAL;

namespace cwiczenia3napodstawie2.Controllers
{

    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        string conString = "Data Source=db-mssq;; Initial Catalog=s17118; Integrated Security=True";
        //string conString = "Data Source=localhost;; Initial Catalog=s17911; Integrated Security=True";
        const string S1 = "Kowalski";
        const string S2 = "Kowalksiiiii";
        const string S3 = "Kowalskiiiiiiiiiiiiii";

        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            var list = new List<StudentInfoDTO>();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name," +
                    " e.Semester from Student s join Enrollment e on e.IdEnrollment = s.IdEnrollment join Studies st on st.IdStudy = e.IdStudy";
                connection.Open();


                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    var st = new StudentInfoDTO();

                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Name = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    list.Add(st);
                }
            }
            return Ok(list);
        }
    
        //   return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";



        // ZADANIE 4
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute]string indeks)
        {
            var st = new StudentInfoDTO();
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester from Student s join Enrollment e on e.IdEnrollment = s.IdEnrollment join Studies st on st.IdStudy = e.IdStudy";
                con.Open();


                SqlDataReader dr = com.ExecuteReader();

                st.FirstName = dr["FirstName"].ToString();
                st.LastName = dr["LastName"].ToString();
                st.BirthDate = dr["BirthDate"].ToString();
                st.Name = dr["Name"].ToString();
                st.Semester = dr["Semester"].ToString();
            }
            return Ok(st);
        }

        // ZADANIE 6

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut]
        public IActionResult UpdateStudent(Student student)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }

}