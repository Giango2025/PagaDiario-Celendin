using System;
using System.Collections.Generic;

namespace PagaDiarioCelendin
{
    public static class NegocioService
    {
        // Registrar Cliente
        public static void RegistrarCliente()
        {
            Utils.MostrarTitulo("REGISTRO DE CLIENTE");
            Console.WriteLine("(Escriba 'REGRESAR' para cancelar)\n");

            string dni = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("DNI (8 digitos, no repetidos): ");
                if (input == "REGRESAR") return;
                dni = input;
                if (Utils.ValidarDNI(dni))
                {
                    if (ArchivoService.ListaClientes.Exists(c => c.DNI == dni))
                    {
                        Utils.MostrarError("Este DNI ya existe.");
                        continue;
                    }
                    break;
                }
                Utils.MostrarError("DNI inválido (8 dígitos, no todos iguales).");
            }

            string nombres = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("Nombres (solo letras): ");
                if (input == "REGRESAR") return;
                nombres = input;
                if (Utils.SoloLetras(nombres)) break;
                Utils.MostrarError("Solo se permiten letras y espacios.");
            }

            string apellidos = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("Apellidos (solo letras): ");
                if (input == "REGRESAR") return;
                apellidos = input;
                if (Utils.SoloLetras(apellidos)) break;
                Utils.MostrarError("Solo se permiten letras y espacios.");
            }

            string telefono = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("Teléfono (9 dígitos): ");
                if (input == "REGRESAR") return;
                telefono = input;
                if (telefono.Length == 9 && long.TryParse(telefono, out _)) break;
                Utils.MostrarError("Teléfono debe tener 9 dígitos.");
            }

            var nuevo = new Cliente(dni, nombres, apellidos, telefono);
            ArchivoService.ListaClientes.Add(nuevo);
            ArchivoService.GuardarDatos();
            Utils.MostrarExito("Cliente registrado exitosamente.");
            Utils.EsperarTecla();
        }

        // Aperturar Ahorro
        public static void AperturarAhorro()
        {
            Utils.MostrarTitulo("APERTURA DE CUENTA DE AHORROS");
            Console.WriteLine("(Escriba 'REGRESAR' para cancelar)\n");

            string dni = Utils.LeerConRegreso("Ingrese DNI del cliente: ");
            if (dni == "REGRESAR") return;

            var cliente = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
            if (cliente == null)
            {
                Utils.MostrarError("Cliente no encontrado.");
                Utils.EsperarTecla();
                return;
            }

            if (cliente.TieneAhorro)
            {
                Utils.MostrarError("Este cliente ya tiene cuenta de ahorros.");
                Utils.EsperarTecla();
                return;
            }

            Console.WriteLine($"Cliente: {cliente.Nombres} {cliente.Apellidos}");
            Console.WriteLine("\n--- PLANES DE AHORRO DISPONIBLES ---");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Plan 20: Depósito adicional de S/ 20.00");
            Console.WriteLine("  Plan 30: Depósito adicional de S/ 30.00");
            Console.WriteLine("  Plan 40: Depósito adicional de S/ 40.00");
            Console.WriteLine("  Plan 50: Depósito adicional de S/ 50.00");
            Console.ResetColor();
            Console.WriteLine("(Saldo base de apertura: S/ 20.00)");

            string planInput = Utils.LeerConRegreso("\nElija un plan (20, 30, 40, 50): ");
            if (planInput == "REGRESAR") return;
            if (!int.TryParse(planInput, out int plan) || (plan != 20 && plan != 30 && plan != 40 && plan != 50))
            {
                Utils.MostrarError("Plan inválido. Debe ser 20, 30, 40 o 50.");
                Utils.EsperarTecla();
                return;
            }

            Console.WriteLine("\n--- CONDICIONES ESTRICTAS DE AHORRO ---");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"* Saldo de apertura: S/ 20.00 (fijo)");
            Console.WriteLine($"* Plan elegido: S/ {plan}.00");
            Console.WriteLine($"* Saldo total inicial: S/ {20 + plan}.00");
            Console.WriteLine($"* Tasa de interés anual: 15% (sobre el saldo total)");
            Console.WriteLine($"* Tasa de interés mensual: 1.25%");
            Console.WriteLine($"* Plazo: 1 año (vencimiento: {DateTime.Now.AddYears(1):dd/MM/yyyy})");
            Console.WriteLine($"* Penalización por retiro anticipado: 5% del monto del plan");
            Console.WriteLine($"* Saldo mínimo para generar intereses: S/ 20.00");
            Console.WriteLine($"* Los intereses se abonan mensualmente a la cuenta.");
            Console.ResetColor();

            string respuesta = Utils.LeerConRegreso("\n¿Acepta las condiciones? (s/n): ");
            if (respuesta == "REGRESAR") return;
            if (respuesta.ToLower() != "s" && respuesta.ToLower() != "si")
            {
                Utils.MostrarError("Operación cancelada por el cliente.");
                Utils.EsperarTecla();
                return;
            }

            var ahorro = new Ahorro(dni, plan);
            ArchivoService.ListaAhorros.Add(ahorro);
            cliente.TieneAhorro = true;
            ArchivoService.GuardarDatos();

            Utils.MostrarExito("CUENTA DE AHORROS APERTURADA EXITOSAMENTE");
            Console.WriteLine($"   Saldo base: S/ {ahorro.SaldoBase:F2}");
            Console.WriteLine($"   Plan elegido: S/ {ahorro.Plan}.00");
            Console.WriteLine($"   Saldo total inicial: S/ {ahorro.SaldoTotal:F2}");
            Console.WriteLine($"   Interés mensual (1.25%): S/ {ahorro.InteresMensual:F2}");
            Console.WriteLine($"   Interés anual (15%): S/ {ahorro.InteresAnual:F2}");
            Console.WriteLine($"   Fecha de vencimiento: {ahorro.FechaVencimiento:dd/MM/yyyy}");
            Utils.EsperarTecla();
        }

        // Otorgar Préstamo
        public static void ProcesarPrestamo()
        {
            Utils.MostrarTitulo("OTORGAR PRÉSTAMO");
            Console.WriteLine("(Escriba 'REGRESAR' para cancelar)\n");

            string dni = Utils.LeerConRegreso("Ingrese DNI del cliente: ");
            if (dni == "REGRESAR") return;

            var cliente = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
            if (cliente == null)
            {
                Utils.MostrarError("Cliente no encontrado.");
                Utils.EsperarTecla();
                return;
            }

            Console.WriteLine($"Cliente: {cliente.Nombres} {cliente.Apellidos}");

            string capitalInput = Utils.LeerConRegreso("Capital solicitado: S/ ");
            if (capitalInput == "REGRESAR") return;
            if (!double.TryParse(capitalInput, out double capital) || capital <= 0)
            {
                Utils.MostrarError("Capital inválido.");
                Utils.EsperarTecla();
                return;
            }

            string garantia = Utils.LeerConRegreso("Garantía: ");
            if (garantia == "REGRESAR") return;

            string url = Utils.LeerConRegreso("URL de la foto: ");
            if (url == "REGRESAR") return;

            double total = Math.Round(capital * 1.15, 1);
            int nuevoID = 1000 + ArchivoService.ListaPrestamos.Count + 1;

            var prestamo = new Prestamo(nuevoID, dni, capital, total, garantia, url);
            ArchivoService.ListaPrestamos.Add(prestamo);
            cliente.TienePrestamo = true;
            ArchivoService.GuardarDatos();

            Utils.MostrarExito($"Préstamo otorgado. Total a pagar: S/ {total:F1} en 30 cuotas.");
            Utils.EsperarTecla();
        }

        // Registrar Pago Diario
        public static void RegistrarPagoDiario()
        {
            Utils.MostrarTitulo("REGISTRO DE PAGO DIARIO");
            Console.WriteLine("(Escriba 'REGRESAR' para cancelar)\n");

            string dni = Utils.LeerConRegreso("Ingrese DNI del cliente: ");
            if (dni == "REGRESAR") return;

            var prestamo = ArchivoService.ListaPrestamos.Find(p => p.DNICliente == dni && p.SaldoPendiente > 0);
            if (prestamo == null)
            {
                Utils.MostrarError("No hay préstamo activo para este cliente.");
                Utils.EsperarTecla();
                return;
            }

            Console.WriteLine($"Préstamo ID: {prestamo.ID}");
            Console.WriteLine($"Saldo pendiente: S/ {prestamo.SaldoPendiente:F1}");
            Console.WriteLine($"Cuotas pagadas: {prestamo.CuotasPagadas}/30");
            double cuotaBase = Math.Round(prestamo.Total / 30, 1);

            string montoInput = Utils.LeerConRegreso($"Monto a pagar (cuota sugerida S/ {cuotaBase:F1}): S/ ");
            if (montoInput == "REGRESAR") return;
            if (!double.TryParse(montoInput, out double monto) || monto <= 0)
            {
                Utils.MostrarError("Monto inválido.");
                Utils.EsperarTecla();
                return;
            }

            if (monto > prestamo.SaldoPendiente)
            {
                Utils.MostrarError("El monto excede el saldo pendiente.");
                Utils.EsperarTecla();
                return;
            }

            prestamo.SaldoPendiente -= monto;
            prestamo.CuotasPagadas++;
            var pago = new PagoDiario(dni, prestamo.ID, monto, prestamo.CuotasPagadas);
            ArchivoService.ListaPagos.Add(pago);

            if (prestamo.SaldoPendiente <= 0.01)
            {
                var cliente = ArchivoService.ListaClientes.Find(c => c.DNI == dni);
                if (cliente != null) cliente.TienePrestamo = false;
                Utils.MostrarExito("PRÉSTAMO CANCELADO COMPLETAMENTE");
            }
            else
            {
                Utils.MostrarExito($"Pago registrado. Saldo pendiente: S/ {prestamo.SaldoPendiente:F1}");
            }
            ArchivoService.GuardarDatos();
            Utils.EsperarTecla();
        }

        // Nueva funcionalidad: Buscar cliente por nombre
        public static void BuscarClientePorNombre()
        {
            Utils.MostrarTitulo("BUSCAR CLIENTE POR NOMBRE");
            Console.WriteLine("(Escriba 'REGRESAR' para cancelar)\n");

            string criterio = Utils.LeerConRegreso("Ingrese nombre o parte del nombre: ");
            if (criterio == "REGRESAR") return;

            var resultados = ArchivoService.ListaClientes.FindAll(c =>
                c.Nombres.ToLower().Contains(criterio.ToLower()) ||
                c.Apellidos.ToLower().Contains(criterio.ToLower())
            );

            if (resultados.Count == 0)
            {
                Utils.MostrarError("No se encontraron clientes.");
            }
            else
            {
                Console.WriteLine($"\n--- Resultados ({resultados.Count}) ---");
                foreach (var c in resultados)
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono}");
            }
            Utils.EsperarTecla();
        }
    }
}