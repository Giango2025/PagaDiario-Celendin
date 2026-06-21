namespace PagaDiarioCelendin
{
    public class Cliente
    {
        // Atributos de la clase
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }

        // Constructor
        public Cliente(string dni, string nombres, string apellidos, string telefono)
        {
            DNI = dni; Nombres = nombres; Apellidos = apellidos; Telefono = telefono;
        }
    }
}