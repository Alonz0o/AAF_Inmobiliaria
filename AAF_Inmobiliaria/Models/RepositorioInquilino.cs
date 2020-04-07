using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AAF_Inmobiliaria.Models
{
    public class RepositorioInquilino
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioInquilino(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public int Alta(Inquilino i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email) " +
                    $"VALUES (@nombre, @apellido, @dni, @telefono, @email);" +
                    $"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@email", i.Email);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.InquilinoId = res;
                    connection.Close();
                }
            }
            return res;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Inquilinos WHERE InquilinoId = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public int Modificacion(Inquilino i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilinos SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email" +
                    $"WHERE InquilinoId = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@id", i.InquilinoId);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inquilino> ObtenerTodos()
        {
            IList<Inquilino> res = new List<Inquilino>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT InquilinoId, Nombre, Apellido, Dni, Telefono, Email, EstaPublicado, EstaHabilitado" +
                    $" FROM Inquilinos";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inquilino i = new Inquilino
                        {
                            InquilinoId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),                        
                        };
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Inquilino ObtenerPorId(int id)
        {
            Inquilino i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT InquilinoId, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos" +
                    $" WHERE InquilinoId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            InquilinoId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }

        public Inquilino ObtenerPorEmail(string email)
        {
            Inquilino i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT InquilinoId, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos" +
                    $" WHERE Email=@email";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            InquilinoId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }

        public IList<Inquilino> BuscarPorNombre(string nombre)
        {
            List<Inquilino> res = new List<Inquilino>();
            Inquilino i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT InquilinoId, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos" +
                    $" WHERE Nombre LIKE %@nombre% OR Apellido LIKE %@nombre";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        i = new Inquilino
                        {
                            InquilinoId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
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
