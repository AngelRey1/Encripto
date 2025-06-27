namespace CesarEncriptador.Services
{
    using Npgsql;
    using CesarEncriptador.Models;
    using System.Data;

    public class CesarService
    {
        public string Cifrar(string texto, int desplazamiento)
        {
            return new string(texto.ToLower().Select(c =>
            {
                if (c >= 'a' && c <= 'z')
                {
                    return (char)(((c - 'a' + desplazamiento) % 26) + 'a');
                }
                return c;
            }).ToArray());
        }

        public string Descifrar(string texto, int desplazamiento)
        {
            return new string(texto.ToLower().Select(c =>
            {
                if (c >= 'a' && c <= 'z')
                {
                    return (char)(((c - 'a' - desplazamiento + 26) % 26) + 'a');
                }
                return c;
            }).ToArray());
        }

        public async Task GuardarMensajeAsync(MessageLog log, string connectionString)
        {
            string tabla = log.Tipo == "encriptar" ? "encryptedmessages" : "decryptedmessages";
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand($@"INSERT INTO {tabla} (username, mensajeoriginal, mensajeprocesado, desplazamiento, fecha)
                                        VALUES (@username, @mensajeoriginal, @mensajeprocesado, @desplazamiento, @fecha)", conn);
            cmd.Parameters.AddWithValue("@username", (object?)log.Username ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mensajeoriginal", log.MensajeOriginal);
            cmd.Parameters.AddWithValue("@mensajeprocesado", log.MensajeProcesado);
            cmd.Parameters.AddWithValue("@desplazamiento", log.Desplazamiento);
            cmd.Parameters.AddWithValue("@fecha", log.Fecha);
            await cmd.ExecuteNonQueryAsync();
        }
    }
} 