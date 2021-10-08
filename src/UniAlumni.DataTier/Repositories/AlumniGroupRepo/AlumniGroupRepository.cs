﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.AlumniGroupRepo
{
    public class AlumniGroupRepository : BaseRepository<Models.AlumniGroup>, IAlumniGroupRepository
    {
        public AlumniGroupRepository(DbContext context) : base(context)
        {
        }
    }
}
