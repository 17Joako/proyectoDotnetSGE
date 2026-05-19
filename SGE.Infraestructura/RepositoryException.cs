public class RepositoryException : Exception
{
    public RepositoryException() {}
    public RepositoryException(string mensaje) :base(mensaje){}
    
    public RepositoryException(string mensaje, Exception innerException) : base(mensaje, innerException) { }   

    }