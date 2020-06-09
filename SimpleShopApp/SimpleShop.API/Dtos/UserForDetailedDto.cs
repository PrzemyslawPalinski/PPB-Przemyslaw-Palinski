using System;
using System.Collections.Generic;
using SimpleShop.API.Models;

namespace SimpleShop.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string KnownAs { get; set; }
    }
}