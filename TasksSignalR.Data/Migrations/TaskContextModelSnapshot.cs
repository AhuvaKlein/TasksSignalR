﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TasksSignalR.Data;

namespace TasksSignalR.Data.Migrations
{
    [DbContext(typeof(TaskContext))]
    partial class TaskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TasksSignalR.Data.Chore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Chores");
                });

            modelBuilder.Entity("TasksSignalR.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TasksSignalR.Data.Chore", b =>
                {
                    b.HasOne("TasksSignalR.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
