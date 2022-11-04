﻿using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        public readonly IAppLogin _appLogin;
        public LoginController(IAppLogin appCliente)
        {
            this._appLogin = appCliente;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDto model)
        {

            var token = _appLogin.Login(model.Username, model.Password);

            if (token == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            return new
            {
                user = token[0].Item1,
                token = token[0].Item2
            };
        }

    }
}