
using OnlineCourseAPI.Model;
using OnlineCourseClient.Base.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace OnlineCourseClient.Repositories.Data
{
    public class SubCourseRepository : GeneralRepository<SubCourse, int>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public SubCourseRepository(Address address, string request = "subcourses/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));

        }

        public async Task<List<SubCourse>> getbycourse(int id)
        {
            List<SubCourse> entities = new List<SubCourse>();

            using (var response = await httpClient.GetAsync(request + "bycourse/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<SubCourse>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<SubCourse>> getallSubCourse(int id)
        {
            List<SubCourse> entities = new List<SubCourse>();

            using (var response = await httpClient.GetAsync(request +"bycourse/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<SubCourse>>(apiResponse);
            }
            return entities;
        }




    }
}
