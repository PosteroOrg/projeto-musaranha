namespace Musaranha.Models
{
    public partial class VendaProduto
    {
        public string QuantidadeUnidade => $"{this.Quantidade} {this.Unidade}";
        public decimal Valor => this.PrecoUnitario * (decimal)this.Quantidade;
    }
}