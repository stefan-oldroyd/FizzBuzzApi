using Microsoft.EntityFrameworkCore;
using System;

namespace Rules
{
    public interface IRuleContext: IDisposable
    {
        DbSet<Rule> Rules { get; set; }
    }
}