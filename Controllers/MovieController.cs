﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class MovieController : ControllerBase
    {
        public MovieController()
        {
        }

        [HttpPost]
        public IAsyncResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IAsyncResult Delete()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IAsyncResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IAsyncResult Update()
        {
            throw new NotImplementedException();
        }
    }
}