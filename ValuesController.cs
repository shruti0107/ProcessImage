using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProcessImage
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value" };
        }
        //GET api/values/imageurl
        public string Get(string imageUrl)
        {
            
            Console.WriteLine("Image Processing Started");
            System.Net.WebRequest request = System.Net.WebRequest.Create(imageUrl);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap bMap = new Bitmap(responseStream);
            string nearestColor = ImageProcessor.FindNearestColor(bMap);

            return "Nearest color to the image: "+nearestColor;
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
