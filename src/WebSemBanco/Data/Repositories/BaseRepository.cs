namespace WebSemBanco.Data.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly DataBaseConfig _context;

        public BaseRepository(DataBaseConfig context)
        {
            _context = context;
        }
    }
}
