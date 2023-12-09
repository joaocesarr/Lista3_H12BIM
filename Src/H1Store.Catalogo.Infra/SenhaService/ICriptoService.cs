namespace H1Store.Catalogo.Infra.SenhaService
{
    public interface ICriptoService
    {
        public string SenhaCriptografada(string senha);
    }
}
