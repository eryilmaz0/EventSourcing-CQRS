﻿using System.Linq.Expressions;
using SecondQueryProject.ReadModel;

namespace SecondQueryProject.Abstract.Repository;

public interface IRepository<T> where T : ReadModel.ReadModel
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    public Task<bool> InsertAsync(T readModel);
    public Task<bool> UpdateAsync(T readModel);
    public Task<bool> RemoveAsync(T readModel);
}