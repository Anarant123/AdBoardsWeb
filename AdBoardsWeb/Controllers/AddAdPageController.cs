﻿using AdBoardsWeb.DTO;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System;

namespace AdBoardsWeb.Controllers
{
    public class AddAdPageController : Controller
    {
        private readonly ILogger<AddAdPageController> _logger;

        public AddAdPageController(ILogger<AddAdPageController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> AddAd(string Name, string Description, string Price, string Cotegory, string City, string buyOrSell)
        {

            AdDTO adDTO = new AdDTO();
            adDTO.Name = Name;
            adDTO.City = City;
            adDTO.Date = DateTime.Now;
            adDTO.CotegorysId = Convert.ToInt32(Cotegory);
            adDTO.Description = Description;
            adDTO.Price = Convert.ToInt32(Price);
            adDTO.TypeOfAdId = Convert.ToInt32(buyOrSell);
            adDTO.PersonId = Context.UserNow.Id;
            adDTO.Photo = System.IO.File.ReadAllBytes(@"C:\Users\misha\Downloads\xcv.jpg");

            var httpClient = new HttpClient();
            using StringContent jsonContent = new(JsonSerializer.Serialize(adDTO), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5228/Ads/Addition", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Ad a = JsonSerializer.Deserialize<Ad>(jsonResponse)!;
                Context.AdNow = a;
                return View("~/Views/Home/AdPage.cshtml", a);
            }
            else
            {
                return View("~/Views/Home/AddAdPage.cshtml", adDTO); ;
            }

        }
    }
}