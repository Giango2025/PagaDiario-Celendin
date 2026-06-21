```csharp
using System;

namespace PagaDiarioCelendin
{
    // CLASE PRINCIPAL DE REPORTES Y PRUEBAS
    public static class ReporteService
    {
        // ============================================================
        // FUNCIÓN 1: MOSTRAR DATOS (Recorriendo las listas con foreach)
        // ============================================================
        public static void MostrarDatos()
        {
            Console.Clear();
            Console.WriteLine($"===== CLIENTES REGISTRADOS ({ArchivoService.ListaClientes.Count}) =====");
            
            if (ArchivoService.ListaClientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados en el sistema.");
            }
            else
            {
                foreach (Cliente c in ArchivoService.ListaClientes)
                {
                    Console.WriteLine($"DNI: {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono}");
                }
            }

            Console.WriteLine($"\n===== PRÉSTAMOS REGISTRADOS ({ArchivoService.ListaPrestamos.Count}) =====");
            if (ArchivoService.ListaPrestamos.Count == 0)
            {
                Console.WriteLine("No hay prestamos registrados en el sistema.");
            }
            else
            {
                foreach (Prestamo p in ArchivoService.ListaPrestamos)
                {
                    Console.WriteLine($"ID: {p.ID} | DNI Cliente: {p.DNICliente} | Capital: S/{p.Capital:F1} | Total con Int: S/{p.Total:F1}");
                }
            }
            Console.ReadKey();
        }

        // ============================================================
        // FUNCIÓN 2: SIMULAR 50 REGISTROS (Generador automático para la evidencia)
        // ============================================================
        public static void Simular50Registros()
        {
            Console.Clear();
            Console.WriteLine("Generando simulacion de 50 registros por clases...");
            Random rnd = new Random();

            for (int i = 1; i <= 50; i++)
            {
                string dniFalso = (10000000 + i).ToString();
                string nombreFalso = "Cliente" + i;
                string apellidoFalso = "Prueba";
                string telefonoFalso = (900000000 + i).ToString();

                Cliente nuevoC = new Cliente(dniFalso, nombreFalso, apellidoFalso, telefonoFalso);
                ArchivoService.ListaClientes.Add(nuevoC);

                int idPrestamo = 1000 + ArchivoService.ListaPrestamos.Count + 1;
                double capitalAleatorio = rnd.Next(100, 5000);
                double totalCalculado = Math.Round(capitalAleatorio * 1.15, 1); // Redondeo a 1 decimal
                string garantiaFalsa = "Garantia " + i;
                string urlFalsa = "http://fotos.com/garantia" + i;

                Prestamo nuevoP = new Prestamo(idPrestamo, dniFalso, capitalAleatorio, totalCalculado, garantiaFalsa, urlFalsa);
                ArchivoService.ListaPrestamos.Add(nuevoP);
            }

            ArchivoService.GuardarDatos();
            Console.WriteLine("\n[OK] ¡50 registros simulados con exito y guardados en archivos!");
            Console.ReadKey();
        }
    }
}
