using System;

namespace XYZBoutiqueApi.Models
{
    public enum EstadoPedido
    {
        PorAtender,
        EnProceso,
        EnDelivery,
        Recibido
    }

    public class Pedido
    {
        public int NroPedido { get; set; }
        public string ListaProductos { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? FechaDespacho { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string VendedorSolicitante { get; set; }
        public string Repartidor { get; set; }
        public EstadoPedido Estado { get; set; }
    }
}
