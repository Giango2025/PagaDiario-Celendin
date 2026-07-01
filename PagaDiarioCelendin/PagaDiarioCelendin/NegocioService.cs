using System;
using System.Linq;

namespace PagaDiarioCelendin
{
    public static class NegocioService
    {
        // ========== REGISTRAR CLIENTE ==========
        public static void RegistrarCliente()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- REGISTRO DE CLIENTE ---");
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            string dni = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("DNI (8 dígitos, no repetidos): ");
                if (input == Constantes.MSJ_REGRESAR) return;
                dni = input;

                if (Utils.ValidarDNI(dni))
                {
                    if (ArchivoService.ListaClientes.Any(c => c.DNI == dni))
                    {
                        Utils.MostrarError("Este DNI ya existe.");
                    }
                    else break;
                }
                else
                {
                    Utils.MostrarError(Constantes.MSJ_ERROR_DNI);
                }
            }

            string nombres = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("Nombres (solo letras): ");
                if (input == Constantes.MSJ_REGRESAR) return;
                nombres = input;
                if (Utils.SoloLetras(nombres)) break;
                Utils.MostrarError(Constantes.MSJ_ERROR_NOMBRES);
            }

            string apellidos = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("Apellidos (solo letras): ");
                if (input == Constantes.MSJ_REGRESAR) return;
                apellidos = input;
                if (Utils.SoloLetras(apellidos)) break;
                Utils.MostrarError(Constantes.MSJ_ERROR_NOMBRES);
            }

            string telefono = "";
            while (true)
            {
                string input = Utils.LeerConRegreso("Teléfono (9 dígitos): ");
                if (input == Constantes.MSJ_REGRESAR) return;
                telefono = input;
                if (Utils.ValidarTelefono(telefono)) break;
                Utils.MostrarError(Constantes.MSJ_ERROR_TELEFONO);
            }

            var nuevo = new Cliente(dni, nombres, apellidos, telefono);
            ArchivoService.ListaClientes.Add(nuevo);
            ArchivoService.GuardarDatos();

            Utils.MostrarExito("Cliente registrado exitosamente.");
            Utils.EsperarTecla();
        }

        // ========== APERTURAR AHORRO (CORREGIDO) ==========
        public static void AperturarAhorro()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- APERTURA DE CUENTA DE AHORROS ---");
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            string dni = Utils.LeerConRegreso("Ingrese DNI del cliente: ");
            if (dni == Constantes.MSJ_REGRESAR) return;

            var cliente = ArchivoService.ListaClientes.FirstOrDefault(c => c.DNI == dni);
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
            foreach (int p in Constantes.PLANES_AHORRO)
                Console.WriteLine($"  Plan {p}: Depósito adicional de S/ {p}.00");
            Console.ResetColor();
            Console.WriteLine($"NOTA: La cuenta se apertura con S/ {Constantes.SALDO_BASE_AHORRO:F2} (fijo) + plan elegido.");
            Console.WriteLine();

            string planInput = Utils.LeerConRegreso("Elija un plan (20, 30, 40, 50): ");
            if (planInput == Constantes.MSJ_REGRESAR) return;

            // *** CORRECCIÓN: Cambiamos el nombre de la variable para evitar conflicto ***
            if (!int.TryParse(planInput, out int planElegido) || !Utils.ValidarPlanAhorro(planElegido))
            {
                Utils.MostrarError("Plan inválido. Debe ser 20, 30, 40 o 50.");
                Utils.EsperarTecla();
                return;
            }

            // Mostrar condiciones
            Console.WriteLine("\n--- CONDICIONES ESTRICTAS DE AHORRO ---");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"* Saldo de apertura: S/ {Constantes.SALDO_BASE_AHORRO:F2} (fijo)");
            Console.WriteLine($"* Plan elegido: S/ {planElegido}.00");
            Console.WriteLine($"* Saldo total inicial: S/ {Constantes.SALDO_BASE_AHORRO + planElegido:F2}");
            Console.WriteLine($"* Tasa de interés anual: {Constantes.TASA_INTERES_AHORRO_ANUAL * 100}% (sobre el saldo total)");
            Console.WriteLine($"* Tasa de interés mensual: {Constantes.TASA_INTERES_AHORRO_MENSUAL * 100}%");
            Console.WriteLine($"* Plazo: 1 año (vencimiento: {DateTime.Now.AddYears(1):dd/MM/yyyy})");
            Console.WriteLine($"* Penalización por retiro anticipado: 5% del monto del plan");
            Console.WriteLine($"* Saldo mínimo para generar intereses: S/ {Constantes.SALDO_BASE_AHORRO:F2}");
            Console.WriteLine("* Los intereses se abonan mensualmente a la cuenta.");
            Console.ResetColor();

            string respuesta = Utils.LeerConRegreso("\n¿Acepta las condiciones? (s/n): ");
            if (respuesta == Constantes.MSJ_REGRESAR) return;
            if (respuesta.ToLower() != "s" && respuesta.ToLower() != "si")
            {
                Utils.MostrarError("Operación cancelada por el cliente.");
                Utils.EsperarTecla();
                return;
            }

            // Crear ahorro con el plan elegido
            var ahorro = new Ahorro(dni, planElegido);
            ArchivoService.ListaAhorros.Add(ahorro);
            cliente.TieneAhorro = true;
            ArchivoService.GuardarDatos();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n*** CUENTA DE AHORROS APERTURADA EXITOSAMENTE ***");
            Console.WriteLine($"   Saldo base: S/ {ahorro.SaldoBase:F2}");
            Console.WriteLine($"   Plan elegido: S/ {ahorro.Plan}.00");
            Console.WriteLine($"   Saldo total inicial: S/ {ahorro.SaldoTotal:F2}");
            Console.WriteLine($"   Interés mensual: S/ {ahorro.InteresMensual:F2}");
            Console.WriteLine($"   Interés anual: S/ {ahorro.InteresAnual:F2}");
            Console.WriteLine($"   Fecha de vencimiento: {ahorro.FechaVencimiento:dd/MM/yyyy}");
            Console.ResetColor();
            Utils.EsperarTecla();
        }

        // ========== OTORGAR PRÉSTAMO ==========
        public static void ProcesarPrestamo()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- OTORGAR PRÉSTAMO ---");
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            string dni = Utils.LeerConRegreso("Ingrese DNI del cliente: ");
            if (dni == Constantes.MSJ_REGRESAR) return;

            var cliente = ArchivoService.ListaClientes.FirstOrDefault(c => c.DNI == dni);
            if (cliente == null)
            {
                Utils.MostrarError("Cliente no encontrado.");
                Utils.EsperarTecla();
                return;
            }

            Console.WriteLine($"Cliente: {cliente.Nombres} {cliente.Apellidos}");

            string capitalInput = Utils.LeerConRegreso("Capital solicitado: S/ ");
            if (capitalInput == Constantes.MSJ_REGRESAR) return;
            if (!double.TryParse(capitalInput, out double capital) || capital <= 0)
            {
                Utils.MostrarError("Capital inválido. Debe ser un número positivo.");
                Utils.EsperarTecla();
                return;
            }

            string garantia = Utils.LeerConRegreso("Garantía: ");
            if (garantia == Constantes.MSJ_REGRESAR) return;

            string url = Utils.LeerConRegreso("URL de la foto: ");
            if (url == Constantes.MSJ_REGRESAR) return;

            double total = Math.Round(capital * (1 + Constantes.TASA_INTERES_PRESTAMO_ANUAL), 1);
            int nuevoID = Constantes.BASE_ID_PRESTAMO + ArchivoService.ListaPrestamos.Count + 1;

            var prestamo = new Prestamo(nuevoID, dni, capital, total, garantia, url);
            ArchivoService.ListaPrestamos.Add(prestamo);
            cliente.TienePrestamo = true;
            ArchivoService.GuardarDatos();

            Utils.MostrarExito($"Préstamo otorgado. Total a pagar: S/ {total:F1} en {Constantes.CUOTAS_PRESTAMO} cuotas.");
            Utils.EsperarTecla();
        }

        // ========== REGISTRAR PAGO DIARIO ==========
        public static void RegistrarPagoDiario()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- REGISTRO DE PAGO DIARIO ---");
            Console.WriteLine("(Escriba 'REGRESAR' en cualquier momento para cancelar)");
            Console.WriteLine();

            string dni = Utils.LeerConRegreso("Ingrese DNI del cliente: ");
            if (dni == Constantes.MSJ_REGRESAR) return;

            var prestamo = ArchivoService.ListaPrestamos.FirstOrDefault(p => p.DNICliente == dni && p.SaldoPendiente > 0);
            if (prestamo == null)
            {
                Utils.MostrarError("No hay préstamo activo para este cliente.");
                Utils.EsperarTecla();
                return;
            }

            Console.WriteLine($"Préstamo ID: {prestamo.ID}");
            Console.WriteLine($"Saldo pendiente: S/ {prestamo.SaldoPendiente:F1}");
            Console.WriteLine($"Cuotas pagadas: {prestamo.CuotasPagadas}/{Constantes.CUOTAS_PRESTAMO}");
            double cuotaBase = Math.Round(prestamo.Total / Constantes.CUOTAS_PRESTAMO, 1);

            string montoInput = Utils.LeerConRegreso($"Monto a pagar (cuota sugerida S/ {cuotaBase:F1}): S/ ");
            if (montoInput == Constantes.MSJ_REGRESAR) return;
            if (!double.TryParse(montoInput, out double monto) || monto <= 0)
            {
                Utils.MostrarError("Monto inválido. Debe ser un número positivo.");
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
                var cliente = ArchivoService.ListaClientes.FirstOrDefault(c => c.DNI == dni);
                if (cliente != null) cliente.TienePrestamo = false;
                Utils.MostrarExito("¡PRÉSTAMO CANCELADO COMPLETAMENTE!");
            }
            else
            {
                Utils.MostrarExito($"Pago registrado. Saldo pendiente: S/ {prestamo.SaldoPendiente:F1}");
            }
            ArchivoService.GuardarDatos();
            Utils.EsperarTecla();
        }

        // ========== BUSCAR CLIENTE POR NOMBRE ==========
        public static void BuscarClientePorNombre()
        {
            Console.Clear();
            Utils.MostrarTitulo("--- BUSCAR CLIENTE POR NOMBRE ---");
            Console.WriteLine("(Escriba 'REGRESAR' para cancelar)");
            Console.WriteLine();

            string criterio = Utils.LeerConRegreso("Ingrese nombre o parte del nombre: ");
            if (criterio == Constantes.MSJ_REGRESAR) return;

            var resultados = ArchivoService.ListaClientes
                .Where(c => c.Nombres.ToLower().Contains(criterio.ToLower()) ||
                            c.Apellidos.ToLower().Contains(criterio.ToLower()))
                .ToList();

            if (resultados.Count == 0)
            {
                Utils.MostrarError("No se encontraron clientes.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (var c in resultados)
                    Console.WriteLine($"  {c.DNI} | {c.Nombres} {c.Apellidos} | Tel: {c.Telefono}");
                Console.ResetColor();
                Console.WriteLine($"\nTotal: {resultados.Count} resultado(s).");
            }
            Utils.EsperarTecla();
        }
    }
}