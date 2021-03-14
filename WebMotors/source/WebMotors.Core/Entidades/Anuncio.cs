namespace WebMotors.Core.Entidades
{
    public class Anuncio
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
        public int VersaoId { get; set; }
    }
}
