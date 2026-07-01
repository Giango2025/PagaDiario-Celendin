using System;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Programa principal del sistema Paga Diario.
    /// </summary>
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.WriteLine("    SISTEMA DE GESTIÓN FINANCIERA 'PAGA DIARIO'   ");
                Console.WriteLine("               CELENDÍN - CAJAMARCA               ");
                Console.WriteLine("==================================================");
                Console.ResetColor();

                Console.WriteLine(" 1. Registrar Nuevo Cliente");
                Console.WriteLine(" 2. Aperturar Cuenta de Ahorros");
                Console.WriteLine(" 3. Otorgar Préstamo (15% Interés)");
                Console.WriteLine(" 4. Registrar Pago Diario");
                Console.WriteLine(" 5. Ver Reporte General");
                Console.WriteLine(" 6. Clientes con Ahorro");
                Console.WriteLine(" 7. Clientes con Crédito");
                Console.WriteLine(" 8. Clientes con Ambos");
                Console.WriteLine(" 9. Estadísticas Financieras");
                Console.WriteLine("10. Buscar Cliente por Nombre"); // NUEVA OPCIÓN
                Console.WriteLine("11. Simular 100 Registros (Prueba)");
                Console.WriteLine("12. Salir");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("En cualquier momento, escriba 'REGRESAR' para");
                Console.WriteLine("cancelar la operación y volver al menú principal.");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción (1-12): ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Utils.MostrarError("Ingrese un número válido.");
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
                    case 10: NegocioService.BuscarClientePorNombre(); break; // NUEVA OPCIÓN
                    case 11: ReporteService.Simular100Registros(); break;
                    case 12:
                        ArchivoService.GuardarDatos();
                        Utils.MostrarExito("Datos guardados. Hasta luego.");
                        break;
                    default:
                        Utils.MostrarError("Opción inválida.");
                        System.Threading.Thread.Sleep(1500);
                        break;
                }

            } while (opcion != 12);
        }
    }
}