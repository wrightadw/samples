using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HostedClientCertificateSample.Models;

namespace HostedClientCertificateSample.Controllers
{
    public class SampleController : ApiController
    {
        private static ConcurrentQueue<SampleItem> sampleItems = new ConcurrentQueue<SampleItem>();

        /// <summary>
        /// Gets all the sample Items. It does not require client to send client certificate. 
        /// </summary>
        public IEnumerable<SampleItem> GetItems()
        {
            return sampleItems.ToArray();
        }

        /// <summary>
        /// Gets an sample Item with the given id. It does not require client to send client certificate. 
        /// </summary>
        /// <param name="id">The id that is part of the route</param>
        public SampleItem GetItem(int id)
        {
            return sampleItems.FirstOrDefault((item) => item.Id == id);
        }

        /// <summary>
        /// Post a new sample item to the list. This requires authentication with client certificate and it 
        /// needs to be in the "administrator" role. 
        /// </summary>
        /// <param name="item">The sample item coming from the request body</param>
        /// <returns>Returns the newly added sample item's string value</returns>
        [Authorize(Roles = "Administrators")]
        public string PostItem(SampleItem item)
        {
            sampleItems.Enqueue(item);
            return item == null ? "null" : item.StringValue;
        }
    }
}
