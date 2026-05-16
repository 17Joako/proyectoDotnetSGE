public record class ContenidoTramite {
    public String contenido{ get; init; }
    public ContenidoTramite(String contenido)
    {
        if (!contenido.IsWhiteSpace)
        {
            this.contenido = contenido;
        }
        else throw new DominioException("El contenido no puede estar en blanco.");}
}
