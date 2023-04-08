namespace NetCoreApi.Models
{
    public class Usuario
    {
        public string id { get; set; }
        public string usuario { get; set; }

        public string password { get; set; }
        public string rol { get; set; }

        public static List<Usuario> ListUsuarioDBSimulator()
        {
            var list = new List<Usuario>()
            {
                new Usuario
                {
                    id = "1",
                    usuario = "Carlos",
                    password = "password123",
                    rol = "ADMIN"

                },

                new Usuario
                {
                    id = "2",
                    usuario = "Juan",
                    password = "password123",
                    rol = "USER"
                },
                new Usuario
                {
                    id = "3",
                    usuario = "Mateo",
                    password = "password123",
                    rol = "USER"
                }

            };
            return list;
        }
    }
}
