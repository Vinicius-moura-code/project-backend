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
        public IActionResult Authenticate([FromBody] UserDto model)
        {

            var token = _appLogin.Login(model.Username, model.Password);

            if (token == null)
                return NotFound();// return NotFound(new { message = "Usuário ou senha inválidos" });

            return new ObjectResult(new
            {
                user = token[0].Item1,
                token = token[0].Item2
            });
        }

    }
}
