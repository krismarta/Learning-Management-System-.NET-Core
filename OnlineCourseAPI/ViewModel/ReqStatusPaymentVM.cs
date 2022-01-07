namespace OnlineCourseAPI.ViewModel
{
    public class ReqStatusPaymentVM
    {
        public string Orderid { get; set;}
        public string Method { get; set; }
        public int Price { get; set; }
        public string VANumber { get; set; }
        public int Courseid { get; set; }
        public string Userid { get; set; }
        public string status { get; set; }

    }
}
