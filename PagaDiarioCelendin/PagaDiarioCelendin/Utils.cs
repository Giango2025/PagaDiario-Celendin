using System;
using System.Text.RegularExpressions;

namespace PagaDiarioCelendin
{
    public static class Utils
    {
        public static bool ValidarDNI(string dni)
        {
            if (dni.Length != 8 || !long.TryParse(dni, out _)) return false;
            char primero = dni[0];
            foreach (char c in dni)
                if (c != primero) return true;
            return false;
        }

        public static bool SoloLetras(string texto)
        {
            return !string.IsNullOrEmpty(texto) && Regex.IsMatch(texto, @"^[a-zA-Z·ÈÌÛ˙¡…Õ”⁄Ò—\s]+$");
        }

        public static string LeerConRegreso(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine().Trim();
            if (entrada.Equals("REGRESAR", StringComparison.OrdinalIgnoreCase))
                return "REGRESAR";
            return entrada;
        }

        public static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[OK] {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarTitulo(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"--- {titulo} ---");
            Console.ResetColor();
        }

        public static void EsperarTecla()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}