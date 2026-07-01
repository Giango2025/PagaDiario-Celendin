using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Servicio que maneja la persistencia de datos en archivos de texto y binarios.
    /// </summary>
    public static class ArchivoService
    {
        // Nombres de archivos (se guardan en la carpeta bin/Debug)
        private static readonly string archClientesTxt = "clientes.txt";
        private static readonly string archPrestamosTxt = "prestamos.txt";
        private static readonly string archAhorrosTxt = "ahorros.txt";
        private static readonly string archPagosTxt = "pagos.txt";

        private static readonly string archClientesBin = "clientes.bin";
        private static readonly string archPrestamosBin = "prestamos.bin";
        private static readonly string archAhorrosBin = "ahorros.bin";
        private static readonly string archPagosBin = "pagos.bin";

        // Listas en memoria
        public static List<Cliente> ListaClientes { get; private set; } = new List<Cliente>();
        public static List<Prestamo> ListaPrestamos { get; private set; } = new List<Prestamo>();
        public static List<Ahorro> ListaAhorros { get; private set; } = new List<Ahorro>();
        public static List<PagoDiario> ListaPagos { get; private set; } = new List<PagoDiario>();

        /// <summary>
        /// Guarda los datos en ambos formatos (texto y binario).
        /// </summary>
        public static void GuardarDatos()
        {
            GuardarTexto();
            GuardarBinario();
            Utils.MostrarInfo("Datos guardados en formato dual (texto y binario).");
        }

        // ==================== GUARDADO EN TEXTO ====================
        private static void GuardarTexto()
        {
            try
            {
                using (var sw = new StreamWriter(archClientesTxt))
                    foreach (var c in ListaClientes)
                        sw.WriteLine($"{c.DNI}|{c.Nombres}|{c.Apellidos}|{c.Telefono}|{c.TieneAhorro}|{c.TienePrestamo}");

                using (var sw = new StreamWriter(archPrestamosTxt))
                    foreach (var p in ListaPrestamos)
                        sw.WriteLine($"{p.ID}|{p.DNICliente}|{p.Capital}|{p.Total}|{p.SaldoPendiente}|{p.CuotasPagadas}|{p.Garantia}|{p.URLFoto}");

                using (var sw = new StreamWriter(archAhorrosTxt))
                    foreach (var a in ListaAhorros)
                        sw.WriteLine($"{a.DNICliente}|{a.SaldoBase}|{a.Plan}|{a.SaldoTotal}|{a.InteresMensual}|{a.InteresAnual}|{a.FechaApertura}|{a.FechaVencimiento}");

                using (var sw = new StreamWriter(archPagosTxt))
                    foreach (var p in ListaPagos)
                        sw.WriteLine($"{p.DNICliente}|{p.PrestamoID}|{p.MontoPagado}|{p.FechaPago}|{p.NumeroCuota}");
            }
            catch (IOException ex)
            {
                Utils.MostrarError($"Error al guardar archivos de texto: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Utils.MostrarError($"Sin permisos para guardar archivos: {ex.Message}");
            }
            catch (Exception ex)
            {
                Utils.MostrarError($"Error inesperado al guardar texto: {ex.Message}");
            }
        }

        // ==================== GUARDADO EN BINARIO ====================
        private static void GuardarBinario()
        {
            try
            {
                var bf = new BinaryFormatter();
                using (var fs = new FileStream(archClientesBin, FileMode.Create)) bf.Serialize(fs, ListaClientes);
                using (var fs = new FileStream(archPrestamosBin, FileMode.Create)) bf.Serialize(fs, ListaPrestamos);
                using (var fs = new FileStream(archAhorrosBin, FileMode.Create)) bf.Serialize(fs, ListaAhorros);
                using (var fs = new FileStream(archPagosBin, FileMode.Create)) bf.Serialize(fs, ListaPagos);
            }
            catch (IOException ex)
            {
                Utils.MostrarError($"Error al guardar archivos binarios: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Utils.MostrarError($"Sin permisos para guardar binarios: {ex.Message}");
            }
            catch (Exception ex)
            {
                Utils.MostrarError($"Error inesperado al guardar binario: {ex.Message}");
            }
        }

        // ==================== CARGA DE DATOS ====================
        /// <summary>
        /// Carga los datos desde archivos (prioriza binarios si existen y son válidos).
        /// Si falla la carga binaria, intenta con texto.
        /// </summary>
        public static void CargarDatos()
        {
            // Intentar cargar desde binarios primero
            if (File.Exists(archClientesBin) && File.Exists(archPrestamosBin) &&
                File.Exists(archAhorrosBin) && File.Exists(archPagosBin))
            {
                try
                {
                    CargarBinario();
                    Utils.MostrarInfo("Datos cargados desde archivos binarios.");
                    return;
                }
                catch (SerializationException)
                {
                    Utils.MostrarError("Los archivos binarios están corruptos o desactualizados. Se eliminarán y se cargará desde texto.");
                    // Eliminar binarios corruptos
                    try
                    {
                        if (File.Exists(archClientesBin)) File.Delete(archClientesBin);
                        if (File.Exists(archPrestamosBin)) File.Delete(archPrestamosBin);
                        if (File.Exists(archAhorrosBin)) File.Delete(archAhorrosBin);
                        if (File.Exists(archPagosBin)) File.Delete(archPagosBin);
                        Utils.MostrarInfo("Archivos binarios corruptos eliminados.");
                    }
                    catch { /* ignorar errores de eliminación */ }
                }
                catch (Exception ex)
                {
                    Utils.MostrarError($"Error al cargar binarios: {ex.Message}. Intentando con texto...");
                }
            }

            // Si no hay binarios o fallaron, cargar desde texto
            if (File.Exists(archClientesTxt) && File.Exists(archPrestamosTxt) &&
                File.Exists(archAhorrosTxt) && File.Exists(archPagosTxt))
            {
                CargarTexto();
                Utils.MostrarInfo("Datos cargados desde archivos de texto.");
            }
            else
            {
                Utils.MostrarInfo("No se encontraron archivos de datos. Se iniciará con listas vacías.");
            }
        }

        // ==================== CARGA DESDE TEXTO ====================
        private static void CargarTexto()
        {
            try
            {
                // Clientes
                if (File.Exists(archClientesTxt))
                {
                    ListaClientes.Clear();
                    foreach (var linea in File.ReadAllLines(archClientesTxt))
                    {
                        var p = linea.Split('|');
                        if (p.Length >= 4)
                        {
                            var c = new Cliente(p[0], p[1], p[2], p[3]);
                            if (p.Length >= 6)
                            {
                                c.TieneAhorro = bool.Parse(p[4]);
                                c.TienePrestamo = bool.Parse(p[5]);
                            }
                            ListaClientes.Add(c);
                        }
                    }
                }

                // Préstamos
                if (File.Exists(archPrestamosTxt))
                {
                    ListaPrestamos.Clear();
                    foreach (var linea in File.ReadAllLines(archPrestamosTxt))
                    {
                        var p = linea.Split('|');
                        if (p.Length >= 6)
                        {
                            var pr = new Prestamo(int.Parse(p[0]), p[1], double.Parse(p[2]), double.Parse(p[3]), p[6], p[7]);
                            pr.SaldoPendiente = double.Parse(p[4]);
                            pr.CuotasPagadas = int.Parse(p[5]);
                            ListaPrestamos.Add(pr);
                        }
                    }
                }

                // Ahorros
                if (File.Exists(archAhorrosTxt))
                {
                    ListaAhorros.Clear();
                    foreach (var linea in File.ReadAllLines(archAhorrosTxt))
                    {
                        var p = linea.Split('|');
                        if (p.Length >= 8)
                        {
                            var a = new Ahorro(p[0], int.Parse(p[2]));
                            a.SaldoBase = double.Parse(p[1]);
                            a.SaldoTotal = double.Parse(p[3]);
                            a.InteresMensual = double.Parse(p[4]);
                            a.InteresAnual = double.Parse(p[5]);
                            a.FechaApertura = DateTime.Parse(p[6]);
                            a.FechaVencimiento = DateTime.Parse(p[7]);
                            ListaAhorros.Add(a);
                        }
                    }
                }

                // Pagos
                if (File.Exists(archPagosTxt))
                {
                    ListaPagos.Clear();
                    foreach (var linea in File.ReadAllLines(archPagosTxt))
                    {
                        var p = linea.Split('|');
                        if (p.Length >= 5)
                        {
                            var pg = new PagoDiario(p[0], int.Parse(p[1]), double.Parse(p[2]), int.Parse(p[4]));
                            ListaPagos.Add(pg);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Utils.MostrarInfo($"Archivo no encontrado (primera ejecución): {ex.Message}");
            }
            catch (IOException ex)
            {
                Utils.MostrarError($"Error al leer archivos de texto: {ex.Message}");
            }
            catch (Exception ex)
            {
                Utils.MostrarError($"Error inesperado al cargar texto: {ex.Message}");
            }
        }

        // ==================== CARGA DESDE BINARIO ====================
        private static void CargarBinario()
        {
            var bf = new BinaryFormatter();
            if (File.Exists(archClientesBin))
                using (var fs = new FileStream(archClientesBin, FileMode.Open))
                    ListaClientes = (List<Cliente>)bf.Deserialize(fs);
            if (File.Exists(archPrestamosBin))
                using (var fs = new FileStream(archPrestamosBin, FileMode.Open))
                    ListaPrestamos = (List<Prestamo>)bf.Deserialize(fs);
            if (File.Exists(archAhorrosBin))
                using (var fs = new FileStream(archAhorrosBin, FileMode.Open))
                    ListaAhorros = (List<Ahorro>)bf.Deserialize(fs);
            if (File.Exists(archPagosBin))
                using (var fs = new FileStream(archPagosBin, FileMode.Open))
                    ListaPagos = (List<PagoDiario>)bf.Deserialize(fs);
        }
    }
}