using System;

namespace PagaDiarioCelendin
{
    [Serializable]
    public class Cliente
    {
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public bool TieneAhorro { get; set; }
        public bool TienePrestamo { get; set; }

        public Cliente(string dni, string nombres, string apellidos, string telefono)
        {
            DNI = dni;
            Nombres = nombres;
            Apellidos = apellidos;
            Telefono = telefono;
            TieneAhorro = false;
            TienePrestamo = false;
        }
    }
}