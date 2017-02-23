using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Requests
{
    [Serializable]
    public class SignUpRequest
    {
        [RegularExpression(@"^[A-Za-z0-9\\-]{5,16}$")]
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{7,16}$")]
        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"Username: {Username} Password: {Password} DateOfBirth: {DateOfBirth}";
        }
    }
}
