using System;
using System.IO;
using System.Collections.Generic;

namespace PagaDiarioCelendin
{
    public static class ArchivoService
    {
        private static string archClientes = "clientes.txt";
        private static string archPrestamos = "prestamos.txt";

        // Listas globales que guardan las Clases y que compartiras con tu equipo
        public static List<Cliente> ListaClientes = new List<Cliente>();
        public static List<Prestamo> ListaPrestamos = new List<Prestamo>();

        // FUNCIÓN 1: Cargar datos desde el archivo de texto
        public static void CargarDatos()
        {
            if (File.Exists(archClientes))
            {
                string[] lineas = File.ReadAllLines(archClientes);
                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');
                    if (partes.Length == 4)
                        ListaClientes.Add(new Cliente(partes[0], partes[1], partes[2], partes[3]));
                }
            }
            Console.WriteLine("[INFO] Datos cargados con exito.");
        }

        // FUNCIÓN 2: Guardar datos en el archivo de texto
        public static void GuardarDatos()
        {
            List<string> lineasClientes = new List<string>();
            foreach (var c in ListaClientes) lineasClientes.Add($"{c.DNI}|{c.Nombres}|{c.Apellidos}|{c.Telefono}");
            File.WriteAllLines(archClientes, lineasClientes);

            List<string> lineasPrestamos = new List<string>();
            foreach (var p in ListaPrestamos) lineasPrestamos.Add($"{p.ID}|{p.DNICliente}|{p.Capital}|{p.Total}|{p.Garantia}|{p.URLFoto}");
            File.WriteAllLines(archPrestamos, lineasPrestamos);

            Console.WriteLine("[INFO] Datos guardados en disco.");
        }
    }
}