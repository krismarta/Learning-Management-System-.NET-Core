
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
    public class MyCourseRepository : GeneralRepository<MyCourse, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;


        public MyCourseRepository(Address address, string request = "MyCourses/") : base(address, request)
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
        public async Task<List<MyCourse>> getallCourse()
        {
            List<MyCourse> entities = new List<MyCourse>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<MyCourse>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<GetUserCourseVM>> GetUserCourse(string id)
        {
            List<GetUserCourseVM> entities = new List<GetUserCourseVM>();
            using (var response = await httpClient.GetAsync(request + "UserCourse/" + id))
            {

                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetUserCourseVM>>(apiResponse);

            }
            return entities;
        }



        public async Task<MyCourse> Get(int id)
        {
            MyCourse entity = null;
            using (var response = await httpClient.GetAsync(request + id))
            {

                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<MyCourse>(apiResponse);

            }
            return entity;
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
