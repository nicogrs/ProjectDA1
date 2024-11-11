﻿// <auto-generated />
using System;
using DataAccess;
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

            modelBuilder.HasSequence("IDeleteableSequence");

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

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ResolvedById");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Dominio.Epic", b =>
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

                    b.Property<int>("FromPanelId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FromPanelId");

                    b.ToTable("Epic");
                });

            modelBuilder.Entity("Dominio.IDeleteable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [IDeleteableSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrashId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrashId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Dominio.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Notifications");
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

            modelBuilder.Entity("Dominio.Panel", b =>
                {
                    b.HasBaseType("Dominio.IDeleteable");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TeamId");

                    b.ToTable("Panels", (string)null);
                });

            modelBuilder.Entity("Dominio.Task", b =>
                {
                    b.HasBaseType("Dominio.IDeleteable");

                    b.Property<int?>("EpicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PanelId")
                        .HasColumnType("int");

                    b.Property<int>("Precedence")
                        .HasColumnType("int");

                    b.HasIndex("EpicId");

                    b.HasIndex("PanelId");

                    b.ToTable("Tasks", (string)null);
                });

            modelBuilder.Entity("Dominio.Comment", b =>
                {
                    b.HasOne("Dominio.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Dominio.User", "ResolvedBy")
                        .WithMany()
                        .HasForeignKey("ResolvedById");

                    b.HasOne("Dominio.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("ResolvedBy");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Dominio.Epic", b =>
                {
                    b.HasOne("Dominio.Panel", "FromPanel")
                        .WithMany()
                        .HasForeignKey("FromPanelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromPanel");
                });

            modelBuilder.Entity("Dominio.IDeleteable", b =>
                {
                    b.HasOne("Dominio.Trash", null)
                        .WithMany("Paperbin")
                        .HasForeignKey("TrashId");
                });

            modelBuilder.Entity("Dominio.Notification", b =>
                {
                    b.HasOne("Dominio.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Dominio.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
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
                    b.HasOne("Dominio.Epic", null)
                        .WithMany("Tasks")
                        .HasForeignKey("EpicId");

                    b.HasOne("Dominio.Panel", "Panel")
                        .WithMany("Tasks")
                        .HasForeignKey("PanelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Panel");
                });

            modelBuilder.Entity("Dominio.Epic", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Dominio.Team", b =>
                {
                    b.Navigation("Panels");
                });

            modelBuilder.Entity("Dominio.Trash", b =>
                {
                    b.Navigation("Paperbin");
                });

            modelBuilder.Entity("Dominio.Panel", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Dominio.Task", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
