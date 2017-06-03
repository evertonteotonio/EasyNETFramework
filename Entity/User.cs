using System;
using Dapper.Contrib.Extensions;
using System.Text;

namespace EFN.Entity
{
    public class User
    {
        [Key]
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? ProfileId { get; set; }

        public bool IsDeleted { get; set; }
        public string sha256(string password)
        {
           /* System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();*/
            return "";
        }
    }
}
