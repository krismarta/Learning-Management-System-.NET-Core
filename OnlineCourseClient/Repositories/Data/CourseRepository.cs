
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineCourseAPI.Model;
using OnlineCourseClient.Base.Urls;
using OnlineCourseClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using OnlineCourseAPI.ViewModel;

namespace OnlineCourseClient.Repositories.Data
{
    public class CourseRepository : GeneralRepository<Course, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;


        public CourseRepository(Address address, string request = "Courses/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));

        }
        public async Task<List<Course>> getallCourse()
        {
            List<Course> entities = new List<Course>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
            }
            return entities;
        }


        public async Task<List<MidtransVM>> getPayment()
        {
            List<MidtransVM> entities = new List<MidtransVM>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<MidtransVM>>(apiResponse);
            }
            return entities;
        }

        //public async Task<List<SubCourse>> getallSubCourse()
        //{
        //    List<SubCourse> entities = new List<SubCourse>();

        //    using (var response = await httpClient.GetAsync(request))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<List<ubCourse>>(apiResponse);
        //    }
        //    return entities;
        //}




    }
}
