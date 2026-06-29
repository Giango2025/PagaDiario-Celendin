using System;

namespace PagaDiarioCelendin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Paga Diario - Celendin";
            ArchivoService.CargarDatos();

            int opcion = 0;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.WriteLine("    SISTEMA DE GESTION FINANCIERA 'PAGA DIARIO'   ");
                Console.WriteLine("               CELENDIN - CAJAMARCA               ");
                Console.WriteLine("==================================================");
                Console.ResetColor();

                Console.WriteLine(" 1. Registrar Nuevo Cliente");
                Console.WriteLine(" 2. Aperturar Cuenta de Ahorros");
                Console.WriteLine(" 3. Otorgar Prestamo (15% Interes)");
                Console.WriteLine(" 4. Registrar Pago Diario");
                Console.WriteLine(" 5. Ver Reporte General");
                Console.WriteLine(" 6. Clientes con Ahorro");
                Console.WriteLine(" 7. Clientes con Credito");
                Console.WriteLine(" 8. Clientes con Ambos");
                Console.WriteLine(" 9. Estadisticas Financieras");
                Console.WriteLine("10. Simular 100 Registros (Prueba)");
                Console.WriteLine("11. Salir");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("En cualquier momento, escriba 'REGRESAR' para");
                Console.WriteLine("cancelar la operacion y volver al menu principal.");
                Console.ResetColor();

                Console.Write("\nSeleccione una opcion (1-11): ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Ingrese un numero valido.");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                switch (opcion)
                {
                    case 1: NegocioService.RegistrarCliente(); break;
                    case 2: NegocioService.AperturarAhorro(); break;
                    case 3: NegocioService.ProcesarPrestamo(); break;
                    case 4: NegocioService.RegistrarPagoDiario(); break;
                    case 5: ReporteService.MostrarDatos(); break;
                    case 6: ReporteService.MostrarClientesAhorro(); break;
                    case 7: ReporteService.MostrarClientesCredito(); break;
                    case 8: ReporteService.MostrarClientesAmbos(); break;
                    case 9: ReporteService.MostrarEstadisticas(); break;
                    case 10: ReporteService.Simular100Registros(); break;
                    case 11:
                        ArchivoService.GuardarDatos();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n*** Datos guardados. Hasta luego. ***");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Opcion invalida.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1500);
                        break;
                }

            } while (opcion != 11);
        }
    }
}