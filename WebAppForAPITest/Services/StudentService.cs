using Newtonsoft.Json;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using WebAppForAPITest.Helpers;
using WebAppForAPITest.Models;
using WebAppForAPITest.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppForAPITest.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/students";

        public StudentService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<StudentModel>> Find()
        {
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAsync<List<StudentModel>>();
        }

        public async Task<IEnumerable<StudentModel>> GetAllStudents()
        {
            try
            {
                StudentModel sendobj = new StudentModel();
                sendobj.FirstName = "Pratik";
                sendobj.LastName = "Sawant";
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendobj);
                //json string is not required but in lambda code i have read the body and expecting data so in order to not cause error, im sending data purposefully
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://21wfyylyve.execute-api.us-east-1.amazonaws.com/get-all-students"),
                    Content = new StringContent(
                        jsonString,
                        Encoding.UTF8,
                        MediaTypeNames.Application.Json), // or "application/json" in older versions
                };

                var response = await _client.SendAsync(request)
                    .ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

                var model = JsonConvert.DeserializeObject<List<StudentModel>>(responseBody);

                return model;

            }
            catch (Exception e1)
            {

                return null;
            }
        }


        public async Task<StudentModel> GetByName()
        {
            try
            {
                StudentModel sendobj = new StudentModel();
                sendobj.FirstName = "Pratik";
                sendobj.LastName = "Sawant";
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendobj);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://21wfyylyve.execute-api.us-east-1.amazonaws.com"+BasePath),
                    Content = new StringContent(
                        jsonString,
                        Encoding.UTF8,
                        MediaTypeNames.Application.Json), // or "application/json" in older versions
                };

                var response = await _client.SendAsync(request)
                    .ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                StudentModel student = new StudentModel();
                student.FirstName = responseBody;
                return student;

            }
            catch (Exception e1)
            {

                return null;
            }
            
            //return await response.ReadContentAsync<List<StudentModel>>();
        }

        public async Task<StudentModel> AddStudent()
        {
            try
            {
                StudentModel sendobj = new StudentModel();
                sendobj.StudentId = "301";
                sendobj.FirstName = "Pratik";
                sendobj.LastName = "Sawant";
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendobj);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    //RequestUri = new Uri("https://21wfyylyve.execute-api.us-east-1.amazonaws.com/add-student"),
                    RequestUri = new Uri("https://27o6sho145.execute-api.us-east-1.amazonaws.com/add-student"),
                    Content = new StringContent(
                        jsonString,
                        Encoding.UTF8,
                        MediaTypeNames.Application.Json), // or "application/json" in older versions
                };

                var response = await _client.SendAsync(request)
                    .ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                StudentModel student = new StudentModel();
                student.FirstName = responseBody;
                return student;

            }
            catch (Exception e1)
            {

                return null;
            }

            //return await response.ReadContentAsync<List<StudentModel>>();
        }
    }
}
