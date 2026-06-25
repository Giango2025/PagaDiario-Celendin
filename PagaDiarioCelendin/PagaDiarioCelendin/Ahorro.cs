using System;

namespace PagaDiarioCelendin
{
    [Serializable] // Crucial para la persistencia binaria avanzada
    public class Ahorro
    {
        public string DNICliente { get; set; }
        public double MontoInicial { get; set; }
        public double InteresGanado { get; set; }
        public double SaldoTotal { get; set; }
        public DateTime FechaApertura { get; set; }

        public Ahorro(string dniCliente, double montoInicial, double interesGanado, double saldoTotal)
        {
            DNICliente = dniCliente;
            MontoInicial = montoInicial;
            InteresGanado = interesGanado;
            SaldoTotal = saldoTotal;
            FechaApertura = DateTime.Now;
        }
    }
}