using System;

namespace PagaDiarioCelendin
{
    [Serializable]
    public class Ahorro
    {
        public string DNICliente { get; set; }
        public double SaldoBase { get; set; }
        public int Plan { get; set; }
        public double SaldoTotal { get; set; }
        public double InteresMensual { get; set; }
        public double InteresAnual { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public Ahorro(string dniCliente, int plan)
        {
            DNICliente = dniCliente;
            SaldoBase = 20.00;
            Plan = plan;
            SaldoTotal = SaldoBase + Plan;
            InteresMensual = Math.Round(SaldoTotal * 0.0125, 2);
            InteresAnual = Math.Round(SaldoTotal * 0.15, 2);
            FechaApertura = DateTime.Now;
            FechaVencimiento = FechaApertura.AddYears(1);
        }
    }
}