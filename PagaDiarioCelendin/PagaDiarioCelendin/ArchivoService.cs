// ================================================================
// SERVICIO DE ARCHIVOS - Persistencia de datos en archivos
// ================================================================
// Este servicio maneja la carga y guardado de datos en archivos
// de texto (.txt) y binarios (.bin) para garantizar la persistencia
// de la información entre ejecuciones del programa.
// ================================================================

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Servicio estático para manejar la persistencia de datos.
    /// Proporciona métodos para cargar y guardar clientes y préstamos
    /// en archivos de texto y binarios.
    /// </summary>
    public static class ArchivoService
    {
        // ================================================================
        // DEFINICIÓN DE RUTAS DE ARCHIVOS
        // ================================================================

        // Archivos de texto (legibles por humanos)
        private static string archClientesTxt = "clientes.txt";
        private static string archPrestamosTxt = "prestamos.txt";

        // Archivos binarios (eficientes y seguros)
        private static string archClientesBin = "clientes.bin";
        private static string archPrestamosBin = "prestamos.bin";

        // ================================================================
        // LISTAS EN MEMORIA (Datos activos durante la ejecución)
        // ================================================================

        /// <summary>
        /// Lista global de clientes registrados en el sistema.
        /// </summary>
        public static List<Cliente> ListaClientes = new List<Cliente>();

        /// <summary>
        /// Lista global de préstamos registrados en el sistema.
        /// </summary>
        public static List<Prestamo> ListaPrestamos = new List<Prestamo>();

        // ================================================================
        // MÉTODOS PARA ARCHIVOS DE TEXTO (.txt)
        // ================================================================

        /// <summary>
        /// Carga los datos desde los archivos de texto (.txt).
        /// Si los archivos no existen, no hace nada.
        /// </summary>
        public static void CargarDatosTexto()
        {
            // === Cargar clientes desde archivo de texto ===
            if (File.Exists(archClientesTxt))
            {
                string[] lineas = File.ReadAllLines(archClientesTxt);
                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');
                    if (partes.Length == 4)
                    {
                        Cliente nuevoCliente = new Cliente(
                            partes[0].Trim(),
                            partes[1].Trim(),
                            partes[2].Trim(),
                            partes[3].Trim()
                        );
                        ListaClientes.Add(nuevoCliente);
                    }
                }
                Console.WriteLine($"[INFO] {ListaClientes.Count} clientes cargados desde {archClientesTxt}.");
            }

            // === Cargar préstamos desde archivo de texto ===
            if (File.Exists(archPrestamosTxt))
            {
                string[] lineas = File.ReadAllLines(archPrestamosTxt);
                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');
                    if (partes.Length == 6)
                    {
                        Prestamo nuevoPrestamo = new Prestamo(
                            int.Parse(partes[0].Trim()),
                            partes[1].Trim(),
                            double.Parse(partes[2].Trim()),
                            double.Parse(partes[3].Trim()),
                            partes[4].Trim(),
                            partes[5].Trim()
                        );
                        ListaPrestamos.Add(nuevoPrestamo);
                    }
                }
                Console.WriteLine($"[INFO] {ListaPrestamos.Count} préstamos cargados desde {archPrestamosTxt}.");
            }
        }

        /// <summary>
        /// Guarda los datos en los archivos de texto (.txt).
        /// Cada registro se guarda en una línea con campos separados por '|'.
        /// </summary>
        public static void GuardarDatosTexto()
        {
            // === Guardar clientes en archivo de texto ===
            List<string> lineasClientes = new List<string>();
            foreach (var c in ListaClientes)
            {
                lineasClientes.Add($"{c.DNI}|{c.Nombres}|{c.Apellidos}|{c.Telefono}");
            }
            File.WriteAllLines(archClientesTxt, lineasClientes);

            // === Guardar préstamos en archivo de texto ===
            List<string> lineasPrestamos = new List<string>();
            foreach (var p in ListaPrestamos)
            {
                lineasPrestamos.Add($"{p.ID}|{p.DNICliente}|{p.Capital}|{p.Total}|{p.Garantia}|{p.URLFoto}");
            }
            File.WriteAllLines(archPrestamosTxt, lineasPrestamos);

            Console.WriteLine($"[OK] Datos guardados en archivos de texto: {ListaClientes.Count} clientes, {ListaPrestamos.Count} préstamos.");
        }

        // ================================================================
        // MÉTODOS PARA ARCHIVOS BINARIOS (.bin)
        // ================================================================

        /// <summary>
        /// Guarda los datos en archivos binarios (.bin).
        /// El formato binario es más eficiente y seguro que el texto.
        /// </summary>
        public static void GuardarDatosBinario()
        {
            try
            {
                // === Guardar clientes en archivo binario ===
                using (FileStream fs = new FileStream(archClientesBin, FileMode.Create))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    // Escribir la cantidad de clientes
                    writer.Write(ListaClientes.Count);

                    // Escribir cada cliente
                    foreach (var c in ListaClientes)
                    {
                        writer.Write(c.DNI);
                        writer.Write(c.Nombres);
                        writer.Write(c.Apellidos);
                        writer.Write(c.Telefono);
                    }
                }

                // === Guardar préstamos en archivo binario ===
                using (FileStream fs = new FileStream(archPrestamosBin, FileMode.Create))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    // Escribir la cantidad de préstamos
                    writer.Write(ListaPrestamos.Count);

                    // Escribir cada préstamo
                    foreach (var p in ListaPrestamos)
                    {
                        writer.Write(p.ID);
                        writer.Write(p.DNICliente);
                        writer.Write(p.Capital);
                        writer.Write(p.Total);
                        writer.Write(p.Garantia);
                        writer.Write(p.URLFoto);
                    }
                }

                Console.WriteLine($"[OK] Datos guardados en archivos binarios: {ListaClientes.Count} clientes, {ListaPrestamos.Count} préstamos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Al guardar archivos binarios: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga los datos desde los archivos binarios (.bin).
        /// </summary>
        public static void CargarDatosBinario()
        {
            try
            {
                // === Cargar clientes desde archivo binario ===
                if (File.Exists(archClientesBin))
                {
                    using (FileStream fs = new FileStream(archClientesBin, FileMode.Open))
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int cantidad = reader.ReadInt32();
                        for (int i = 0; i < cantidad; i++)
                        {
                            string dni = reader.ReadString();
                            string nombres = reader.ReadString();
                            string apellidos = reader.ReadString();
                            string telefono = reader.ReadString();
                            ListaClientes.Add(new Cliente(dni, nombres, apellidos, telefono));
                        }
                        Console.WriteLine($"[INFO] {cantidad} clientes cargados desde {archClientesBin}.");
                    }
                }

                // === Cargar préstamos desde archivo binario ===
                if (File.Exists(archPrestamosBin))
                {
                    using (FileStream fs = new FileStream(archPrestamosBin, FileMode.Open))
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int cantidad = reader.ReadInt32();
                        for (int i = 0; i < cantidad; i++)
                        {
                            int id = reader.ReadInt32();
                            string dni = reader.ReadString();
                            double capital = reader.ReadDouble();
                            double total = reader.ReadDouble();
                            string garantia = reader.ReadString();
                            string url = reader.ReadString();
                            ListaPrestamos.Add(new Prestamo(id, dni, capital, total, garantia, url));
                        }
                        Console.WriteLine($"[INFO] {cantidad} préstamos cargados desde {archPrestamosBin}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Al cargar archivos binarios: {ex.Message}");
            }
        }

        // ================================================================
        // MÉTODOS PRINCIPALES (Unificados)
        // ================================================================

        /// <summary>
        /// Carga los datos desde archivos. Prioriza binarios si existen.
        /// </summary>
        public static void CargarDatos()
        {
            // Primero intentar cargar desde binarios (más rápidos)
            if (File.Exists(archClientesBin) && File.Exists(archPrestamosBin))
            {
                CargarDatosBinario();
                Console.WriteLine("[INFO] Datos cargados desde archivos binarios.");
            }
            else
            {
                CargarDatosTexto();
                Console.WriteLine("[INFO] Datos cargados desde archivos de texto.");
            }
        }

        /// <summary>
        /// Guarda los datos en archivos de texto y binarios simultáneamente.
        /// </summary>
        public static void GuardarDatos()
        {
            // Guardar en ambos formatos para mayor seguridad
            GuardarDatosTexto();
            GuardarDatosBinario();

            Console.WriteLine("[OK] Datos guardados en ambos formatos (texto y binario).");
        }
    }
}