using System;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Clase que representa una cuenta de ahorro.
    /// </summary>
    [Serializable]
    public class Ahorro
    {
        /// <summary>DNI del cliente titular.</summary>
        public string DNICliente { get; set; }

        /// <summary>Saldo base fijo de apertura (S/ 20.00).</summary>
        public double SaldoBase { get; set; }

        /// <summary>Plan de ahorro elegido (20, 30, 40 o 50).</summary>
        public int Plan { get; set; }

        /// <summary>Saldo total (SaldoBase + Plan).</summary>
        public double SaldoTotal { get; set; }

        /// <summary>Interés mensual generado (1.25% del SaldoTotal).</summary>
        public double InteresMensual { get; set; }

        /// <summary>Interés anual generado (15% del SaldoTotal).</summary>
        public double InteresAnual { get; set; }

        /// <summary>Fecha de apertura de la cuenta.</summary>
        public DateTime FechaApertura { get; set; }

        /// <summary>Fecha de vencimiento (1 año después de la apertura).</summary>
        public DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// Constructor de la cuenta de ahorro.
        /// </summary>
        /// <param name="dniCliente">DNI del cliente.</param>
        /// <param name="plan">Plan elegido (20, 30, 40, 50).</param>
        public Ahorro(string dniCliente, int plan)
        {
            DNICliente = dniCliente;
            SaldoBase = Constantes.SALDO_BASE_AHORRO;
            Plan = plan;
            SaldoTotal = SaldoBase + Plan;
            InteresMensual = Math.Round(SaldoTotal * Constantes.TASA_INTERES_AHORRO_MENSUAL, 2);
            InteresAnual = Math.Round(SaldoTotal * Constantes.TASA_INTERES_AHORRO_ANUAL, 2);
            FechaApertura = DateTime.Now;
            FechaVencimiento = FechaApertura.AddYears(1);
        }
    }
}