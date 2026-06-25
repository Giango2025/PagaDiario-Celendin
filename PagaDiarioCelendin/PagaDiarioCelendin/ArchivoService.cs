using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace PagaDiarioCelendin
{
    public static class ArchivoService
    {
        // DEFINICIÓN DE RUTAS DE ARCHIVOS
        private static string archClientesTxt = "clientes.txt";
        private static string archPrestamosTxt = "prestamos.txt";
        private static string archAhorrosTxt = "ahorros.txt"; // Nuevo

        private static string archClientesBin = "clientes.bin";
        private static string archPrestamosBin = "prestamos.bin";
        private static string archAhorrosBin = "ahorros.bin"; // Nuevo

        // LISTAS GLOBALES EN MEMORIA RAM
        public static List<Cliente> ListaClientes = new List<Cliente>();
        public static List<Prestamo> ListaPrestamos = new List<Prestamo>();
        public static List<Ahorro> ListaAhorros = new List<Ahorro>(); // Nuevo

        // PERSISTENCIA EN FORMATO TEXTO (.TXT)
        private static void GuardarDatosTexto()
        {
            try
            {
                // Guardar Clientes
                using (StreamWriter sw = new StreamWriter(archClientesTxt))
                {
                    foreach (Cliente c in ListaClientes)
                    {
                        sw.WriteLine($"{c.DNI}|{c.Nombres}|{c.Apellidos}|{c.Telefono}");
                    }
                }

                // Guardar Préstamos
                using (StreamWriter sw = new StreamWriter(archPrestamosTxt))
                {
                    foreach (Prestamo p in ListaPrestamos)
                    {
                        sw.WriteLine($"{p.ID}|{p.DNICliente}|{p.Capital}|{p.Total}|{p.Garantia}|{p.URLFoto}");
                    }
                }

                // Guardar Ahorros (Nuevo)
                using (StreamWriter sw = new StreamWriter(archAhorrosTxt))
                {
                    foreach (Ahorro a in ListaAhorros)
                    {
                        sw.WriteLine($"{a.DNICliente}|{a.MontoInicial}|{a.InteresGanado}|{a.SaldoTotal}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Al escribir archivos de texto: {ex.Message}");
            }
        }

        private static void CargarDatosTexto()
        {
            try
            {
                // Cargar Clientes
                if (File.Exists(archClientesTxt))
                {
                    ListaClientes.Clear();
                    string[] lineas = File.ReadAllLines(archClientesTxt);
                    foreach (string l in lineas)
                    {
                        string[] p = l.Split('|');
                        if (p.Length == 4) ListaClientes.Add(new Cliente(p[0], p[1], p[2], p[3]));
                    }
                }

                // Cargar Préstamos
                if (File.Exists(archPrestamosTxt))
                {
                    ListaPrestamos.Clear();
                    string[] lineas = File.ReadAllLines(archPrestamosTxt);
                    foreach (string l in lineas)
                    {
                        string[] p = l.Split('|');
                        if (p.Length == 6) ListaPrestamos.Add(new Prestamo(int.Parse(p[0]), p[1], double.Parse(p[2]), double.Parse(p[3]), p[4], p[5]));
                    }
                }

                // Cargar Ahorros (Nuevo)
                if (File.Exists(archAhorrosTxt))
                {
                    ListaAhorros.Clear();
                    string[] lineas = File.ReadAllLines(archAhorrosTxt);
                    foreach (string l in lineas)
                    {
                        string[] p = l.Split('|');
                        if (p.Length == 4) ListaAhorros.Add(new Ahorro(p[0], double.Parse(p[1]), double.Parse(p[2]), double.Parse(p[3])));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Al leer archivos de texto: {ex.Message}");
            }
        }

        // PERSISTENCIA EN FORMATO BINARIO (.BIN)
        private static void GuardarDatosBinario()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                using (FileStream fs = new FileStream(archClientesBin, FileMode.Create)) bf.Serialize(fs, ListaClientes);
                using (FileStream fs = new FileStream(archPrestamosBin, FileMode.Create)) bf.Serialize(fs, ListaPrestamos);
                using (FileStream fs = new FileStream(archAhorrosBin, FileMode.Create)) bf.Serialize(fs, ListaAhorros); // Nuevo
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Al serializar datos binarios: {ex.Message}");
            }
        }

        private static void CargarDatosBinario()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                if (File.Exists(archClientesBin))
                {
                    using (FileStream fs = new FileStream(archClientesBin, FileMode.Open)) ListaClientes = (List<Cliente>)bf.Deserialize(fs);
                }
                if (File.Exists(archPrestamosBin))
                {
                    using (FileStream fs = new FileStream(archPrestamosBin, FileMode.Open)) ListaPrestamos = (List<Prestamo>)bf.Deserialize(fs);
                }
                if (File.Exists(archAhorrosBin)) // Nuevo
                {
                    using (FileStream fs = new FileStream(archAhorrosBin, FileMode.Open)) ListaAhorros = (List<Ahorro>)bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Al deserializar datos binarios: {ex.Message}");
            }
        }

        // CONTROLADORES UNIFICADOS
        public static void CargarDatos()
        {
            if (File.Exists(archClientesBin) && File.Exists(archPrestamosBin) && File.Exists(archAhorrosBin))
            {
                CargarDatosBinario();
                Console.WriteLine("[INFO] Datos maestros y transaccionales cargados desde binarios.");
            }
            else
            {
                CargarDatosTexto();
                Console.WriteLine("[INFO] Datos consolidados cargados desde archivos planos de texto.");
            }
        }

        public static void GuardarDatos()
        {
            GuardarDatosTexto();
            GuardarDatosBinario();
            Console.WriteLine("[OK] Sincronización exitosa en almacenamiento dual (Texto y Binario).");
        }
    }
}