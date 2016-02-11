namespace Musaranha.Models
{
    public partial class Endereco
    {
        public override string ToString() => $"{this.Logradouro}, {this.Numero} - {this.Bairro}, {this.CEP.MaskCEP()}";
    }
}