using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PagaDiarioCelendin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Paga Diario - Celendín";
            ArchivoService.CargarDatos();

            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("  CORE BANCARIO - PAGA DIARIO (CELENDIN)");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar Cliente");
                Console.WriteLine("2. Aperturar Ahorro");
                Console.WriteLine("3. Registrar Prestamo");
                Console.WriteLine("4. Mostrar Datos (Reportes)");
                Console.WriteLine("5. Simular 50 Registros (Evidencia)");
                Console.WriteLine("6. Salir y Guardar");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion (1-6): ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1: NegocioService.RegistrarCliente(); break;
                    case 2: NegocioService.AperturarAhorro(); break;
                    case 3: NegocioService.ProcesarPrestamo(); break;
                    case 4: ReporteService.MostrarDatos(); break;
                    case 5: ReporteService.Simular50Registros(); break;
                    case 6: 
                        ArchivoService.GuardarDatos();
                        Console.WriteLine("\n[OK] Datos respaldados. Saliendo...");
                        break;
                }
            } while (opcion != 6);
        }
    }
}