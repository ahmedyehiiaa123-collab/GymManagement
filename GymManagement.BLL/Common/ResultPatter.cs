using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Common
{
    public sealed record Result<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }

        public T? Data { get; private set; }

        // Factory methods for easy creation
        public static Result<T> Ok(T data, string? message = null)
        {
            return new Result<T> { Success = true, Data = data, Message = message };
        }

        public static Result<T> Fail(string message)
        {
            return new Result<T> { Success = false, Message = message };
        }
    }
}