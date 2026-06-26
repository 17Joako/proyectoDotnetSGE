public interface IPasswordHasher
{
    string Hash(string password, string salt);
    string GenerateSalt();
    bool Verify(string password, string hash, string salt);
}