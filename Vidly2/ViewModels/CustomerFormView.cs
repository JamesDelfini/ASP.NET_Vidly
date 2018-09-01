using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly2.Models;
namespace Vidly2.ViewModels
{
    public class CustomerFormView
    {
        /* Difference between using IEnumerable and List<> is using for 
         * read-only data, only use when iterating a collection of data.
         * */
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}