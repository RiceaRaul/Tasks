﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220907224331_Update conn str")]
    partial class Updateconnstr
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .HasColumnType("longtext");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Models.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TaskProjectId")
                        .HasColumnType("int");

                    b.Property<int>("TaskStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TaskProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Domain.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TeamOwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TeamOwnerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Secret")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.UserTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTeams");
                });

            modelBuilder.Entity("Domain.Models.Project", b =>
                {
                    b.HasOne("Domain.Models.User", "Owner")
                        .WithMany("Projects")
                        .HasForeignKey("OwnerId");

                    b.HasOne("Domain.Models.Team", "Team")
                        .WithMany("TeamProjects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.Models.TaskModel", b =>
                {
                    b.HasOne("Domain.Models.Project", "TaskProject")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskProject");
                });

            modelBuilder.Entity("Domain.Models.Team", b =>
                {
                    b.HasOne("Domain.Models.User", "TeamOwner")
                        .WithMany()
                        .HasForeignKey("TeamOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeamOwner");
                });

            modelBuilder.Entity("Domain.Models.UserTeam", b =>
                {
                    b.HasOne("Domain.Models.Team", "UserTeams")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Teams")
                        .HasForeignKey("UserId");

                    b.Navigation("User");

                    b.Navigation("UserTeams");
                });

            modelBuilder.Entity("Domain.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Domain.Models.Team", b =>
                {
                    b.Navigation("TeamMembers");

                    b.Navigation("TeamProjects");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
