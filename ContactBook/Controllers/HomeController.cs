﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactBook.Models;

namespace ContactBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    //custom route for errors
    [Route("/Home/HandleError/{code:int}")]
    public IActionResult HandleError (int code)
    {
        var customError = new CustomError();

        customError.code = code;

        if(code == 404)
        {
            customError.message = "Page not found";
        }
        else
        {
            customError.message = "Sorry, something went wrong";
        }

        return View("~/Views/Shared/CustomError.cshtml", customError);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

