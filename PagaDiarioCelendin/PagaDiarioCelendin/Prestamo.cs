using System;

namespace PagaDiarioCelendin
{
    [Serializable]
    public class Prestamo
    {
        public int ID { get; set; }
        public string DNICliente { get; set; }
        public double Capital { get; set; }
        public double Total { get; set; }
        public double SaldoPendiente { get; set; }
        public int CuotasPagadas { get; set; }
        public string Garantia { get; set; }
        public string URLFoto { get; set; }

        public Prestamo(int id, string dniCliente, double capital, double total, string garantia, string urlFoto)
        {
            ID = id;
            DNICliente = dniCliente;
            Capital = capital;
            Total = total;
            SaldoPendiente = total;
            CuotasPagadas = 0;
            Garantia = garantia;
            URLFoto = urlFoto;
        }
    }
}