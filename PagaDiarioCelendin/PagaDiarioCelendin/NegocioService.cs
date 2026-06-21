using System;

namespace PagaDiarioCelendin
{
    public static class NegocioService
    {
        // ============================================================
        // FUNCIÓN 1: REGISTRAR CLIENTE (CON VALIDACIONES EXTREMAS)
        // ============================================================
        public static void RegistrarCliente()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== REGISTRO DE CLIENTE ===");
            Console.ResetColor();

            // Validación DNI (8 dígitos, solo números)
            string dni = "";
            bool valido = false;
            while (!valido)
            {
                Console.Write("DNI (8 digitos): ");
                dni = Console.ReadLine().Trim();

                // Validar que sea exactamente 8 caracteres y todos sean números
                if (dni.Length == 8 && long.TryParse(dni, out _))
                {
                    valido = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] DNI inválido (debe tener 8 dígitos numéricos).");
                    Console.ResetColor();
                }
            }

            Console.Write("Nombres: ");
            string nombres = Console.ReadLine().Trim();

            // Validar que no esté vacío
            while (string.IsNullOrWhiteSpace(nombres))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Los nombres no pueden estar vacíos.");
                Console.ResetColor();
                Console.Write("Nombres: ");
                nombres = Console.ReadLine().Trim();
            }

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine().Trim();
            while (string.IsNullOrWhiteSpace(apellidos))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Los apellidos no pueden estar vacíos.");
                Console.ResetColor();
                Console.Write("Apellidos: ");
                apellidos = Console.ReadLine().Trim();
            }

            // Validación Teléfono (9 dígitos, solo números)
            string telefono = "";
            valido = false;
            while (!valido)
            {
                Console.Write("Teléfono (9 digitos): ");
                telefono = Console.ReadLine().Trim();

                if (telefono.Length == 9 && long.TryParse(telefono, out _))
                {
                    valido = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Teléfono inválido (debe tener 9 dígitos numéricos).");
                    Console.ResetColor();
                }
            }

            // CREACIÓN DEL OBJETO E INSERCIÓN EN LA LISTA GLOBAL
            Cliente nuevoCliente = new Cliente(dni, nombres, apellidos, telefono);
            ArchivoService.ListaClientes.Add(nuevoCliente);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Cliente registrado correctamente.");
            Console.WriteLine($"   DNI: {dni} | {nombres} {apellidos}");
            Console.ResetColor();
            Console.ReadKey();
        }

        // ============================================================
        // FUNCIÓN 2: APERTURAR AHORRO (CON VALIDACIONES)
        // ============================================================
        public static void AperturarAhorro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== APERTURA DE AHORRO ===");
            Console.ResetColor();

            Console.WriteLine("Montos permitidos: 20, 30, 40, 50 soles");
            Console.Write("Ingrese monto: S/ ");

            if (!double.TryParse(Console.ReadLine(), out double monto) || monto <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Monto inválido. Debe ser un número positivo.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            // Validar montos permitidos
            if (monto == 20 || monto == 30 || monto == 40 || monto == 50)
            {
                double interes = monto * 0.15;
                double saldo = monto + interes;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ Depósito: S/ {monto:F2}");
                Console.WriteLine($"   Interés (15%): S/ {interes:F2}");
                Console.WriteLine($"   Saldo Final: S/ {saldo:F2}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Monto no permitido. Solo: 20, 30, 40, 50 soles.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        // ============================================================
        // FUNCIÓN 3: PROCESAR PRÉSTAMO (CON VALIDACIONES EXTREMAS)
        // ============================================================
        public static void ProcesarPrestamo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== REGISTRO DE PRESTAMO ===");
            Console.ResetColor();

            Console.Write("DNI del cliente: ");
            string dni = Console.ReadLine().Trim();

            // Buscar si el objeto Cliente existe en la lista de ArchivoService
            Cliente clienteEncontrado = ArchivoService.ListaClientes.Find(c => c.DNI == dni);

            if (clienteEncontrado == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] El cliente no existe. Regístrelo primero.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Cliente verificado: {clienteEncontrado.Nombres} {clienteEncontrado.Apellidos}");
            Console.ResetColor();

            // Validación del capital
            Console.Write("Capital solicitado: S/ ");
            if (!double.TryParse(Console.ReadLine(), out double capital) || capital <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Capital debe ser mayor a cero y numérico.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.Write("Descripción de la garantía: ");
            string garantia = Console.ReadLine().Trim();

            // Validar garantía
            while (string.IsNullOrWhiteSpace(garantia))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] La garantía no puede estar vacía.");
                Console.ResetColor();
                Console.Write("Descripción de la garantía: ");
                garantia = Console.ReadLine().Trim();
            }

            Console.Write("URL de la foto de la garantía: ");
            string url = Console.ReadLine().Trim();

            // Validar URL
            while (string.IsNullOrWhiteSpace(url))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] La URL no puede estar vacía.");
                Console.ResetColor();
                Console.Write("URL de la foto de la garantía: ");
                url = Console.ReadLine().Trim();
            }

            // Cálculos con redondeo a 1 decimal
            double total = capital * 1.15;
            double cuota = total / 30;
            double cuotaRedondeada = Math.Round(cuota, 1);
            double totalRedondeado = Math.Round(total, 1);

            // Generar ID autoincremental
            int nuevoID = 1000 + ArchivoService.ListaPrestamos.Count + 1;

            // CREACIÓN DEL OBJETO PRÉSTAMO EN LA LISTA GLOBAL
            Prestamo nuevoPrestamo = new Prestamo(nuevoID, dni, capital, totalRedondeado, garantia, url);
            ArchivoService.ListaPrestamos.Add(nuevoPrestamo);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== CRONOGRAMA DE PAGO (30 cuotas diarias) ===");
            Console.ResetColor();

            for (int i = 1; i <= 30; i++)
            {
                Console.WriteLine($"Cuota {i}: S/ {cuotaRedondeada:F1} [Pendiente]");

                // Mostrar un poco más bonito cada 10 cuotas
                if (i % 10 == 0 && i < 30)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  --- Progreso: {i}/30 cuotas ---");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Total a pagar: S/ {totalRedondeado:F1}");
            Console.WriteLine($"✅ Préstamo guardado con ID: {nuevoID}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}