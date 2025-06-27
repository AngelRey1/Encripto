namespace CesarEncriptador.Services
{
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
    }
} 