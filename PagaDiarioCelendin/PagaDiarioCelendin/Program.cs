// ================================================================
// PROGRAMA PRINCIPAL - PAGA DIARIO CELENDÍN
// ================================================================
// Sistema de gestión financiera para microcréditos y ahorros
// Desarrollado para la financiera "Paga Diario" en Celendín, Cajamarca.
// ================================================================

using System;

namespace PagaDiarioCelendin
{
    class Program
    {
        /// <summary>
        /// Punto de entrada del programa.
        /// Inicializa los datos, muestra el menú principal y gestiona
        /// el flujo de la aplicación de manera estructurada.
        /// </summary>
        static void Main(string[] args)
        {
            // Configuración estética de la consola
            Console.Title = "Paga Diario - Celendín (Core Bancario v2.0)";

            // ================================================================
            // INICIALIZACIÓN DE DATOS (Persistencia Avanzada)
            // ================================================================
            ArchivoService.CargarDatos();
            System.Threading.Thread.Sleep(1500);

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
                Console.WriteLine(" 2. Aperturar Cuenta de Ahorros (Monto Fijo)");
                Console.WriteLine(" 3. Procesar y Otorgar Préstamo (15% Interés)");
                Console.WriteLine(" 4. Ver Reportes Generales (Clientes, Créditos y Ahorros)");
                Console.WriteLine(" 5. Ejecutar Simulador Masivo (Inyección de 100 Registros)");
                Console.WriteLine(" 6. Sincronizar Almacenamiento Dual y Salir");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.ResetColor();
                Console.Write("Seleccione una opción del menú (1-6): ");

                // Validación avanzada de entrada de menú para evitar caídas por excepciones
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[ERROR] Por favor, ingrese un número válido entre 1 y 6.");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                // ================================================================
                // ENRUTADOR DE OPERACIONES (Reglas de Negocio y Reportes)
                // ================================================================
                switch (opcion)
                {
                    case 1:
                        // Llama al registro de Clientes con validaciones
                        NegocioService.RegistrarCliente();
                        break;
                    case 2:
                        // Llama al método persistente y validado de Ahorros
                        NegocioService.AperturarAhorro();
                        break;
                    case 3:
                        // Llama al generador de cronogramas y créditos
                        NegocioService.ProcesarPrestamo();
                        break;
                    case 4:
                        // Muestra las tres colecciones unificadas
                        ReporteService.MostrarDatos();
                        break;
                    case 5:
                        // Llama al inyector de estrés escalado a 100 registros
                        ReporteService.Simular50Registros();
                        break;
                    case 6:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Iniciando proceso de cierre seguro...");
                        Console.ResetColor();

                        // Sincroniza las listas de RAM en archivos planos (.txt) y serializados (.bin)
                        ArchivoService.GuardarDatos();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[ÉXITO] Datos respaldados correctamente. Saliendo de la aplicación.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1500);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[ERROR] Opción fuera de rango. Seleccione de 1 a 6.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1500);
                        break;
                }

            } while (opcion != 6);
        }
    }
}