﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PrimeTech.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PrimeTech.Data
{
    public class PrimeTechDbContext : IdentityDbContext<ApplicationUser>
    {
        public PrimeTechDbContext(DbContextOptions<PrimeTechDbContext> options)
            : base(options){}
    }
}