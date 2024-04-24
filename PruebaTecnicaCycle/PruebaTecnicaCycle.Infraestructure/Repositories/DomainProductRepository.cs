using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaCycle.Domain.Entities;
using PruebaTecnicaCycle.Domain.Repositories;
using PruebaTecnicaCycle.Infraestructure.Persistance;
using System.Data;

namespace PruebaTecnicaCycle.Infraestructure.Repositories
{
    public class DomainProductRepository : IDomainProductRepository
    {
        private readonly PruebaTecnicaCycleDBContext _context;

        public DomainProductRepository(PruebaTecnicaCycleDBContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            var resultDelete = false;

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();

            if (result > 0) resultDelete = true;

            return resultDelete;
        }

        public async Task<List<Product>> GetAllDapperAsync()
        {
            var conection = _context.Database.GetConnectionString();
            IDbConnection db = new SqlConnection(conection);

            return db.Query<Product>("Catalog.GetAllProducts", commandType: CommandType.StoredProcedure).ToList();
        }

        public async Task<List<Product>> GetAllEntityFrameworkAsync()
        {
            return await _context.Products.FromSqlRaw("EXEC Catalog.GetAllProducts").ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var resultUpdate = false;

            _context.Products.Update(product);

            var result = await _context.SaveChangesAsync();

            if (result > 0) { resultUpdate = true; }

            return resultUpdate;
        }
    }
}