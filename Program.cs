using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessImage
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            //string imageUrl = "";
            //imageUrl = "http://pngimg.com/uploads/donut/donut_PNG55.png";
            //imageUrl = "http://pngimg.com/uploads/google/google_PNG19625.png";
            //imageUrl = "http://pngimg.com/uploads/google/google_PNG19630.png";
            //"https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-teal.png";

            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter the image url.");
                return 1;
            }

            
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/values?imageUrl="+args[0]).Result;
                //added threading, wait time of 500 seconds
                var response = await client.GetAsync(baseAddress + "api/values?imageUrl=" + args[0], (new CancellationTokenSource(TimeSpan.FromSeconds(500))).Token);
                
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
            return 0;
        }
    }
}
