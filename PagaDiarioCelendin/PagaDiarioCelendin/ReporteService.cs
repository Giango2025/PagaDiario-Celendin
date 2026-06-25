using System;

namespace PagaDiarioCelendin
{
    public static class ReporteService
    {
        // EXPOSITOR VISUAL DE REPORTES
        public static void MostrarDatos()
        {
            Console.Clear();

            // 1. Mostrar Clientes
            Console.WriteLine($"===== MAESTRO DE CLIENTES ({ArchivoService.ListaClientes.Count}) =====");
            if (ArchivoService.ListaClientes.Count == 0) Console.WriteLine("Colección vacía.");
            else
            {
                foreach (Cliente c in ArchivoService.ListaClientes)
                {
                    Console.WriteLine($"DNI: {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono}");
                }
            }

            // 2. Mostrar Préstamos
            Console.WriteLine($"\n===== CARTERA DE PRÉSTAMOS OTORGADOS ({ArchivoService.ListaPrestamos.Count}) =====");
            if (ArchivoService.ListaPrestamos.Count == 0) Console.WriteLine("Colección vacía.");
            else
            {
                foreach (Prestamo p in ArchivoService.ListaPrestamos)
                {
                    Console.WriteLine($"ID: {p.ID} | DNI Ref: {p.DNICliente} | Capital: S/ {p.Capital} | Total: S/ {p.Total}");
                }
            }

            // 3. Mostrar Ahorros (Nuevo)
            Console.WriteLine($"\n===== PORTAFOLIO DE CUENTAS DE AHORRO ({ArchivoService.ListaAhorros.Count}) =====");
            if (ArchivoService.ListaAhorros.Count == 0) Console.WriteLine("Colección vacía.");
            else
            {
                foreach (Ahorro a in ArchivoService.ListaAhorros)
                {
                    Console.WriteLine($"Cliente DNI: {a.DNICliente} | Capitalizado: S/ {a.MontoInicial} | Rendimiento: S/ {a.InteresGanado} | Saldo: S/ {a.SaldoTotal}");
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para regresar al menú principal...");
            Console.ReadKey();
        }

        // MOTOR DE SIMULACIÓN AVANZADA (Actualizado a 100 registros cruzados)
        public static void Simular50Registros()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== INICIANDO MOTOR DE PRUEBAS DE ESTRÉS MASIVO ===");
            Console.WriteLine("Generando e indexando 100 registros relacionales en paralelo...");
            Console.ResetColor();

            Random rnd = new Random();

            // Limpieza transaccional previa para evitar colisiones de redundancia
            ArchivoService.ListaClientes.Clear();
            ArchivoService.ListaPrestamos.Clear();
            ArchivoService.ListaAhorros.Clear();

            for (int i = 1; i <= 100; i++) // Escalado a 100 para sobrepasar el mínimo (>=50)
            {
                string dniFalso = (10000000 + i).ToString();

                // 1. Inyección de Clientes
                Cliente nuevoC = new Cliente(dniFalso, $"Asesorado_{i}", "Celendín", "9" + (100000000 + i).ToString().Substring(1, 8));
                ArchivoService.ListaClientes.Add(nuevoC);

                // 2. Inyección de Créditos vinculados
                int idPrestamo = 1000 + i;
                double capitalAleatorio = rnd.Next(200, 4500);
                double totalCalculado = Math.Round(capitalAleatorio * 1.15, 1);
                Prestamo nuevoP = new Prestamo(idPrestamo, dniFalso, capitalAleatorio, totalCalculado, $"Prenda Tipo Electrónico #{i}", $"http://cloudfinanciera.pe/garantias/img_{i}.png");
                ArchivoService.ListaPrestamos.Add(nuevoP);

                // 3. Inyección de Ahorros vinculados (Nuevo)
                double[] montosDisponibles = { 20, 30, 40, 50 };
                double montoAhorro = montosDisponibles[rnd.Next(0, montosDisponibles.Length)];
                double interesCalculado = Math.Round(montoAhorro * 0.02, 2);
                Ahorro nuevoA = new Ahorro(dniFalso, montoAhorro, interesCalculado, montoAhorro + interesCalculado);
                ArchivoService.ListaAhorros.Add(nuevoA);
            }

            // Forzar guardado inmediato en disco duro (Formato dual plano y binario)
            ArchivoService.GuardarDatos();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[ÉXITO] 100 registros maestros y transaccionales creados con integridad relacional.");
            Console.WriteLine("Los archivos binarios (.bin) y planos (.txt) fueron actualizados correctamente.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}