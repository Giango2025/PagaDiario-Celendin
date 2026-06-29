using System;

namespace PagaDiarioCelendin
{
    [Serializable]
    public class PagoDiario
    {
        public string DNICliente { get; set; }
        public int PrestamoID { get; set; }
        public double MontoPagado { get; set; }
        public DateTime FechaPago { get; set; }
        public int NumeroCuota { get; set; }

        public PagoDiario(string dniCliente, int prestamoID, double montoPagado, int numeroCuota)
        {
            DNICliente = dniCliente;
            PrestamoID = prestamoID;
            MontoPagado = montoPagado;
            FechaPago = DateTime.Now;
            NumeroCuota = numeroCuota;
        }
    }
}