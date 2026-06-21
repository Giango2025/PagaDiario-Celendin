using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagaDiarioCelendin

{
    class Program
    {
        // ESTRUCTURAS Y GLOBALES (No se mueven de aquí)
        struct Cliente { public string DNI; public string Nombres; public string Apellidos; public string Telefono; }
        struct Prestamo { public int ID; public string DNICliente; public double Capital; public double Total; public string Garantia; public string URLFoto; }

        static Cliente[] clientes = new Cliente[101];
        static Prestamo[] prestamos = new Prestamo[101];
        static int contClientes = 0, contPrestamos = 0;
        static string archClientes = "clientes.txt";
        static string archPrestamos = "prestamos.txt";

        static void Main(string[] args)
        {
            // ALUMNO A: Aquí irá tu menú interactivo
        }

        static void CargarDatos()
        {
            // ALUMNO A: Aquí va tu lógica de lectura de archivos
        }

        static void GuardarDatos()
        {
            // ALUMNO A: Aquí va tu lógica de escritura de archivos
        }

        static void RegistrarCliente()
        {
            // ALUMNO B: Aquí vas a programar el registro de clientes
        }

        static void AperturarAhorro()
        {
            // ALUMNO B: Aquí vas a programar la apertura de ahorros
        }

        static void ProcesarPrestamo()
        {
            // ALUMNO B: Aquí vas a programar el flujo de préstamos
        }

        static void MostrarDatos()
        {
            // ALUMNO C: Aquí vas a programar los listados en pantalla
        }

        static void Simular50Registros()
        {
            // ALUMNO C: Aquí vas a programar el generador de pruebas
        }
    }
}