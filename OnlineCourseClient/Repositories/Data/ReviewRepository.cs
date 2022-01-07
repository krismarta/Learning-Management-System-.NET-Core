
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Base.Urls;
using OnlineCourseClient.Models;
using OnlineCourseClient.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineCourseClient.Repositories.Data
{
    public class ReviewRepository : GeneralRepository<ReviewCourse, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        


        public ReviewRepository(Address address, string request = "reviewcourses/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }


        public async Task<List<ReviewCourse>> getreview(int id)
        {
            List<ReviewCourse> entities = new List<ReviewCourse>();

            using (var response = await httpClient.GetAsync(request +"bycourse/"+id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ReviewCourse>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<ReviewCourse>> getreviewuser(int id,string userid)
        {
            List<ReviewCourse> entities = new List<ReviewCourse>();

            using (var response = await httpClient.GetAsync(request + "bycourse/" + id+"/"+userid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ReviewCourse>>(apiResponse);
            }
            return entities;
        }



    }
}