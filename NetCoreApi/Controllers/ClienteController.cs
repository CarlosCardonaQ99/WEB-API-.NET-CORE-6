using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Models;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace NetCoreApi.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            List<Cliente> clientes = new List<Cliente>
            {
            new Cliente
            {
            id = "1",
            name = "carlos",
            edad = "20",
            correo = "carlos@gmail.com"
            },
             new Cliente
            {
            id = "2",
            name = "pepito",
            edad = "24",
            correo = "pepito@gmail.com"
            }
            };
            return clientes;
        }

        [HttpGet]
        [Route("listarPorId")]
        public dynamic listarClientePorId(string _id)
        {

            return new Cliente
            {
                id = _id.ToString(),
                name = "Bernardo peña",
                edad = "80",
                correo = "porid@gmail.com"
            };
        }

        [HttpPost]
        [Route("/guardar")]
        public dynamic guardar(Cliente cliente)
        {
            cliente.id = "3";

            return new
            {
                succes = true,
                message = "cliente registrado",
                result = cliente

            };
        }

            [HttpPost]
            [Route("eliminar")]
            public dynamic eliminarCliente(Cliente cliente)
            {

            var identity = HttpContext.User.Identity as ClaimsIdentity; // 

            var rToken = Jwt.validarToken(identity);

            if (!rToken.success)
                return rToken;

            Usuario usuario = rToken.result;

            if(usuario.rol != "ADMIN")
            {
                return new
                {
                    success = false,
                    message = "No tienes permisos para eliminar clientes",
                    values = ""
                };
            }

                return new
                {
                    succes = true,
                    message = "cliente eliminado ",
                    result = cliente

                };

            }
        }
    }
