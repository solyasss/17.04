namespace _17._04.Services
{
    public interface ihash_service
    {
        string generate_salt();
        string compute_hash(string salt, string password);
    }
}