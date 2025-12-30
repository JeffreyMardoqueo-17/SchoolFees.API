using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolFees.DAL.Context;
using SchoolFees.DAL.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolFeesDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(SchoolFeesDbContext context)
    {
        _context = context;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }
}
