using System;

namespace XYZBoutiqueApi.ViewModels
{
    public class PedidoDto
    {
        public string ListaProductos { get; set; }
        public DateTime FechaPedido { get; set; }
        public string VendedorSolicitante { get; set; }
        public string Repartidor { get; set; }
    }
}
