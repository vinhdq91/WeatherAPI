﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public interface IWebApiCall
    {
        public Task<WeatherModel> CallApi(string query);
    }
}
