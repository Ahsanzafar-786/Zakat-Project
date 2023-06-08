using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetCharityLedger
    {

        public static List<CharityTransactionLookupModel> GetCharityLedgerDtl(string id,string month,string fromDate,string toDate,string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Benificary/GetCharityTransactionList?benificaryId="+id + "&month="+month +"&fromDate="+fromDate + "&toDate=" +toDate);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var   companyDto = JsonConvert.DeserializeObject<List<CharityTransactionLookupModel>>(content1);

            return companyDto;
        }
    }
}