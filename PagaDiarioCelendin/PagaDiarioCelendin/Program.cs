// ================================================================
// PROGRAMA PRINCIPAL - PAGA DIARIO CELENDÍN
// ================================================================
// Sistema de gestión financiera para microcréditos y ahorros
// Desarrollado para la financiera "Paga Diario" en Celendín, Cajamarca.
//
// Funcionalidades principales:
//   1. Registrar clientes con validación de DNI y teléfono
//   2. Aperturar cuentas de ahorro (montos: 20, 30, 40, 50)
//   3. Procesar préstamos con interés del 15% y 30 cuotas diarias
//   4. Mostrar reportes de clientes y préstamos
//   5. Simular 50 registros para pruebas
//   6. Guardar datos y salir
// ================================================================

using System;

namespace PagaDiarioCelendin
{
    class Program
    {
        /// <summary>
        /// Punto de entrada del programa.
        /// Inicializa los datos, muestra el menú principal y gestiona
        /// el flujo de la aplicación hasta que el usuario elige salir.
        /// </summary>
        static void Main(string[] args)
        {
            Console.Title = "Paga Diario - Celendín";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // ================================================================
            // INICIALIZACIÓN: Cargar datos desde archivos
            // ================================================================
            ArchivoService.CargarDatos();

            int opcion = 0;
            do
            {
                // ================================================================
                // MOSTRAR MENÚ PRINCIPAL
                // ================================================================
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========================================");
                Console.WriteLine("  💰 CORE BANCARIO - PAGA DIARIO");
                Console.WriteLine("         CELENDÍN - CAJAMARCA");
                Console.WriteLine("========================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. 📝 Registrar Cliente");
                Console.WriteLine("2. 🏦 Aperturar Ahorro");
                Console.WriteLine("3. 📊 Registrar Préstamo");
                Console.WriteLine("4. 📋 Mostrar Datos (Reportes)");
                Console.WriteLine("5. 🧪 Simular 50 Registros (Evidencia)");
                Console.WriteLine("6. 💾 Salir y Guardar");
                Console.WriteLine("========================================");
                Console.ResetColor();

                Console.Write("Seleccione una opción (1-6): ");

                // Validación de entrada del usuario (con TryParse)
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Ingrese un número válido.");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                // ================================================================
                // EJECUTAR OPCIÓN SELECCIONADA
                // ================================================================
                switch (opcion)
                {
                    case 1: NegocioService.RegistrarCliente(); break;
                    case 2: NegocioService.AperturarAhorro(); break;
                    case 3: NegocioService.ProcesarPrestamo(); break;
                    case 4: ReporteService.MostrarDatos(); break;
                    case 5: ReporteService.Simular50Registros(); break;
                    case 6:
                        ArchivoService.GuardarDatos();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Datos respaldados. Saliendo...");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[ERROR] Opción inválida. Intente de nuevo.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            } while (opcion != 6);
        }
    }
}