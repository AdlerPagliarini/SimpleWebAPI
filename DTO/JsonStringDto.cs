using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi.DTO
{
    public class JsonStringDto
    {
        public int Id { get; set; }
        public string Projeto { get; set; }
        public string Texto { get; set; }
    }
}