using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NFC.Data.Entities;

namespace NFC.Data
{
	public class NFCDbContext : IdentityDbContext<NFCUser>
	{
		public NFCDbContext(DbContextOptions<NFCDbContext> options)
			: base(options)
		{
		}
		public DbSet<Hearing> Hearings { get; set; }
		public DbSet<KT_TW_SPL> KT_TW_SPLs { get; set; }
		public DbSet<KT_MIC_WF_SPL> KT_MIC_WF_SPLs { get; set; }
		public DbSet<Sensor> Sensors { get; set; }
		public DbSet<HistoryUpload> HistoryUploads { get; set; }
		public DbSet<ProductionLine> ProductionLines { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Hearing>().HasIndex(b => b.NUM);
			builder.Entity<KT_TW_SPL>().HasIndex(b => b.NUM);
			builder.Entity<KT_MIC_WF_SPL>().HasIndex(b => b.NUM);
			builder.Entity<Sensor>().HasIndex(b => b.NUM);

			builder.Entity<NFCUser>()
				   .HasOne(c => c.ProductionLine)
				   .WithMany()
				   .HasForeignKey(c => c.ProductionLineId)
				   .OnDelete(DeleteBehavior.Restrict);
			builder.Entity<NFCUser>()
				  .HasOne(c => c.Role)
				  .WithMany()
				  .HasForeignKey(c => c.RoleId)
				  .OnDelete(DeleteBehavior.Restrict);
			builder.Entity<KT_TW_SPL>()
					.HasOne(c => c.ProductionLine)
					.WithMany()
					.HasForeignKey(c => c.ProductionLineId)
					.OnDelete(DeleteBehavior.Restrict);
			builder.Entity<KT_MIC_WF_SPL>()
					.HasOne(c => c.ProductionLine)
					.WithMany()
					.HasForeignKey(c => c.ProductionLineId)
					.OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Sensor>()
					.HasOne(c => c.ProductionLine)
					.WithMany()
					.HasForeignKey(c => c.ProductionLineId)
					.OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Hearing>()
				   .HasOne(c => c.ProductionLine)
				   .WithMany()
				   .HasForeignKey(c => c.ProductionLineId)
				   .OnDelete(DeleteBehavior.Restrict);

		}
	}
}
