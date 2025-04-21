namespace Challenge.API.Modules.Challenge
{
    public class RequestOrderUpdate
    {
        public string Cliente { get; set; }

        public DateTime FechaCreacion { get; set; }
        public List<RequestOrderUpdateItem> Items { get; set; }

        public class RequestOrderUpdateItem 
        {
            public Guid? Id { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
        }
    }
}
