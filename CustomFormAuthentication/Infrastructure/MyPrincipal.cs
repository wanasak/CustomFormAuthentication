using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CustomFormAuthentication.Infrastructure
{
    public class MyPrincipal : IPrincipal
    {
        private readonly MyIdentity _MyIdentity;

        public MyPrincipal(MyIdentity myIdentity)
        {
            _MyIdentity = myIdentity;
        }

        public IIdentity Identity
        {
            get { return _MyIdentity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}
