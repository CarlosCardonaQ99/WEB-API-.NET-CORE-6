using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NetCoreApi.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace NetCoreApi.Controllers
{

    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public dynamic login([FromBody] Object obtenerData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(obtenerData.ToString());

            string user = data.usuario.ToString();
            string password = data.password.ToString();

            Usuario usuario = Usuario.ListUsuarioDBSimulator()
                .Where(x => x.usuario == user && x.password == password).FirstOrDefault();

            if(usuario == null)
            {
                return new
                {
                    success = false,
                    message = "El usuario no existe o las credenciales son incorrectas",
                    result = ""
                };
            }
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>(); //CON ESTO ESTAMOS OBTENIENDO LO DE appsetting.json, pero mapeado dentro de la clase Jwt creada con los mismos keys

            //AQUÍ ES DÓNDE EXPLICAMOS TODO LO QUE VA A ENCAPSULAR NUESTRO TOKEN 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim ("id", usuario.id),
                new Claim ("usuario", usuario.usuario)

            };

            //La contraseña (Key) que se encuentra en la clase Jwt se transforma a bytes y se encripta mediante el método de SymmetricSecurityKey

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));

            //CREAMOS EL INICIO DE SESIÓN.
            // EN SigningCredentials pasamos la Key (clave secreta) y usamos el algoritmo de seguridad HASH 256)
            var sigIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //CREAMOS EL TOKEN, EXPIRA EN 2 MINUTOS
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: sigIn
                );
            //SI TODO SALE BIEN, SE RETORNA UN NUEVO OBJETO DE TIPO: 
            // Y SE ENVÍA EL TOKEN EN LA VARIABLE RESULT
            return new
            {
                success = true,
                message = "éxito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
