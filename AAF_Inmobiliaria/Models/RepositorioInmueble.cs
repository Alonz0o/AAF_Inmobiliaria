using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AAF_Inmobiliaria.Models
{
    public class RepositorioInmueble
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;
        public RepositorioInmueble(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public int Alta(Inmueble i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inmuebles (Direccion, Ambientes, Superficie, Latitud, Longitud, propietarioId, EstaPublicado, EstaHabilitado) " +
                    $"VALUES (@direccion, @ambientes, @superficie, @latitud, @longitud, @propietarioId, 1, 1);" +
                    $"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@direccion", i.Direccion);
                    command.Parameters.AddWithValue("@ambientes", i.Ambientes);
                    command.Parameters.AddWithValue("@superficie", i.Superficie);
                    command.Parameters.AddWithValue("@latitud", i.Latitud);
                    command.Parameters.AddWithValue("@longitud", i.Longitud);
                    command.Parameters.AddWithValue("@propietarioId", i.PropietarioId);
                    command.Parameters.AddWithValue("@estaPublicado", i.EstaPublicado);
                    command.Parameters.AddWithValue("@estaHabilitado", i.EstaHabilitado);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.InmuebleId = res;
                    connection.Close();
                }
            }
            return res;
        }
        public IList<Inmueble> ObtenerTodosDelPropietario(int id)
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT InmuebleId, Direccion, Ambientes, Superficie, Latitud, Longitud, PropietarioId, EstaPublicado, EstaHabilitado" +
                    $" FROM Inmuebles" +
                    $" WHERE PropietarioId = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble i = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Superficie = reader.GetInt32(3),
                            Latitud = reader.GetString(4),
                            Longitud = reader.GetString(5),
                            PropietarioId = reader.GetInt32(6),
                            EstaPublicado = reader.GetBoolean(7),
                            EstaHabilitado = reader.GetBoolean(8),
                        };
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inmueble> ObtenerTodos()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT i.InmuebleId, Direccion, Ambientes, Superficie, Latitud, Longitud, Precio, i.PropietarioId, " +
                    "p.Nombre,p.Apellido " +
                    " FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.propietarioId " +
                    "ORDER BY Direccion ";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble i = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Superficie = reader.GetInt32(3),
                            Latitud = reader.GetString(4),
                            Longitud = reader.GetString(5),
                            Precio = reader.GetDecimal(6),
                            PropietarioId = reader.GetInt32(7),
                            Propietario = new Propietario
                            {
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                            
                            },
                        };
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inmueble> ObtenerInmueblePorDni(string dni)
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT InmuebleId, Direccion, Ambientes, Superficie, Latitud, Longitud, Precio, i.PropietarioId, " +
                    $" p.Nombre, p.Apellido, p.Dni, p.Telefono, p.Email" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.propietarioId" +
                    $" WHERE p.Dni = @dni";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble i = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Superficie = reader.GetInt32(3),
                            Latitud = reader.GetString(4),
                            Longitud = reader.GetString(5),
                            Precio = reader.GetDecimal(6),
                            PropietarioId = reader.GetInt32(7),
                            Propietario = new Propietario
                            {
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                                Dni = reader.GetString(10),
                                Telefono = reader.GetString(11),
                                Email = reader.GetString(12),
                            },
                        };
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }
    }
}
