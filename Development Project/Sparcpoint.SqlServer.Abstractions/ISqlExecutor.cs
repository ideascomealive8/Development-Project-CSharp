using System;
using System.Data;
using System.Threading.Tasks;

namespace Sparcpoint.SqlServer.Abstractions
{
    //I'm sure it works great, but i'm making a decision to ignore it in favor of EF
    //As understanding how exactly you planned to utilize it and then organising implementations around it would take more than 2 hrs
    //As opposed to writing a few POCOs and scaffolding a simple db in EF taking about 15 min
    //Not to mention that SQL project is not supported in VSCode/Rider, so I would have to download VS2022
    public interface ISqlExecutor
    {
        Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> command);
        Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> command);
        T Execute<T>(Func<IDbConnection, IDbTransaction, T> command);
    }
}
