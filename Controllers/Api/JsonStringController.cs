using AutoMapper;
using SimpleWebApi.CRUD;
using SimpleWebApi.DTO;
using SimpleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWebApi.Controllers.Api
{
    public class JsonStringController : ApiController
    {
        private CrudDAOJsonString crudDAO;

        public JsonStringController()
        {
            crudDAO = new CrudDAOJsonString();
        }

        //GET /api/JsonString
        public IEnumerable<JsonString> GetJsonString()
        {
            return crudDAO.ListarTodos();
        }
        //GET /api/JsonString/1
        public IHttpActionResult GetJsonString(int id)
        {
            var JsonString = crudDAO.ListarPorId(id);

            if (JsonString == null)
                return NotFound();

            return Ok(Mapper.Map<JsonString, JsonStringDto>(JsonString));

        }

        //POST /api/JsonString
        [HttpPost]
        public IHttpActionResult CreateJsonString(JsonString JsonString)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            JsonString.Id = int.Parse(crudDAO.InserirReturnId(JsonString));
            return Created(new Uri(Request.RequestUri + "/" + JsonString.Id), JsonString);
        }


        //GET /api/JsonStrings/produto/texto
        [HttpGet]
        public IHttpActionResult CreateJsonString(string projeto, string texto)
        {
            JsonString JsonString = new JsonString();
            JsonString.Projeto = projeto;
            JsonString.Texto = texto;
            if (!ModelState.IsValid)
                return BadRequest();

            JsonString.Id = int.Parse(crudDAO.InserirReturnId(JsonString));
            return Created(new Uri(Request.RequestUri + "/" + JsonString.Id), JsonString);
        }
        
        //PUT /api/JsonString/1
        [HttpPut]
        public IHttpActionResult UpdateJsonString(int id, JsonString JsonString)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            crudDAO.Salvar(JsonString);

            return Ok();
        }

        //PUT /api/JsonString/1
        [HttpPut]
        public IHttpActionResult UpdateJsonString(int id, string projeto, string texto)
        {
            JsonString JsonString = new JsonString();
            JsonString.Projeto = projeto;
            JsonString.Texto = texto;
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            crudDAO.Salvar(JsonString);

            return Ok();
        }

        //DELETE /api/JsonStrings/1
        [HttpDelete]
        public IHttpActionResult DeleteJsonString(int id)
        {
            crudDAO.Excluir(id);
            return Ok();
        }
    }
}
