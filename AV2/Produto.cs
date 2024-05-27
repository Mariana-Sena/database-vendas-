namespace AV2.Models;
class Produto
{
    public int      CodProduto { get; set; }
    public string  Descricao { get; set; }
    public int   ValorUnitario { get; set; }

    public Produto(int codproduto, string descricao, int valorunitario)
    {
        CodProduto = codproduto;
        Descricao = descricao;
        ValorUnitario = valorunitario;
    }
}