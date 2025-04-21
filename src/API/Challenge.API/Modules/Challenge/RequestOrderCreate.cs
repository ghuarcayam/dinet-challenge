namespace Challenge.API.Modules.Challenge
{
    public class RequestOrderCreate
    {
        public string Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<RequestOrderCreateItem> Items { get; set; }

        public class RequestOrderCreateItem 
        {
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
        }
    }
}
