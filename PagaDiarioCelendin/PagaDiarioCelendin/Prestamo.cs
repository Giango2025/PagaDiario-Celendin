namespace PagaDiarioCelendin
{
    public class Prestamo
    {
        public int ID { get; set; }
        public string DNICliente { get; set; }
        public double Capital { get; set; }
        public double Total { get; set; }
        public string Garantia { get; set; }
        public string URLFoto { get; set; }

        public Prestamo(int id, string dniCliente, double capital, double total, string garantia, string urlFoto)
        {
            ID = id; DNICliente = dniCliente; Capital = capital; Total = total; Garantia = garantia; URLFoto = urlFoto;
        }
    }
}