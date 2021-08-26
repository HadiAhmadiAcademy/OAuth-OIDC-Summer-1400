using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFlowRaw.Models
{
    public class LoginCallbackViewModel
    {
        public UserInfoResponse UserInfo { get; set; }
        public TokenResponse Token { get; set; }
    }
}
