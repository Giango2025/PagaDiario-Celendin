using System;

namespace PagaDiarioCelendin
{
    public static class ReporteService
    {
        public static void MostrarDatos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- REPORTE GENERAL ---");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** CLIENTES ({ArchivoService.ListaClientes.Count}) ***");
            Console.ResetColor();
            foreach (var c in ArchivoService.ListaClientes)
                Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono} | Ahorro: {(c.TieneAhorro ? "SI" : "NO")} | Credito: {(c.TienePrestamo ? "SI" : "NO")}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** PRESTAMOS ACTIVOS ({ArchivoService.ListaPrestamos.Count}) ***");
            Console.ResetColor();
            foreach (var p in ArchivoService.ListaPrestamos)
                Console.WriteLine($"  ID: {p.ID} | Cliente: {p.DNICliente} | Capital: S/ {p.Capital:F1} | Saldo: S/ {p.SaldoPendiente:F1} | Pagadas: {p.CuotasPagadas}/30");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** AHORROS ({ArchivoService.ListaAhorros.Count}) ***");
            Console.ResetColor();
            foreach (var a in ArchivoService.ListaAhorros)
                Console.WriteLine($"  Cliente: {a.DNICliente} | Saldo base: S/ {a.SaldoBase:F2} | Plan: S/ {a.Plan}.00 | Saldo total: S/ {a.SaldoTotal:F2} | Interes mensual: S/ {a.InteresMensual:F2} | Interes anual: S/ {a.InteresAnual:F2} | Vence: {a.FechaVencimiento:dd/MM/yyyy}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** PAGOS REGISTRADOS ({ArchivoService.ListaPagos.Count}) ***");
            Console.ResetColor();
            foreach (var p in ArchivoService.ListaPagos)
                Console.WriteLine($"  Cliente: {p.DNICliente} | Prestamo ID: {p.PrestamoID} | Monto: S/ {p.MontoPagado:F1} | Cuota #{p.NumeroCuota}");

            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        public static void MostrarClientesAhorro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- CLIENTES CON AHORRO ---");
            Console.ResetColor();
            int count = 0;
            foreach (var c in ArchivoService.ListaClientes)
                if (c.TieneAhorro)
                {
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
                    count++;
                }
            Console.WriteLine($"\nTotal: {count} clientes con ahorro.");
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        public static void MostrarClientesCredito()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- CLIENTES CON CREDITO ACTIVO ---");
            Console.ResetColor();
            int count = 0;
            foreach (var c in ArchivoService.ListaClientes)
                if (c.TienePrestamo)
                {
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
                    count++;
                }
            Console.WriteLine($"\nTotal: {count} clientes con credito activo.");
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        public static void MostrarClientesAmbos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- CLIENTES CON AHORRO Y CREDITO ---");
            Console.ResetColor();
            int count = 0;
            foreach (var c in ArchivoService.ListaClientes)
                if (c.TieneAhorro && c.TienePrestamo)
                {
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
                    count++;
                }
            Console.WriteLine($"\nTotal: {count} clientes en ambos servicios.");
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        public static void MostrarEstadisticas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- ESTADISTICAS FINANCIERAS ---");
            Console.ResetColor();

            int totalClientes = ArchivoService.ListaClientes.Count;
            int conAhorro = 0, conPrestamo = 0, ambos = 0;
            double totalAhorros = 0, totalPrestamos = 0, totalPagado = 0;

            foreach (var c in ArchivoService.ListaClientes)
            {
                if (c.TieneAhorro) conAhorro++;
                if (c.TienePrestamo) conPrestamo++;
                if (c.TieneAhorro && c.TienePrestamo) ambos++;
            }

            foreach (var a in ArchivoService.ListaAhorros) totalAhorros += a.SaldoTotal;
            foreach (var p in ArchivoService.ListaPrestamos) totalPrestamos += p.SaldoPendiente;
            foreach (var p in ArchivoService.ListaPagos) totalPagado += p.MontoPagado;

            Console.WriteLine($"\n*** CLIENTES: {totalClientes}");
            Console.WriteLine($"  - Con Ahorro: {conAhorro}");
            Console.WriteLine($"  - Con Credito: {conPrestamo}");
            Console.WriteLine($"  - Con Ambos: {ambos}");
            Console.WriteLine($"\n*** TOTAL AHORROS: S/ {totalAhorros:F2}");
            Console.WriteLine($"*** TOTAL PRESTAMOS PENDIENTES: S/ {totalPrestamos:F2}");
            Console.WriteLine($"*** TOTAL PAGADO: S/ {totalPagado:F2}");

            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        public static void Simular100Registros()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- SIMULADOR MASIVO (100 REGISTROS) ---");
            Console.ResetColor();

            Random rnd = new Random();
            ArchivoService.ListaClientes.Clear();
            ArchivoService.ListaPrestamos.Clear();
            ArchivoService.ListaAhorros.Clear();
            ArchivoService.ListaPagos.Clear();

            int[] planes = { 20, 30, 40, 50 };

            for (int i = 1; i <= 100; i++)
            {
                string dni = (10000000 + i).ToString();
                var cliente = new Cliente(dni, $"Cliente_{i}", "Apellido", "9" + (100000000 + i).ToString().Substring(1, 8));
                ArchivoService.ListaClientes.Add(cliente);

                if (i % 2 == 0)
                {
                    int plan = planes[rnd.Next(0, planes.Length)];
                    var ahorro = new Ahorro(dni, plan);
                    ArchivoService.ListaAhorros.Add(ahorro);
                    cliente.TieneAhorro = true;
                }
                else
                {
                    double capital = rnd.Next(200, 4500);
                    double total = Math.Round(capital * 1.15, 1);
                    var prestamo = new Prestamo(1000 + i, dni, capital, total, "Garantia", "http://foto.com");
                    ArchivoService.ListaPrestamos.Add(prestamo);
                    cliente.TienePrestamo = true;
                }
            }

            ArchivoService.GuardarDatos();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** 100 registros simulados creados exitosamente. ***");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }
    }
}