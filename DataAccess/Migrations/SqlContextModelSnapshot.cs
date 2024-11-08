﻿// <auto-generated />
using System;
using Dominio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(SqlContext))]
    partial class SqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dominio.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<bool>("Resolved")
                        .HasColumnType("bit");

                    b.Property<int?>("ResolvedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("ResolvedTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ResolvedById");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Dominio.DeletedPanel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PanelId")
                        .HasColumnType("int");

                    b.Property<int>("TrashId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PanelId");

                    b.HasIndex("TrashId");

                    b.ToTable("DeletedPanels");
                });

            modelBuilder.Entity("Dominio.DeletedTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("TrashId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TrashId");

                    b.ToTable("DeletedTasks");
                });

            modelBuilder.Entity("Dominio.Panel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TeamId");

                    b.ToTable("Panels");
                });

            modelBuilder.Entity("Dominio.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PanelId")
                        .HasColumnType("int");

                    b.Property<int>("Precedence")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PanelId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Dominio.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxUsers")
                        .HasColumnType("int");

                    b.Property<int>("MembersCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TasksDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Dominio.Trash", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ElementsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Trashes");
                });

            modelBuilder.Entity("Dominio.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaperBinId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PaperBinId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<int>("TeamMembersId")
                        .HasColumnType("int");

                    b.Property<int>("UserTeamsId")
                        .HasColumnType("int");

                    b.HasKey("TeamMembersId", "UserTeamsId");

                    b.HasIndex("UserTeamsId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("Dominio.Comment", b =>
                {
                    b.HasOne("Dominio.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Dominio.User", "ResolvedBy")
                        .WithMany()
                        .HasForeignKey("ResolvedById");

                    b.HasOne("Dominio.Task", null)
                        .WithMany("Comments")
                        .HasForeignKey("TaskId");

                    b.Navigation("CreatedBy");

                    b.Navigation("ResolvedBy");
                });

            modelBuilder.Entity("Dominio.DeletedPanel", b =>
                {
                    b.HasOne("Dominio.Panel", "Panel")
                        .WithMany()
                        .HasForeignKey("PanelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Dominio.Trash", "Trash")
                        .WithMany()
                        .HasForeignKey("TrashId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Panel");

                    b.Navigation("Trash");
                });

            modelBuilder.Entity("Dominio.DeletedTask", b =>
                {
                    b.HasOne("Dominio.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Trash", "Trash")
                        .WithMany()
                        .HasForeignKey("TrashId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("Trash");
                });

            modelBuilder.Entity("Dominio.Panel", b =>
                {
                    b.HasOne("Dominio.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Team", null)
                        .WithMany("Panels")
                        .HasForeignKey("TeamId");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Dominio.Task", b =>
                {
                    b.HasOne("Dominio.Panel", null)
                        .WithMany("Tasks")
                        .HasForeignKey("PanelId");
                });

            modelBuilder.Entity("Dominio.User", b =>
                {
                    b.HasOne("Dominio.Trash", "PaperBin")
                        .WithMany()
                        .HasForeignKey("PaperBinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaperBin");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("Dominio.User", null)
                        .WithMany()
                        .HasForeignKey("TeamMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Team", null)
                        .WithMany()
                        .HasForeignKey("UserTeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Panel", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Dominio.Task", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Dominio.Team", b =>
                {
                    b.Navigation("Panels");
                });
#pragma warning restore 612, 618
        }
    }
}
