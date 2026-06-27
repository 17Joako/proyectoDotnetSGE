public interface IPasswordHasher
{
    string Hash(string password);
    string GenerateSalt();
    bool Verify(string password, string hash);
}