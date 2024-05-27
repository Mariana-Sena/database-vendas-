namespace AV2.Models;

class Vendedor
{
    public int      CodVendedor { get; set; }
    public string  Nome { get; set; }
    public decimal   SalarioFixo { get; set; }

    public string  FaixaComissao { get; set; }
 public Vendedor(int codvendedor, string nome, decimal salariofixo,string faixacomissao)
    {
        CodVendedor = codvendedor;
        Nome = nome;
        SalarioFixo = salariofixo;
        FaixaComissao = faixacomissao;
    }
}