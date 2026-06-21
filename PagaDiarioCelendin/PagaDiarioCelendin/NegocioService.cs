using System;

namespace PagaDiarioCelendin
{
    // CLASE PRINCIPAL DE NEGOCIO
    public static class NegocioService
    {
        // ============================================================
        // FUNCIÓN 1: REGISTRAR CLIENTE
        // ============================================================
        public static void RegistrarCliente()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE CLIENTE ===");

            string dni = "";
            bool valido = false;
            while (!valido)
            {
                Console.Write("DNI (8 digitos): ");
                dni = Console.ReadLine().Trim();
                if (dni.Length == 8) valido = true;
                else Console.WriteLine("[ERROR] DNI invalido (debe tener 8 digitos).");
            }

            Console.Write("Nombres: ");
            string nombres = Console.ReadLine().Trim();
            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine().Trim();

            string telefono = "";
            valido = false;
            while (!valido)
            {
                Console.Write("Telefono (9 digitos): ");
                telefono = Console.ReadLine().Trim();
                if (telefono.Length == 9) valido = true;
                else Console.WriteLine("[ERROR] Telefono invalido (debe tener 9 digitos).");
            }

            // CREACIÓN DEL OBJETO E INSERCIÓN EN LA LISTA GLOBAL
            Cliente nuevoCliente = new Cliente(dni, nombres, apellidos, telefono);
            ArchivoService.ListaClientes.Add(nuevoCliente);

            Console.WriteLine($"\n[OK] Cliente registrado correctamente por la clase NegocioService.");
            Console.ReadKey();
        }

        // ============================================================
        // FUNCIÓN 2: APERTURAR AHORRO
        // ============================================================
        public static void AperturarAhorro()
        {
            Console.Clear();
            Console.WriteLine("=== APERTURA DE AHORRO ===");
            Console.WriteLine("Montos permitidos: 20, 30, 40, 50");
            Console.Write("Ingrese monto: S/ ");

            if (!double.TryParse(Console.ReadLine(), out double monto))
            {
                Console.WriteLine("[ERROR] Monto invalido.");
                Console.ReadKey();
                return;
            }

            if (monto == 20 || monto == 30 || monto == 40 || monto == 50)
            {
                double interes = monto * 0.15;
                double saldo = monto + interes;
                Console.WriteLine($"\nDeposito: S/ {monto:F2}");
                Console.WriteLine($"Interes (15%): S/ {interes:F2}");
                Console.WriteLine($"Saldo Final Calc: S/ {saldo:F2}");
            }
            else
            {
                Console.WriteLine("[ERROR] Monto no permitido por las politicas de la financiera.");
            }
            Console.ReadKey();
        }

        // ============================================================
        // FUNCIÓN 3: PROCESAR PRÉSTAMO (Con redondeo a 1 decimal)
        // ============================================================
        public static void ProcesarPrestamo()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE PRESTAMO ===");
            Console.Write("DNI del cliente: ");
            string dni = Console.ReadLine().Trim();

            // Buscar si el objeto Cliente existe en la lista de ArchivoService
            Cliente clienteEncontrado = ArchivoService.ListaClientes.Find(c => c.DNI == dni);

            if (clienteEncontrado == null)
            {
                Console.WriteLine("[ERROR] El cliente no existe. Registrelo primero.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Cliente verificado: {clienteEncontrado.Nombres} {clienteEncontrado.Apellidos}");
            Console.Write("Capital solicitado: S/ ");
            if (!double.TryParse(Console.ReadLine(), out double capital) || capital <= 0)
            {
                Console.WriteLine("[ERROR] Capital debe ser mayor a cero.");
                Console.ReadKey();
                return;
            }

            Console.Write("Descripcion de la garantia: ");
            string garantia = Console.ReadLine().Trim();
            Console.Write("URL de la foto de la garantia: ");
            string url = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(garantia) || string.IsNullOrEmpty(url))
            {
                Console.WriteLine("[ERROR] Garantia incompleta. Prestamo rechazado.");
                Console.ReadKey();
                return;
            }

            // Cálculos con redondeo a 1 decimal
            double total = capital * 1.15;
            double cuota = total / 30;
            double cuotaRedondeada = Math.Round(cuota, 1);
            double totalRedondeado = Math.Round(total, 1);

            // Generar ID autoincremental basado en el conteo de la lista
            int nuevoID = 1000 + ArchivoService.ListaPrestamos.Count + 1;

            // CREACIÓN DEL OBJETO PRÉSTAMO EN LA LISTA GLOBAL
            Prestamo nuevoPrestamo = new Prestamo(nuevoID, dni, capital, totalRedondeado, garantia, url);
            ArchivoService.ListaPrestamos.Add(nuevoPrestamo);

            Console.WriteLine("\n=== CRONOGRAMA DE PAGO (30 cuotas diarias) ===");
            for (int i = 1; i <= 30; i++)
            {
                Console.WriteLine($"Cuota {i}: S/ {cuotaRedondeada:F1} [Pendiente]");
            }
            Console.WriteLine($"\nTotal a pagar: S/ {totalRedondeado:F1}");
            Console.WriteLine($"Prestamo guardado con ID generado: {nuevoID}");
            Console.ReadKey();
        }
    }
}