﻿using ENF.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ENF.RESTFul.Controllers
{
    public class BaseController : Controller
    {
        public Context DbContext;
        public BaseController()
        {
            DbContext = new Context();
        }
    }
}
