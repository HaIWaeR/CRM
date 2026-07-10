using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Persistence.Repositories
{
    public class BranchRepository(ApplicationContext context) : IBranchRepository
    {
        public async Task AddAsync(BranchEntity branch)
        {
            branch.Id = Guid.NewGuid();
            branch.CreatedAt = DateTime.UtcNow;
            await context.Branches.AddAsync(branch);
            await context.SaveChangesAsync();
        }

        public async Task<List<BranchEntity>> GetAllAsync()
        {
            return await context.Branches.ToListAsync();
        }

        public async Task<BranchEntity?> GetByIdAsync(Guid id)
        {
            return await context.Branches.FindAsync(id);
        }

        public async Task<BranchEntity> UpdateAsync(BranchEntity branch)
        {
            branch.UpdatedAt = DateTime.UtcNow;
            context.Branches.Update(branch);
            await context.SaveChangesAsync();
            return branch;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Branches.Remove(new BranchEntity { Id = id });
            await context.SaveChangesAsync();
        }

        public async Task<bool> ChangeBranchStatusAsync(Guid id, BranchStatus status)
        {
            BranchEntity branch = new BranchEntity { Id = id };
            context.Branches.Attach(branch);

            branch.Status = status;
            branch.UpdatedAt = DateTime.UtcNow;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<BranchEntity>> GetFilteredAsync(string? searchTerm = null, BranchStatus? status = null)
        {
            IQueryable<BranchEntity> query = context.Branches.AsQueryable();

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

        public async Task<bool> HasUsersAsync(Guid branchId) => 
            await context.Users.AnyAsync(u => u.BranchId == branchId);

        public async Task<bool> HasOrdersAsync(Guid branchId) => 
            await context.Orders.AnyAsync(o => o.BranchId == branchId);

        public async Task<bool> HasWarehousesAsync(Guid branchId) =>
            await context.Warehouses.AnyAsync(w => w.BranchId == branchId);
    }
}