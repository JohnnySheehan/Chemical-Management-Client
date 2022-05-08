using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Chemical_Management.Models;


namespace Chemical_Management_Client
{
    class Program
    {

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            try
            {
                
                client.BaseAddress = new Uri("https://chemical-management.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception)
            {
                Console.WriteLine("Failure: Please check base address. Could not create HTTP client with this address.");
                return;
            }

            GetAllReagents(client).Wait();

            GetAllLabs(client).Wait();

            GetAllSupplies(client).Wait();


        }

        //Get All Reagents from Web API
        private static async Task GetAllReagents(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync("api/reagents1");
            if (response.IsSuccessStatusCode)
                
            {
                var All = await response.Content.ReadAsAsync<IEnumerable<Reagent>>();
                Console.WriteLine("List of Reagents:");
                foreach (var reagent in All)
                {
                    Console.WriteLine("Reagent Name:" + reagent.ReagentName + " | Reagent Type: " + reagent.Type + " | Assigned ID: "+ reagent.ReagentID);
                }
                Console.WriteLine();
            }
        }
        private static async Task GetAllLabs(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync("api/labs1");
            if (response.IsSuccessStatusCode)

            {
                var All = await response.Content.ReadAsAsync<IEnumerable<Lab>>();
                Console.WriteLine("List of Labs:");
                foreach (var lab in All)
                {
                    Console.WriteLine("Lab Name:" + lab.LabName + " | Site Name: " + lab.LabSite);
                }
                Console.WriteLine();
            }
        }
        private static async Task GetAllSupplies(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync("api/supplies1");
            HttpResponseMessage response1 = await client.GetAsync("api/reagents1");
            if (response.IsSuccessStatusCode)

            {
                var All = await response.Content.ReadAsAsync<IEnumerable<Supply>>();
                var All1 = await response1.Content.ReadAsAsync<IEnumerable<Reagent>>();
                


                Console.WriteLine("List of Supplies:");
                foreach (var supply in All)
                {
                    foreach(var reagent in All1)
                    {
                        if(supply.ReagentId ==reagent.ReagentID)
                        {
                            Console.WriteLine("Reagent Name: " + reagent.ReagentName + " | Stock Level: " + supply.ReagentStock + " | Reagent ID: " + reagent.ReagentID);
                           
                        } 
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

