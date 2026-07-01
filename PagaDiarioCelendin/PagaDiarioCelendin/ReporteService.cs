using System;
using System.Linq;
using System.Collections.Generic;

namespace PagaDiarioCelendin
{
    public static class ReporteService
    {
        public static void MostrarDatos()
        {
            Utils.MostrarTitulo("REPORTE GENERAL");

            // Ordenar clientes por DNI
            var clientesOrdenados = ArchivoService.ListaClientes.OrderBy(c => c.DNI).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** CLIENTES ({clientesOrdenados.Count}) ***");
            Console.ResetColor();
            foreach (var c in clientesOrdenados)
                Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono} | Ahorro: {(c.TieneAhorro ? "SI" : "NO")} | Crédito: {(c.TienePrestamo ? "SI" : "NO")}");

            // Ordenar préstamos por saldo pendiente (mayor a menor)
            var prestamosOrdenados = ArchivoService.ListaPrestamos.OrderByDescending(p => p.SaldoPendiente).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** PRÉSTAMOS ACTIVOS ({prestamosOrdenados.Count}) ***");
            Console.ResetColor();
            foreach (var p in prestamosOrdenados)
                Console.WriteLine($"  ID: {p.ID} | Cliente: {p.DNICliente} | Capital: S/ {p.Capital:F1} | Saldo: S/ {p.SaldoPendiente:F1} | Pagadas: {p.CuotasPagadas}/30");

            // Ahorros (ordenados por saldo)
            var ahorrosOrdenados = ArchivoService.ListaAhorros.OrderByDescending(a => a.SaldoTotal).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** AHORROS ({ahorrosOrdenados.Count}) ***");
            Console.ResetColor();
            foreach (var a in ahorrosOrdenados)
                Console.WriteLine($"  Cliente: {a.DNICliente} | Saldo base: S/ {a.SaldoBase:F2} | Plan: S/ {a.Plan}.00 | Saldo total: S/ {a.SaldoTotal:F2} | Interés mensual: S/ {a.InteresMensual:F2} | Interés anual: S/ {a.InteresAnual:F2} | Vence: {a.FechaVencimiento:dd/MM/yyyy}");

            // Pagos (ordenados por fecha)
            var pagosOrdenados = ArchivoService.ListaPagos.OrderBy(p => p.FechaPago).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** PAGOS REGISTRADOS ({pagosOrdenados.Count}) ***");
            Console.ResetColor();
            foreach (var p in pagosOrdenados)
                Console.WriteLine($"  Cliente: {p.DNICliente} | Préstamo ID: {p.PrestamoID} | Monto: S/ {p.MontoPagado:F1} | Cuota #{p.NumeroCuota} | Fecha: {p.FechaPago:dd/MM/yyyy}");

            Utils.EsperarTecla();
        }

        public static void MostrarClientesAhorro()
        {
            Utils.MostrarTitulo("CLIENTES CON AHORRO");
            var lista = ArchivoService.ListaClientes.Where(c => c.TieneAhorro).OrderBy(c => c.DNI).ToList();
            foreach (var c in lista)
                Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
            Console.WriteLine($"\nTotal: {lista.Count} clientes con ahorro.");
            Utils.EsperarTecla();
        }

        public static void MostrarClientesCredito()
        {
            Utils.MostrarTitulo("CLIENTES CON CRÉDITO ACTIVO");
            var lista = ArchivoService.ListaClientes.Where(c => c.TienePrestamo).OrderBy(c => c.DNI).ToList();
            foreach (var c in lista)
                Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
            Console.WriteLine($"\nTotal: {lista.Count} clientes con crédito activo.");
            Utils.EsperarTecla();
        }

        public static void MostrarClientesAmbos()
        {
            Utils.MostrarTitulo("CLIENTES CON AHORRO Y CRÉDITO");
            var lista = ArchivoService.ListaClientes.Where(c => c.TieneAhorro && c.TienePrestamo).OrderBy(c => c.DNI).ToList();
            foreach (var c in lista)
                Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
            Console.WriteLine($"\nTotal: {lista.Count} clientes en ambos servicios.");
            Utils.EsperarTecla();
        }

        public static void MostrarEstadisticas()
        {
            Utils.MostrarTitulo("ESTADÍSTICAS FINANCIERAS");

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
            Console.WriteLine($"  - Con Crédito: {conPrestamo}");
            Console.WriteLine($"  - Con Ambos: {ambos}");
            Console.WriteLine($"\n*** TOTAL AHORROS: S/ {totalAhorros:F2}");
            Console.WriteLine($"*** TOTAL PRÉSTAMOS PENDIENTES: S/ {totalPrestamos:F2}");
            Console.WriteLine($"*** TOTAL PAGADO: S/ {totalPagado:F2}");
            Utils.EsperarTecla();
        }

        public static void Simular100Registros()
        {
            Utils.MostrarTitulo("SIMULADOR MASIVO (100 REGISTROS)");

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

                if (i % 2 == 0) // Ahorro
                {
                    int plan = planes[rnd.Next(0, planes.Length)];
                    var ahorro = new Ahorro(dni, plan);
                    ArchivoService.ListaAhorros.Add(ahorro);
                    cliente.TieneAhorro = true;
                }
                else // Préstamo
                {
                    double capital = rnd.Next(200, 4500);
                    double total = Math.Round(capital * 1.15, 1);
                    var prestamo = new Prestamo(1000 + i, dni, capital, total, "Garantía", "http://foto.com");
                    ArchivoService.ListaPrestamos.Add(prestamo);
                    cliente.TienePrestamo = true;
                }
            }

            ArchivoService.GuardarDatos();
            Utils.MostrarExito("100 registros simulados creados exitosamente.");
            Utils.EsperarTecla();
        }
    }
}