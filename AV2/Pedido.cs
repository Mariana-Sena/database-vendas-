namespace AV2.Models;

class Pedido
{
    public int      CodPedido { get; set; }
    public string PrazoEntrega { get; set; }
    public DateTime DataPedido { get; set; }
    public int      PedidoCodCliente { get; set; }    
    public int      PedidoCodVendedor { get; set; }    

    public Pedido(int codpedido, string prazoentrega, DateTime datapedido, int pedidocodcliente, int pedidocodvendedor)
    {
        CodPedido           = codpedido;
        PrazoEntrega        = prazoentrega;
        DataPedido          = datapedido;
        PedidoCodCliente    = pedidocodcliente;        
        PedidoCodVendedor   = pedidocodvendedor;        
    }
}
