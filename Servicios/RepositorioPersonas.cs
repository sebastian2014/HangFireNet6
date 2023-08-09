using Dapper;
using Microsoft.Data.SqlClient;

namespace HangFireNet6.Servicios
{

    public interface IRepositorioPersonas
    {
        Task Crear(string nombrePersona);
        Task ActualizarNombre(Persona persona);
    }

    public class RepositorioPersonas: IRepositorioPersonas
    {
        private readonly ILogger<RepositorioPersonas> logger;
        private readonly string connectionString;

        public RepositorioPersonas(IConfiguration configuration, ILogger<RepositorioPersonas> logger)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.logger = logger;
        }
        public async Task Crear(string nombrePersona)
        {
            // logger.LogInformation($"agregando a la persona {nombrePersona} desde el servicio");
            //await Task.Delay(5000);
            var persona = new Persona { Nombre = nombrePersona, Fecha = DateTime.Now };
          
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Personas (Nombre,Fecha)
                    VALUES (@Nombre,@Fecha);
                    SELECT SCOPE_IDENTITY();", persona);

            persona.Id = id;
            logger.LogInformation($"agregada la persona {nombrePersona} desde el servicio");
        }

        public async Task ActualizarNombre(Persona persona)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Persona
                                            SET Nombre = @Nombre
                                            WHERE Id = @Id", persona);
        }

    }
}


