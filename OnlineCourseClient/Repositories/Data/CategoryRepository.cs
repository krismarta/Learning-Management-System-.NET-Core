
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineCourseAPI.Model;
using OnlineCourseClient.Base.Urls;
using OnlineCourseClient.Repositories;
using System;
using System.Net.Http;
using System.Text;

namespace OnlineCourseClient.Repositories.Data
{
    public class CategoryRepository : GeneralRepository<Category, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;


        public CategoryRepository(Address address, string request = "Categories/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }

    
    }
}