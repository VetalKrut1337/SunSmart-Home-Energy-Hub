﻿using FluentResults;

namespace Services.Interfaces;

public interface ICrudService<TModel>
{
    Task<Result<TModel>> AddAsync(TModel model);
    Task<Result> DeleteAsync(int id);
    Task<Result> EditAsync(TModel model);
    Task<TModel?> GetByIdAsync(int id);
}