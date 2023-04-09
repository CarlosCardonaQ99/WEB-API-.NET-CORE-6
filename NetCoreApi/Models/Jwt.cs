using System.Security.Claims;

namespace NetCoreApi.Models
{
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estás enviando un token válido ",
                        value = ""

                    }; 

                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                Usuario usuario = Usuario.ListUsuarioDBSimulator().FirstOrDefault(x => x.id == id);

                return new
                {
                    success = true,
                    message = "Éxito",
                    values = usuario
                };

                
            }catch (Exception ex){
                return new
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    value = ""
                };
            
            }
        }

    }
}
