using System;

namespace PagaDiarioCelendin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Paga Diario - Celendín";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Inicializamos la carga de datos
            ArchivoService.CargarDatos();

            int opcion = 0;
            do
            {
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

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Ingrese un número válido.");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

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