using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Models;
using System.Runtime.CompilerServices;

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
               string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;

                if (token != "carlos123")
                {
                    return new
                    {
                        succes = false,
                        message = " Token incorrecto ",
                        result = ""

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
