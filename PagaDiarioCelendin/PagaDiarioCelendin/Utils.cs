using System;
using System.Text.RegularExpressions;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Clase estática con utilidades para validaciones y mensajes.
    /// </summary>
    public static class Utils
    {
        // ========== MENSAJES CON COLOR ==========

        /// <summary>Muestra un mensaje de éxito en color verde.</summary>
        public static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[OK] {mensaje}");
            Console.ResetColor();
        }

        /// <summary>Muestra un mensaje de error en color rojo.</summary>
        public static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {mensaje}");
            Console.ResetColor();
        }

        /// <summary>Muestra un mensaje informativo en color cian.</summary>
        public static void MostrarInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] {mensaje}");
            Console.ResetColor();
        }

        /// <summary>Muestra un título en color amarillo.</summary>
        public static void MostrarTitulo(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(titulo);
            Console.ResetColor();
        }

        /// <summary>Espera que el usuario presione una tecla para continuar.</summary>
        public static void EsperarTecla()
        {
            Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        // ========== VALIDACIONES ==========

        /// <summary>Valida que el DNI tenga 8 dígitos y NO todos iguales.</summary>
        public static bool ValidarDNI(string dni)
        {
            if (string.IsNullOrEmpty(dni) || dni.Length != Constantes.LONGITUD_DNI)
                return false;
            if (!long.TryParse(dni, out _))
                return false;

            char primero = dni[0];
            foreach (char c in dni)
                if (c != primero)
                    return true;
            return false; // Todos iguales
        }

        /// <summary>Valida que el texto contenga solo letras y espacios.</summary>
        public static bool SoloLetras(string texto)
        {
            return !string.IsNullOrEmpty(texto) && Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
        }

        /// <summary>Valida que el teléfono tenga exactamente 9 dígitos.</summary>
        public static bool ValidarTelefono(string telefono)
        {
            return !string.IsNullOrEmpty(telefono) &&
                   telefono.Length == Constantes.LONGITUD_TELEFONO &&
                   long.TryParse(telefono, out _);
        }

        /// <summary>Valida que un monto sea positivo.</summary>
        public static bool ValidarMontoPositivo(double monto)
        {
            return monto > 0;
        }

        /// <summary>Valida que un plan de ahorro sea válido (20,30,40,50).</summary>
        public static bool ValidarPlanAhorro(int plan)
        {
            return Array.Exists(Constantes.PLANES_AHORRO, p => p == plan);
        }

        /// <summary>Lee una entrada del usuario con la opción de cancelar con "REGRESAR".</summary>
        public static string LeerConRegreso(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine().Trim();
            if (entrada.Equals(Constantes.MSJ_REGRESAR, StringComparison.OrdinalIgnoreCase))
                return Constantes.MSJ_REGRESAR;
            return entrada;
        }
    }
}