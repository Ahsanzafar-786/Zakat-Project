﻿using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetPaymentWiseTransection
    {

        public static PaymentWiseOpeningClosingModel GetPaymentWiseTransectionDtl(string benificaryId, string userId,string fromdate,string todate, string paymentType, string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Benificary/GetPaymentReportQuery?benificaryId=" + benificaryId+ "&userId="+userId+"&fromDate="+fromdate+"&toDate="+todate+ "&paymentType="+ paymentType);
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var Payment = JsonConvert.DeserializeObject<PaymentWiseOpeningClosingModel>(content1);

            return Payment;
        }
    }
}