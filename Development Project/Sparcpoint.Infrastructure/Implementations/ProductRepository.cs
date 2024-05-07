using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sparcpoint.Models;

namespace Sparcpoint.Infrastructure.Implementations;

public class ProductRepository: IProductRepository
{
    private readonly ProductContext _context;
    

    public ProductRepository(ProductContext context)
    {
        _context = context;
    }
    
    public Task<Product> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Products
            .Where(x => x.Id == id)
            .Include(x => x.Categories)
            .Include(x => x.Attributes)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<Product>> GetByNameAsync(string nameContains, CancellationToken cancellationToken = default)
    {
        return _context.Products
            .Where(x => x.Name.Contains(nameContains))
            .Include(x => x.Categories)
            .Include(x => x.Attributes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    //EVAL: this is a quick and dirty implementation, but can be easily improved given time
    public Task<List<Product>> GetByFilterAsync(FilterProduct filter, CancellationToken cancellationToken = default)
    {
        if (!filter.Attributes.Any() && !filter.Categories.Any())
        {
            return _context.Products.ToListAsync(cancellationToken);
        }

        var query = _context.Products
            .Include(x => x.Categories)
            .Include(x => x.Attributes)
            .AsQueryable();

        foreach (var attribute in filter.Attributes)
        {
            //collation works a bit differently with SQLite and I didn't have time to make a case insensitive version
            query = query.Where(x => x.Attributes.Any(a => a.Name.Contains(attribute)));
        }

        foreach (var category in filter.Categories)
        {
            query = query.Where(x => x.Categories.Any(a => a.Name.Contains(category)));
        }

        return query.ToListAsync(cancellationToken);
    }

    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.Products
            .Include(x => x.Categories)
            .Include(x => x.Attributes)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
}