﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OverviewApplication.Services
{
    public interface ITypedHttpClient
    {
        HttpClient GetClient();
    }
}
