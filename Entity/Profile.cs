using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Entity
{
    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// <description>Created By: Amr Swalha</description>
    /// </item>
    /// <item>
    /// <description>Created At: 03-05-2017</description>
    /// </item>
    /// <item>
    /// <description>Modified At: 03-05-2017</description>
    /// </item>
    /// </list>
    /// </summary>
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
