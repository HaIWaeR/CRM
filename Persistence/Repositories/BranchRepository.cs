using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationContext _context;

        public BranchRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BranchEntity branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BranchEntity>> GetAllAsync()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<BranchEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<BranchEntity> UpdateAsync(BranchEntity branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task DeleteAsync(Guid id)
        {
            _context.Branches.Remove(new BranchEntity { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeBranchStatusAsync(Guid id, BranchStatus status)
        {
            BranchEntity branch = new BranchEntity { Id = id };
            _context.Branches.Attach(branch);

            branch.Status = status;
            branch.UpdatedAt = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Branches.AnyAsync(b => b.Id == id);
        }

        public async Task<List<BranchEntity>> GetFilteredAsync(string? searchTerm = null, BranchStatus? status = null)
        {
            IQueryable<BranchEntity> query = _context.Branches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim();
                query = query.Where(b =>
                    b.Name.Contains(search) ||
                    b.Address.Contains(search)
                );
            }

            if (status.HasValue)
            {
                query = query.Where(b => b.Status == status.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> HasUsersAsync(Guid branchId)
        {
            return await _context.Users.AnyAsync(u => u.BranchId == branchId);
        }

        public async Task<bool> HasOrdersAsync(Guid branchId)
        {
            return await _context.Orders.AnyAsync(o => o.BranchId == branchId);
        }

        public async Task<bool> HasWarehousesAsync(Guid branchId)
        {
            return await _context.Warehouses.AnyAsync(w => w.BranchId == branchId);
        }
    }
}