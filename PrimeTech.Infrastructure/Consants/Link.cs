using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeTech.Infrastructure.Consants
{
    public static class Link
    {
       public const string EmailConfirmLink = "https://localhost:5001/api/auth/verify-email/userId={userId}&code={code}";
       public const string ResetPassword = "https://localhost:5001/api/auth/reset-password/userId={userId}&code={code}";
    }
}
