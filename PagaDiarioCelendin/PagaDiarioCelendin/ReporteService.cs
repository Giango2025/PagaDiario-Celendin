using System;
using System.Linq;
using System.Collections.Generic;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Servicio que genera todos los reportes del sistema.
    /// </summary>
    public static class ReporteService
    {
        /// <summary>Muestra el reporte general con todos los datos ordenados.</summary>
        public static void MostrarDatos()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- REPORTE GENERAL ---");

            // ========== CLIENTES (ordenados por DNI) ==========
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** CLIENTES ({ArchivoService.ListaClientes.Count}) ***");
            Console.ResetColor();

            var clientesOrdenados = ArchivoService.ListaClientes.OrderBy(c => c.DNI).ToList();
            foreach (var c in clientesOrdenados)
                Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono} | Ahorro: {(c.TieneAhorro ? "SI" : "NO")} | Crédito: {(c.TienePrestamo ? "SI" : "NO")}");

            // ========== PRÉSTAMOS (ordenados por saldo pendiente descendente) ==========
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** PRÉSTAMOS ACTIVOS ({ArchivoService.ListaPrestamos.Count}) ***");
            Console.ResetColor();

            var prestamosOrdenados = ArchivoService.ListaPrestamos.OrderByDescending(p => p.SaldoPendiente).ToList();
            foreach (var p in prestamosOrdenados)
                Console.WriteLine($"  ID: {p.ID} | Cliente: {p.DNICliente} | Capital: S/ {p.Capital:F1} | Saldo: S/ {p.SaldoPendiente:F1} | Pagadas: {p.CuotasPagadas}/{Constantes.CUOTAS_PRESTAMO}");

            // ========== AHORROS ==========
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** AHORROS ({ArchivoService.ListaAhorros.Count}) ***");
            Console.ResetColor();

            foreach (var a in ArchivoService.ListaAhorros)
                Console.WriteLine($"  Cliente: {a.DNICliente} | Saldo base: S/ {a.SaldoBase:F2} | Plan: S/ {a.Plan}.00 | Saldo total: S/ {a.SaldoTotal:F2} | Int. mensual: S/ {a.InteresMensual:F2} | Int. anual: S/ {a.InteresAnual:F2} | Vence: {a.FechaVencimiento:dd/MM/yyyy}");

            // ========== PAGOS ==========
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** PAGOS REGISTRADOS ({ArchivoService.ListaPagos.Count}) ***");
            Console.ResetColor();

            foreach (var p in ArchivoService.ListaPagos)
                Console.WriteLine($"  Cliente: {p.DNICliente} | Préstamo ID: {p.PrestamoID} | Monto: S/ {p.MontoPagado:F1} | Cuota #{p.NumeroCuota}");

            Utils.EsperarTecla();
        }

        /// <summary>Muestra solo los clientes que tienen cuenta de ahorro.</summary>
        public static void MostrarClientesAhorro()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- CLIENTES CON AHORRO ---");
            int count = 0;
            foreach (var c in ArchivoService.ListaClientes.OrderBy(c => c.DNI))
                if (c.TieneAhorro)
                {
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
                    count++;
                }
            Console.WriteLine($"\nTotal: {count} clientes con ahorro.");
            Utils.EsperarTecla();
        }

        /// <summary>Muestra solo los clientes con crédito activo.</summary>
        public static void MostrarClientesCredito()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- CLIENTES CON CRÉDITO ACTIVO ---");
            int count = 0;
            foreach (var c in ArchivoService.ListaClientes.OrderBy(c => c.DNI))
                if (c.TienePrestamo)
                {
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
                    count++;
                }
            Console.WriteLine($"\nTotal: {count} clientes con crédito activo.");
            Utils.EsperarTecla();
        }

        /// <summary>Muestra clientes que tienen tanto ahorro como crédito.</summary>
        public static void MostrarClientesAmbos()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- CLIENTES CON AHORRO Y CRÉDITO ---");
            int count = 0;
            foreach (var c in ArchivoService.ListaClientes.OrderBy(c => c.DNI))
                if (c.TieneAhorro && c.TienePrestamo)
                {
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos}");
                    count++;
                }
            Console.WriteLine($"\nTotal: {count} clientes en ambos servicios.");
            Utils.EsperarTecla();
        }

        /// <summary>Muestra estadísticas financieras resumidas.</summary>
        public static void MostrarEstadisticas()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- ESTADÍSTICAS FINANCIERAS ---");

            int totalClientes = ArchivoService.ListaClientes.Count;
            int conAhorro = ArchivoService.ListaClientes.Count(c => c.TieneAhorro);
            int conPrestamo = ArchivoService.ListaClientes.Count(c => c.TienePrestamo);
            int ambos = ArchivoService.ListaClientes.Count(c => c.TieneAhorro && c.TienePrestamo);

            double totalAhorros = ArchivoService.ListaAhorros.Sum(a => a.SaldoTotal);
            double totalPrestamos = ArchivoService.ListaPrestamos.Sum(p => p.SaldoPendiente);
            double totalPagado = ArchivoService.ListaPagos.Sum(p => p.MontoPagado);

            Console.WriteLine($"\n*** CLIENTES: {totalClientes}");
            Console.WriteLine($"  - Con Ahorro: {conAhorro}");
            Console.WriteLine($"  - Con Crédito: {conPrestamo}");
            Console.WriteLine($"  - Con Ambos: {ambos}");
            Console.WriteLine($"\n*** TOTAL AHORROS: S/ {totalAhorros:F2}");
            Console.WriteLine($"*** TOTAL PRÉSTAMOS PENDIENTES: S/ {totalPrestamos:F2}");
            Console.WriteLine($"*** TOTAL PAGADO: S/ {totalPagado:F2}");

            Utils.EsperarTecla();
        }

        /// <summary>Simula 100 registros de prueba (clientes, préstamos y ahorros).</summary>
        public static void Simular100Registros()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- SIMULADOR MASIVO (100 REGISTROS) ---");

            Random rnd = new Random();
            ArchivoService.ListaClientes.Clear();
            ArchivoService.ListaPrestamos.Clear();
            ArchivoService.ListaAhorros.Clear();
            ArchivoService.ListaPagos.Clear();

            for (int i = 1; i <= 100; i++)
            {
                string dni = (10000000 + i).ToString();
                var cliente = new Cliente(dni, $"Cliente_{i}", "Apellido", "9" + (100000000 + i).ToString().Substring(1, 8));
                ArchivoService.ListaClientes.Add(cliente);

                if (i % 2 == 0)
                {
                    int plan = Constantes.PLANES_AHORRO[rnd.Next(Constantes.PLANES_AHORRO.Length)];
                    var ahorro = new Ahorro(dni, plan);
                    ArchivoService.ListaAhorros.Add(ahorro);
                    cliente.TieneAhorro = true;
                }
                else
                {
                    double capital = rnd.Next(200, 4500);
                    double total = Math.Round(capital * (1 + Constantes.TASA_INTERES_PRESTAMO_ANUAL), 1);
                    var prestamo = new Prestamo(Constantes.BASE_ID_PRESTAMO + i, dni, capital, total, "Garantía", "http://foto.com");
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