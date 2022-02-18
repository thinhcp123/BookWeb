using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;

namespace BookWeb.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
