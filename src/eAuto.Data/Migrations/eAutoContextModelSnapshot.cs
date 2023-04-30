﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using eAuto.Data.Context;

namespace eAuto.Data.Migrations
{
    [DbContext(typeof(EAutoContext))]
    partial class EAutoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.BodyTypeDataModel", b =>
                {
                    b.Property<int>("BodyTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BodyTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BodyTypeId");

                    b.ToTable("BodyTypes");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.BrandDataModel", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.CarDataModel", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<int>("BodyTypeId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("BrandId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("DateArrival")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DriveTypeId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("EngineCapacity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EngineFuelType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("EngineFuelTypeId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("EngineIdentificationName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("EnginePower")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("GenerationId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Odometer")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("PriceInitial")
                        .HasMaxLength(50)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TransmissionId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("Year")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.HasKey("CarId");

                    b.HasIndex("BodyTypeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("DriveTypeId");

                    b.HasIndex("GenerationId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TransmissionId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.DriveTypeDataModel", b =>
                {
                    b.Property<int>("DriveTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriveTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DriveTypeId");

                    b.ToTable("DriveTypes");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.EngineTypeDataModel", b =>
                {
                    b.Property<int>("EngineTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EngineTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EngineTypeId");

                    b.ToTable("EngineTypes");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.GenerationDataModel", b =>
                {
                    b.Property<int>("GenerationId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenerationId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GenerationId");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.ToTable("Generations");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ModelDataModel", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ModelId");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.MotorOilDataModel", b =>
                {
                    b.Property<int>("MotorOilDataModelId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MotorOilDataModelId"));

                    b.Property<string>("Composition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("Price")
                        .HasMaxLength(50)
                        .HasColumnType("float");

                    b.Property<int>("ProductBrandId")
                        .HasColumnType("int");

                    b.Property<string>("Viscosity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Volume")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("MotorOilDataModelId");

                    b.HasIndex("ProductBrandId");

                    b.ToTable("MotorOils");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ProductBrandDataModel", b =>
                {
                    b.Property<int>("ProductBrandDataModelId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductBrandDataModelId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductBrandDataModelId");

                    b.ToTable("ProductBrands");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ShoppingCartDataModel", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCartId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.TransmissionDataModel", b =>
                {
                    b.Property<int>("TransmissionId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransmissionId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TransmissionId");

                    b.ToTable("Transmissions");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.CarDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BodyTypeDataModel", "BodyType")
                        .WithMany()
                        .HasForeignKey("BodyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.DriveTypeDataModel", "DriveType")
                        .WithMany()
                        .HasForeignKey("DriveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.GenerationDataModel", "Generation")
                        .WithMany()
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.ModelDataModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.TransmissionDataModel", "Transmission")
                        .WithMany()
                        .HasForeignKey("TransmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyType");

                    b.Navigation("Brand");

                    b.Navigation("DriveType");

                    b.Navigation("Generation");

                    b.Navigation("Model");

                    b.Navigation("Transmission");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.GenerationDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.ModelDataModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ModelDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.MotorOilDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.ProductBrandDataModel", "ProductBrand")
                        .WithMany()
                        .HasForeignKey("ProductBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductBrand");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ShoppingCartDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.MotorOilDataModel", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}