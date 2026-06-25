using System;

namespace PagaDiarioCelendin
{
    public static class NegocioService
    {
        // FUNCIÓN 1: REGISTRAR CLIENTE
        public static void RegistrarCliente()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== REGISTRO DE CLIENTE ===");
            Console.ResetColor();

            string dni = "";
            bool valido = false;
            while (!valido)
            {
                Console.Write("DNI (8 digitos): ");
                dni = Console.ReadLine().Trim();
                if (dni.Length == 8 && long.TryParse(dni, out _)) valido = true;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] DNI inválido (debe tener 8 dígitos numéricos).");
                    Console.ResetColor();
                }
            }

            Console.Write("Nombres: ");
            string nombres = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(nombres))
            {
                Console.Write("[ERROR] No puede estar vacío. Nombres: ");
                nombres = Console.ReadLine().Trim();
            }

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(apellidos))
            {
                Console.Write("[ERROR] No puede estar vacío. Apellidos: ");
                apellidos = Console.ReadLine().Trim();
            }

            Console.Write("Teléfono (9 dígitos): ");
            string telefono = Console.ReadLine().Trim();
            while (telefono.Length != 9 || !long.TryParse(telefono, out _))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[ERROR] Teléfono inválido. Reintente: ");
                Console.ResetColor();
                telefono = Console.ReadLine().Trim();
            }

            Cliente nuevoCliente = new Cliente(dni, nombres, apellidos, telefono);
            ArchivoService.ListaClientes.Add(nuevoCliente);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[OK] Cliente indexado exitosamente en memoria temporal.");
            Console.ResetColor();
            Console.ReadKey();
        }

        // FUNCIÓN 2: APERTURAR AHORRO (¡NUEVA LOGICA COMPLETA AL 100%!)
        public static void AperturarAhorro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== APERTURA DE CUENTA DE AHORROS ===");
            Console.ResetColor();

            Console.Write("Ingrese el DNI del cliente: ");
            string dni = Console.ReadLine().Trim();

            // VALIDACIÓN INTEGRAL: Comprobar existencia en maestro de clientes
            Cliente cliente = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
            if (cliente == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] El DNI ingresado no pertenece a ningún cliente registrado.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Cliente verificado: {cliente.Nombres} {cliente.Apellidos}");
            Console.Write("Seleccione monto de apertura (20, 30, 40, 50): S/ ");

            if (!double.TryParse(Console.ReadLine(), out double monto) || (monto != 20 && monto != 30 && monto != 40 && monto != 50))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Monto inválido. El sistema solo acepta abonos fijos de S/ 20, 30, 40 o 50.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            // Regla financiera: Cálculo automatizado de tasa preferencial (2% diario estático)
            double tasaInteres = 0.02;
            double interes = Math.Round(monto * tasaInteres, 2);
            double saldoTotal = monto + interes;

            // INSTANCIACIÓN Y PERSISTENCIA TEMPORAL
            Ahorro nuevoAhorro = new Ahorro(dni, monto, interes, saldoTotal);
            ArchivoService.ListaAhorros.Add(nuevoAhorro);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[ÉXITO] Operación de ahorro procesada.");
            Console.WriteLine($"Monto Inicial: S/ {monto:F2} | Interés Generado: S/ {interes:F2} | Saldo Líquido: S/ {saldoTotal:F2}");
            Console.ResetColor();
            Console.ReadKey();
        }

        // FUNCIÓN 3: PROCESAR PRÉSTAMO
        public static void ProcesarPrestamo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== PROCESAR PRÉSTAMO ===");
            Console.ResetColor();

            Console.Write("Ingrese DNI del cliente: ");
            string dni = Console.ReadLine().Trim();

            Cliente cl = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
            if (cl == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Cliente no encontrado. Registre al cliente antes de otorgar un préstamo.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.Write("Monto del Capital solicitado: S/ ");
            if (!double.TryParse(Console.ReadLine(), out double capital) || capital <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Capital inválido.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.Write("Descripción de la Garantía: ");
            string garantia = Console.ReadLine().Trim();

            Console.Write("URL de la foto de la Garantía: ");
            string url = Console.ReadLine().Trim();

            // Reglas de negocio del crédito
            double total = capital * 1.15; // Interés del 15% obligado por la guía
            double cuota = total / 30;
            double cuotaRedondeada = Math.Round(cuota, 1);
            double totalRedondeado = Math.Round(total, 1);

            int nuevoID = 1000 + ArchivoService.ListaPrestamos.Count + 1;

            Prestamo nuevoPrestamo = new Prestamo(nuevoID, dni, capital, totalRedondeado, garantia, url);
            ArchivoService.ListaPrestamos.Add(nuevoPrestamo);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== CRONOGRAMA DE PAGO GENERADO (30 cuotas diarias) ===");
            Console.ResetColor();

            for (int i = 1; i <= 30; i++)
            {
                Console.WriteLine($"Cuota {i}: S/ {cuotaRedondeada:F1} [Pendiente]");
                if (i % 10 == 0 && i < 30)
                {
                    Console.WriteLine($"  --- Control de corte: {i}/30 cuotas ---");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[OK] Crédito aprobado. Total a devolver: S/ {totalRedondeado:F1} en cuotas de S/ {cuotaRedondeada:F1}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}