using System;
using System.Text.RegularExpressions;

namespace PagaDiarioCelendin
{
    public static class NegocioService
    {
        private static bool ValidarDNI(string dni)
        {
            if (dni.Length != 8 || !long.TryParse(dni, out _)) return false;
            char primero = dni[0];
            foreach (char c in dni)
                if (c != primero) return true;
            return false;
        }

        private static bool SoloLetras(string texto)
        {
            return !string.IsNullOrEmpty(texto) && Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
        }

        private static string LeerConRegreso(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine().Trim();
            if (entrada.Equals("REGRESAR", StringComparison.OrdinalIgnoreCase))
                return "REGRESAR";
            return entrada;
        }

        // ============================================================
        // REGISTRAR CLIENTE (ya funciona bien, con reintentos)
        // ============================================================
        public static void RegistrarCliente()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- REGISTRO DE CLIENTE ---");
            Console.ResetColor();
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            string dni = "";
            while (true)
            {
                string input = LeerConRegreso("DNI (8 digitos, no repetidos): ");
                if (input == "REGRESAR") return;
                dni = input;
                if (ValidarDNI(dni))
                {
                    if (ArchivoService.ListaClientes.Exists(c => c.DNI == dni))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Este DNI ya existe.");
                        Console.ResetColor();
                    }
                    else break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: DNI invalido (8 digitos, no todos iguales).");
                    Console.ResetColor();
                }
            }

            string nombres = "";
            while (true)
            {
                string input = LeerConRegreso("Nombres (solo letras): ");
                if (input == "REGRESAR") return;
                nombres = input;
                if (SoloLetras(nombres)) break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Solo se permiten letras y espacios.");
                Console.ResetColor();
            }

            string apellidos = "";
            while (true)
            {
                string input = LeerConRegreso("Apellidos (solo letras): ");
                if (input == "REGRESAR") return;
                apellidos = input;
                if (SoloLetras(apellidos)) break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Solo se permiten letras y espacios.");
                Console.ResetColor();
            }

            string telefono = "";
            while (true)
            {
                string input = LeerConRegreso("Telefono (9 digitos): ");
                if (input == "REGRESAR") return;
                telefono = input;
                if (telefono.Length == 9 && long.TryParse(telefono, out _)) break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Telefono debe tener 9 digitos.");
                Console.ResetColor();
            }

            var nuevo = new Cliente(dni, nombres, apellidos, telefono);
            ArchivoService.ListaClientes.Add(nuevo);
            ArchivoService.GuardarDatos();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** Cliente registrado exitosamente. ***");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        // ============================================================
        // APERTURAR AHORRO (con reintentos en errores)
        // ============================================================
        public static void AperturarAhorro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- APERTURA DE CUENTA DE AHORROS ---");
            Console.ResetColor();
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            Cliente cliente = null;
            // Bucle para pedir DNI hasta que sea válido o se cancele
            while (true)
            {
                string dni = LeerConRegreso("Ingrese DNI del cliente: ");
                if (dni == "REGRESAR") return;

                cliente = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
                if (cliente == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Cliente no encontrado. Intente nuevamente.");
                    Console.ResetColor();
                    continue;
                }

                if (cliente.TieneAhorro)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Este cliente ya tiene cuenta de ahorros.");
                    Console.ResetColor();
                    continue;
                }

                break; // Cliente válido y sin ahorro
            }

            // Mostrar información del cliente
            Console.WriteLine($"Cliente: {cliente.Nombres} {cliente.Apellidos}");

            int plan = 0;
            // Bucle para pedir el plan hasta que sea válido o se cancele
            while (true)
            {
                Console.WriteLine("\n--- PLANES DE AHORRO DISPONIBLES ---");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Plan 20: Deposito adicional de S/ 20.00");
                Console.WriteLine("  Plan 30: Deposito adicional de S/ 30.00");
                Console.WriteLine("  Plan 40: Deposito adicional de S/ 40.00");
                Console.WriteLine("  Plan 50: Deposito adicional de S/ 50.00");
                Console.ResetColor();
                Console.WriteLine("NOTA: La cuenta se apertura con S/ 20.00 (fijo) + plan elegido.");
                Console.WriteLine();

                string planInput = LeerConRegreso("Elija un plan (20, 30, 40, 50): ");
                if (planInput == "REGRESAR") return;

                if (!int.TryParse(planInput, out plan) || (plan != 20 && plan != 30 && plan != 40 && plan != 50))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Plan invalido. Debe ser 20, 30, 40 o 50.");
                    Console.ResetColor();
                    continue;
                }
                break; // Plan válido
            }

            // Mostrar condiciones y pedir aceptación
            bool condicionesAceptadas = false;
            while (!condicionesAceptadas)
            {
                Console.WriteLine("\n--- CONDICIONES ESTRICTAS DE AHORRO ---");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"* Saldo de apertura: S/ 20.00 (fijo)");
                Console.WriteLine($"* Plan elegido: S/ {plan}.00");
                Console.WriteLine($"* Saldo total inicial: S/ {20 + plan}.00");
                Console.WriteLine($"* Tasa de interes anual: 15% (sobre el saldo total)");
                Console.WriteLine($"* Tasa de interes mensual: 1.25%");
                Console.WriteLine($"* Plazo: 1 año (vencimiento: {DateTime.Now.AddYears(1):dd/MM/yyyy})");
                Console.WriteLine($"* Penalizacion por retiro anticipado: 5% del monto del plan");
                Console.WriteLine($"* Saldo minimo para generar intereses: S/ 20.00");
                Console.WriteLine($"* Los intereses se abonan mensualmente a la cuenta.");
                Console.ResetColor();

                string respuesta = LeerConRegreso("\n¿Acepta las condiciones? (s/n): ");
                if (respuesta == "REGRESAR") return;

                if (respuesta.ToLower() == "s" || respuesta.ToLower() == "si")
                {
                    condicionesAceptadas = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Debe aceptar las condiciones para aperturar la cuenta.");
                    Console.ResetColor();
                }
            }

            // Crear ahorro
            var ahorro = new Ahorro(cliente.DNI, plan);
            ArchivoService.ListaAhorros.Add(ahorro);
            cliente.TieneAhorro = true;
            ArchivoService.GuardarDatos();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n*** CUENTA DE AHORROS APERTURADA EXITOSAMENTE ***");
            Console.WriteLine($"   Saldo base: S/ {ahorro.SaldoBase:F2}");
            Console.WriteLine($"   Plan elegido: S/ {ahorro.Plan}.00");
            Console.WriteLine($"   Saldo total inicial: S/ {ahorro.SaldoTotal:F2}");
            Console.WriteLine($"   Interes mensual (1.25%): S/ {ahorro.InteresMensual:F2}");
            Console.WriteLine($"   Interes anual (15%): S/ {ahorro.InteresAnual:F2}");
            Console.WriteLine($"   Fecha de vencimiento: {ahorro.FechaVencimiento:dd/MM/yyyy}");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        // ============================================================
        // OTORGAR PRESTAMO (con reintentos en errores)
        // ============================================================
        public static void ProcesarPrestamo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- OTORGAR PRESTAMO ---");
            Console.ResetColor();
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            Cliente cliente = null;
            while (true)
            {
                string dni = LeerConRegreso("Ingrese DNI del cliente: ");
                if (dni == "REGRESAR") return;

                cliente = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
                if (cliente == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Cliente no encontrado. Intente nuevamente.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            Console.WriteLine($"Cliente: {cliente.Nombres} {cliente.Apellidos}");

            double capital = 0;
            while (true)
            {
                string capitalInput = LeerConRegreso("Capital solicitado: S/ ");
                if (capitalInput == "REGRESAR") return;

                if (!double.TryParse(capitalInput, out capital) || capital <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Capital invalido. Debe ser un número positivo.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            string garantia = "";
            while (true)
            {
                string input = LeerConRegreso("Garantia: ");
                if (input == "REGRESAR") return;
                if (!string.IsNullOrWhiteSpace(input))
                {
                    garantia = input;
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: La garantia no puede estar vacia.");
                Console.ResetColor();
            }

            string url = "";
            while (true)
            {
                string input = LeerConRegreso("URL de la foto: ");
                if (input == "REGRESAR") return;
                if (!string.IsNullOrWhiteSpace(input))
                {
                    url = input;
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: La URL no puede estar vacia.");
                Console.ResetColor();
            }

            double total = Math.Round(capital * 1.15, 1);
            int nuevoID = 1000 + ArchivoService.ListaPrestamos.Count + 1;

            var prestamo = new Prestamo(nuevoID, cliente.DNI, capital, total, garantia, url);
            ArchivoService.ListaPrestamos.Add(prestamo);
            cliente.TienePrestamo = true;
            ArchivoService.GuardarDatos();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"*** Prestamo otorgado. Total a pagar: S/ {total:F1} en 30 cuotas ***");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        // ============================================================
        // REGISTRAR PAGO DIARIO (con reintentos en errores)
        // ============================================================
        public static void RegistrarPagoDiario()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- REGISTRO DE PAGO DIARIO ---");
            Console.ResetColor();
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            Prestamo prestamo = null;
            while (true)
            {
                string dni = LeerConRegreso("Ingrese DNI del cliente: ");
                if (dni == "REGRESAR") return;

                prestamo = ArchivoService.ListaPrestamos.Find(p => p.DNICliente == dni && p.SaldoPendiente > 0);
                if (prestamo == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: No hay prestamo activo para este cliente.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            Console.WriteLine($"Prestamo ID: {prestamo.ID}");
            Console.WriteLine($"Saldo pendiente: S/ {prestamo.SaldoPendiente:F1}");
            Console.WriteLine($"Cuotas pagadas: {prestamo.CuotasPagadas}/30");
            double cuotaBase = Math.Round(prestamo.Total / 30, 1);

            double monto = 0;
            while (true)
            {
                string montoInput = LeerConRegreso($"Monto a pagar (cuota sugerida S/ {cuotaBase:F1}): S/ ");
                if (montoInput == "REGRESAR") return;

                if (!double.TryParse(montoInput, out monto) || monto <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Monto invalido. Debe ser un número positivo.");
                    Console.ResetColor();
                    continue;
                }

                if (monto > prestamo.SaldoPendiente)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: El monto excede el saldo pendiente (S/ {prestamo.SaldoPendiente:F1}).");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            // Procesar pago
            prestamo.SaldoPendiente -= monto;
            prestamo.CuotasPagadas++;
            var pago = new PagoDiario(prestamo.DNICliente, prestamo.ID, monto, prestamo.CuotasPagadas);
            ArchivoService.ListaPagos.Add(pago);

            if (prestamo.SaldoPendiente <= 0.01)
            {
                var cliente = ArchivoService.ListaClientes.Find(c => c.DNI == prestamo.DNICliente);
                if (cliente != null) cliente.TienePrestamo = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*** PRESTAMO CANCELADO COMPLETAMENTE ***");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"*** Pago registrado. Saldo pendiente: S/ {prestamo.SaldoPendiente:F1} ***");
            }
            ArchivoService.GuardarDatos();
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }
    }
}