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
            ArchivoService.CargarDatos(); // Llama a tu clase de archivos

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
                Console.WriteLine("4. Mostrar Datos");
                Console.WriteLine("5. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion (1-5): ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1: /* Se llamara a la clase de Alumno B */ break;
                    case 2: /* Se llamara a la clase de Alumno B */ break;
                    case 3: /* Se llamara a la clase de Alumno B */ break;
                    case 4: /* Se llamara a la clase de Alumno C */ break;
                    case 5: ArchivoService.GuardarDatos(); break;
                }
            } while (opcion != 5);
        }
    }
}