using Argon.Core.Communication;
using Argon.Core.Data;
using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Argon.Customers.Infra.Data
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        private readonly IBus _bus;

        public CustomerContext(
            DbContextOptions<CustomerContext> options, 
            IBus bus) 
            : base(options)
        {
            _bus = bus;
        }

        public CustomerContext(DbContextOptions<CustomerContext> options) 
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
        }

        public async Task<bool> CommitAsync()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success) await _bus.PublicarEventos(this);

            return success;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
