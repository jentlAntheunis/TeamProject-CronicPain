using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<string> GetCategoryNameByIdAsync(Guid id);
    Task<Category> GetCategoryByNameAsync(string name);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly PebblesContext _context;
    public CategoryRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync() => await _context.Category.ToListAsync();

    public async Task<string> GetCategoryNameByIdAsync(Guid id)
    {
        var categoryName = await _context.Category
            .Where(c => c.Id == id)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        return categoryName;
    }

    public async Task<Category> GetCategoryByNameAsync(string name) => await _context.Category.FirstOrDefaultAsync(c => c.Name == name);

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        await _context.Category.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _context.Category.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
    }
}